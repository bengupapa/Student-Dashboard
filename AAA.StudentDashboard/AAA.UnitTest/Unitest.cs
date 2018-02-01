using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AAA.Core;
using AAA.Shared;
using System.Collections.Generic;
using System.Linq;
using AAA.DataAccess;

namespace AAA.UnitTest
{
    [TestClass, DeploymentItem(@"..\..\..\..\Data\Students.csv", "DataFile")]
    public class Unitest
    {
        private readonly string[] _ExcludedFields = new[] 
        {
            "Subjects",
            "Scores"
        };

        [TestMethod]
        public void CSVParser_Should_Parse_File_To_IEnumerable()
        {
            using (var parser = new CSVParser(@"DataFile\Students.csv"))
            {
                parser.SetExcludedFields(_ExcludedFields);

                IEnumerable<Student> students;
                parser.TryParse<Student>(out students);

                Assert.IsTrue(students.Any());
                Assert.AreEqual(students.Count(), 570);
                Assert.AreEqual(students.Count(s => s.GetFullName() == "Randy Strickland"), 6);
            }
        }

        [TestMethod]
        public void StudentsCSVParser_Should_Sanitize_Student_List()
        {
            using (var parser = new StudentsCSVParser(@"DataFile\Students.csv"))
            {
                IEnumerable<Student> students;
                parser.TryParse(out students);

                var student = students.First(s => s.GetFullName() == "Randy Strickland");
                var scoreManager = new ScoreManager(student);

                Assert.AreEqual(students.Count(), 95);
                Assert.AreEqual(students.Count(s => s.GetFullName() == student.GetFullName()), 1);
                Assert.AreEqual(scoreManager.GetAlphaScore(Subject.Mathematics), "B");
            }
        }

        [TestMethod]
        public void ProgressManager_Should_Complite_Student_Report()
        {
            using (var parser = new StudentsCSVParser(@"DataFile\Students.csv"))
            {
                IEnumerable<Student> students;
                parser.TryParse(out students);

                var student = students.First(s => s.GetFullName() == "Randy Strickland");
                var report = ProgressManager.GetStudentProgressReport(student);

                Assert.IsTrue(report.ProceedToNextGrade.GetValue());
                Assert.IsTrue(report.SubjectsStatuses[Subject.Mathematics].GetValue());
            }
        }
    }
}
