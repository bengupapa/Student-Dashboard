using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AAA.Shared;

namespace AAA.Core
{
    public class SubjectsCriterion : ICriterion
    {
        public bool Evaluate(IDictionary<Subject, Status> subjects)
        {
            return subjects.Values.Count(s => s == Status.Failed) < 2;
        }
    }
}
