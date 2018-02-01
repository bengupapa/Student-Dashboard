using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AAA.Shared
{
    public enum BooleanType
    {
        [BooleanType(true)]Yes,
        [BooleanType(false)]No,
        [BooleanType(true)]Y,
        [BooleanType(false)]N
    }
}
