using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace JgDatevExportAnzeige
{
    public partial class FormEditVorgabeWert : Window
    {
        private readonly Type _MyType;
        private readonly object _MyObject;
        private readonly string _FeldName;

        public string Ergebniss
        {
            get => tbFeldVorgabe.Text;
        }

        public FormEditVorgabeWert(Type MyType, object MyObject, string FeldName, int Min, int Max)
        {
            InitializeComponent();

            _MyType = MyType;
            _MyObject = MyObject;
            _FeldName = FeldName;

            tblFeldname.Text = FeldName;
            tblMin.Text = Min.ToString();
            tblMax.Text = Max.ToString();

            var info = _MyType.GetProperty(_FeldName);
            if (Nullable.GetUnderlyingType(info.PropertyType) != null)
                btnNullEintragen.Visibility = Visibility.Visible;
            else
                btnNullEintragen.Visibility = Visibility.Hidden;

            var myBinding = new Binding(_FeldName)
            {
                Source = _MyObject,
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

        private void Click_NullEintragen(object sender, RoutedEventArgs e)
        {
            var info = _MyType.GetProperty(_FeldName); //, BindingFlags.NonPublic | BindingFlags.Instance);
            info.SetValue(_MyObject, null);
        }
    }
}
