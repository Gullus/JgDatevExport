using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace JgDatevExportAnzeige
{
    public partial class FormEditVorgabeEnum : Window
    {
        public string Ergebniss
        {
            get => cbFeldVorgabe.SelectedItem.ToString();
        }

        public FormEditVorgabeEnum(Type MyType, object MyObject, string FeldName)
        {
            InitializeComponent();

            tblFeldname.Text = FeldName;

            var info = MyType.GetProperty(FeldName);
            cbFeldVorgabe.ItemsSource = Enum.GetNames(info.PropertyType);
            cbFeldVorgabe.SelectionChanged += (sen, erg) => info.SetValue(MyObject, Enum.Parse(info.PropertyType, cbFeldVorgabe.SelectedItem.ToString()));
            cbFeldVorgabe.SelectedItem = info.GetValue(MyObject).ToString();
        }

        private void Click_Fertig(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
