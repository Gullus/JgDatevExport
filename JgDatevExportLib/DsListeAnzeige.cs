using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace JgDatevExportLib
{
    interface IListeAnzeige
    {
        List<DsListeAnzeige> ListeAnzeigeErstellen();
    }

    public class DsListeAnzeige : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public int Id { get; set; }
        public string FeldName { get; set; }
        public string TypeName { get; set; }
        public bool IstErforderlich { get; set; }
        public int Min { get; set; }
        public int Max { get; set; }
        public string Format { get; set; }

        private string _FeldZuordnung = null;
        public string FeldZuordnung
        {
            get => _FeldZuordnung;
            set
            {
                if (_FeldZuordnung != value)
                {
                    _FeldZuordnung = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string _FeldWert = null;
        public string FeldWert
        {
            get => _FeldWert;
            set
            {
                if (value != _FeldWert)
                {
                    _FeldWert = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public override string ToString()
        {
            return FeldName;
        }
    }
}
