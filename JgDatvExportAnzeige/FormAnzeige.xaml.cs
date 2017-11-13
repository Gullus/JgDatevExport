using JgDatevExportLib;
using System.Windows;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Input;

namespace JgDatevExportAnzeige
{
    public partial class FormAnzeige : RibbonWindow
    {
        private CollectionViewSource _VsAnzeigeDaten { get => (CollectionViewSource)this.FindResource("vsDaten"); }

        private DatevHeader _DatevHeader = new DatevHeader();
        private DatevKoerper _DatevKoerper = new DatevKoerper();
        private DatevFeldZuordnung _Zuordnung = new DatevFeldZuordnung(DatevFeldZuordnung.ZuordnungArt.DatevModus);

        private enum EnumAuswahlAnzeige
        {
            Header,
            Koerper
        }
        private EnumAuswahlAnzeige _AuswahlAnzeige = EnumAuswahlAnzeige.Header;

        public FormAnzeige()
        {
            InitializeComponent();
            InitCommands();
        }

        private void RibbonWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Wenn öffnen Fehlschlägt, werden die Objekte mit Vorgeben genommen.
            try
            {
                var lad = DatevHelper.DatenLaden(DatevHelper.GetNameConfigDatei());
                _DatevHeader = lad.Header;
                _DatevKoerper = lad.Koerper;
                _Zuordnung.StringInFelder(_DatevKoerper.FelderZuordnungDatevExport);
            }
            catch { }

            _VsAnzeigeDaten.Source = _DatevHeader.ListeAnzeigeErstellen();
        }

        private void InitCommands()
        {
            CommandBindings.Add(new CommandBinding(MyCommands.HeaderAnzeigen, (sen, erg) =>
            {
                _AuswahlAnzeige = EnumAuswahlAnzeige.Header;
                ColZuordnungFeld.Visibility = Visibility.Collapsed;
                ColZuordnungSchalter.Visibility = Visibility.Collapsed;
                _VsAnzeigeDaten.Source = _DatevHeader.ListeAnzeigeErstellen();
            }, (sen, erg) => erg.CanExecute = _AuswahlAnzeige != EnumAuswahlAnzeige.Header));

            CommandBindings.Add(new CommandBinding(MyCommands.KoerperAnzeigen, (sen, erg) =>
            {
                _AuswahlAnzeige = EnumAuswahlAnzeige.Koerper;
                ColZuordnungFeld.Visibility = Visibility.Visible;
                ColZuordnungSchalter.Visibility = Visibility.Visible;
                var lAnzeige = _DatevKoerper.ListeAnzeigeKoerperErstellen();

                foreach (var ds in lAnzeige)
                    ds.OnGetFeldZuordnung = GetZuordnungZuFeld;

                _VsAnzeigeDaten.Source = lAnzeige;
            }, (sen, erg) => erg.CanExecute = _AuswahlAnzeige != EnumAuswahlAnzeige.Koerper));

            CommandBindings.Add(new CommandBinding(MyCommands.BelegInformationAnzeigen, (sen, erg) =>
            {
                var fo = new FormEditBelegInfo(_DatevKoerper);
                fo.Show();
            }));

            CommandBindings.Add(new CommandBinding(MyCommands.ZusatzInformationAnzeigen, (sen, erg) =>
            {
                var fo = new FormEditZusatzInfo(_DatevKoerper);
                fo.Show();
            }));

            CommandBindings.Add(new CommandBinding(MyCommands.Speichern, (sen, erg) =>
            {
                _DatevKoerper.FelderZuordnungDatevExport = _Zuordnung.ToString();
                DatevHelper.DatenSpeichern(DatevHelper.GetNameConfigDatei(), _DatevHeader, _DatevKoerper);
            }));

            CommandBindings.Add(new CommandBinding(MyCommands.Beenden, (sen, erg) =>
            {
                this.Close();
            }));
        }

        private string GetZuordnungZuFeld(string FeldName)
        {
            return _Zuordnung[FeldName];
        }

        private void EditFeldZuordnung(object sender, RoutedEventArgs e)
        {
            var fo = new FormEditFeldZuordnung();
            fo.ShowDialog();
            var item = (DsListeAnzeige)_VsAnzeigeDaten.View.CurrentItem;
            if (fo.FeldZuordnung == "leer")
                _Zuordnung[item.FeldName] = null;
            else
                _Zuordnung[item.FeldName] = fo.FeldZuordnung;
            item.NotifyPropertyChanged("FeldZuordnung");
        }

        private void EditWertVorgabe(object sender, RoutedEventArgs e)
        {
            var item = (DsListeAnzeige)_VsAnzeigeDaten.View.CurrentItem;

            if (_AuswahlAnzeige == EnumAuswahlAnzeige.Header)
            {
                var info = typeof(DatevHeader).GetProperty(item.FeldName);
                if (info.PropertyType.IsEnum)
                {
                    var fo = new FormEditVorgabeEnum(typeof(DatevHeader), _DatevHeader, item.FeldName);
                    if (fo.ShowDialog() ?? false)
                        (item as DsListeAnzeige).FeldWert = fo.Ergebniss;
                }
                else
                {
                    var fo = new FormEditVorgabeWert(typeof(DatevHeader), _DatevHeader, item.FeldName, item.Min, item.Max);
                    if (fo.ShowDialog() ?? false)
                        (item as DsListeAnzeige).FeldWert = fo.Ergebniss;
                }
            }
            else
            {
                var info = typeof(DatevKoerper).GetProperty(item.FeldName);
                if (info.PropertyType.IsEnum)
                {
                    var fo = new FormEditVorgabeEnum(typeof(DatevKoerper), _DatevKoerper, item.FeldName);
                    if (fo.ShowDialog() ?? false)
                        (item as DsListeAnzeige).FeldWert = fo.Ergebniss;
                }
                else
                {
                    var fo = new FormEditVorgabeWert(typeof(DatevKoerper), _DatevKoerper, item.FeldName, item.Min, item.Max);
                    if (fo.ShowDialog() ?? false)
                        (item as DsListeAnzeige).FeldWert = fo.Ergebniss;
                }
            }
        }
    }
}
