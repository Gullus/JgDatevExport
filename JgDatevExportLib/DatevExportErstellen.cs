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
        public DatevKoerper DatevKoerper { get => _DatevKoerper; set => _DatevKoerper = value; }

        private DatevFeldZuordnung _Zuordnung = new DatevFeldZuordnung(DatevFeldZuordnung.ZuordnungArt.FelderEintragen);

        private List<string> _AusgabeKoerper = new List<string>();
        private string _Pfad = null;
        private string _DatName = null;

        public DatevExportErstellen(string Pfad, string DatName)
        {
            _Pfad = Pfad;
            _DatName = DatName;

            DatevHelper.DatenLaden(DatevHelper.GetNameConfigDatei(), ref _DatevHeader, ref _DatevKoerper);
            _Zuordnung.StringInFelder(DatevKoerper.FelderZuordnungDatevExport);
        }

        public void SetzeWert(EnumFelderZuordnung FeldZuordnung, object Wert)
        {
            var feld = _Zuordnung[FeldZuordnung.ToString()];
            if (feld != "-")
            {
                var info = typeof(DatevKoerper).GetProperty(feld);
                if (info != null)
                {
                    if (info.PropertyType == typeof(Int32))
                    {
                        if ((Wert != null) && (Wert == typeof(Int32)))
                            info.SetValue(DatevKoerper, Wert, null);
                        else
                            info.SetValue(DatevKoerper, Convert.ToInt32(Wert), null);
                    } else if (info.PropertyType == typeof(Decimal))
                    {
                        if ((Wert != null) && (Wert == typeof(Decimal)))
                            info.SetValue(DatevKoerper, Wert, null);
                        else
                            info.SetValue(DatevKoerper, Convert.ToDecimal(Wert), null);
                    }
                    else if (info.PropertyType == typeof(DateTime))
                    {
                        if ((Wert != null) && (Wert == typeof(DateTime)))
                            info.SetValue(DatevKoerper, Wert, null);
                        else
                            info.SetValue(DatevKoerper, Convert.ToDateTime(Wert), null);
                    }
                    else
                        info.SetValue(DatevKoerper, Wert.ToString(), null);
                }
            }
        }

        public void SchreibeDatensatz()
        {
            _AusgabeKoerper.Add(DatevKoerper.ToString());
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
