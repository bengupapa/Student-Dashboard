using AAA.DataAccess;
using AAA.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AAA.Core
{
    public static class ProgressManager
    {
        public static StudentProgressReport GetStudentProgressReport(Student student)
        {
            ICriteria criteria = new Criteria(student);

            return criteria.Run();
        }

        public static SchoolProgressReport GetSchoolProgressReport()
        {
            IEnumerable<Student> students = new StudentRepository().GetAll();
            var groupedStudents = students.GroupBy(s => s.Grade);
            var schoolPogressReport = new SchoolProgressReport();

            foreach (var group in groupedStudents)
            {
                Grade grade = group.Key;
                string bestSubject = ScoreManager.GetHighestScoreSubject(group.ToArray(), grade).ToString();
                string worstSubject = ScoreManager.GetLowestScoreSubject(group.ToArray(), grade).ToString();
                string bestStudent = ScoreManager.GetTopAchiever(group.ToArray()).GetFullName();
                string worstStudent = ScoreManager.GetWorstAchiever(group.ToArray()).GetFullName();
                double averageScore = Math.Round(ScoreManager.GetAverageScore(group.ToArray(), grade: grade))/100;

                var report = new GradeReport((int)grade, bestSubject, worstSubject, bestStudent, worstStudent, averageScore);
                schoolPogressReport.AddGradeReport(report);
            }

            return schoolPogressReport;
        }
    } 
}
