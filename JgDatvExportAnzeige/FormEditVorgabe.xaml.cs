using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace JgDatvExportAnzeige
{
    public partial class FormEditVorgabe : Window
    {
        public FormEditVorgabe(Type MyType, object MyObject, string FeldName)
        {
            InitializeComponent();

            var info = MyType.GetProperty(FeldName);

            if (info.PropertyType.IsEnum)
            {
                tbFeldVorgabe.Visibility = Visibility.Collapsed;

                cbFeldVorgabe.ItemsSource = Enum.GetNames(info.PropertyType);
                cbFeldVorgabe.SelectionChanged += (sen, erg) => info.SetValue(MyObject, Enum.Parse(info.PropertyType, cbFeldVorgabe.SelectedItem.ToString()));
                cbFeldVorgabe.SelectedItem = info.GetValue(MyObject).ToString();
            }
            else
            {
                var myBinding = new Binding(FeldName);
                myBinding.Source = MyObject;

                cbFeldVorgabe.Visibility = Visibility.Collapsed;
                tbFeldVorgabe.SetBinding(TextBox.TextProperty, myBinding);
            }
        }

        private void Click_Fertig(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
