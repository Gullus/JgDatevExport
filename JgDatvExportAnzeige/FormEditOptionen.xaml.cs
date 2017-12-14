using JgDatevExportLib;
using System.Windows;

namespace JgDatevExportAnzeige
{

    public partial class FormEditOptionen : Window
    {
        public FormEditOptionen(DatevOptionen Optionen)
        {
            InitializeComponent();
            GridOptionen.DataContext = Optionen;
        }

        private void Click_Fertig(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
