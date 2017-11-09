using System;
using System.Collections.Generic;
using System.Linq;

namespace JgDatevExportLib
{
    public class DatevFeldZuordnung
    {
        public enum ZuordnungArt
        {
            DatevModus,
            FelderEintragen
        }

        public enum FelderZuordnung
        {
            Konto,
            SollHaben,
            Kost_1,
            Kost_2,
            Wert,
            Belegnummer,
            Skonto,
            FirmaNummer,
            FirmaName,
            BuchungsDatum,
            BelegDatum
        }

        public string this[string Feld]
        {
            get
            {
                if (_DicZuordnung.ContainsKey(Feld))
                    return _DicZuordnung[Feld];
                return "-";
            } 
            set
            {
                if (_Zuordnungsart == ZuordnungArt.DatevModus)
                {
                    if (value == null)
                        _DicZuordnung.Remove(Feld);
                    else
                        _DicZuordnung[Feld] = value;
                }
            }
        }

        private ZuordnungArt _Zuordnungsart;
        private Dictionary<string, string> _DicZuordnung = new Dictionary<string, string>();

        public DatevFeldZuordnung(ZuordnungArt Zuordnung)
        {
            _Zuordnungsart = Zuordnung;
        }

        public DatevFeldZuordnung(ZuordnungArt Zuordnung, string StringFelder)
            : this(Zuordnung)
        {
            StringInFelder(StringFelder);
        }

        public void StringInFelder(string StringFelder)
        {
            _DicZuordnung.Clear();
            var ds = StringFelder.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var felder in ds)
            {
                var spalten = felder.Split(new char[] { '|' });
                if (_Zuordnungsart == ZuordnungArt.DatevModus)
                    _DicZuordnung.Add(spalten[0], spalten[1]);
                else
                    _DicZuordnung.Add(spalten[1], spalten[0]);
            }
        }

        public override string ToString()
        {
            return string.Join(";", _DicZuordnung.Select(s => s.Key + "|" + s.Value));
        }
    }
}
