using AAA.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AAA.Core
{
    public interface ICriteria
    {
        StudentProgressReport Run(params ICriterion[] critaria);
        StudentProgressReport Run();
    }
}
