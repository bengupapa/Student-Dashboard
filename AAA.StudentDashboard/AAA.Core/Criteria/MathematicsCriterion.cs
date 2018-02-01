using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AAA.Shared;

namespace AAA.Core
{
    public class MathematicsCriterion : ICriterion
    {
        public bool Evaluate(IDictionary<Subject, Status> subjects)
        {
            bool passed = subjects[Subject.Mathematics].GetValue();
            bool failedOne = !subjects.Any(kvp => kvp.Value == Status.Failed && kvp.Key != Subject.Mathematics);

            if (passed) return passed;

            return failedOne;
        }
    }
}
