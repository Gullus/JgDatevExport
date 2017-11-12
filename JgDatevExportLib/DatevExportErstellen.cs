using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using static JgDatevExportLib.DatevEnum;

namespace JgDatevExportLib
{
    public class DatevExportErstellen
    {
        private DatevHeader _DatevHeader = new DatevHeader();
        public DatevHeader DatevHeader { get => _DatevHeader; set => _DatevHeader = value; }

        private DatevKoerper _DatevKoerper = new DatevKoerper();
        private DatevFeldZuordnung _Zuordnung = new DatevFeldZuordnung(DatevFeldZuordnung.ZuordnungArt.FelderEintragen);

        private List<string> _AusgabeKoerper = new List<string>();
        private string _Pfad = null;
        private string _DatName = null;

        public DatevExportErstellen(string Pfad, string DatName)
        {
            _Pfad = Pfad;
            _DatName = DatName;

            var lad = DatevHelper.DatenLaden(DatevHelper.GetNameConfigDatei());
            DatevHeader = lad.Header;
            _DatevKoerper = lad.Koerper;
            _Zuordnung.StringInFelder(_DatevKoerper.FelderZuordnungDatevExport);
        }

        public void SetzeWert<T>(EnumFelderZuordnung FeldZuordnung, T Wert)
        {
            var info = typeof(DatevKoerper).GetProperty(_Zuordnung[FeldZuordnung.ToString()]);
            if (info != null)
                info.SetValue(_DatevKoerper, Wert);
        }

        public void SchreibeDatensatz()
        {
            _AusgabeKoerper.Add(_DatevKoerper.ToString());
        }

        public static string DateinameAusgabe(string Pfad, string DateiName)
        {
            return Pfad + @"\EXTF_" + DateiName + "_" + DateTime.Now.ToString("ddMMyy_mmHH") + ".csv";
        }

        public void SchreibeInDatei()
        {
            var datName = DateinameAusgabe(_Pfad, _DatName);

            var sb = new StringBuilder();
            sb.AppendLine(_DatevHeader.ToString());
            sb.AppendLine(Properties.Resource.KoerperHeader);
            foreach (var ds in _AusgabeKoerper)
                sb.AppendLine(ds);
            try
            {
                File.WriteAllText(datName, sb.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fehler bei speichern der Datei '{datName}'. \nGrund: {ex.Message}", "Fehler !", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            MessageBox.Show($"Datei '{datName}' erfolgreich erstellt.", "Fehler !", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
