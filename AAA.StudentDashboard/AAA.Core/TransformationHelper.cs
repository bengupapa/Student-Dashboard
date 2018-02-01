using AAA.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static System.Console;

namespace AAA.Core
{
    public static class TransformationHelper
    {
        public static IEnumerable<Student> Transform(IEnumerable<SerializableStudent> serialStudents)
        {
            var students = new List<Student>();
            var groups = serialStudents
                .GroupBy(s => s.GetFullName())
                .ToList();

            foreach (var group in groups)
            {
                var std = group.First();
                var student = new Student
                {
                    Name = std.Name,
                    Surname = std.Surname,
                    IsClassLeader = std.IsClassLeader,
                    Scores = new Dictionary<Subject, double>(),
                    Grade = (Grade)Enum.Parse(typeof(Grade), std.Grade)
                };

                foreach (var gr in group.Where(s => !String.IsNullOrEmpty(s.GetFullName())))
                {
                    double score;
                    double.TryParse(gr.Score, out score);
                    student.Scores.Add(gr.Subject, Math.Round(ScoreManager.AdjustScore(student, score)));
                }

                students.Add(student);
            }

            return students;
        }
        public static IEnumerable<SerializableStudent> Deform(IEnumerable<Student> students)
        {
            var serializableStudents = new List<SerializableStudent>();

            foreach (Student student in students)
            {
                StudentProgressReport report = ProgressManager.GetStudentProgressReport(student);

                foreach (var subjectKvp in student.Scores)
                {
                    Subject subject = subjectKvp.Key;
                    var serialStudent = new SerializableStudent();

                    serialStudent.Name = student.Name;
                    serialStudent.Surname = student.Surname;
                    serialStudent.IsClassLeader = GetFriendlyType(student.IsClassLeader);
                    serialStudent.Subject = subject;
                    serialStudent.Score = subjectKvp.Value.ToString("N0");
                    serialStudent.Grade = ((int)student.Grade).ToString();
                    serialStudent.AlphaScore = report.SubjectsAlphaScores[subject];
                    serialStudent.SubjectStatus = report.SubjectsStatuses[subject];
                    serialStudent.AverageScore = report.AverageScore.ToString("N0");
                    serialStudent.HighestScoreSubject = report.HighestScoreSubject;
                    serialStudent.LowestScoreSubject = report.LowestScoreSubject;
                    serialStudent.OverallStatus = report.OverallStatus;

                    serializableStudents.Add(serialStudent);
                }
            }

            return serializableStudents;
        }

        private static BooleanType GetFriendlyType(BooleanType type)
        {
            if (type == BooleanType.Y) return BooleanType.Yes;
            else if (type == BooleanType.N) return BooleanType.No;
            else return type;
        }
    }
}
