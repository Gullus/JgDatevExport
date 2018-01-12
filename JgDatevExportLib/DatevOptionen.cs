using System;

namespace JgDatevExportLib
{
    [Serializable]
    public class DatevOptionen
    {
        private string _FormatKontonummerEingangsrechnung = "{0}";
        public string FormatKontonummerEingangsrechnung { get => _FormatKontonummerEingangsrechnung; set => _FormatKontonummerEingangsrechnung = value; }

        private string _FormatGegenkontoEingangsrechnung = "{0}";
        public string FormatGegenkontoEingangsrechnung { get => _FormatGegenkontoEingangsrechnung; set => _FormatGegenkontoEingangsrechnung = value; }

        private string _FormatKontonummerAusgangsrechnung = "{0}";
        public string FormatKontonummerAusgangsrechnung { get => _FormatKontonummerAusgangsrechnung; set => _FormatKontonummerAusgangsrechnung = value; }

        private string _FormatGegenkontoAusgangsrechnung = "{0}";
        public string FormatGegenkontoAusgangsrechnung { get => _FormatGegenkontoAusgangsrechnung; set => _FormatGegenkontoAusgangsrechnung = value; }

        private bool _SollHabenTauschen = false;
        public bool SollHabenTauschen { get => _SollHabenTauschen; set => _SollHabenTauschen = value; }

        private int _CodierungZeichen = -100;
        public int CodierungZeichen { get => _CodierungZeichen; set => _CodierungZeichen = value; }

        private bool _BackUpDateiAnlegen = true;
        public bool BackUpDateiAnlegen { get => _BackUpDateiAnlegen; set => _BackUpDateiAnlegen = value; }
    }
}

