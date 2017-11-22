using JgDatevExportLib;
using System;
using static JgDatevExportLib.DatevEnum;

namespace TestConsole
{
    namespace ConsoleApplication1
    {
        class Program
        {

            static void Main(string[] args)
            {


                var pfad = DatevHelper.PfadProgramm();

                //try
                //{
                var erst = new DatevExportErstellen(pfad, "Buch_1");

                erst.DatevHeader.DatumVon = DateTime.Now.AddDays(-1);
                erst.DatevHeader.DatumBis = DateTime.Now.AddDays(1);

                erst.DatevHeader.ErzeugtAm = DateTime.Now;
                erst.DatevHeader.DiktatKürzel = "JG";

                //var t = typeof(DatevKoerper);
                //var inf = t.GetProperty("Umsatz");

                //inf.SetValue(erst.DatevKoerper, 5245.54m, null);

                erst.DatevKoerper.Umsatz = 47363.456m;

                Console.WriteLine(erst.DatevKoerper.Umsatz);





                for (int i = 0; i < 5; i++)
                {
                    erst.SetzeWert(DatevEnum.EnumFelderZuordnung.NettoBetrag, (785587).ToString());
                    erst.DatevKoerper.SollHaben = EnumSollHaben.Soll;
                    erst.SetzeWert(DatevEnum.EnumFelderZuordnung.Kontonummer, "12343432");

                    erst.SetzeWert(DatevEnum.EnumFelderZuordnung.Gegenkonto, "258624587");
                    erst.SetzeWert(DatevEnum.EnumFelderZuordnung.Belegdatum, DateTime.Now.ToString());

                    erst.SetzeWert(EnumFelderZuordnung.Belegnummer, "123" + i.ToString());

                    erst.SchreibeDatensatz();
                }

                erst.SchreibeInDatei();
                //}
                //catch (Exception ex)
                //{
                //    Console.WriteLine(ex.Message);
                //    Console.ReadKey();
                //}
            }
        }
    }
}
