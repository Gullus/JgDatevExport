using JgDatevExportLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
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

                try
                {
                    var erst = new DatevExportErstellen(pfad, "Buch_1");

                    erst.DatevHeader.DatumVon = DateTime.Now.AddDays(-1);
                    erst.DatevHeader.DatumBis = DateTime.Now.AddDays(1);

                    erst.DatevHeader.ErzeugtAm = DateTime.Now;
                    erst.DatevHeader.DiktatKürzel = "JG";


                    for (int i = 0; i < 5; i++)
                    {
                        erst.SetzeWert<Decimal>(DatevEnum.EnumFelderZuordnung.Umsatz, 10.358m);
                        erst.SetzeWert<EnumSollHaben>(DatevEnum.EnumFelderZuordnung.SollHaben, EnumSollHaben.Soll);
                        erst.SetzeWert<String>(DatevEnum.EnumFelderZuordnung.Konto,"12343432");

                        erst.SetzeWert<String>(DatevEnum.EnumFelderZuordnung.GegenKonto, "258624587");
                        erst.SetzeWert<DateTime>(DatevEnum.EnumFelderZuordnung.BelegDatum, DateTime.Now);

                        erst.SetzeWert<String>(EnumFelderZuordnung.Belegnummer, "123" + i.ToString());

                        erst.SchreibeDatensatz();
                    }

                    erst.SchreibeInDatei();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
