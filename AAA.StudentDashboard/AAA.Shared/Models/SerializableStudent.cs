using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AAA.Shared
{
    public sealed partial class SerializableStudent
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public BooleanType IsClassLeader { get; set; }
        public string Grade { get; set; }
        public Subject Subject { get; set; }
        public string Score { get; set; }
    }

    public sealed partial class SerializableStudent
    {
        public string AlphaScore { get; set; }
        public string AverageScore { get; set; }
        public Status SubjectStatus { get; set; }
        public string HighestScoreSubject { get; set; }
        public string LowestScoreSubject { get; set; }
        public Status OverallStatus { get; set; }

        public string GetFullName()
        {
            return $"{Name} {Surname}";
        }
    }
}
