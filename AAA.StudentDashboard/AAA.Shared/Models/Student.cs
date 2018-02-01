using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AAA.Shared
{
    public sealed partial class Student
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public BooleanType IsClassLeader { get; set; }
        public Grade Grade { get; set; }
        public IDictionary<Subject, double> Scores { get; set; }
    }

    public partial class Student
    {
        public string GetFullName()
        {
            return $"{Name} {Surname}";
        }
    }
}
