using JgDatevExportLib;
using JgDatvExportAnzeige;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace JgDatevExportAnzeige
{
    public partial class FormAnzeige : RibbonWindow
    {
        private CollectionViewSource _VsDaten { get => (CollectionViewSource)this.FindResource("vsDaten"); }

        private DatevHeader _DatevHeader = new DatevHeader();
        private DatevKoerper _DatevKoerper = new DatevKoerper();

        public FormAnzeige()
        {
            InitializeComponent();
            InitCommands();

        }

        private void RibbonWindow_Loaded(object sender, RoutedEventArgs e)
        {
            HeaderEinstellen();
        }

        private void HeaderEinstellen()
        {
            _VsDaten.Source = _DatevHeader.ListeAnzeigeErstellen();
        }

        private void InitCommands()
        {
            CommandBindings.Add(new CommandBinding(MyCommands.HeaderAnzeigen, (sen, erg) =>
            {
                HeaderEinstellen();
            }));
        }

        private void EditWert(object sender, RoutedEventArgs e)
        {
            var item = (DsListeAnzeige)_VsDaten.View.CurrentItem;
            var fo = new FormEditWert(typeof(DatevHeader), _DatevHeader, item.FeldName, item.IstEnum, item.EnumStringsFuerAuswahl);
            fo.ShowDialog();
        }
    }
}
