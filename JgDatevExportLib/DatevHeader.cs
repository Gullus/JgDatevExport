using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using static JgDatevExportLib.DatevEnum;

namespace JgDatevExportLib
{
    [Serializable]
    public class DatevHeader : INotifyPropertyChanged
    {
        [NonSerialized]
        public DatevOptionen DvOptionen;

        [field: NonSerializedAttribute()]
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        [JgInfo(true, AnzeigeEnum = JgInfoAttribute.AnzeigeEnums.AlsString)]
        private EnumDatevFormat _DatevFormatKz = EnumDatevFormat.EXTF;
        /// <summary>
        /// EXTF = für Dateiformate, die von externen Programmen erstellt wurden
        /// DTVF = für DATEV reserviert
        /// </summary>
        public EnumDatevFormat DatevFormatKz { get => _DatevFormatKz; set => _DatevFormatKz = value; }

        [JgInfo(true, 999)]
        private int _Versionsnummer = 510;
        /// <summary>
        /// Versionsnummer des Headers.
        /// Ergeben sich künftig Änderungen am Aufbau des Headers(z.B.zusätzliche Felder), kann dieser über die Headerversion abwärtskompatibel verarbeitet werden.
        /// </summary>
        public int Versionsnummer
        {
            get => _Versionsnummer;
            set
            {
                if (this.SetzeWertInObject(v => v._Versionsnummer, value))
                    NotifyPropertyChanged("Versionsnummer");
            }
        }

        [JgInfo(true)]
        private EnumDatenkategorie _DatenKategorie = EnumDatenkategorie.Buchungsstapel;
        public EnumDatenkategorie DatenKategorie { get => _DatenKategorie; set => _DatenKategorie = value; }

        [JgInfo(true, AnzeigeEnum = JgInfoAttribute.AnzeigeEnums.AlsString)]
        private EnumFormatname _FormatName = EnumFormatname.Buchungsstapel;
        public EnumFormatname FormatName { get => _FormatName; set => _FormatName = value; }   // Unterstriche umwandeln !

        [JgInfo(true, AnzeigeEnum = JgInfoAttribute.AnzeigeEnums.LetztesZeichen)]
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
                if (this.SetzeWertInObject(v => v._Importiert, value))
                    NotifyPropertyChanged("Importiert");
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
                if (this.SetzeWertInObject(v => v._Herkunft, value))
                    NotifyPropertyChanged("Herkunft");
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
                if (this.SetzeWertInObject(v => v._ExportiertVon, value))
                    NotifyPropertyChanged("ExportiertVon");
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
                if (this.SetzeWertInObject(v => v._ImportiertVon, value))
                    NotifyPropertyChanged("ImportiertVon");
            }
        }

        [JgInfo(true, 1001, 9999999)]
        private int _BeraterNummer = 1001;
        public int BeraterNummer
        {
            get => _BeraterNummer;
            set
            {
                if (this.SetzeWertInObject(v => v._BeraterNummer, value))
                    NotifyPropertyChanged("BeraterNummer");
            }
        }

        [JgInfo(true, 1, 99999)]
        private int _MandantenNummer = 1;
        public int MandantenNummer
        {
            get => _MandantenNummer;
            set
            {
                if (this.SetzeWertInObject(v => v._MandantenNummer, value))
                    NotifyPropertyChanged("MandantenNummer");
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
                if (this.SetzeWertInObject(v => v._Bezeichnung, value))
                    NotifyPropertyChanged("Bezeichnung");
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
                if (this.SetzeWertInObject(v => v._DiktatKürzel, value))
                    NotifyPropertyChanged("DiktatKürzel");
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

        [JgInfo(false, AnzeigeEnum = JgInfoAttribute.AnzeigeEnums.AlsString)]
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
        private string _SKR = null;
        public string SKR { get => _SKR; set => _SKR = value; }

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
                if (this.SetzeWertInObject(v => v._AnwendungsInformation, value))
                    NotifyPropertyChanged("AnwendungsInformation");
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.Append(this.JgDruck(v => v._DatevFormatKz) + ";");
            sb.Append(this.JgDruck(v => v._Versionsnummer) + ";");
            sb.Append(this.JgDruck(v => v._DatenKategorie) + ";");
            sb.Append(DatevHelper.UnterstricheInWert(this.JgDruck(v => v._FormatName)) + ";");
            sb.Append(this.JgDruck(v => v._FormatVersion) + ";");
            sb.Append(this.JgDruck(v => v._ErzeugtAm) + ";");
            sb.Append(this.JgDruck(v => v._Importiert) + ";");
            sb.Append(this.JgDruck(v => v._Herkunft) + ";");
            sb.Append(this.JgDruck(v => v._ExportiertVon) + ";");
            sb.Append(this.JgDruck(v => v._ImportiertVon) + ";");

            sb.Append(this.JgDruck(v => v._BeraterNummer) + ";");
            sb.Append(this.JgDruck(v => v._MandantenNummer) + ";");

            sb.Append(this.JgDruck(v => v._WjBeginn) + ";");
            sb.Append(this.JgDruck(v => v._SachkontenNummernLaenge) + ";");
            sb.Append(this.JgDruck(v => v._DatumVon) + ";");
            sb.Append(this.JgDruck(v => v._DatumBis) + ";");
            sb.Append(this.JgDruck(v => v._Bezeichnung) + ";");
            sb.Append(this.JgDruck(v => v._DiktatKürzel) + ";");
            sb.Append(this.JgDruck(v => v._BuchungsTyp) + ";");
            sb.Append(this.JgDruck(v => v._Rechnungszweck) + ";");
            sb.Append(this.JgDruck(v => v._Festschreibung) + ";");
            sb.Append(this.JgDruck(v => v._WkZ) + ";");
            sb.Append(";");
            sb.Append(this.JgDruck(v => v._Derivatskennzeichen) + ";");
            sb.Append(";");
            sb.Append(";");
            sb.Append(this.JgDruck(v => v._SKR) + ";");
            sb.Append(this.JgDruck(v => v._BranchenloesungId) + ";");
            sb.Append(";");
            sb.Append(";");
            sb.Append(this.JgDruck(v => v._AnwendungsInformation));

            return sb.ToString();
        }

        public List<DsListeAnzeige> ListeAnzeigeErstellen()
        {
            var lAnzeige = new List<DsListeAnzeige>
            {
                this.JgAnzeige(v => v._DatevFormatKz),
                this.JgAnzeige(v => v._Versionsnummer),
                this.JgAnzeige(v => v._DatenKategorie),
                this.JgAnzeige(v => v._FormatName),
                this.JgAnzeige(v => v._FormatVersion),
                this.JgAnzeige(v => v._ErzeugtAm),
                this.JgAnzeige(v => v._Importiert),
                this.JgAnzeige(v => v._Herkunft),
                this.JgAnzeige(v => v._ExportiertVon),
                this.JgAnzeige(v => v._ImportiertVon),
                this.JgAnzeige(v => v._BeraterNummer),
                this.JgAnzeige(v => v._MandantenNummer),
                this.JgAnzeige(v => v._WjBeginn),
                this.JgAnzeige(v => v._SachkontenNummernLaenge),
                this.JgAnzeige(v => v._DatumVon),
                this.JgAnzeige(v => v._DatumBis),
                this.JgAnzeige(v => v._Bezeichnung),
                this.JgAnzeige(v => v._DiktatKürzel),
                this.JgAnzeige(v => v._BuchungsTyp),
                this.JgAnzeige(v => v._Rechnungszweck),
                this.JgAnzeige(v => v._Festschreibung),
                this.JgAnzeige(v => v._WkZ),
                this.JgAnzeige(v => v._Derivatskennzeichen),
                this.JgAnzeige(v => v._SKR),
                this.JgAnzeige(v => v._BranchenloesungId),
                this.JgAnzeige(v => v._AnwendungsInformation)
            };

            var id = 1;
            foreach (var ds in lAnzeige)
                ds.Id = id++;

            return lAnzeige;
        }
    }
}
