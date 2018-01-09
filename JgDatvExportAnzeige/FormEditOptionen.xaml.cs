using JgDatevExportLib;
using System.Text;
using System.Windows;
using System.Linq;
using System.Collections.Generic;

namespace JgDatevExportAnzeige
{

    public partial class FormEditOptionen : Window
    {

        public FormEditOptionen(DatevOptionen Optionen)
        {
            InitializeComponent();

            var dicEncoding = new Dictionary<int, string>();
            dicEncoding.Add(-100, "Default");
            dicEncoding.Add(-101, "ASCII");
            dicEncoding.Add(-102, "Unicode");
            dicEncoding.Add(-103, "UTF7");
            dicEncoding.Add(-104, "UTF8");
            dicEncoding.Add(-105, "UTF32");

            foreach(var enc in Encoding.GetEncodings().OrderBy(o => o.DisplayName))
                dicEncoding.Add(enc.CodePage, enc.DisplayName);
            cmbEncoding.ItemsSource = dicEncoding;

            GridOptionen.DataContext = Optionen;
        }

        private void Click_Fertig(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
