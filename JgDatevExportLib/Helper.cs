using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
