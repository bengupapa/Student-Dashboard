using AAA.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AAA.Core
{
    public interface ICriterion
    {
        bool Evaluate(IDictionary<Subject, Status> subjects);
    }
}
