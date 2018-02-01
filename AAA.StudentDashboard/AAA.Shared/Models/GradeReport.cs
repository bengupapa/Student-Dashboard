using AAA.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AAA.Shared
{
    public class GradeReport
    {
        public int Grade { get; }
        public string BestSubject { get; }
        public string WorstSubject { get; }
        public string BestStudent { get; }
        public string WorstStudent { get; }
        public double AvgScore { get; }
        public string AvgScorePercentage { get; }

        public GradeReport(int grade,
            string bestSubject,
            string worstSubject,
            string bestStudent,
            string worstStudent,
            double avgScore)
        {
            Grade = grade;
            BestSubject = bestSubject.ToWords();
            WorstSubject = worstSubject.ToWords();
            BestStudent = bestStudent.ToWords();
            WorstStudent = worstStudent.ToWords();
            AvgScorePercentage = avgScore.ToString("P0");
            AvgScore = avgScore;
        }
    }
}
