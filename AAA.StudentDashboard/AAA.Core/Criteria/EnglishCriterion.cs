using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AAA.Shared;

namespace AAA.Core
{
    public class EnglishCriterion : ICriterion
    {
        public bool Evaluate(IDictionary<Subject, Status> subjects)
        {
            return subjects[Subject.English].GetValue();
        }
    }
}
