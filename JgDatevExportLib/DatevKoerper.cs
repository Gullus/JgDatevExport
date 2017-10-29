using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using static JgDatevExportLib.DatevEnum;

namespace JgDatevExportLib
{
    [AttributeUsage(AttributeTargets.Property)]
    public class JgInfoAttribute : Attribute
    {
        public enum AnzeigeEnums
        {
            AlsZahl,
            AlsString,
            ErsterBuchstabe
        }

        public bool IstErforderlich { get; set; } = true;

        public int Min { get; set; } = 0;
        public int Max { get; set; } = 40;
        public string Format { get; set; } = null;

        public AnzeigeEnums AnzeigeEnum { get; set; } = AnzeigeEnums.AlsZahl;

        public JgInfoAttribute(bool IstErforderlich)
        {
            this.IstErforderlich = IstErforderlich;
        }

        public JgInfoAttribute(bool IstErforderlich, int Max)
            : this (IstErforderlich)
        {
            this.Max = Max;
        }

        public JgInfoAttribute(bool IstErforderlich, int Min, int Max)
            : this(IstErforderlich, Max)
        {
            this.Min = Min;
        }
    }

    public static class JgInfoExtensions
    {
        private static JgInfoAttribute GetAttrib(MemberExpression MemberExp)
        {
            var attrTemp = MemberExp.Member.GetCustomAttributes(typeof(JgInfoAttribute), true);
            return (JgInfoAttribute)attrTemp[0];
        }

        public static bool GetJgInfoAttribute<T>(this T obj, Expression<Func<T, string>> value, string WertNeu, ref string Wert)
        {
            if (WertNeu != Wert)
            {
                Wert = WertNeu;

                var mExpression = (MemberExpression)value.Body;
                var attr = GetAttrib(mExpression);

                // var type = obj.GetType();
                // var prop = type.GetProperty(mExpression.Member.Name);
                // var wert = prop.GetValue(obj);

                return true;
            }
            return false;
        }

        public static bool GetJgInfoAttribute<T>(this T obj, Expression<Func<T, int>> value, int WertNeu, ref int Wert)
        {
            if (WertNeu != Wert)
            {
                Wert = WertNeu;

                var mExpression = (MemberExpression)value.Body;
                var attr = GetAttrib(mExpression);

                return true;
            }
            return false;
        }

        public static bool GetJgInfoAttribute<T>(this T obj, Expression<Func<T, int?>> value, int? WertNeu, ref int? Wert)
        {
            if (WertNeu != Wert)
            {
                Wert = WertNeu;

                var mExpression = (MemberExpression)value.Body;
                var attr = GetAttrib(mExpression);

                return true;
            }
            return false;
        }

        public static bool GetJgInfoAttribute<T>(this T obj, Expression<Func<T, decimal?>> value, decimal? WertNeu, ref decimal? Wert)
        {
            if (WertNeu != Wert)
            {
                Wert = WertNeu;

                var mExpression = (MemberExpression)value.Body;
                var attr = GetAttrib(mExpression);

                return true;
            }
            return false;
        }

        public static bool GetJgInfoAttribute<T>(this T obj, Expression<Func<T, decimal>> value, decimal WertNeu, ref decimal Wert)
        {
            if (WertNeu != Wert)
            {
                Wert = WertNeu;

                var mExpression = (MemberExpression)value.Body;
                var attr = GetAttrib(mExpression);

                return true;
            }
            return false;
        }
    }

    public class TBelegInfo
    {
        private string _BeleginfoArt = "";
        public string BeleginfoArt { get => _BeleginfoArt; set => _BeleginfoArt = value; }

        private string _BeleginfoInhalt = "";
        public string BeleginfoInhalt { get => _BeleginfoInhalt; set => _BeleginfoInhalt = value; }
    }

    public class TZusatzInformation
    {
        private string _ZusatzinformationArt = "";
        public string ZusatzinformationArt { get => _ZusatzinformationArt; set => _ZusatzinformationArt = value; }


        private string _ZusatzinformationInhalt = "";
        public string ZusatzinformationInhalt { get => _ZusatzinformationInhalt; set => _ZusatzinformationInhalt = value; }

        public override string ToString()
        {
            return _ZusatzinformationArt + "; " + _ZusatzinformationInhalt ;
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

        private decimal _Umsatz = 0;

        [JgInfo(true, 13, Format = "N2")]
        public decimal Umsatz
        {
            get => _Umsatz;
            set
            {
                if (this.GetJgInfoAttribute(v => v.Umsatz, value, ref _Umsatz))
                    NotifyPropertyChanged();
            }
        }
        
        // Soll/Haben-Kennzeichen

        [JgInfo(true, AnzeigeEnum = JgInfoAttribute.AnzeigeEnums.ErsterBuchstabe)]
        public EnumSollHaben SollHaben { get; set; } = EnumSollHaben.Soll;

        // WKZ Umsatz

        [JgInfo(false, AnzeigeEnum = JgInfoAttribute.AnzeigeEnums.AlsString)]
        public EnumWaehrung WkzUmsatz { get; set; } = EnumWaehrung.leer;

        // Kurs

        public decimal? _Kurs = null;

        [JgInfo(false, 11, Format = "N6")]
        public decimal? Kurs
        {
            get => _Kurs;
            set
            {
                if (this.GetJgInfoAttribute(v => v.Kurs, value, ref _Kurs))
                    NotifyPropertyChanged();
            }
        }

        // Basis-Umsatz

        private decimal? _BasisUmsatz = 0;

        [JgInfo(false, 13, Format = "N2")]
        public decimal? BasisUmsatz
        {
            get => _BasisUmsatz;
            set
            {
                if (this.GetJgInfoAttribute(v => v.BasisUmsatz, value, ref _BasisUmsatz))
                    NotifyPropertyChanged();
            }
        }

        // WKZ Basis-Umsatz

        [JgInfo(false)]
        public EnumWaehrung WkzBasisUmsatz { get; set; } = EnumWaehrung.leer;

        // Konto

        private string _Konto = "";

        [JgInfo(true, 4, 9)]
        public string Konto
        {
            get => _Konto;
            set
            {
                if (this.GetJgInfoAttribute(v => v.Konto, value, ref _Konto))
                    NotifyPropertyChanged();
            }
        }

        // Gegenkonto(ohne BU-Schlüssel)

        private string _GegenKonto = "";

        [JgInfo(true, 4, 9)]
        public string GegenKonto
        {
            get => _GegenKonto;
            set
            {
                if (this.GetJgInfoAttribute(v => v.GegenKonto, value, ref _GegenKonto))
                    NotifyPropertyChanged();
            }
        }

        // BU-Schlüssel

        private string _BuSchluessel = null;

        [JgInfo(false, 2)]
        public string BuSchluessel
        {
            get => _BuSchluessel;
            set
            {
                if (this.GetJgInfoAttribute(v => v.BuSchluessel, value, ref _BuSchluessel))
                    NotifyPropertyChanged();
            }
        }

        // Belegdatum

        [JgInfo(true, 4, Format = "ddMM")]
        public DateTime Belegdatum { get; set; } = DateTime.Now;

        // Belegfeld 1

        private string _Belegfeld1 = null;

        [JgInfo(false, 12)]
        public string Belegfeld1
        {
            get => _Belegfeld1;
            set
            {
                if (this.GetJgInfoAttribute(v => v.Belegfeld1, value, ref _Belegfeld1))
                    NotifyPropertyChanged();
            }
        }

        // Belegfeld 2

        private string _Belegfeld2 = null;

        [JgInfo(false, 12)]
        public string Belegfeld2
        {
            get => _Belegfeld2;
            set
            {
                if (this.GetJgInfoAttribute(v => v.Belegfeld2, value, ref _Belegfeld2))
                    NotifyPropertyChanged();
            }
        }

        // Skonto

        private decimal? _Skonto = null;

        [JgInfo(false, 11, Format = "N2")]
        public decimal? Skonto
        {
            get => _Skonto;
            set
            {
                if (this.GetJgInfoAttribute(v => v.Skonto, value, ref _Skonto))
                    NotifyPropertyChanged();
            }
        }

        // Buchungstext

        private string _Buchungstext = null;

        [JgInfo(false, 60)]
        public string Buchungstext
        {
            get => _Buchungstext;
            set
            {
                if (this.GetJgInfoAttribute(v => v.Buchungstext, value, ref _Buchungstext))
                    NotifyPropertyChanged();
            }
        }

        // Postensperre

        [JgInfo(false)]
        public EnumPostensperre PostenSperre { get; set; } = EnumPostensperre.leer;

        // Diverse Adressnummer

        private string _DiverseAdressNummer = null;

        [JgInfo(false, 9)]
        public string DiverseAdressnummer
        {
            get => _DiverseAdressNummer;
            set
            {
                if (this.GetJgInfoAttribute(v => v.DiverseAdressnummer, value, ref _DiverseAdressNummer))
                    NotifyPropertyChanged();
            }
        }

        // Geschäftspartnerbank

        private int? _GeschäftspartnerBank = null;

        [JgInfo(false, 999)]
        public int? GeschäftspartnerBank
        {
            get => _GeschäftspartnerBank;
            set
            {
                if (this.GetJgInfoAttribute(v => v.GeschäftspartnerBank, value, ref _GeschäftspartnerBank))
                    NotifyPropertyChanged();
            }
        }

        // Sachverhalt

        private int? _Sachverhalt = null;

        [JgInfo(false, 99)]
        public int? Sachverhalt
        {
            get => _Sachverhalt;
            set
            {
                if (this.GetJgInfoAttribute(v => v.Sachverhalt, value, ref _Sachverhalt))
                    NotifyPropertyChanged();
            }
        }

        // Zinssperre

        [JgInfo(false)]
        public EnumZissperre Zinssperre { get; set; } = EnumZissperre.leer;

        //  20 - Beleglink

        private string _BelegLink = null;

        [JgInfo(false, 210)]
        public string BelegLink
        {
            get => _BelegLink;
            set
            {
                if (this.GetJgInfoAttribute(v => v.BelegLink, value, ref _BelegLink))
                    NotifyPropertyChanged();
            }
        }

        // Beleginfo-Art und Inhalt 1 - 8 ********

        private TBelegInfo[] _BelegInfo = new TBelegInfo[8];
        public TBelegInfo[] BelegInfo { get => _BelegInfo; set => _BelegInfo = value; }

        // KOST1-Kostenstelle

        private string _Kost1 = null;

        [JgInfo(false, 8)]
        public string Kost1
        {
            get => _Kost1;
            set
            {
                if (this.GetJgInfoAttribute(v => v.Kost1, value, ref _Kost1))
                    NotifyPropertyChanged();
            }
        }

        // KOST2-Kostenstelle

        private string _Kost2 = null;

        [JgInfo(false, 8)]
        public string Kost2
        {
            get => _Kost2;
            set
            {
                if (this.GetJgInfoAttribute(v => v.Kost2, value, ref _Kost2))
                    NotifyPropertyChanged();
            }
        }

        // Kost-Menge

        private decimal? _KostMenge = null;

        [JgInfo(false, 12, Format = "N2")]
        public decimal? KostMenge
        {
            get => _KostMenge;
            set
            {
                if (this.GetJgInfoAttribute(v => v.KostMenge, value, ref _KostMenge))
                    NotifyPropertyChanged();
            }
        }

        // 40 - EU-Land u.UStID

        private string _UStId = null;

        [JgInfo(false, 15)]
        public string UStId
        {
            get => _UStId;
            set
            {
                if (this.GetJgInfoAttribute(v => v.UStId, value, ref _UStId))
                    NotifyPropertyChanged();
            }
        }

        // EU-Steuersatz

        private decimal? _EuSteuersatz = null;

        [JgInfo(false, 99, Format = "N2")]
        public decimal? EuSteuersatz
        {
            get => _EuSteuersatz;
            set
            {
                if (this.GetJgInfoAttribute(v => v.EuSteuersatz, value, ref _EuSteuersatz))
                    NotifyPropertyChanged();
            }
        }

        // Abw.Versteuerungsart

        [JgInfo(false, AnzeigeEnum = JgInfoAttribute.AnzeigeEnums.ErsterBuchstabe)]
        public EnumAbwVersteuerungsart AbwVersteuerungsart { get; set; } = EnumAbwVersteuerungsart.leer;

        // Sachverhalt L+L

        private int? _Sachverhalt_L_L = null;

        [JgInfo(false, 999)]
        public int? Sachverhalt_L_L
        {
            get => _Sachverhalt_L_L;
            set
            {
                if (this.GetJgInfoAttribute(v => v.Sachverhalt_L_L, value, ref _Sachverhalt_L_L))
                    NotifyPropertyChanged();
            }
        }

        // Funktionsergänzung L+L

        private int? _FunktionsErgänzung_L_L = null;

        [JgInfo(false, 999)]
        public int? FunktionsErgänzung_L_L
        {
            get => _Sachverhalt_L_L;
            set
            {
                if (this.GetJgInfoAttribute(v => v.FunktionsErgänzung_L_L, value, ref _FunktionsErgänzung_L_L))
                    NotifyPropertyChanged();
            }
        }

        // BU 49 Hauptfunktionstyp

        private int? _Bu49Hauptfunktionstyp = null;

        [JgInfo(false, 9)]
        public int? Bu49Hauptfunktionstyp
        {
            get => _Bu49Hauptfunktionstyp;
            set
            {
                if (this.GetJgInfoAttribute(v => v.Bu49Hauptfunktionstyp, value, ref _Bu49Hauptfunktionstyp))
                    NotifyPropertyChanged();
            }
        }

        // BU 49 Hauptfunktionsnummer

        private int? _Bu49Hauptfunktionsnummer = null;

        [JgInfo(false, 99)]
        public int? Bu49Hauptfunktionsnummer
        {
            get => _Bu49Hauptfunktionsnummer;
            set
            {
                if (this.GetJgInfoAttribute(v => v.Bu49Hauptfunktionsnummer, value, ref _Bu49Hauptfunktionsnummer))
                    NotifyPropertyChanged();
            }
        }

        // BU 49 Funktionsergänzung

        private int? _Bu49Funktionsergänzung = null;

        [JgInfo(false, 99)]
        public int? Bu49Funktionsergänzung
        {
            get => _Bu49Funktionsergänzung;
            set
            {
                if (this.GetJgInfoAttribute(v => v.Bu49Funktionsergänzung, value, ref _Bu49Funktionsergänzung))
                    NotifyPropertyChanged();
            }
        }

        // Zusatzinformation-Art und Inhalt 1 - 20 ********

        private TZusatzInformation[] _ZusatzInformation = new TZusatzInformation[20];
        public TZusatzInformation[] ZusatzInformation { get => _ZusatzInformation; set => _ZusatzInformation = value; }

        // Stück

        private int? _Stueck = null;

        [JgInfo(false, 99999999)]
        public int? Stueck
        {
            get => _Stueck;
            set
            {
                if (this.GetJgInfoAttribute(v => v.Stueck, value, ref _Stueck))
                    NotifyPropertyChanged();
            }
        }

        // Gewicht

        private decimal? _Gewicht = null;

        [JgInfo(false, 99999999, Format = "N2")]
        public decimal? Gewicht
        {
            get => _Gewicht;
            set
            {
                if (this.GetJgInfoAttribute(v => v.Gewicht, value, ref _Gewicht))
                    NotifyPropertyChanged();
            }
        }

        // 90 - Zahlweise

        [JgInfo(false)]
        public EnumZahlweise Zahlweise { get; set; } = EnumZahlweise.leer;

        // Forderungsart
        // Veranlagungsjahr
        // Zugeordnete Fälligkeit
        // Skontotyp
        // Auftragsnummer
        // Buchungstyp
        // Ust-Schlüssel(Anzahlungen)
        // EU-Land(Anzahlungen)
        // Sachverhalt L+L(Anzahlungen)
        // EU-Steuersatz(Anzahlungen)
        // Erlöskonto(Anzahlungen)
        // Herkunft-Kz
        // Leerfeld
        // KOST-Datum
        // Mandatsreferenz
        // Skontosperre
        // Gesellschaftername
        // Beteiligtennummer
        // Identifikationsnummer
        // Zeichnernummer
        // Postensperre bis
        // Bezeichnung SoBil-Sachverhalt
        // Kennzeichen SoBil-Buchung
        // Festschreibung
        // Leistungsdatum
        // Datum Zuord.Steuerperiode
    }
}
