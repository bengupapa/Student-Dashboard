using AAA.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AAA.Core
{
    public class StudentsCSVParser : IDisposable
    {
        private ICollectionFileParser _Parser { get; }
        private readonly string[] _ExcludedFields = new[]
        {
            "AlphaScore",
            "AverageScore",
            "SubjectStatus",
            "HighestScoreSubject",
            "LowestScoreSubject",
            "OverallStatus"
        };

        public StudentsCSVParser(string filePath)
        {
            _Parser = new CSVParser(filePath);
            _Parser.SetExcludedFields(_ExcludedFields);
        }

        public bool TryParse(out IEnumerable<Student> listOfStudents)
        {
            IEnumerable<SerializableStudent> students;
            var parsed = _Parser.TryParse<SerializableStudent>(out students);

            listOfStudents = parsed ? TransformationHelper.Transform(students) : null;
            return parsed;
        }

        public void Dispose()
        {
            _Parser.Dispose();
        }
    }
}
