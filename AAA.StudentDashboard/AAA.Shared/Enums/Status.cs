using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AAA.Shared
{
    public enum Status
    {
        [BooleanType(true)]
        Passed,
        [BooleanType(false)]
        Failed
    }
}
