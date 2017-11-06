using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace JgDatevExportLib
{
    public static class Helper
    {
        public static bool KontrStringOk(ref string Vorhanden, string StringNeu, string Feldname, int Min, int Max)
        {
            if (StringNeu.Length < Min)
            {
                Fehler($"Für Feld {Feldname} muss die Zeichenanzahl größer {Min} sein. ({StringNeu})");
                return false;
            }
            if ((StringNeu.Length > Max))
            {
                Fehler($"Für Feld {Feldname} muss die Zeichenanzahl kleiner {Max} sein. ({StringNeu})");
                return false;
            }

            Vorhanden = StringNeu;
            return true;
        }

        public static bool KontrIntOk(ref int Vorhanden, int IntNeu, string Feldname, int Min, int Max)
        {
            return KontrIntOk(ref Vorhanden, IntNeu, Feldname, Min, Max);
        }

        public static bool KontrIntOk(ref int? Vorhanden, int? IntNeu, string Feldname, int Min, int Max)
        {
            if (Vorhanden != IntNeu)
            {
                if (IntNeu == null)
                    Vorhanden = IntNeu;
                else
                {
                    var test = Convert.ToInt64(IntNeu);
                    if (test < Min)
                    {
                        Fehler($"Für Feld {Feldname} muss die Zahl größer {Min} sein. ({IntNeu})");
                        return false;
                    }
                    if (test > Max)
                    {
                        Fehler($"Für Feld {Feldname} muss die Zeichenanzahl kleiner {Max} sein. ({IntNeu})");
                        return false;
                    }
                }
            }

            return true;
        }

        public static void Fehler(string FehlerText)
        {

        }

        public static string Konvert(object Wert, string Format = null)
        {
            if (Wert != null)
            {
                var t = Wert.GetType();

                if (t == typeof(string))
                    return "\"" + Wert.ToString().Replace("\"", "\"\"") + "\"";
                else if (t == typeof(DateTime))
                    return ((DateTime)Wert).ToString(Format);
                else if ((t == typeof(int)) || ((t == typeof(long))))
                    return Wert.ToString();
                else
                {
                    if (Wert.ToString() == "leer")
                        return "";
                    else
                      return (Convert.ToByte(Wert)).ToString();
                }
            }

            return "";
        }

        public static string UnterstricheInWert(string Wert)
        {
            return Wert.Replace("_", " ").Replace("__", "//").Replace("___", "-");
        }

        public static string DateinameErstellen(string DateiName, DateTime Datum)
        {
            return "EXTF_" + DateiName + "_" + Datum.ToString("ddMMyy_mmHH") + ".csv";
        }

        public static Dictionary<string, string> DatevFeldZuordnungAusString(string FelderAsString)
        {
            var erg = new Dictionary<string, string>();

            var ds = FelderAsString.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var felder in ds)
            {
                var spalten = felder.Split(new char[] { '|' });
                erg.Add(spalten[0], spalten[1]);
            }

            return erg;
        }

        public static string DatevFeldZuordnungInString(Dictionary<string, string> Dic) => string.Join(";", Dic.Select(s => s.Key + "|" + s.Value));

        public static void DatenSpeichern(string DateiName, DatevHeader Header, DatevKoerper Koerper)
        {
            var arrList = new ArrayList();
            arrList.Add(Header);
            arrList.Add(Koerper);

            var fs = new FileStream(DateiName, FileMode.Create);
            var formatter = new BinaryFormatter();

            try
            {
                formatter.Serialize(fs, arrList);
            }
            catch (SerializationException e)
            {
                Console.WriteLine("Failed to serialize. Reason: " + e.Message);
                throw;
            }
            finally
            {
                fs.Close();
            }
        }

        public static Tuple<DatevHeader, DatevKoerper> DatenLaden(string Dateiname)
        {
            var fs = new FileStream(Dateiname, FileMode.Open);
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                var arrList = (ArrayList)formatter.Deserialize(fs);

                DatevHeader header = null;
                DatevKoerper koerper = null;

                foreach (var obj in arrList)
                {
                    if (obj is DatevHeader)
                        header = (DatevHeader)obj;
                    else if (obj is DatevKoerper)
                        koerper = (DatevKoerper)obj;
                }

                return new Tuple<DatevHeader, DatevKoerper>(header, koerper);
            }
            catch (SerializationException e)
            {
                Console.WriteLine("Failed to deserialize. Reason: " + e.Message);
                throw;
            }
            finally
            {
                fs.Close();
            }
        }
    }
}
