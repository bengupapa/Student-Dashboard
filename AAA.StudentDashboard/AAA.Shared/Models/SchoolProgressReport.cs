using AAA.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AAA.Core
{
    public class SchoolProgressReport
    {
        private ICollection<GradeReport> _Report { get; }

        public SchoolProgressReport()
        {
            _Report = new List<GradeReport>();
        }

        public void AddGradeReport(GradeReport report)
        {
            _Report.Add(report);
        }

        public List<GradeReport> GetGradeReport()
        {
            return _Report.OrderByDescending(gr => gr.AvgScore).ToList();
        }
    }
}
