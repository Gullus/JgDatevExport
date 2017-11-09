using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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

        public DatevExportErstellen(string Pfad)
        {
            var lad = Helper.DatenLaden(Helper.GetNameConfigDatei());
            DatevHeader = lad.Header;
            _DatevKoerper = lad.Koerper;
            _Zuordnung.StringInFelder(_DatevKoerper.FelderZuordnungDatevExport);
        }

        public void SetzeWert(DatevFeldZuordnung FeldZuordnung, object Wert)
        {
            var info = typeof(DatevHeader).GetProperty(_Zuordnung[FeldZuordnung.ToString()]);
            info.SetValue(_DatevKoerper, Wert);
        }

        public void SchreibeDatensatz()
        {
            _AusgabeKoerper.Add(_DatevKoerper.ToString());
        }

        public void SchreibeInDatei()
        {
            var sb = new StringBuilder();
            sb.AppendLine(_DatevHeader.ToString());
            sb.AppendLine(Properties.Resource.KoerperHeader);
            foreach (var ds in _AusgabeKoerper)
                sb.AppendLine(ds);
            try
            {
                File.WriteAllText(_DateiName, sb.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fehler bei speichern der Datei '{_DateiName}'. \nGrund: {ex.Message}", "Fehler !", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            MessageBox.Show($"Datei '{_DateiName}' erfolgreich erstellt.", "Fehler !", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
