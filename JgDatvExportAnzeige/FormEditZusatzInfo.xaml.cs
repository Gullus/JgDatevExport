using JgDatevExportLib;
using System.Windows;
using System.Windows.Data;

namespace JgDatevExportAnzeige
{
    public partial class FormEditZusatzInfo : Window
    {

        private DatevKoerper _Koerper;

        public FormEditZusatzInfo(DatevKoerper Koerper)
        {
            InitializeComponent();
            _Koerper = Koerper;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            int zaehler = 0;
            foreach(var ds in _Koerper.ZusatzInformation)
            {
                zaehler++;
                ds.Id = 46 + (zaehler * 2);
                ds.Feldname = "Zusatzinfo " + zaehler.ToString();
            }

            var vsKoerper = (CollectionViewSource)this.FindResource("vsZusatzInfo");
            vsKoerper.Source = _Koerper.ZusatzInformation;
        }

        private void Click_Fertig(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
