using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace JgDatevExportAnzeige
{
    public partial class FormEditVorgabeWert : Window
    {
        public string Ergebniss
        {
            get => tbFeldVorgabe.Text;
        }

        public FormEditVorgabeWert(Type MyType, object MyObject, string FeldName, int Min, int Max)
        {
            InitializeComponent();

            tblFeldname.Text = FeldName;
            tblMin.Text = Min.ToString();
            tblMax.Text = Max.ToString();

            var myBinding = new Binding(FeldName)
            {
                Source = MyObject,
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                ValidatesOnExceptions = true,
                UpdateSourceExceptionFilter = new UpdateSourceExceptionFilterCallback((Bind, Exep) =>
                {
                    lblError.Text = Exep.Message;
                    return "";
                })
            };

            tbFeldVorgabe.SetBinding(TextBox.TextProperty, myBinding);
        }

        private void Click_Fertig(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void tbFeldVorgabe_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            lblError.Text = "";
        }
    }
}
