using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JgDatevExportLib
{
    [AttributeUsage(AttributeTargets.All)]
    public class StringLengthAttribute : Attribute
    {
        public int Min;
        public int Max;

        public StringLengthAttribute()
        { }
    }

    [AttributeUsage(AttributeTargets.All)]
    public class IntSizeAttribute : Attribute
    {
        public int Min;
        public int Max;

        public IntSizeAttribute()
        { }
    }
}
