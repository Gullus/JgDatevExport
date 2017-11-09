using JgDatevExportLib;
using System.Windows;
using System.Windows.Data;

namespace JgDatevExportAnzeige
{
    public partial class FormEditBelegInfo : Window
    {

        private DatevKoerper _Koerper;

        public FormEditBelegInfo(DatevKoerper Koerper)
        {
            InitializeComponent();
            _Koerper = Koerper;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var vsKoerper = (CollectionViewSource)this.FindResource("vsZusatzInfo");
            vsKoerper.Source = _Koerper.ZusatzInformation;
        }

        private void Click_Fertig(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
