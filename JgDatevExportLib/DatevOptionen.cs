using System;

namespace JgDatevExportLib
{
    [Serializable]
    public class DatevOptionen
    {
        private string _FormatKontonummer = "{0}";
        public string FormatKontonummer { get => _FormatKontonummer; set => _FormatKontonummer = value; }

        private string _FormatGegenkonto = "{0}";
        public string FormatGegenkonto { get => _FormatGegenkonto; set => _FormatGegenkonto = value; }

        private string _FormatMandantennummer = "{0}";
        public string FormatMandantennummer { get => _FormatMandantennummer; set => _FormatMandantennummer = value; }

        private int _CodierungZeichen = -100;
        public int CodierungZeichen { get => _CodierungZeichen; set => _CodierungZeichen = value; }

    }
}

