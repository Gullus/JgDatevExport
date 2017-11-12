using System;
using System.Windows;
using static JgDatevExportLib.DatevEnum;

namespace JgDatevExportAnzeige
{
    public partial class FormEditFeldZuordnung : Window
    {
        public string FeldZuordnung => cbFeldZuordnen?.SelectedItem?.ToString();

        public FormEditFeldZuordnung()
        {
            InitializeComponent();
            cbFeldZuordnen.ItemsSource = Enum.GetNames(typeof(EnumFelderZuordnung));
        }

        private void Click_Fertig(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
