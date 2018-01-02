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
                try
                {
                    var pfad = DatevHelper.PfadProgramm();

                    var erst = new DatevExportErstellen(pfad, "Buch_1");

                    erst.DatevHeader.DatumVon = DateTime.Now.AddDays(-1);
                    erst.DatevHeader.DatumBis = DateTime.Now.AddDays(1);

                    erst.DatevHeader.ErzeugtAm = DateTime.Now;
                    erst.DatevHeader.DiktatKürzel = "JG";

                    erst.DatevKoerper.Umsatz = 47363.456m;

                    Console.WriteLine(erst.DatevKoerper.Umsatz);


                    for (int i = 0; i < 5; i++)
                    {

                        erst.DatevKoerper.SollHaben = EnumSollHaben.Soll;
                        erst.SetzeWert(DatevEnum.EnumFelderZuordnung.Kontonummer, "1234");

                        erst.SetzeWert(DatevEnum.EnumFelderZuordnung.Gegenkonto, "4321");
                        erst.SetzeWert(DatevEnum.EnumFelderZuordnung.Belegdatum, DateTime.Now.ToString());

                        erst.SetzeWert(EnumFelderZuordnung.Belegnummer, "123" + i.ToString());

                        double ddd = 485541.2587;
                        erst.SetzeWert(EnumFelderZuordnung.NettoBetrag, ddd);

                        ddd = 565764.487;
                        erst.SetzeWert(EnumFelderZuordnung.Menge, ddd);

                        erst.SchreibeDatensatz();
                    }

                    erst.SchreibeInDatei();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.ReadKey();
                }
            }
        }
    }
}
