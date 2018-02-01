using AAA.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AAA.Core
{
    public class Criteria: ICriteria
    {
        private Student _Student { get; }

        public Criteria(Student student)
        {
            _Student = student;
        }

        public StudentProgressReport Run()
        {
            return Run(new SubjectsCriterion(), new EnglishCriterion(), new MathematicsCriterion());
        }

        public StudentProgressReport Run(params ICriterion[] critera)
        {
            IDictionary<Subject, Status> subjectsStatuses = GetSubjectsStatuses();
            IDictionary<Subject, string> subjectsAlphaScores = GetSubjectsAlphaScores();

            IEnumerable<bool> results = critera.Select(criterion => criterion.Evaluate(subjectsStatuses));
            bool status = results.Any(r => r == false);
            Status overallStatus = status ? Status.Failed : Status.Passed;

            ScoreManager scoreManager = new ScoreManager(_Student);
            double average = scoreManager.GetAverageScore();
            Subject highestSubject = scoreManager.GetHighestScoreSubject();
            Subject lowestSubject = scoreManager.GetLowestScoreSubject();

            return new StudentProgressReport(
                overallStatus,
                average,
                FormatSubject(highestSubject),
                FormatSubject(lowestSubject),
                subjectsStatuses,
                subjectsAlphaScores);
        }

        private string FormatSubject(Subject subject)
        {
            return $"{subject} - {_Student.Scores[subject]}";
        }

        private IDictionary<Subject, Status> GetSubjectsStatuses()
        {
            var scoreManager = new ScoreManager(_Student);
            var subjectStatus = new Dictionary<Subject, Status>();

            foreach (var scoreKvp in _Student.Scores)
            {
                Status status = scoreManager.EvaluateSubject(scoreKvp.Key);
                subjectStatus.Add(scoreKvp.Key, status);
            }

            return subjectStatus;
        }

        private IDictionary<Subject, string> GetSubjectsAlphaScores()
        {
            var scoreManager = new ScoreManager(_Student);
            var subjectAlphaScore = new Dictionary<Subject, string>();

            foreach (var scoreKvp in _Student.Scores)
            {
                string alpha = scoreManager.GetAlphaScore(scoreKvp.Key);
                subjectAlphaScore.Add(scoreKvp.Key, alpha);
            }

            return subjectAlphaScore;
        }
    }
}
