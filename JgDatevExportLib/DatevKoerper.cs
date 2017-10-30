using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using static JgDatevExportLib.DatevEnum;

namespace JgDatevExportLib
{
    public class TBelegInfo : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        [JgInfo(false, 20)]
        private string _BeleginfoArt = null;
        public string BeleginfoArt
        {
            get => _BeleginfoArt;
            set
            {
                if (this.GetJgInfoAttribute(v => v._BeleginfoArt, value))
                    NotifyPropertyChanged();
            }
        }

        [JgInfo(false, 210)]
        private string _BeleginfoInhalt = null;
        public string BeleginfoInhalt
        {
            get => _BeleginfoInhalt; set
            {
                if (this.GetJgInfoAttribute(v => v._BeleginfoInhalt, value))
                    NotifyPropertyChanged();
            }
        }

        public override string ToString()
        {
            return (_BeleginfoArt ?? "Art") + "; " + (_BeleginfoInhalt ?? "Inhalt");
        }
    }

    public class TZusatzInformation : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        [JgInfo(false, 20)]
        private string _ZusatzinformationArt = null;
        public string ZusatzinformationArt
        {
            get => _ZusatzinformationArt;
            set
            {
                if (this.GetJgInfoAttribute(v => v._ZusatzinformationArt, value))
                    NotifyPropertyChanged();
            }
        }

        [JgInfo(false, 210)]
        private string _ZusatzinformationInhalt = null;
        public string ZusatzinformationInhalt
        {
            get => _ZusatzinformationInhalt; set
            {
                if (this.GetJgInfoAttribute(v => v._ZusatzinformationInhalt, value))
                    NotifyPropertyChanged();
            }
        }

        public override string ToString()
        {
            return (_ZusatzinformationArt ?? "Art") + "; " + (_ZusatzinformationInhalt ?? "Inhalt");
        }
    }

    public class DatevKoerper : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public DatevKoerper()
        {
            for (int i = 0; i < 8; i++)
                _BelegInfo[i] = new TBelegInfo();

            for (int i = 0; i < 20; i++)
            {
                _ZusatzInformation[i] = new TZusatzInformation();
            }
        }

        // Umsatz(ohne Soll/Haben-Kz)

        [JgInfo(true, 13, Format = "N2")]
        private decimal _Umsatz = 39.90m;
        public decimal Umsatz
        {
            get => _Umsatz;
            set
            {
                if (this.GetJgInfoAttribute(v => v._Umsatz, value))
                    NotifyPropertyChanged();
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
        public decimal? _Kurs = null;
        public decimal? Kurs
        {
            get => _Kurs;
            set
            {
                if (this.GetJgInfoAttribute(v => v._Kurs, value))
                    NotifyPropertyChanged();
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
                    NotifyPropertyChanged();
            }
        }

        // WKZ Basis-Umsatz

        [JgInfo(false)]
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
                    NotifyPropertyChanged();
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
                    NotifyPropertyChanged();
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
                    NotifyPropertyChanged();
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
                    NotifyPropertyChanged();
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
                    NotifyPropertyChanged();
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
                    NotifyPropertyChanged();
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
                    NotifyPropertyChanged();
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
                    NotifyPropertyChanged();
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
                    NotifyPropertyChanged();
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
                    NotifyPropertyChanged();
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
                    NotifyPropertyChanged();
            }
        }

        // Beleginfo-Art und Inhalt 1 - 8 ********

        private TBelegInfo[] _BelegInfo = new TBelegInfo[8];
        public TBelegInfo[] BelegInfo { get => _BelegInfo; set => _BelegInfo = value; }

        // KOST1-Kostenstelle

        [JgInfo(false, 8)]
        private string _Kost1 = null;
        public string Kost1
        {
            get => _Kost1;
            set
            {
                if (this.GetJgInfoAttribute(v => v._Kost1, value))
                    NotifyPropertyChanged();
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
                    NotifyPropertyChanged();
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
                    NotifyPropertyChanged();
            }
        }

        // 40 - EU-Land u.UStID

        [JgInfo(false, 15)]
        private string _UStId = null;
        public string UStId
        {
            get => _UStId;
            set
            {
                if (this.GetJgInfoAttribute(v => v._UStId, value))
                    NotifyPropertyChanged();
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
                    NotifyPropertyChanged();
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
                    NotifyPropertyChanged();
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
                    NotifyPropertyChanged();
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
                    NotifyPropertyChanged();
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
                    NotifyPropertyChanged();
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
                    NotifyPropertyChanged();
            }
        }

        // Zusatzinformation-Art und Inhalt 1 - 20 ********

        private TZusatzInformation[] _ZusatzInformation = new TZusatzInformation[20];
        public TZusatzInformation[] ZusatzInformation { get => _ZusatzInformation; set => _ZusatzInformation = value; }

        // Stück

        [JgInfo(false, 99999999)]
        private int? _Stueck = null;
        public int? Stueck
        {
            get => _Stueck;
            set
            {
                if (this.GetJgInfoAttribute(v => v._Stueck, value))
                    NotifyPropertyChanged();
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
                    NotifyPropertyChanged();
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
                    NotifyPropertyChanged();
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
                    NotifyPropertyChanged();
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
                    NotifyPropertyChanged();
            }
        }

        // Buchungstyp

        [JgInfo(false, AnzeigeEnum = JgInfoAttribute.AnzeigeEnums.ErsteZweiBuchstaben)]
        private EnumBuchungsTypKoerper _BuchungsTyp = EnumBuchungsTypKoerper.leer;
        public EnumBuchungsTypKoerper BuchungsTyp { get => _BuchungsTyp; set => _BuchungsTyp = value; }

        // Ust-Schlüssel(Anzahlungen)

        [JgInfo(false, 99)]
        private int? _UstUstSchluesselAnzahlungen = null;
        public int? UstUstSchluesselAnzahlungen
        {
            get => _UstUstSchluesselAnzahlungen;
            set
            {
                if (this.GetJgInfoAttribute(v => v._UstUstSchluesselAnzahlungen, value))
                    NotifyPropertyChanged();
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
                    NotifyPropertyChanged();
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
                    NotifyPropertyChanged();
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
                    NotifyPropertyChanged();
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
                    NotifyPropertyChanged();
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
                    NotifyPropertyChanged();
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
                    NotifyPropertyChanged();
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
                    NotifyPropertyChanged();
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
                    NotifyPropertyChanged();
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
                    NotifyPropertyChanged();
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
                    NotifyPropertyChanged();
            }
        }

        // 110 - Zeichnernummer

        [JgInfo(false, 20)]
        private string _ZeichenNummer = null;
        public string ZeichenNummer
        {
            get => _ZeichenNummer; set
            {
                if (this.GetJgInfoAttribute(v => v._ZeichenNummer, value))
                    NotifyPropertyChanged();
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
                    NotifyPropertyChanged();
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
                    NotifyPropertyChanged();
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
    }
}
