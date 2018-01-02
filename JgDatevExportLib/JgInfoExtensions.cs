using System;
using System.Linq.Expressions;
using System.Reflection;

namespace JgDatevExportLib
{
    public static class JgInfoExtensions
    {
        private static JgInfoAttribute GetAttrib(MemberExpression MemberExp)
        {
            var attrTemp = MemberExp.Member.GetCustomAttributes(typeof(JgInfoAttribute), true);
            return (JgInfoAttribute)attrTemp[0];
        }

        public static bool SetzeWertInObject<T>(this T obj, Expression<Func<T, object>> value, object WertNeu)
        {
            var member = value.Body as MemberExpression;
            var unary = value.Body as UnaryExpression;
            var MemberExp = member ?? (unary != null ? unary.Operand as MemberExpression : null);

            var info = obj.GetType().GetField(MemberExp.Member.Name, BindingFlags.NonPublic | BindingFlags.Instance);
            var wertAlt = info.GetValue(obj);
            var type = info.FieldType;
            var attr = GetAttrib(MemberExp);

            try
            {
                if (WertNeu == null)
                {
                    if (Nullable.GetUnderlyingType(type) != null)
                        info.SetValue(obj, null);
                    else if (type == typeof(int))
                        info.SetValue(obj, 0);
                    else if (type == typeof(decimal))
                        info.SetValue(obj, 0);
                    else if (type == typeof(DateTime))
                        info.SetValue(obj, DateTime.Now);
                    else
                        info.SetValue(obj, "");
                }
                else
                {
                    if (type == typeof(String))
                    {
                        var erg = WertNeu.ToString();
                        if (erg.Length < attr.Min)
                            throw new ArgumentOutOfRangeException($"Feld: {MemberExp.Member.Name} -> Anzahl der Zeichen muss größer {attr.Min} sein.");
                        else if (erg.Length > attr.Max)
                            throw new ArgumentOutOfRangeException($"Feld: {MemberExp.Member.Name} -> Anzahl der Zeichen muss kleiner {attr.Max} sein.");

                        info.SetValue(obj, erg);
                    }
                    else if (type == typeof(int))
                    {
                        var erg = (int)WertNeu;

                        if (erg < attr.Min)
                            throw new ArgumentOutOfRangeException($"Feld: {MemberExp.Member.Name} -> Die Zahl muss größer {attr.Min} sein.");
                        else if (erg > attr.Max)
                            throw new ArgumentOutOfRangeException($"Feld: {MemberExp.Member.Name} -> Die Zahl muss kleiner {attr.Max} sein.");

                        info.SetValue(obj, erg);
                    }
                    else if (type == typeof(decimal))
                    {
                        var erg = (decimal)WertNeu;

                        if (erg < attr.Min)
                            throw new ArgumentOutOfRangeException($"Feld: {MemberExp.Member.Name} -> Die Zahl muss größer {attr.Min} sein.");
                        else if (erg > attr.Max)
                            throw new ArgumentOutOfRangeException($"Feld: {MemberExp.Member.Name} -> Die Zahl muss kleiner {attr.Max} sein.");

                        info.SetValue(obj, erg);
                    }
                    else if (type == typeof(DateTime))
                    {
                        var erg = (DateTime)WertNeu;
                        info.SetValue(obj, erg);
                    }
                    else
                        info.SetValue(obj, WertNeu);
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Feld: {MemberExp.Member.Name} -> Fehler bei eintragen des Wertes.", ex);
            }
        }

        public static string JgDruck<T>(this T obj, Expression<Func<T, object>> value, String Format = null)
            where T : class
        {
            var member = value.Body as MemberExpression;
            var unary = value.Body as UnaryExpression;
            var MemberExp = member ?? (unary != null ? unary.Operand as MemberExpression : null);

            var info = obj.GetType().GetField(MemberExp.Member.Name, BindingFlags.NonPublic | BindingFlags.Instance);
            var wert = info.GetValue(obj);
            var type = info.FieldType;
            var attr = GetAttrib(MemberExp);

            if (type == typeof(string))
            {
                if (wert == null)
                  return attr.IstErforderlich ? "\"\"" : "";

                var ms = string.IsNullOrEmpty(Format) ? wert.ToString() : string.Format(Format, wert);
                if (ms.Length > attr.Max)
                    ms = ms.Substring(0, attr.Max);

                return "\"" + ms + "\"";
            }
            else if ((type == typeof(int)) || (type == typeof(int?)))
            {
                if (wert == null)
                    return attr.IstErforderlich ? "0" : "";
                return string.IsNullOrEmpty(Format) ? wert.ToString() : string.Format(Format, wert);
            }
            else if ((type == typeof(decimal)) || (type == typeof(decimal?)))
            {
                if (wert == null)
                    return attr.IstErforderlich ? 0.ToString(attr.Format) : "";

                return ((decimal)wert).ToString(attr.Format);
            }
            else if ((type == typeof(DateTime)) || (type == typeof(DateTime?)))
            {
                if (wert != null)
                    return ((DateTime)wert).ToString(attr.Format);
            }
            else
            {
                var ss = wert.ToString();
                if (ss != "leer")
                {
                    switch (attr.AnzeigeEnum)
                    {
                        case JgInfoAttribute.AnzeigeEnums.AlsZahl:
                            return Convert.ToString((byte)wert);
                        case JgInfoAttribute.AnzeigeEnums.AlsString:
                            return "\"" + ss + "\"";
                        case JgInfoAttribute.AnzeigeEnums.ErsterBuchstabe:
                            return "\"" + ss[0].ToString() + "\"";
                        case JgInfoAttribute.AnzeigeEnums.ErsteZweiBuchstaben:
                            return "\"" + ss[0].ToString() + ss[1].ToString() + "\"";
                        case JgInfoAttribute.AnzeigeEnums.LetztesZeichen:
                            return ss[ss.Length - 1].ToString();
                    }
                }
            }

            return "";
        }

        public static DsListeAnzeige JgAnzeige<T>(this T obj, Expression<Func<T, object>> value)
            where T : class
        {
            var member = value.Body as MemberExpression;
            var unary = value.Body as UnaryExpression;
            var MemberExp = member ?? (unary != null ? unary.Operand as MemberExpression : null);

            var info = obj.GetType().GetField(MemberExp.Member.Name, BindingFlags.NonPublic | BindingFlags.Instance);
            var attr = GetAttrib(MemberExp);

            var erg = new DsListeAnzeige()
            {
                FeldName = info.Name.Substring(1),

                IstErforderlich = attr.IstErforderlich,

                TypeName = info.FieldType.ToString(),
                Min = attr.Min,
                Max = attr.Max,
                Format = attr.Format,
            };

            var wert = info.GetValue(obj);
            if (wert != null)
                erg.FeldWert = info.GetValue(obj).ToString();

            return erg;
        }
    }
}
