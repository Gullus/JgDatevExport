using JgDatevExportLib;
using JgDatvExportAnzeige;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Input;

namespace JgDatevExportAnzeige
{
    public partial class FormAnzeige : RibbonWindow
    {
        private CollectionViewSource _VsDaten { get => (CollectionViewSource)this.FindResource("vsDaten"); }

        private DatevHeader _DatevHeader = new DatevHeader();
        private DatevKoerper _DatevKoerper = new DatevKoerper();

        private Dictionary<string, string> _ListeFeldZuordnungen = new Dictionary<string, string>();

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
            _VsDaten.Source = _DatevHeader.ListeAnzeigeErstellen();
        }

        private void InitCommands()
        {
            CommandBindings.Add(new CommandBinding(MyCommands.HeaderAnzeigen, (sen, erg) =>
            {
                _AuswahlAnzeige = EnumAuswahlAnzeige.Header;
                _VsDaten.Source = _DatevHeader.ListeAnzeigeErstellen();
            }, (sen, erg) => erg.CanExecute = _AuswahlAnzeige != EnumAuswahlAnzeige.Header));

            CommandBindings.Add(new CommandBinding(MyCommands.KoerperAnzeigen, (sen, erg) =>
            {
                _AuswahlAnzeige = EnumAuswahlAnzeige.Koerper;
                _VsDaten.Source = _DatevKoerper.ListeAnzeigeKoerperErstellen();
            }, (sen, erg) => erg.CanExecute = _AuswahlAnzeige != EnumAuswahlAnzeige.Koerper));

            CommandBindings.Add(new CommandBinding(MyCommands.BeleginfoAnzeigen, (sen, erg) =>
            {
                var fo = new FormEditBelegInfo(_DatevKoerper);
                fo.Show();
            }));

            CommandBindings.Add(new CommandBinding(MyCommands.ZusatzInformationAnzeigen, (sen, erg) =>
            {
                var fo = new FormEditBelegInfo(_DatevKoerper);
                fo.Show();
            }));

            CommandBindings.Add(new CommandBinding(MyCommands.Beenden, (sen, erg) =>
            {
                this.Close();
            }));
        }

        private void EditFeldZuordnung(object sender, RoutedEventArgs e)
        {
            var fo = new FormEditFeldZuordnung();
            fo.ShowDialog();
            var item = (DsListeAnzeige)_VsDaten.View.CurrentItem;
            if (fo.FeldZuordnung == "leer")
            {
                item.FeldZuordnung = null;
                _ListeFeldZuordnungen.Remove(item.FeldName);
            }
            else
            {
                item.FeldZuordnung = fo.FeldZuordnung;
                _ListeFeldZuordnungen[item.FeldName] = item.FeldZuordnung;
            }
        }

        private void EditWertVorgabe(object sender, RoutedEventArgs e)
        {
            var item = (DsListeAnzeige)_VsDaten.View.CurrentItem;
            var fo = new FormEditVorgabe(typeof(DatevHeader), _DatevHeader, item.FeldName);
            fo.ShowDialog();
            var info = typeof(DatevHeader).GetProperty(item.FeldName);
            (item as DsListeAnzeige).FeldWert = info.GetValue(_DatevHeader).ToString();
        }
    }
}
