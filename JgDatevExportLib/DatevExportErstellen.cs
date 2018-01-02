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

        private DatevOptionen _DatevOptionen = new DatevOptionen();
        public DatevOptionen DatevOptionen { get => _DatevOptionen; set => _DatevOptionen = value; }

        private DatevFeldZuordnung _Zuordnung = new DatevFeldZuordnung(DatevFeldZuordnung.ZuordnungArt.FelderEintragen);

        private List<string> _AusgabeKoerper = new List<string>();
        private string _Pfad = null;
        private string _DatName = null;

        public DatevExportErstellen(string Pfad, string DatName)
        {
            _Pfad = Pfad;
            _DatName = DatName;

            DatevHelper.DatenLaden(DatevHelper.GetNameConfigDatei(), ref _DatevHeader, ref _DatevKoerper, ref _DatevOptionen);
            _Zuordnung.StringInFelder(DatevKoerper.FelderZuordnungDatevExport);
        }

        public void SetzeWert(EnumFelderZuordnung FeldZuordnung, object Wert)
        {
            var feld = _Zuordnung[FeldZuordnung.ToString()];
            
            if (feld != "-")
            {
                try
                {
                    //var msg = string.Format("Eintragen der Daten das Objekt. Eintrag in Feld: {0} ; von Feld: {1}; Wert: {2}", feld, FeldZuordnung, Wert);
                    //MessageBox.Show(msg);

                    var info = typeof(DatevKoerper).GetProperty(feld);
                    var type = info.PropertyType;
                    if (Nullable.GetUnderlyingType(type) != null)
                        type = Nullable.GetUnderlyingType(type);

                    if (info != null)
                    {
                        if (Wert == null)
                            info.SetValue(DatevKoerper, null, null);
                        else
                        {
                            if (type == typeof(Int32))
                                info.SetValue(DatevKoerper, Convert.ToInt32(Wert), null);
                            else if (type == typeof(Decimal))
                                info.SetValue(DatevKoerper, Convert.ToDecimal(Wert), null);
                            else if (info.PropertyType == typeof(DateTime))
                                info.SetValue(DatevKoerper, Convert.ToDateTime(Wert), null);
                            else
                                info.SetValue(DatevKoerper, Wert, null);
                        }
                    }
                }
                catch (Exception f)
                {
                    var msg = string.Format("Fehler bei eintragen der Daten das Objekt. Eintrag in Feld: {0} ; von Feld: {1}; Wert: {2}", feld, FeldZuordnung, Wert);
                    throw new Exception(msg + "\nGrund: " + f.Message, f);
                }
            }
        }

        public void SchreibeDatensatz()
        {
            _AusgabeKoerper.Add(DatevKoerper.ToString());
        }

        public static string DateinameAusgabe(string Pfad, string DateiName)
        {
            return Pfad + @"\EXTF_" + DateiName + "_" + DateTime.Now.ToString("ddMMyy_HHmmss") + ".csv";
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
