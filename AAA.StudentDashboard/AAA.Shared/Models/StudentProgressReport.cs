using AAA.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AAA.Shared
{
    public class StudentProgressReport
    {
        public IDictionary<Subject, Status> SubjectsStatuses { get; }
        public IDictionary<Subject, string> SubjectsAlphaScores { get; }
        public BooleanType ProceedToNextGrade { get; }
        public Status OverallStatus { get; }
        public double AverageScore { get; }
        public string HighestScoreSubject { get; }
        public string LowestScoreSubject { get; }

        public StudentProgressReport(Status overallStatus,
            double averageScore,
            string highestScoreSubject,
            string lowestScoreSubject,
            IDictionary<Subject, Status> subjectsStatuses,
            IDictionary<Subject, string> subjectsAlphaScores)
        {
            SubjectsAlphaScores = subjectsAlphaScores;
            SubjectsStatuses = subjectsStatuses;
            OverallStatus = overallStatus;
            AverageScore = averageScore;
            HighestScoreSubject = highestScoreSubject;
            LowestScoreSubject = lowestScoreSubject;
            ProceedToNextGrade = overallStatus.GetValue() ? BooleanType.Yes : BooleanType.No;
        }
    }
}
