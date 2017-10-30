using System;

namespace JgDatevExportLib
{
    [AttributeUsage(AttributeTargets.Field)]
    public class JgInfoAttribute : Attribute
    {
        public enum AnzeigeEnums
        {
            AlsZahl,
            AlsString,
            ErsterBuchstabe,
            ErsteZweiBuchstaben,
            LetztesZeichen
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
            : this(IstErforderlich)
        {
            this.Max = Max;
        }

        public JgInfoAttribute(bool IstErforderlich, int Min, int Max)
            : this(IstErforderlich, Max)
        {
            this.Min = Min;
        }
    }
}
