using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AAA.Shared
{
    public class BooleanTypeAttribute : Attribute
    {
        public bool BooleanValue { get; set; }

        public BooleanTypeAttribute(bool value)
        {
            BooleanValue = value;
        }
    }
}
