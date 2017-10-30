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

        [JgInfo(true, AnzeigeEnum = JgInfoAttribute.AnzeigeEnums.AlsString )]
        private EnumDatevFormat _DatevFormatKz = EnumDatevFormat.EXTF;
        /// <summary>
        /// EXTF = für Dateiformate, die von externen Programmen erstellt wurden
        /// DTVF = für DATEV reserviert
        /// </summary>
        public EnumDatevFormat DatevFormatKz { get => _DatevFormatKz; set => value = _DatevFormatKz ; }

        [JgInfo(true, 999)]
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
                if (this.GetJgInfoAttribute(v => v._Versionsnummer, value))
                    NotifyPropertyChanged();
            }
        }

        [JgInfo(true)]
        private EnumDatenkategorie _DatenKategorie = EnumDatenkategorie.Buchungsstapel;
        public EnumDatenkategorie DatenKategorie { get => _DatenKategorie; set => _DatenKategorie = value; }

        [JgInfo(true, AnzeigeEnum = JgInfoAttribute.AnzeigeEnums.AlsString)]
        private EnumFormatname _FormatName = EnumFormatname.Buchungsstapel;
        public EnumFormatname FormatName { get => _FormatName; set => _FormatName = value; }   // Unterstriche umwandeln !

        [JgInfo(true)]
        private EnumFormatversion _FormatVersion = EnumFormatversion.Buchungsstapel_7;
        /// <summary>
        /// Versionsnummer des Formats. Ergeben sich künftig Änderungen am Aufbau des Formats(z.B.neue Felder), kann dies über die Formatversion abwärtskompatibel verarbeitet werden.
        /// </summary>
        public EnumFormatversion FormatVersion { get => _FormatVersion; set => _FormatVersion = value; }

        [JgInfo(false, Format = "yyyyMMddHHmmssfff")]
        private DateTime? _ErzeugtAm = null;
        public DateTime? ErzeugtAm { get => _ErzeugtAm; set => _ErzeugtAm = value; }

        [JgInfo(false, 1)]
        private int? _Importiert = null;   // Wird beim Import in Datev belegt
        /// <summary>
        /// Das Header-Feld darf nicht gefüllt werden. Wird durch den Import gesetzt.
        /// </summary>
        public int? Importiert
        {
            get => _Importiert;
            set
            {
                if (this.GetJgInfoAttribute(v => v._Importiert, value))
                    NotifyPropertyChanged();
            }
        }   // Wird beim Import in Datev belegt
 
        [JgInfo(false, 2, 2)]
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
                if (this.GetJgInfoAttribute(v => v._Herkunft, value))
                    NotifyPropertyChanged();
            }
        }

        [JgInfo(false, 25)]
        private string _ExportiertVon = "";
        /// <summary>
        /// Beim Export aus einem DATEV pro-Rechnungswesen-Programm wird der Benutzername des Users exportiert, der den Export durchgeführt hat.
        /// </summary>
        public string ExportiertVon
        {
            get => _ExportiertVon;
            set
            {
                if (this.GetJgInfoAttribute(v => v._ExportiertVon, value))
                    NotifyPropertyChanged();
            }
        }

        [JgInfo(false, 25)]
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
                if (this.GetJgInfoAttribute(v => v._ImportiertVon, value))
                    NotifyPropertyChanged();
            }
        }

        [JgInfo(true, 9999999)]
        private int _BeraterNummer = 0;
        public int Beraternummer
        {
            get => _BeraterNummer;
            set
            {
                if (this.GetJgInfoAttribute(v => v._BeraterNummer, value))
                    NotifyPropertyChanged();
            }
        }

        [JgInfo(true, 99999)]
        private int _MandantenNummer = 0;
        public int MandantenNummer
        {
            get => _MandantenNummer;
            set
            {
                if (this.GetJgInfoAttribute(v => v._MandantenNummer, value))
                    NotifyPropertyChanged();
            }
        }

        [JgInfo(true, Format = "yyyyMMdd")]
        private DateTime _WjBeginn = new DateTime(DateTime.Now.Year, 1, 1);
        /// <summary>
        /// Wirtschaftsjahresbeginn
        /// </summary>
        public DateTime WjBeginn { get => _WjBeginn; set => _WjBeginn = value; }

        [JgInfo(true)]
        private EnumSachkontenNummernLaenge _SachkontenNummernLaenge = EnumSachkontenNummernLaenge.Stellen_4;
        /// <summary>
        /// Kleinste Sachkontennummernlänge = 4 ➝ Debitoren/Kreditoren dann 5 Stellen
        /// Größe Sachkontennummernlänge = 8 ➝ Debitoren/Kreditoren dann 9 Stellen
        /// </summary>
        public EnumSachkontenNummernLaenge SachkontenNummernLaenge { get => _SachkontenNummernLaenge; set => _SachkontenNummernLaenge = value; }

        [JgInfo(true, Format = "yyyyMMdd")]
        private DateTime _DatumVon = DateTime.Now;
        /// <summary>
        /// Datum „von“ des Buchungsstapels -> Wird vom Programm gesetzt
        /// </summary>
        public DateTime DatumVon { get => _DatumVon; set => _DatumVon = value; }

        [JgInfo(true, Format = "yyyyMMdd")]
        private DateTime _DatumBis = DateTime.Now;
        /// <summary>
        /// Datum „bis“ des Buchungsstapels -> Wird vom Programm gesetzt
        /// </summary>
        public DateTime DatumBis { get => _DatumBis; set => _DatumBis = value; }

        [JgInfo(false, 30)]
        private string _Bezeichnung = "";
        /// <summary>
        /// Bezeichnung des Buchungsstapels
        /// </summary>
        public string Bezeichnung
        {
            get => _Bezeichnung;
            set
            {
                if (this.GetJgInfoAttribute(v => v._Bezeichnung, value))
                    NotifyPropertyChanged();
            }
        }

        [JgInfo(false, 2)]
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
                if (this.GetJgInfoAttribute(v => v._DiktatKürzel, value))
                    NotifyPropertyChanged();
            }
        }

        [JgInfo(false)]
        private EnumBuchungstypHeader _BuchungsTyp = EnumBuchungstypHeader.Finanzbuchführung;
        /// <summary>
        /// Wird kein Buchungstyp angegeben, wird standardmäßig ein FIBU-Stapel erzeugt.
        /// </summary>
        public EnumBuchungstypHeader BuchungsTyp { get => _BuchungsTyp; set => _BuchungsTyp = value; }


        [JgInfo(false)]
        private EnumRechnungszweck _Rechnungszweck = EnumRechnungszweck.Unabhängig;
        public EnumRechnungszweck Rechnungszweck { get => _Rechnungszweck; set => _Rechnungszweck = value; }

        [JgInfo(true)]
        private EnumFestschreibung _Festschreibung = EnumFestschreibung.KeineFestschreibung;
        /// <summary>
        /// leer = nicht definiert; wird ab Jahreswechselversion 2016/2017 automatisch festgeschrieben
        /// </summary>
        public EnumFestschreibung Festschreibung { get => _Festschreibung; set => _Festschreibung = value; }

        [JgInfo(false, AnzeigeEnum = JgInfoAttribute.AnzeigeEnums.AlsString )]
        private EnumWaehrung _WkZ = EnumWaehrung.EUR;
        /// <summary>
        /// Währungskennzeichen
        /// Beim Import wird bei Belegen ohne WKZ dieses WKZ ergänzt.
        /// </summary>
        public EnumWaehrung WkZ { get => _WkZ; set => _WkZ = value; }

        [JgInfo(false)]
        private string _Derivatskennzeichen = null;
        public string Derivatskennzeichen { get => _Derivatskennzeichen; set => _Derivatskennzeichen = value; }

        [JgInfo(false)]
        private string _SKR  = null;
        public string SKR { get => _SKR; set =>_SKR = value; }

        [JgInfo(false)]
        private int? _BranchenloesungId = null;
        public int? BranchenloesungId { get => _BranchenloesungId; set => _BranchenloesungId = value; }

        [JgInfo(false, 16)]
        private string _AnwendungsInformation = null;
        /// <summary>
        /// Verarbeitungskennzeichen der abgebenden Anwendung
        /// </summary>
        public string AnwendungsInformation
        {
            get => _AnwendungsInformation;
            set
            {
                if (this.GetJgInfoAttribute(v => v._AnwendungsInformation, value))
                    NotifyPropertyChanged();
            }
        }

        public override string ToString()
        {
            return this.JgDruck(v => v._DatevFormatKz) + ";"
                + this.JgDruck(v => v._Versionsnummer) + ";"
                + this.JgDruck(v => v._DatenKategorie) + ";"
                + Helper.UnterstricheInWert(this.JgDruck(v => v._FormatName)) + ";"
                + this.JgDruck(v => v._FormatVersion) + ";"
                + this.JgDruck(v => v._ErzeugtAm) + ";"
                + this.JgDruck(v => v._Importiert) + ";"
                + this.JgDruck(v => v._Herkunft) + ";"
                + this.JgDruck(v => v._ExportiertVon) + ";"
                + this.JgDruck(v => v._ImportiertVon) + ";"
                + this.JgDruck(v => v._BeraterNummer) + ";"
                + this.JgDruck(v => v._MandantenNummer) + ";"
                + this.JgDruck(v => v._WjBeginn) + ";"
                + this.JgDruck(v => v._SachkontenNummernLaenge) + ";"
                + this.JgDruck(v => v._DatumVon) + ";"
                + this.JgDruck(v => v._DatumBis) + ";"
                + this.JgDruck(v => v._Bezeichnung) + ";"
                + this.JgDruck(v => v._DiktatKürzel) + ";"
                + this.JgDruck(v => v._BuchungsTyp) + ";"
                + this.JgDruck(v => v._Rechnungszweck) + ";"
                + this.JgDruck(v => v._Festschreibung) + ";"
                + this.JgDruck(v => v._WkZ) + ";"
                + ";"
                + this.JgDruck(v => v._Derivatskennzeichen) + ";"
                + ";"
                + ";"
                + this.JgDruck(v => v._SKR) + ";"
                + this.JgDruck(v => v._BranchenloesungId) + ";"
                + ";"
                + ";"
                + this.JgDruck(v => v._AnwendungsInformation);
        }
    }
}
