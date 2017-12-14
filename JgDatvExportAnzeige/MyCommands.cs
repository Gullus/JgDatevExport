using System.Windows.Input;

namespace JgDatevExportAnzeige
{
    public static class MyCommands
    {
        private static RoutedUICommand _HeaderAnzeigen;
        public static RoutedUICommand HeaderAnzeigen
        {
            get { return _HeaderAnzeigen; }
        }

        private static RoutedUICommand _KoerperAnzeigen;
        public static RoutedUICommand KoerperAnzeigen
        {
            get { return _KoerperAnzeigen; }
        }

        private static RoutedUICommand _BelegInfoAnzeigen;
        public static RoutedUICommand BelegInformationAnzeigen
        {
            get { return _BelegInfoAnzeigen; }
        }

        private static RoutedUICommand _ZusatzInformationAnzeigen;
        public static RoutedUICommand ZusatzInformationAnzeigen
        {
            get { return _ZusatzInformationAnzeigen; }
        }

        private static RoutedUICommand _Speichern;
        public static RoutedUICommand Speichern
        {
            get { return _Speichern; }
        }

        private static RoutedUICommand _Laden;
        public static RoutedUICommand Laden
        {
            get { return _Laden; }
        }

        private static RoutedUICommand _Beenden;
        public static RoutedUICommand Beenden
        {
            get { return _Beenden; }
        }

        private static RoutedUICommand _DatevOptionen;
        public static RoutedUICommand DatevOptionen
        {
            get { return _DatevOptionen; }
        }

        static MyCommands()
        {
            _HeaderAnzeigen = new RoutedUICommand("Header anzeigen", "HeaderAnzeigen", typeof(MyCommands));
            _KoerperAnzeigen = new RoutedUICommand("Koerper anzeigen", "KoerperAnzeigen", typeof(MyCommands));
            _BelegInfoAnzeigen = new RoutedUICommand("BelegInfo anzeigen", "BelegInfoAnzeigen", typeof(MyCommands));
            _ZusatzInformationAnzeigen = new RoutedUICommand("ZusatzInformation anzeigen", "ZusatzInformationAnzeigen", typeof(MyCommands));

            _DatevOptionen = new RoutedUICommand();

            _Laden = new RoutedUICommand("Daten laden", "DatenLaden", typeof(MyCommands));
            _Speichern = new RoutedUICommand("Daten speichern", "DatenSpeichern", typeof(MyCommands));
            _Beenden = new RoutedUICommand("Programm beenden", "ProgrammBeenden", typeof(MyCommands));
        }
    }
}