using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AAA.Shared;

namespace AAA.Processor
{
    public class Validation
    {
        public string ValidationMessage { get; private set; }

        private Dictionary<string, Func<string, string>> validations = new Dictionary<string, Func<string, string>>
        {
            { "IsClassLeader", ClassLeaderValidator },
            { "Grade", GradeValidator },
            { "Score", ScoreValidator }
        };

        public bool Validate(string input, string property)
        {
            Func<string, string> validate = GetValidator(property);
            ValidationMessage = validate(input);

            return input == ValidationMessage;
        }

        private Func<string, string> GetValidator(string property)
        {
            Func<string, string> function;
            if (!validations.TryGetValue(property, out function))
                return DefaultValidator;

            return function;
        }

        #region Validators

        private static string DefaultValidator(string input)
        {
            return input;
        }

        private static string ClassLeaderValidator(string input)
        {
            var isValid = input.ToLower().In(new[] { "yes", "y", "no", "n" });
            return isValid ? input : "Is Class Leader (yes/no): ";
        }

        private static string GradeValidator(string input)
        {
            var message = "Grade (8-12): ";

            int num;
            if (int.TryParse(input, out num))
            {
                var isValid = num.In(Enumerable.Range(8, 5));
                return isValid ? input : message;
            }

            return message;
        }

        private static string ScoreValidator(string input)
        {
            var message = "Score (0-100): ";

            int num;
            if (int.TryParse(input, out num))
            {
                var isValid = num >= 0 && num <= 100;
                return isValid ? input : message;
            }

            return message;
        } 

        #endregion
    }
}
