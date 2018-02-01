using AAA.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AAA.Core
{
    public class ScoreManager
    {
        private IEnumerable<ScoreRange> _Ranges = null;
        private Student _Student { get; }

        public ScoreManager(Student student)
        {
            _Student = student;
        }

        public string GetAlphaScore(Subject subject)
        {
            var score = _Student.Scores[subject];

            IEnumerable<ScoreRange> ranges = GetScoreRanges();
            ScoreRange range = ranges.FirstOrDefault(r => r.Contains(score));

            if (range == null)
                throw new ArgumentOutOfRangeException(nameof(score));

            return range.ToString();
        }

        public Status EvaluateSubject(Subject subject)
        {
            double score = _Student.Scores[subject];
            return score >= Constants.PassRate ? Status.Passed : Status.Failed;
        }

        public double GetAverageScore()
        {
            return GetAverageScore(_Student);
        }

        public Subject GetHighestScoreSubject()
        {
            return GetHighestScoreSubject(new[] { _Student });
        }

        public Subject GetLowestScoreSubject()
        {
            return GetLowestScoreSubject(new[] { _Student });
        }

        #region Report Methods
        public static Student GetTopAchiever(Student[] students)
        {
            var studentsAverages = new Dictionary<Student, double>();
            foreach (var student in students)
                studentsAverages.Add(student, GetAverageScore(student));

            double maxAvgScore = studentsAverages.Max(kvp => kvp.Value);
            return studentsAverages.FirstOrDefault(kvp => kvp.Value == maxAvgScore).Key;
        }

        public static Student GetWorstAchiever(Student[] students)
        {
            var studentsAverages = new Dictionary<Student, double>();
            foreach (var student in students)
                studentsAverages.Add(student, GetAverageScore(student));

            double minAvgScore = studentsAverages.Min(kvp => kvp.Value);
            return studentsAverages.FirstOrDefault(kvp => kvp.Value == minAvgScore).Key;
        } 

        public static Subject GetHighestScoreSubject(Student[] students, Grade? grade = null)
        {
            var scores = !grade.HasValue ?
                students.SelectMany(s => s.Scores) :
                students.Where(s => s.Grade == grade).SelectMany(s => s.Scores);
            var maxScore = scores.Max(kvp => kvp.Value);
            return scores.FirstOrDefault(kvp => kvp.Value == maxScore).Key;
        }

        public static Subject GetLowestScoreSubject(Student[] students, Grade? grade = null)
        {
            var scores = !grade.HasValue ?
                students.SelectMany(s => s.Scores) :
                students.Where(s => s.Grade == grade).SelectMany(s => s.Scores);
            var minScore = scores.Min(kvp => kvp.Value);
            return scores.LastOrDefault(kvp => kvp.Value == minScore).Key;
        }

        public static double GetAverageScore(Student student)
        {
            return GetAverageScore(new[] { student });
        }

        public static double GetAverageScore(Student[] students, Grade? grade = null, Subject? subject = null)
        {
            var scores = !grade.HasValue ?
                students.SelectMany(s => s.Scores) :
                students.Where(s => s.Grade == grade).SelectMany(s => s.Scores);

            var avgScore = !subject.HasValue ?
                scores.Select(kvp => kvp.Value).Average() :
                scores.Where(kvp => kvp.Key == subject).Select(kvp => kvp.Value).Average();

            return Math.Round(avgScore);
        }
        #endregion

        #region Helper Methods
        private IEnumerable<ScoreRange> GetScoreRanges()
        {
            return _Ranges ?? (_Ranges = InitialRanges());
        }

        private IEnumerable<ScoreRange> InitialRanges()
        {
            return new List<ScoreRange>
            {
                new ScoreRange("A", 90, 100),
                new ScoreRange("B", 80, 89),
                new ScoreRange("C", 70, 79),
                new ScoreRange("D", 60, 69),
                new ScoreRange("E", 50, 59),
                new ScoreRange("F", 40, 49),
                new ScoreRange("G", 30, 39),
                new ScoreRange("H", 20, 29),
                new ScoreRange("I", 10, 19),
                new ScoreRange("J", 0, 9)
            };
        } 

        public static double AdjustScore(Student student, double score)
        {
            if (!student.IsClassLeader.GetValue())
                return score;

            score = score * Constants.ClassLeaderAdjustment;
            return score > Constants.MaxScore ? Constants.MaxScore : Math.Round(score);
        }
        #endregion
    }
}
