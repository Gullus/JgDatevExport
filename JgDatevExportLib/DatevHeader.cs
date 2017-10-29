using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using static JgDatevExportLib.DatevEnum;

namespace JgDatevExportLib
{
    public class DatevHeader : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// EXTF = für Dateiformate, die von externen Programmen erstellt wurden
        /// DTVF = für DATEV reserviert
        /// </summary>
        public EnumDatevFormat DatevFormatKz { get; set; } = EnumDatevFormat.EXTF;

        private int _Versionsnummer = 1;
        /// <summary>
        /// Versionsnummer des Headers.
        /// Ergeben sich künftig Änderungen am Aufbau des Headers(z.B.zusätzliche Felder), kann dieser über die Headerversion abwärtskompatibel verarbeitet werden.
        /// </summary>
        public int Versionsnummer
        {
            get => _Versionsnummer;
            set
            {
                if (Helper.KontrIntOk(ref _Versionsnummer, value, "VersionsNummer", 0, 999))
                    NotifyPropertyChanged();
            }
        }

        public EnumDatenkategorie DatenKategorie { get; set; } = EnumDatenkategorie.Buchungsstapel;

        public EnumFormatname FormatName { get; set; } = EnumFormatname.Buchungsstapel;   // Unterstriche umwandeln !

        /// <summary>
        /// Versionsnummer des Formats. Ergeben sich künftig Änderungen am Aufbau des Formats(z.B.neue Felder), kann dies über die Formatversion abwärtskompatibel verarbeitet werden.
        /// </summary>
        public EnumFormatversion FormatVersion { get; set; } = EnumFormatversion.Buchungsstapel_7;

        public DateTime? ErzeugtAm { get; set; } = null;

        /// <summary>
        /// Das Header-Feld darf nicht gefüllt werden. Wird durch den Import gesetzt.
        /// </summary>
        public long? Importiert { get; } = null;   // Wird beim Import in Datev belegt
 
        private string _Herkunft = "RE";
        /// <summary>
        /// Herkunfts-Kennzeichen 2 frei wählbare Zeichen.
        /// Beim Import wird das Herkunfts-Kennzeichen durch „SV“ (= Stapelverarbeitung) ersetzt.
        /// </summary>
        public string Herkunft
        {
            get => _Herkunft;
            set
            {
                if (Helper.KontrStringOk(ref _Herkunft, value, "Herkunft", 1, 2))
                    NotifyPropertyChanged();
            }
        }

        private string _ExportiertVon = "";
        /// <summary>
        /// Beim Export aus einem DATEV pro-Rechnungswesen-Programm wird der Benutzername des Users exportiert, der den Export durchgeführt hat.
        /// </summary>
        public string ExportiertVon
        {
            get => _ExportiertVon;
            set
            {
                if (Helper.KontrStringOk(ref _ExportiertVon, value, "ExportiertVon", 0, 25))
                    NotifyPropertyChanged();
            }
        }

        private string _ImportiertVon = "";
        /// <summary>
        /// Wird durch den Import gesetzt.
        /// Beim Import in das DATEV pro-Rechnungswesen-Programm wird der Benutzername des Users verwendet.
        /// </summary>
        public string ImportiertVon
        {
            get => _ImportiertVon;
            set
            {
                if (Helper.KontrStringOk(ref _ImportiertVon, value, "ImportiertVon", 0, 25))
                    NotifyPropertyChanged();
            }
        }

        private int _BeraterNummer = 1000;
        public int Beraternummer
        {
            get => _BeraterNummer;
            set
            {
                if (Helper.KontrIntOk(ref _BeraterNummer, value, "Beraternummer", 1000, 9999999))
                    NotifyPropertyChanged();
            }
        }

        private int _MandantenNummer = 1;
        public int MandantenNummer
        {
            get => _MandantenNummer;
            set
            {
                if (Helper.KontrIntOk(ref _MandantenNummer, value, "Mandantennummer", 1, 99999))
                    NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Wirtschaftsjahresbeginn
        /// </summary>
        public DateTime WjBegin { get; set; } = new DateTime(DateTime.Now.Year, 1, 1);

        /// <summary>
        /// Kleinste Sachkontennummernlänge = 4 ➝ Debitoren/Kreditoren dann 5 Stellen
        /// Größe Sachkontennummernlänge = 8 ➝ Debitoren/Kreditoren dann 9 Stellen
        /// </summary>
        public EnumSachkontenNummernLaenge SachkontenNummernLaenge { get; set; } = EnumSachkontenNummernLaenge.Stellen_4;

        /// <summary>
        /// Datum „von“ des Buchungsstapels -> Wird vom Programm gesetzt
        /// </summary>
        public DateTime DatumVon { get; set; } = DateTime.Now;

        /// <summary>
        /// Datum „bis“ des Buchungsstapels -> Wird vom Programm gesetzt
        /// </summary>
        public DateTime DatumBis { get; set; } = DateTime.Now;

        private string _Bezeichnung = "";
        /// <summary>
        /// Bezeichnung des Buchungsstapels
        /// </summary>
        public string Bezeichnung
        {
            get => _Bezeichnung;
            set
            {
                if (Helper.KontrStringOk(ref _Bezeichnung, value, "Bezeichnung", 0, 30))
                    NotifyPropertyChanged();
            }
        }

        private string _DiktatKürzel = "";
        /// <summary>
        /// Beispiel: MM = Max Mustermann
        /// Beim Export aus einem DATEV pro-Rechnungswesen-Programm wird das Diktatkürzel aus dem exportierten Buchungsstapel verwendet.
        /// </summary>
        public string DiktatKürzel
        {
            get => _DiktatKürzel;
            set
            {
                if (Helper.KontrStringOk(ref _DiktatKürzel, value, "DiktatKürzel", 0, 2))
                    NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Wird kein Buchungstyp angegeben, wird standardmäßig ein FIBU-Stapel erzeugt.
        /// </summary>
        public EnumBuchungstyp Buchungstyp { get; set; } = EnumBuchungstyp.Finanzbuchführung;

        public EnumRechnungszweck Rechnungszweck { get; set; } = EnumRechnungszweck.Unabhängig;

        /// <summary>
        /// leer = nicht definiert; wird ab Jahreswechselversion 2016/2017 automatisch festgeschrieben
        /// </summary>
        public EnumFestschreibung Festschreibung { get; set; } = EnumFestschreibung.KeineFestschreibung;

        private string _WkZ = "EUR";
        /// <summary>
        /// Währungskennzeichen
        /// Beim Import wird bei Belegen ohne WKZ dieses WKZ ergänzt.
        /// </summary>
        public string WkZ
        {
            get => _WkZ;
            set
            {
                if (Helper.KontrStringOk(ref _WkZ, value, "WkZ", 0, 3))
                    NotifyPropertyChanged();
            }
        }

        public string Derivatskennzeichen { get; set; } = "";

        public string SKR { get; set; } = null;

        public int? BranchenlösungId { get; set; } = null;

        private string _AnwendungsInformation = "";
        /// <summary>
        /// Verarbeitungskennzeichen der abgebenden Anwendung
        /// </summary>
        public string AnwendungsInformation
        {
            get => _AnwendungsInformation;
            set
            {
                if (Helper.KontrStringOk(ref _AnwendungsInformation, value, "_AnwendungsInformation", 0, 3))
                    NotifyPropertyChanged();
            }
        }

        private int FormatversionInInt(string Wert)
        {
            return Convert.ToInt32(Wert.Substring(Wert.Length - 1));
        }

        public override string ToString()
        {
            return Helper.Konvert(DatevFormatKz.ToString()) + ";"
                + Helper.Konvert(Versionsnummer) + ";"
                + Helper.Konvert(DatenKategorie) + ";"
                + Helper.Konvert(Helper.UnterstricheInWert(FormatName.ToString())) + ";"
                + Helper.Konvert(FormatversionInInt(FormatVersion.ToString())) + ";"
                + Helper.Konvert(ErzeugtAm, "yyyyMMddHHmmssfff") + ";"
                + Helper.Konvert(Importiert) + ";"
                + Helper.Konvert(Herkunft) + ";"
                + Helper.Konvert(ExportiertVon) + ";"
                + Helper.Konvert(ImportiertVon) + ";"
                + Helper.Konvert(Beraternummer) + ";"
                + Helper.Konvert(MandantenNummer) + ";"
                + Helper.Konvert(WjBegin, "yyyMMdd") + ";"
                + Helper.Konvert(SachkontenNummernLaenge) + ";"
                + Helper.Konvert(DatumVon, "yyyMMdd") + ";"
                + Helper.Konvert(DatumBis, "yyyMMdd") + ";"
                + Helper.Konvert(Bezeichnung) + ";"
                + Helper.Konvert(DiktatKürzel) + ";"
                + Helper.Konvert(Buchungstyp) + ";"
                + Helper.Konvert(Rechnungszweck) + ";"
                + Helper.Konvert(Festschreibung) + ";"
                + Helper.Konvert(WkZ) + ";"
                + Helper.Konvert(null) + ";"
                + Helper.Konvert(Derivatskennzeichen) + ";"
                + Helper.Konvert(null) + ";"
                + Helper.Konvert(null) + ";"
                + Helper.Konvert(SKR) + ";"
                + Helper.Konvert(BranchenlösungId) + ";"
                + Helper.Konvert(null) + ";"
                + Helper.Konvert(null) + ";"
                + Helper.Konvert(AnwendungsInformation);
        }
    }
}
