using JgDatevExportLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole
{
    namespace ConsoleApplication1
    {
        //[AttributeUsage(AttributeTargets.Property)]
        //public class StringLaengeAttribute : Attribute
        //{
        //    public int Max { get; set; }

        //    public StringLaengeAttribute()
        //    { }
        //}

        //public static class MyClassExtensions
        //{
        //    public static int GetStringLaengeAttribute<T>(this T obj, Expression<Func<T, string>> value)
        //    {
        //        var memberExpression = value.Body as MemberExpression;
        //        var attr = memberExpression.Member.GetCustomAttributes(typeof(StringLaengeAttribute), true);
        //        return ((StringLaengeAttribute)attr[0]).Max;
        //    }

        //    public static string MeinTester<T>(this T obj, Expression<Func<T, string>> value, string Zusatzwert)
        //    {
        //        var mExpression = (MemberExpression)value.Body;
        //        var type = obj.GetType();
        //        var prop = type.GetProperty(mExpression.Member.Name);
        //        var wert =  prop.GetValue(obj);
        //        return $"Feld: {mExpression.Member.Name} Wert: {wert} {Zusatzwert}";
        //    }
        //}

        //public  class MyTest
        //{
        //    [StringLaenge(Max = 23)]
        //    public string TestString { get; set; } = "Hallo Ballo";
        //}

        class Program
        {

            static void Main(string[] args)
            {

                var koerper = new DatevHeader();
                Console.WriteLine(koerper.ToString());


                //Console.WriteLine(Properties.Resource.ResourceManager.GetString("String1"));

                Console.ReadKey();
            }
        }
    }



    //class Program
    //{
    //    //static void Main(string[] args)
    //    //{
    //    //    var t = new DatevHeader();
    //    //    Console.WriteLine(t.ToString());

    //    //    Console.ReadKey();

    //    //}

    //    //public static int Max(string PropName)
    //    //{
    //    //    var t = typeof(DatevHeader);
    //    //    var p = t.GetProperty(PropName);    

    //    //    if (p.GetCustomAttributes(typeof(StringLengthAttribute), false).Length > 0)
    //    //    {
    //    //        var attr = (StringLengthAttribute)p.GetCustomAttributes(typeof(StringLengthAttribute), true)[0];
    //    //        return attr.Max;
    //    //    }
    //    //    return -1;
    //    //}
    //}
}
