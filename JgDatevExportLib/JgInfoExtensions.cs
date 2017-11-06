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

        public static bool GetJgInfoAttribute<T>(this T obj, Expression<Func<T, object>> value, object WertNeu)
        {
            var member = value.Body as MemberExpression;
            var unary = value.Body as UnaryExpression;
            var MemberExp = member ?? (unary != null ? unary.Operand as MemberExpression : null);

            var info = obj.GetType().GetField(MemberExp.Member.Name, BindingFlags.NonPublic | BindingFlags.Instance);
            var wert = info.GetValue(obj);
            var type = info.FieldType;
            var attr = GetAttrib(MemberExp);

            if (wert.ToString() != WertNeu.ToString())
            {
                info.SetValue(obj, WertNeu);
                return true;
            }

            return false;
        }

        public static string JgDruck<T>(this T obj, Expression<Func<T, object>> value)
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
                if (wert != null)
                    return "\"" + wert.ToString() + "\"";

                if (attr.IstErforderlich)
                    return "\"\"";
            }
            else if ((type == typeof(int)) || (type == typeof(int?)))
            {
                if (wert != null)
                    return wert.ToString();

                if (attr.IstErforderlich)
                    return "0";
            }
            else if ((type == typeof(decimal)) || (type == typeof(decimal?)))
            {
                if (wert != null)
                    return ((decimal)wert).ToString(attr.Format);

                if (attr.IstErforderlich)
                    return "0,00";
            }
            else if ((type == typeof(DateTime)) || (type == typeof(DateTime?)))
            {
                if (wert != null)
                    return ((DateTime)wert).ToString(attr.Format);
            }
            else
            {
                var ar = Enum.GetNames(type);

                var ss = wert.ToString();
                if (ss != "leer")
                {
                    switch (attr.AnzeigeEnum)
                    {
                        case JgInfoAttribute.AnzeigeEnums.AlsZahl:
                            return Convert.ToString((byte)wert);
                        case JgInfoAttribute.AnzeigeEnums.AlsString:
                            return ss;
                        case JgInfoAttribute.AnzeigeEnums.ErsterBuchstabe:
                            return ss[0].ToString();
                        case JgInfoAttribute.AnzeigeEnums.ErsteZweiBuchstaben:
                            return ss[0].ToString() + ss[1].ToString();
                        case JgInfoAttribute.AnzeigeEnums.LetztesZeichen:
                            return ss[ss.Length].ToString();
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
