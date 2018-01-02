using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;

namespace JgDatevExportLib
{
    public static class DatevHelper
    {
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

        public static string GetNameConfigDatei()
        {
            string dat = PfadProgramm();
            return System.IO.Path.GetDirectoryName(dat) + @"\JgDatevExport.config";
        }

        public static string PfadProgramm()
        {
            var m = System.Reflection.Assembly.GetExecutingAssembly().Location;
            return Path.GetDirectoryName(m) + @"\";
        }

        public static string UnterstricheInWert(string Wert)
        {
            return Wert.Replace("_", " ").Replace("__", "//").Replace("___", "-");
        }

        public static void DatenSpeichern(string DateiName, DatevHeader Header, DatevKoerper Koerper, DatevOptionen Optionen)
        {
            var fs = new FileStream(DateiName, FileMode.Create);

            var arrList = new ArrayList();
            arrList.Add(Header);
            arrList.Add(Koerper);
            arrList.Add(Optionen);

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

        public static void DatenLaden(string DateiName, ref DatevHeader Header, ref DatevKoerper Koerper, ref DatevOptionen Optionen)
        {
            FileStream fs = null;

            try
            {
                fs = new FileStream(DateiName, FileMode.Open);

                BinaryFormatter formatter = new BinaryFormatter();
                var arrList = (ArrayList)formatter.Deserialize(fs);

                foreach (var obj in arrList)
                {
                    if (obj is DatevHeader)
                        Header = (DatevHeader)obj;
                    else if (obj is DatevKoerper)
                        Koerper = (DatevKoerper)obj;
                    else if (obj is DatevOptionen)
                        Optionen = (DatevOptionen)obj;
                }

                Header.DvOptionen = Optionen;
                Koerper.DvOptionen = Optionen;
            }
            catch (SerializationException e)
            {
                Console.WriteLine("Failed to deserialize. Reason: " + e.Message);
                throw;
            }
            finally
            {
                fs?.Close();
            }
        }
    }
}
