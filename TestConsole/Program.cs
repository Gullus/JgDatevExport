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
                var format = "{0}";

                Console.WriteLine(string.Format(format, "66"));

                Console.ReadKey();

                //var pfad = DatevHelper.PfadProgramm();

                //var erst = new DatevExportErstellen(pfad, "Buch_1");

                //erst.DatevHeader.DatumVon = DateTime.Now.AddDays(-1);
                //erst.DatevHeader.DatumBis = DateTime.Now.AddDays(1);

                //erst.DatevHeader.ErzeugtAm = DateTime.Now;
                //erst.DatevHeader.DiktatKürzel = "JG";

                //erst.DatevKoerper.Umsatz = 47363.456m;

                //Console.WriteLine(erst.DatevKoerper.Umsatz);


                //for (int i = 0; i < 5; i++)
                //{
                //    //erst.SetzeWert(DatevEnum.EnumFelderZuordnung.NettoBetrag, (785587).ToString());
                //    //erst.DatevKoerper.SollHaben = EnumSollHaben.Soll;
                //    //erst.SetzeWert(DatevEnum.EnumFelderZuordnung.Kontonummer, "12343432");

                //    //erst.SetzeWert(DatevEnum.EnumFelderZuordnung.Gegenkonto, "258624587");
                //    //erst.SetzeWert(DatevEnum.EnumFelderZuordnung.Belegdatum, DateTime.Now.ToString());

                //    //erst.SetzeWert(EnumFelderZuordnung.Belegnummer, "123" + i.ToString());

                //    double ddd = 1.2587;
                //    erst.SetzeWert(EnumFelderZuordnung.NettoBetrag, ddd);

                //    ddd = 54.487;
                //    erst.SetzeWert(EnumFelderZuordnung.Menge, ddd);



                //    erst.SchreibeDatensatz();
                //}

                //erst.SchreibeInDatei();
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
