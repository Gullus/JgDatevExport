using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JgDatevExportLib
{
    [Serializable]
    public class DatevOptionen
    {
        private string _FormatKontonummer = "{0}";
        public string FormatKontonummer { get => _FormatKontonummer; set => _FormatKontonummer = value; }

        private string _FormatGegenkonto = "{0}";
        public string FormatGegenkonto { get => _FormatGegenkonto; set => _FormatGegenkonto = value; }
    }
}
