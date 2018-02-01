using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AAA.Core
{
    public class ScoreRange
    {
        public double Maximum { get; }
        public double Minimum { get; }
        public string Alpha { get; }

        public ScoreRange(string alpha, double value1, double value2)
        {
            if (string.IsNullOrWhiteSpace(alpha))
                throw new ArgumentNullException(nameof(alpha));

            Maximum = (value2.CompareTo(value1) >= 0) ? value2 : value1;
            Minimum = (value2.CompareTo(value1) >= 0) ? value1 : value2;
            Alpha = alpha.ToUpper();
        }

        public bool Contains(double score)
        {
            return score >= Minimum && score <= Maximum;
        }

        public override string ToString()
        {
            return Alpha;
        }
    }
}
