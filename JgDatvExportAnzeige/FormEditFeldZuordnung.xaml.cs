using System;
using System.Windows;
using static JgDatevExportLib.DatevFeldZuordnung;

namespace JgDatevExportAnzeige
{
    public partial class FormEditFeldZuordnung : Window
    {
        public string FeldZuordnung => cbFeldZuordnen.SelectedItem.ToString();

        public FormEditFeldZuordnung()
        {
            InitializeComponent();
            cbFeldZuordnen.ItemsSource = Enum.GetNames(typeof(FelderZuordnung));
        }

        private void Click_Fertig(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
