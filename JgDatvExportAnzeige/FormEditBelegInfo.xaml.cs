﻿using JgDatevExportLib;
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
            int zaehler = 0;
            foreach(var ds in _Koerper.BelegInfo)
            {
                zaehler++;
                ds.Id = 19 + (2 * zaehler);
                ds.Feldname = "Beleginfo " + zaehler.ToString();
            }

            var vsKoerper = (CollectionViewSource)this.FindResource("vsZusatzInfo");
            vsKoerper.Source = _Koerper.BelegInfo;
        }

        private void Click_Fertig(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
