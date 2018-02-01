using AAA.Core;
using AAA.DataAccess;
using AAA.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using static System.Console;

namespace AAA.Processor
{
    public static class StudentHandler
    {
        private static IDictionary<string, string> _FieldAnswers = new Dictionary<string, string>();
        private static IDictionary<string, string> _SubjectAnswers = new Dictionary<string, string>();
        private static ICollection<SerializableStudent> _Students = new List<SerializableStudent>();

        public static void AddStudent(string filePath)
        {
            Start:
            if (_Students.Any())
                WriteLine($"You have add {_Students.Count / Subjects.Count } student(s)");

            WriteLine(DisplayHelper.Underline);
            Write("Add additional student? Y/N: ");

            bool addMore = ReadLine().ToUpper() == "Y";

            while (addMore)
            {
                AddStudentDetails();

                DisplayHelper.Wrap("Enter subject scores");

                AddStudentSubjects();

                CreateSerializableStudent();

                Clear();
                goto Start;
            }

            if(!addMore && _Students.Any())
            {
                var studentRepository = new StudentRepository();
                var students = TransformationHelper.Transform(_Students);
               
                studentRepository.AddRange(students);                

                WriteLine($"\rProcessed {_Students.Count/6} more student(s).");
                WriteLine($"\rTotal : {studentRepository.Count()} student(s).");
                WriteLine("Completed.");
                _Students.Clear();
            }
        }

        private static void AddStudentSubjects()
        {
            _SubjectAnswers.Clear();

            Subjects.ForEach(subject =>
            {
                Write("{0} : ", subject);

                var validation = new Validation();

                startA:
                var input = ReadLine();
                bool isValid = validation.Validate(input, "Score");
                input = isValid ? input : validation.ValidationMessage;

                if (!isValid)
                {
                    Write($"\r{input}");
                    goto startA;
                }

                _SubjectAnswers.Add(subject, input);
            });
        }

        private static void AddStudentDetails()
        {
            WriteLine(DisplayHelper.Underline);

            _FieldAnswers.Clear();

            var validation = new Validation();

            StudentProperties.Except(ExclusionList).ToList().ForEach(property =>
            {
                Write("{0} : ", property.ToWords());

                startB:
                var input = ReadLine();
                bool isValid = validation.Validate(input, property);
                input = isValid ? input : validation.ValidationMessage;

                if(!isValid)
                {
                    Write($"\r{input}");
                    goto startB;
                }

                _FieldAnswers.Add(property, input.ToTitleCase());
            });
        }

        private static List<string> StudentProperties
        {
            get
            {
                Type type = typeof(SerializableStudent);
                return type.GetProperties().Select(p => p.Name).ToList();
            }
        }

        private static List<string> ExclusionList
        {
            get
            {
                return new List<string>
                {
                    "Subject",
                    "Score",
                    "AlphaScore",
                    "AverageScore",
                    "SubjectStatus",
                    "HighestScoreSubject",
                    "LowestScoreSubject",
                    "OverallStatus",
                };
            }
        }

        private static List<string> Subjects
        {
            get
            {
                return Enum.GetNames(typeof(Subject)).ToList();
            }
        }

        private static void CreateSerializableStudent()
        {
            var objType = typeof(SerializableStudent);

            foreach(var subjectKvp in _SubjectAnswers)
            {
                var serialStudent = new SerializableStudent();

                foreach (var kvp in _FieldAnswers)
                {
                    var fieldPropInfo = objType.GetProperty(kvp.Key);
                    Mapper.MapProperties<SerializableStudent>(kvp.Value, serialStudent, fieldPropInfo);
                };

                var subjectPropInfo = objType.GetProperty("Subject");
                Mapper.MapProperties<SerializableStudent>(subjectKvp.Key, serialStudent, subjectPropInfo);

                var scorePropInfo = objType.GetProperty("Score");
                Mapper.MapProperties<SerializableStudent>(subjectKvp.Value, serialStudent, scorePropInfo);

                _Students.Add(serialStudent);
            }

            _FieldAnswers.Clear();
            _SubjectAnswers.Clear();
        }
    }
}
