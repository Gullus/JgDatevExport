using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using static JgDatevExportLib.DatevEnum;

namespace JgDatevExportLib
{
    [Serializable]
    public class TStamm
    {
        [NonSerialized]
        private int _Id = 0;
        public int Id { get => _Id; set => _Id = value; }

        [NonSerialized]
        private string _Feldname = "";
        public string Feldname { get => _Feldname; set => _Feldname = value; }
    }

    [Serializable]
    public class DsBelegInformation : TStamm, INotifyPropertyChanged
    {
        [field: NonSerializedAttribute()]
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string _BeleginfoArt = null;
        public string BeleginfoArt
        {
            get => _BeleginfoArt;
            set
            {
                if (_BeleginfoArt != value)
                {
                    _BeleginfoArt = value;
                    NotifyPropertyChanged("BeleginfoArt");
                }
            }
        }

        public string _BeleginfoInhalt = null;
        public string BeleginfoInhalt
        {
            get => _BeleginfoInhalt; set
            {
                if (_BeleginfoInhalt != value)
                {
                    _BeleginfoInhalt = value;
                    NotifyPropertyChanged("AnwendungsInformation");
                }
            }
        }

        public override string ToString()
        {
            return (_BeleginfoArt ?? "Art") + "; " + (_BeleginfoInhalt ?? "Inhalt");
        }
    }

    [Serializable]
    public class DsZusatzInformation : TStamm, INotifyPropertyChanged
    {
        [field: NonSerializedAttribute()]
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string _ZusatzinformationArt = null;
        public string ZusatzinformationArt
        {
            get => _ZusatzinformationArt;
            set
            {
                if (_ZusatzinformationArt != value)
                {
                    _ZusatzinformationArt = value;
                    NotifyPropertyChanged("ZusatzinformationArt");
                }
            }
        }

        private string _ZusatzinformationInhalt = null;
        public string ZusatzinformationInhalt
        {
            get => _ZusatzinformationInhalt;
            set
            {
                if (_ZusatzinformationInhalt != value)
                {
                    _ZusatzinformationInhalt = value;
                    NotifyPropertyChanged("ZusatzinformationInhalt");
                }
            }
        }

        public override string ToString()
        {
            return (_ZusatzinformationArt ?? "Art") + "; " + (_ZusatzinformationInhalt ?? "Inhalt");
        }
    }

    [Serializable]
    public class DatevKoerper : INotifyPropertyChanged
    {
        [field: NonSerializedAttribute()]
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public DatevKoerper()
        {
            FelderArrayMitNummerUndFeldBelegen();
        }

        public void FelderArrayMitNummerUndFeldBelegen()
        {
            for (int i = 0; i < 8; i++)
                _BelegInfo[i] = new DsBelegInformation()
                {
                    Id = 21 + (i * 2),
                    Feldname = "Beleginfo " + (i + 1).ToString()

                };

            for (int i = 0; i < 20; i++)
            {
                _ZusatzInformation[i] = new DsZusatzInformation()
                {
                    Id = 48 + (i * 2),
                    Feldname = "ZusatzInfo " + (i + 1).ToString()
                };
            }
        }

        // Felder Zuordnung Programm -> DatevExport speichern

        public string FelderZuordnungDatevExport = ""; 

        // Umsatz(ohne Soll/Haben-Kz)

        [JgInfo(true, 99999999, Format = "N2")]
        private decimal _Umsatz = 0m;
        public decimal Umsatz
        {
            get => _Umsatz;
            set
            {
                if (this.GetJgInfoAttribute(v => v._Umsatz, value))
                    NotifyPropertyChanged("Umsatz");
            }
        }

        // Soll/Haben-Kennzeichen

        [JgInfo(true, AnzeigeEnum = JgInfoAttribute.AnzeigeEnums.ErsterBuchstabe)]
        private EnumSollHaben _SollHaben = EnumSollHaben.Soll;
        public EnumSollHaben SollHaben { get => _SollHaben; set => _SollHaben = value; }

        // WKZ Umsatz

        [JgInfo(false, AnzeigeEnum = JgInfoAttribute.AnzeigeEnums.AlsString)]
        private EnumWaehrung _WkzUmsatz = EnumWaehrung.leer;
        public EnumWaehrung WkzUmsatz { get => _WkzUmsatz; set => _WkzUmsatz = value; }

        // Kurs

        [JgInfo(false, 11, Format = "N6")]
        private decimal? _Kurs = null;
        public decimal? Kurs
        {
            get => _Kurs;
            set
            {
                if (this.GetJgInfoAttribute(v => v._Kurs, value))
                    NotifyPropertyChanged("Kurs");
            }
        }

        // Basis-Umsatz

        [JgInfo(false, 13, Format = "N2")]
        private decimal? _BasisUmsatz = 0;
        public decimal? BasisUmsatz
        {
            get => _BasisUmsatz;
            set
            {
                if (this.GetJgInfoAttribute(v => v._BasisUmsatz, value))
                    NotifyPropertyChanged("BasisUmsatz");
            }
        }

        // WKZ Basis-Umsatz

        [JgInfo(false, AnzeigeEnum = JgInfoAttribute.AnzeigeEnums.AlsString)]
        private EnumWaehrung _WkzBasisUmsatz = EnumWaehrung.leer;
        public EnumWaehrung WkzBasisUmsatz { get => _WkzBasisUmsatz; set => _WkzBasisUmsatz = value; }

        // Konto

        [JgInfo(true, 4, 9)]
        private string _Konto = "";
        public string Konto
        {
            get => _Konto;
            set
            {
                if (this.GetJgInfoAttribute(v => v._Konto, value))
                    NotifyPropertyChanged("Konto");
            }
        }

        // Gegenkonto(ohne BU-Schlüssel)

        [JgInfo(true, 4, 9)]
        private string _GegenKonto = "";
        public string GegenKonto
        {
            get => _GegenKonto;
            set
            {
                if (this.GetJgInfoAttribute(v => v._GegenKonto, value))
                    NotifyPropertyChanged("GegenKonto");
            }
        }

        // BU-Schlüssel

        [JgInfo(false, 2)]
        private string _BuSchluessel = null;
        public string BuSchluessel
        {
            get => _BuSchluessel;
            set
            {
                if (this.GetJgInfoAttribute(v => v._BuSchluessel, value))
                    NotifyPropertyChanged("BuSchluessel");
            }
        }

        // Belegdatum

        [JgInfo(true, 4, Format = "ddMM")]
        private DateTime _Belegdatum = DateTime.Now;
        public DateTime Belegdatum { get => _Belegdatum; set => _Belegdatum = value; }

        // Belegfeld 1

        [JgInfo(false, 12)]
        private string _Belegfeld1 = null;
        public string Belegfeld1
        {
            get => _Belegfeld1;
            set
            {
                if (this.GetJgInfoAttribute(v => v._Belegfeld1, value))
                    NotifyPropertyChanged("Belegfeld1");
            }
        }

        // Belegfeld 2

        [JgInfo(false, 12)]
        private string _Belegfeld2 = null;
        public string Belegfeld2
        {
            get => _Belegfeld2;
            set
            {
                if (this.GetJgInfoAttribute(v => v._Belegfeld2, value))
                    NotifyPropertyChanged("Belegfeld2");
            }
        }

        // Skonto

        [JgInfo(false, 11, Format = "N2")]
        private decimal? _Skonto = null;
        public decimal? Skonto
        {
            get => _Skonto;
            set
            {
                if (this.GetJgInfoAttribute(v => v._Skonto, value))
                    NotifyPropertyChanged("Skonto");
            }
        }

        // Buchungstext

        [JgInfo(false, 60)]
        private string _Buchungstext = null;
        public string Buchungstext
        {
            get => _Buchungstext;
            set
            {
                if (this.GetJgInfoAttribute(v => v._Buchungstext, value))
                    NotifyPropertyChanged("Buchungstext");
            }
        }

        // Postensperre

        [JgInfo(false)]
        private EnumPostensperre _PostenSperre = EnumPostensperre.leer;
        public EnumPostensperre PostenSperre { get => _PostenSperre; set => _PostenSperre = value; }

        // Diverse Adressnummer

        [JgInfo(false, 9)]
        private string _DiverseAdressNummer = null;
        public string DiverseAdressNummer
        {
            get => _DiverseAdressNummer;
            set
            {
                if (this.GetJgInfoAttribute(v => v._DiverseAdressNummer, value))
                    NotifyPropertyChanged("DiverseAdressNummer");
            }
        }

        // Geschäftspartnerbank

        [JgInfo(false, 999)]
        private int? _GeschäftspartnerBank = null;
        public int? GeschäftspartnerBank
        {
            get => _GeschäftspartnerBank;
            set
            {
                if (this.GetJgInfoAttribute(v => v._GeschäftspartnerBank, value))
                    NotifyPropertyChanged("GeschäftspartnerBank");
            }
        }

        // Sachverhalt

        [JgInfo(false, 99)]
        private int? _Sachverhalt = null;
        public int? Sachverhalt
        {
            get => _Sachverhalt;
            set
            {
                if (this.GetJgInfoAttribute(v => v._Sachverhalt, value))
                    NotifyPropertyChanged("Sachverhalt");
            }
        }

        // Zinssperre

        [JgInfo(false)]
        private EnumZissperre _Zinssperre = EnumZissperre.leer;
        public EnumZissperre Zinssperre { get => _Zinssperre; set => _Zinssperre = value; }

        //  20 - Beleglink

        [JgInfo(false, 210)]
        private string _BelegLink = null;
        public string BelegLink
        {
            get => _BelegLink;
            set
            {
                if (this.GetJgInfoAttribute(v => v._BelegLink, value))
                    NotifyPropertyChanged("BelegLink");
            }
        }

        // Beleginfo-Art und Inhalt 1 - 8 ********

        private DsBelegInformation[] _BelegInfo = new DsBelegInformation[8];
        public DsBelegInformation[] BelegInfo { get => _BelegInfo; set => _BelegInfo = value; }

        // KOST1-Kostenstelle

        [JgInfo(false, 8)]
        private string _Kost1 = null;
        public string Kost1
        {
            get => _Kost1;
            set
            {
                if (this.GetJgInfoAttribute(v => v._Kost1, value))
                    NotifyPropertyChanged("Kost1");
            }
        }

        // KOST2-Kostenstelle

        [JgInfo(false, 8)]
        private string _Kost2 = null;
        public string Kost2
        {
            get => _Kost2;
            set
            {
                if (this.GetJgInfoAttribute(v => v._Kost2, value))
                    NotifyPropertyChanged("Kost2");
            }
        }

        // Kost-Menge

        [JgInfo(false, 12, Format = "N2")]
        private decimal? _KostMenge = null;
        public decimal? KostMenge
        {
            get => _KostMenge;
            set
            {
                if (this.GetJgInfoAttribute(v => v._KostMenge, value))
                    NotifyPropertyChanged("KostMenge");
            }
        }

        // 40 - EU-Land u.UStID

        [JgInfo(false, 15)]
        private string _EuMitgliedsStaatUStId = null;
        public string EuMitgliedsStaatUStId
        {
            get => _EuMitgliedsStaatUStId;
            set
            {
                if (this.GetJgInfoAttribute(v => v._EuMitgliedsStaatUStId, value))
                    NotifyPropertyChanged("EuMitgliedsStaatUStId");
            }
        }

        // EU-Steuersatz

        [JgInfo(false, 99, Format = "N2")]
        private decimal? _EuSteuersatz = null;
        public decimal? EuSteuersatz
        {
            get => _EuSteuersatz;
            set
            {
                if (this.GetJgInfoAttribute(v => v._EuSteuersatz, value))
                    NotifyPropertyChanged("EuSteuersatz");
            }
        }

        // Abw.Versteuerungsart

        [JgInfo(false, AnzeigeEnum = JgInfoAttribute.AnzeigeEnums.ErsterBuchstabe)]
        private EnumAbwVersteuerungsart _AbwVersteuerungsart = EnumAbwVersteuerungsart.leer;
        public EnumAbwVersteuerungsart AbwVersteuerungsart { get => _AbwVersteuerungsart; set => _AbwVersteuerungsart = value; }

        // Sachverhalt L+L

        [JgInfo(false, 999)]
        private int? _Sachverhalt_L_L = null;
        public int? Sachverhalt_L_L
        {
            get => _Sachverhalt_L_L;
            set
            {
                if (this.GetJgInfoAttribute(v => v._Sachverhalt_L_L, value))
                    NotifyPropertyChanged("Sachverhalt_L_L");
            }
        }

        // Funktionsergänzung L+L

        [JgInfo(false, 999)]
        private int? _FunktionsErgänzung_L_L = null;
        public int? FunktionsErgänzung_L_L
        {
            get => _Sachverhalt_L_L;
            set
            {
                if (this.GetJgInfoAttribute(v => v._FunktionsErgänzung_L_L, value))
                    NotifyPropertyChanged("FunktionsErgänzung_L_L");
            }
        }

        // BU 49 Hauptfunktionstyp

        [JgInfo(false, 9)]
        private int? _Bu49Hauptfunktionstyp = null;
        public int? Bu49Hauptfunktionstyp
        {
            get => _Bu49Hauptfunktionstyp;
            set
            {
                if (this.GetJgInfoAttribute(v => v._Bu49Hauptfunktionstyp, value))
                    NotifyPropertyChanged("Bu49Hauptfunktionstyp");
            }
        }

        // BU 49 Hauptfunktionsnummer

        [JgInfo(false, 99)]
        private int? _Bu49Hauptfunktionsnummer = null;
        public int? Bu49Hauptfunktionsnummer
        {
            get => _Bu49Hauptfunktionsnummer;
            set
            {
                if (this.GetJgInfoAttribute(v => v._Bu49Hauptfunktionsnummer, value))
                    NotifyPropertyChanged("Bu49Hauptfunktionsnummer");
            }
        }

        // BU 49 Funktionsergänzung

        [JgInfo(false, 99)]
        private int? _Bu49Funktionsergänzung = null;
        public int? Bu49Funktionsergänzung
        {
            get => _Bu49Funktionsergänzung;
            set
            {
                if (this.GetJgInfoAttribute(v => v._Bu49Funktionsergänzung, value))
                    NotifyPropertyChanged("Bu49Funktionsergänzung");
            }
        }

        // Zusatzinformation-Art und Inhalt 1 - 20 ********

        private DsZusatzInformation[] _ZusatzInformation = new DsZusatzInformation[20];
        public DsZusatzInformation[] ZusatzInformation { get => _ZusatzInformation; set => _ZusatzInformation = value; }

        // Stück

        [JgInfo(false, 99999999)]
        private int? _Stueck = null;
        public int? Stueck
        {
            get => _Stueck;
            set
            {
                if (this.GetJgInfoAttribute(v => v._Stueck, value))
                    NotifyPropertyChanged("Stueck");
            }
        }

        // Gewicht

        [JgInfo(false, 99999999, Format = "N2")]
        private decimal? _Gewicht = null;
        public decimal? Gewicht
        {
            get => _Gewicht;
            set
            {
                if (this.GetJgInfoAttribute(v => v._Gewicht, value))
                    NotifyPropertyChanged("Gewicht");
            }
        }

        // 90 - Zahlweise

        [JgInfo(false)]
        private EnumZahlweise _Zahlweise = EnumZahlweise.leer;
        public EnumZahlweise Zahlweise { get => _Zahlweise; set => _Zahlweise = value; }

        // Forderungsart

        [JgInfo(false, 10)]
        private string _ForderungsArt = null;
        public string ForderungsArt
        {
            get => _ForderungsArt;
            set
            {
                if (this.GetJgInfoAttribute(v => v._ForderungsArt, value))
                    NotifyPropertyChanged("ForderungsArt");
            }
        }

        // Veranlagungsjahr

        [JgInfo(false, 1000, 9999)]
        private int? _Veranlagungsjahr = 2017;
        public int? Veranlagungsjahr
        {
            get => _Veranlagungsjahr;
            set
            {
                if (this.GetJgInfoAttribute(v => v._Veranlagungsjahr, value))
                    NotifyPropertyChanged("Veranlagungsjahr");
            }
        }

        // Zugeordnete Fälligkeit

        [JgInfo(false, Format = "ddMMyyyy")]
        private DateTime? _ZugeordneteFaelligkeit = null;
        public DateTime? ZugeordneteFaelligkeit { get => _ZugeordneteFaelligkeit; set => _ZugeordneteFaelligkeit = value; }

        // Skontotyp

        [JgInfo(false)]
        private EnumSkontoTyp _SkontoType = EnumSkontoTyp.leer;
        public EnumSkontoTyp SkontoType { get => _SkontoType; set => _SkontoType = value; }

        // Auftragsnummer

        [JgInfo(false, 30)]
        private string _Auftragsnummer = "";
        public string Auftragsnummer
        {
            get => _Auftragsnummer;
            set
            {
                if (this.GetJgInfoAttribute(v => v._Auftragsnummer, value))
                    NotifyPropertyChanged("Auftragsnummer");
            }
        }

        // Buchungstyp

        [JgInfo(false, AnzeigeEnum = JgInfoAttribute.AnzeigeEnums.ErsteZweiBuchstaben)]
        private EnumBuchungsTypKoerper _BuchungsTyp = EnumBuchungsTypKoerper.leer;
        public EnumBuchungsTypKoerper BuchungsTyp { get => _BuchungsTyp; set => _BuchungsTyp = value; }

        // Ust-Schlüssel(Anzahlungen)

        [JgInfo(false, 99)]
        private int? _UstSchluesselAnzahlungen = null;
        public int? UstSchluesselAnzahlungen
        {
            get => _UstSchluesselAnzahlungen;
            set
            {
                if (this.GetJgInfoAttribute(v => v._UstSchluesselAnzahlungen, value))
                    NotifyPropertyChanged("UstSchluesselAnzahlungen");
            }
        }

        // EU-Land(Anzahlungen)

        [JgInfo(false, 2)]
        private string _EuMitgliedstaatAnzahlungen = null;
        public string EuMitgliedstaatAnzahlungen
        {
            get => _EuMitgliedstaatAnzahlungen;
            set
            {
                if (this.GetJgInfoAttribute(v => v._EuMitgliedstaatAnzahlungen, value))
                    NotifyPropertyChanged("EuMitgliedstaatAnzahlungen");
            }
        }

        // Sachverhalt L+L(Anzahlungen)

        [JgInfo(false, 999)]
        private int? _Sachverhalt_L_L_Anzahlungen = null;
        public int? Sachverhalt_L_L_Anzahlungen
        {
            get => _Sachverhalt_L_L_Anzahlungen;
            set
            {
                if (this.GetJgInfoAttribute(v => v._Sachverhalt_L_L_Anzahlungen, value))
                    NotifyPropertyChanged("Sachverhalt_L_L_Anzahlungen");
            }
        }

        // 100 - EU-Steuersatz(Anzahlungen)

        [JgInfo(false, 99, Format = "N2")]
        private decimal? _EuSteuersatzAnzahlungen = null;
        public decimal? EuSteuersatzAnzahlungen
        {
            get => _EuSteuersatzAnzahlungen;
            set
            {
                if (this.GetJgInfoAttribute(v => v._EuSteuersatzAnzahlungen, value))
                    NotifyPropertyChanged("EuSteuersatzAnzahlungen");
            }
        }

        // Erlöskonto(Anzahlungen)

        [JgInfo(false, 9)]
        private string _ErloeskontoAnzahlungen = null;
        public string ErloeskontoAnzahlungen
        {
            get => _ErloeskontoAnzahlungen;
            set
            {
                if (this.GetJgInfoAttribute(v => v._ErloeskontoAnzahlungen, value))
                    NotifyPropertyChanged("ErloeskontoAnzahlungen");
            }
        }

        // Herkunft-Kz

        [JgInfo(false, 2)]
        private string _HerkunftKfz = null;
        public string HerkunftKfz
        {
            get => _HerkunftKfz;
            set
            {
                if (this.GetJgInfoAttribute(v => v._HerkunftKfz, value))
                    NotifyPropertyChanged("HerkunftKfz");
            }
        }

        // Leerfeld -> Wird von Datev verwendet

        [JgInfo(false, 36)]
        private string _Leerfeld = null;
        public string Leerfeld { get => _Leerfeld; }

        // KOST-Datum

        [JgInfo(false, Format = "ddMMyyyy")]
        private DateTime? _KostDatum = null;
        public DateTime? KostDatum { get => _KostDatum; set => _KostDatum = value; }

        // Mandatsreferenz

        [JgInfo(false, 35)]
        private string _SepaMandatsreferenz = null;
        public string SepaMandatsreferenz
        {
            get => _SepaMandatsreferenz; set
            {
                if (this.GetJgInfoAttribute(v => v._SepaMandatsreferenz, value))
                    NotifyPropertyChanged("SepaMandatsreferenz");
            }
        }

        // Skontosperre

        [JgInfo(false, 9)]
        private int? _Skontosperre = null;
        public int? Skontosperre
        {
            get => _Skontosperre; set
            {
                if (this.GetJgInfoAttribute(v => v._Skontosperre, value))
                    NotifyPropertyChanged("Skontosperre");
            }
        }

        // Gesellschaftername

        [JgInfo(false, 76)]
        private string _GesellschafterName = null;
        public string GesellschafterName
        {
            get => _GesellschafterName;
            set
            {
                if (this.GetJgInfoAttribute(v => v._GesellschafterName, value))
                    NotifyPropertyChanged("GesellschafterName");
            }
        }

        // Beteiligtennummer

        [JgInfo(false, 9999)]
        private int? _BeteiligtenNummer = null;
        public int? BeteiligtenNummer
        {
            get => _BeteiligtenNummer; set
            {
                if (this.GetJgInfoAttribute(v => v._BeteiligtenNummer, value))
                    NotifyPropertyChanged("BeteiligtenNummer");
            }
        }

        // Identifikationsnummer

        [JgInfo(false, 11)]
        private string _IdentifikationsNummer = null;
        public string IdentifikationsNummer
        {
            get => _IdentifikationsNummer; set
            {
                if (this.GetJgInfoAttribute(v => v._IdentifikationsNummer, value))
                    NotifyPropertyChanged("IdentifikationsNummer");
            }
        }

        // 110 - Zeichnernummer

        [JgInfo(false, 20)]
        private string _ZeichnerNummer = null;
        public string ZeichnerNummer
        {
            get => _ZeichnerNummer; set
            {
                if (this.GetJgInfoAttribute(v => v._ZeichnerNummer, value))
                    NotifyPropertyChanged("ZeichnerNummer");
            }
        }

        // Postensperre bis

        [JgInfo(false, Format = "ddMMyyyy")]
        private DateTime? _PostensperreBis = null;
        public DateTime? PostensperreBis { get => _PostensperreBis; set => _PostensperreBis = value; }

        // Bezeichnung SoBil-Sachverhalt

        [JgInfo(false, 30)]
        private string _BezeichnungSoBilSachverhalt = null;
        public string BezeichnungSoBilSachverhalt
        {
            get => _BezeichnungSoBilSachverhalt; set
            {
                if (this.GetJgInfoAttribute(v => v._BezeichnungSoBilSachverhalt, value))
                    NotifyPropertyChanged("BezeichnungSoBilSachverhalt");
            }
        }

        // Kennzeichen SoBil-Buchung

        [JgInfo(false, 99)]
        private int? _KennzeichenSoBilBuchung = null;
        public int? KennzeichenSoBilBuchung
        {
            get => _KennzeichenSoBilBuchung; set
            {
                if (this.GetJgInfoAttribute(v => v._KennzeichenSoBilBuchung, value))
                    NotifyPropertyChanged("KennzeichenSoBilBuchung");
            }
        }

        // Festschreibung

        [JgInfo(false)]
        private EnumFestschreibung _Festschreibung = EnumFestschreibung.leer;
        public EnumFestschreibung Festschreibung { get => _Festschreibung; set => _Festschreibung = value; }

        // Leistungsdatum

        [JgInfo(false, Format = "ddMMyyyy")]
        private DateTime? _Leistungsdatum = null;
        public DateTime? Leistungsdatum { get => _Leistungsdatum; set => _Leistungsdatum = value; }

        // Datum Zuord.Steuerperiode

        [JgInfo(false, Format = "ddMMyyyy")]
        private DateTime? _DatumZuordnungSteuerperiode = null;
        public DateTime? DatumZuordnungSteuerperiode { get => _DatumZuordnungSteuerperiode; set => _DatumZuordnungSteuerperiode = value; }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.Append(this.JgDruck(v => v._Umsatz) + ";");
            sb.Append(this.JgDruck(v => v._SollHaben) + ";");
            sb.Append(this.JgDruck(v => v._WkzUmsatz) + ";");
            sb.Append(this.JgDruck(v => v._Kurs) + ";");
            sb.Append(this.JgDruck(v => v._BasisUmsatz) + ";");
            sb.Append(this.JgDruck(v => v._WkzBasisUmsatz) + ";");
            sb.Append(this.JgDruck(v => v._Konto) + ";");
            sb.Append(this.JgDruck(v => v._GegenKonto) + ";");
            sb.Append(this.JgDruck(v => v._BuSchluessel) + ";");
            sb.Append(this.JgDruck(v => v._Belegdatum) + ";");
            sb.Append(this.JgDruck(v => v._Belegfeld1) + ";");
            sb.Append(this.JgDruck(v => v._Belegfeld2) + ";");
            sb.Append(this.JgDruck(v => v._Skonto) + ";");
            sb.Append(this.JgDruck(v => v._Buchungstext) + ";");
            sb.Append(this.JgDruck(v => v._PostenSperre) + ";");
            sb.Append(this.JgDruck(v => v._DiverseAdressNummer) + ";");
            sb.Append(this.JgDruck(v => v._GeschäftspartnerBank) + ";");
            sb.Append(this.JgDruck(v => v._Sachverhalt) + ";");
            sb.Append(this.JgDruck(v => v._Zinssperre) + ";");
            sb.Append(this.JgDruck(v => v._BelegLink) + ";"); ;

            foreach (var belegInfo in this._BelegInfo)
                sb.Append("\"" + belegInfo.BeleginfoArt + "\";\"" + belegInfo.BeleginfoInhalt + "\"" + ";");

            sb.Append(this.JgDruck(v => v._Kost1) + ";");
            sb.Append(this.JgDruck(v => v._Kost2) + ";");
            sb.Append(this.JgDruck(v => v._KostMenge) + ";");
            sb.Append(this.JgDruck(v => v._EuMitgliedsStaatUStId) + ";");
            sb.Append(this.JgDruck(v => v._EuSteuersatz) + ";");
            sb.Append(this.JgDruck(v => v._AbwVersteuerungsart) + ";");
            sb.Append(this.JgDruck(v => v._Sachverhalt_L_L) + ";");
            sb.Append(this.JgDruck(v => v._FunktionsErgänzung_L_L) + ";");
            sb.Append(this.JgDruck(v => v._Bu49Hauptfunktionstyp) + ";");
            sb.Append(this.JgDruck(v => v._Bu49Hauptfunktionsnummer) + ";");
            sb.Append(this.JgDruck(v => v._Bu49Funktionsergänzung) + ";"); ;

            foreach (var zusatzInfo in this._ZusatzInformation)
                sb.Append("\"" + zusatzInfo.ZusatzinformationArt + "\";\"" + zusatzInfo.ZusatzinformationInhalt + "\"" + ";");

            sb.Append(this.JgDruck(v => v._Stueck) + ";");
            sb.Append(this.JgDruck(v => v._Gewicht) + ";");
            sb.Append(this.JgDruck(v => v._Zahlweise) + ";");
            sb.Append(this.JgDruck(v => v._ForderungsArt) + ";");
            sb.Append(this.JgDruck(v => v._Veranlagungsjahr) + ";");
            sb.Append(this.JgDruck(v => v._ZugeordneteFaelligkeit) + ";");
            sb.Append(this.JgDruck(v => v._SkontoType) + ";");
            sb.Append(this.JgDruck(v => v._Auftragsnummer) + ";");
            sb.Append(this.JgDruck(v => v._BuchungsTyp) + ";");
            sb.Append(this.JgDruck(v => v._UstSchluesselAnzahlungen) + ";");
            sb.Append(this.JgDruck(v => v._EuMitgliedstaatAnzahlungen) + ";");
            sb.Append(this.JgDruck(v => v._Sachverhalt_L_L_Anzahlungen) + ";");
            sb.Append(this.JgDruck(v => v._EuSteuersatzAnzahlungen) + ";");
            sb.Append(this.JgDruck(v => v._ErloeskontoAnzahlungen) + ";");
            sb.Append(this.JgDruck(v => v._HerkunftKfz) + ";");
            sb.Append(this.JgDruck(v => v._Leerfeld) + ";");
            sb.Append(this.JgDruck(v => v._KostDatum) + ";");
            sb.Append(this.JgDruck(v => v._SepaMandatsreferenz) + ";");
            sb.Append(this.JgDruck(v => v._Skontosperre) + ";");
            sb.Append(this.JgDruck(v => v._GesellschafterName) + ";");
            sb.Append(this.JgDruck(v => v._BeteiligtenNummer) + ";");
            sb.Append(this.JgDruck(v => v._IdentifikationsNummer) + ";");
            sb.Append(this.JgDruck(v => v._ZeichnerNummer) + ";");
            sb.Append(this.JgDruck(v => v._PostensperreBis) + ";");
            sb.Append(this.JgDruck(v => v._BezeichnungSoBilSachverhalt) + ";");
            sb.Append(this.JgDruck(v => v._KennzeichenSoBilBuchung) + ";");
            sb.Append(this.JgDruck(v => v._Festschreibung) + ";");
            sb.Append(this.JgDruck(v => v._Leistungsdatum) + ";");
            sb.Append(this.JgDruck(v => v._DatumZuordnungSteuerperiode));

            return sb.ToString();
        }

        public List<DsListeAnzeige> ListeAnzeigeKoerperErstellen()
        {
            var lAnzeige = new List<DsListeAnzeige>();

            lAnzeige.Add(this.JgAnzeige(v => v._Umsatz));
            lAnzeige.Add(this.JgAnzeige(v => v._SollHaben));
            lAnzeige.Add(this.JgAnzeige(v => v._WkzUmsatz));
            lAnzeige.Add(this.JgAnzeige(v => v._Kurs));
            lAnzeige.Add(this.JgAnzeige(v => v._BasisUmsatz));
            lAnzeige.Add(this.JgAnzeige(v => v._WkzBasisUmsatz));
            lAnzeige.Add(this.JgAnzeige(v => v._Konto));
            lAnzeige.Add(this.JgAnzeige(v => v._GegenKonto));
            lAnzeige.Add(this.JgAnzeige(v => v._BuSchluessel));
            lAnzeige.Add(this.JgAnzeige(v => v._Belegdatum));
            lAnzeige.Add(this.JgAnzeige(v => v._Belegfeld1));
            lAnzeige.Add(this.JgAnzeige(v => v._Belegfeld2));
            lAnzeige.Add(this.JgAnzeige(v => v._Skonto));
            lAnzeige.Add(this.JgAnzeige(v => v._Buchungstext));
            lAnzeige.Add(this.JgAnzeige(v => v._PostenSperre));
            lAnzeige.Add(this.JgAnzeige(v => v._DiverseAdressNummer));
            lAnzeige.Add(this.JgAnzeige(v => v._GeschäftspartnerBank));
            lAnzeige.Add(this.JgAnzeige(v => v._Sachverhalt));
            lAnzeige.Add(this.JgAnzeige(v => v._Zinssperre));
            lAnzeige.Add(this.JgAnzeige(v => v._BelegLink)); ;

            lAnzeige.Add(this.JgAnzeige(v => v._Kost1));
            lAnzeige.Add(this.JgAnzeige(v => v._Kost2));
            lAnzeige.Add(this.JgAnzeige(v => v._KostMenge));
            lAnzeige.Add(this.JgAnzeige(v => v._EuMitgliedsStaatUStId));
            lAnzeige.Add(this.JgAnzeige(v => v._EuSteuersatz));
            lAnzeige.Add(this.JgAnzeige(v => v._AbwVersteuerungsart));
            lAnzeige.Add(this.JgAnzeige(v => v._Sachverhalt_L_L));
            lAnzeige.Add(this.JgAnzeige(v => v._FunktionsErgänzung_L_L));
            lAnzeige.Add(this.JgAnzeige(v => v._Bu49Hauptfunktionstyp));
            lAnzeige.Add(this.JgAnzeige(v => v._Bu49Hauptfunktionsnummer));
            lAnzeige.Add(this.JgAnzeige(v => v._Bu49Funktionsergänzung)); ;

            lAnzeige.Add(this.JgAnzeige(v => v._Stueck));
            lAnzeige.Add(this.JgAnzeige(v => v._Gewicht));
            lAnzeige.Add(this.JgAnzeige(v => v._Zahlweise));
            lAnzeige.Add(this.JgAnzeige(v => v._ForderungsArt));
            lAnzeige.Add(this.JgAnzeige(v => v._Veranlagungsjahr));
            lAnzeige.Add(this.JgAnzeige(v => v._ZugeordneteFaelligkeit));
            lAnzeige.Add(this.JgAnzeige(v => v._SkontoType));
            lAnzeige.Add(this.JgAnzeige(v => v._Auftragsnummer));
            lAnzeige.Add(this.JgAnzeige(v => v._BuchungsTyp));
            lAnzeige.Add(this.JgAnzeige(v => v._UstSchluesselAnzahlungen));
            lAnzeige.Add(this.JgAnzeige(v => v._EuMitgliedstaatAnzahlungen));
            lAnzeige.Add(this.JgAnzeige(v => v._Sachverhalt_L_L_Anzahlungen));
            lAnzeige.Add(this.JgAnzeige(v => v._EuSteuersatzAnzahlungen));
            lAnzeige.Add(this.JgAnzeige(v => v._ErloeskontoAnzahlungen));
            lAnzeige.Add(this.JgAnzeige(v => v._HerkunftKfz));
            lAnzeige.Add(this.JgAnzeige(v => v._Leerfeld));
            lAnzeige.Add(this.JgAnzeige(v => v._KostDatum));
            lAnzeige.Add(this.JgAnzeige(v => v._SepaMandatsreferenz));
            lAnzeige.Add(this.JgAnzeige(v => v._Skontosperre));
            lAnzeige.Add(this.JgAnzeige(v => v._GesellschafterName));
            lAnzeige.Add(this.JgAnzeige(v => v._BeteiligtenNummer));
            lAnzeige.Add(this.JgAnzeige(v => v._IdentifikationsNummer));
            lAnzeige.Add(this.JgAnzeige(v => v._ZeichnerNummer));
            lAnzeige.Add(this.JgAnzeige(v => v._PostensperreBis));
            lAnzeige.Add(this.JgAnzeige(v => v._BezeichnungSoBilSachverhalt));
            lAnzeige.Add(this.JgAnzeige(v => v._KennzeichenSoBilBuchung));
            lAnzeige.Add(this.JgAnzeige(v => v._Festschreibung));
            lAnzeige.Add(this.JgAnzeige(v => v._Leistungsdatum));
            lAnzeige.Add(this.JgAnzeige(v => v._DatumZuordnungSteuerperiode));

            var zaehler = 0;
            foreach (var ds in lAnzeige)
            {
                zaehler++;
                switch (zaehler)
                {
                    case 21: zaehler = 37; break;
                    case 48: zaehler = 88; break;
                }

                ds.Id = zaehler;
            }

            return lAnzeige;
        }
    }
}
