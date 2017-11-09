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

        public void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public delegate string GetFeldZuordnungDelegate(string Feldnme);
        public GetFeldZuordnungDelegate OnGetFeldZuordnung = null;

        public int Id { get; set; }
        public string FeldName { get; set; }
        public string TypeName { get; set; }
        public bool IstErforderlich { get; set; }
        public int Min { get; set; }
        public int Max { get; set; }
        public string Format { get; set; }

        public string FeldZuordnung
        {
            get => OnGetFeldZuordnung(FeldName);
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
