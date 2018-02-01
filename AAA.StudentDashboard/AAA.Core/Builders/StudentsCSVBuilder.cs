using AAA.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AAA.Core
{
    public class StudentsCSVBuilder
    {
        private string _OutputDir { get; set; }

        public void SetOutputDirectory(string outputDir)
        {
            _OutputDir = outputDir;
        }

        public void Build(params Student[] students)
        {
            foreach(Student student in students)
            {
                string studentName = student.GetFullName();
                IEnumerable<SerializableStudent> serializableStudents = TransformationHelper.Deform(new[] { student });

                var builder = new CSVBuilder(studentName, _OutputDir);
                builder.Build<SerializableStudent>(serializableStudents.ToArray());
            }
        }
    }
}
