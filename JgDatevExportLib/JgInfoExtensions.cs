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

        public static bool GetJgInfoAttribute<T>(this T obj, Expression<Func<T, string>> value, string WertNeu)
        {
            var MemberExp = (MemberExpression)value.Body;
            var info = obj.GetType().GetField(MemberExp.Member.Name, BindingFlags.NonPublic | BindingFlags.Instance);
            var wert = (string)info.GetValue(obj);

            if (wert != WertNeu)
            {
                info.SetValue(obj, WertNeu);

                var attr = GetAttrib(MemberExp);


                return true;
            }

            return false;
        }

        public static bool GetJgInfoAttribute<T>(this T obj, Expression<Func<T, int>> value, int? WertNeu)
        {
            var MemberExp = (MemberExpression)value.Body;
            var info = obj.GetType().GetField(MemberExp.Member.Name, BindingFlags.NonPublic | BindingFlags.Instance);
            var wert = (int?)info.GetValue(obj);

            if (wert != WertNeu)
            {
                info.SetValue(obj, WertNeu);

                var attr = GetAttrib(MemberExp);


                return true;
            }

            return false;
        }

        public static bool GetJgInfoAttribute<T>(this T obj, Expression<Func<T, decimal>> value, decimal WertNeu)
            where T : class
        {
            var MemberExp = (MemberExpression)value.Body;
            var info = obj.GetType().GetField(MemberExp.Member.Name, BindingFlags.NonPublic | BindingFlags.Instance);
            var wert = (decimal?)info.GetValue(obj);

            if (wert != WertNeu)
            {
                info.SetValue(obj, WertNeu);

                var attr = GetAttrib(MemberExp);


                return true;
            }

            return false;
        }

        public static bool GetJgInfoAttribute<T>(this T obj, Expression<Func<T, decimal?>> value, decimal? WertNeu)
           where T : class
        {
            var MemberExp = (MemberExpression)value.Body;
            var info = obj.GetType().GetField(MemberExp.Member.Name, BindingFlags.NonPublic | BindingFlags.Instance);
            var wert = (decimal?)info.GetValue(obj);

            if (wert != WertNeu)
            {
                info.SetValue(obj, WertNeu);

                var attr = GetAttrib(MemberExp);


                return true;
            }

            return false;
        }

        public static string JgDruck<T>(this T obj, Expression<Func<T, object>> value)
            where T : class
        {
            var member = value.Body as MemberExpression;
            var unary = value.Body as UnaryExpression;
            var MemberExp =  member ?? (unary != null ? unary.Operand as MemberExpression : null);

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
            else if (type == typeof(int))
            {


            }
            else if (type == typeof(int?))
            {



            }
            else if (type == typeof(decimal))
            {



            }
            else if (type == typeof(decimal?))
            {

            }
            else if (type == typeof(DateTime))
            {

            }
            else if (type == typeof(DateTime?))
            {

            }
            else
            {

            }





            return "";
        }
    }
}
