using AAA.Core;
using AAA.DataAccess;
using AAA.Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using static System.Console;

namespace AAA.Processor
{
    public static class FileProcessor
    {
        public static string GetFilePath(string[] args)
        {
            string filePath = String.Empty;

            if (args.Any())
            {
                filePath = args[0];
            }
            else
            {
                WriteLine("Please enter file path: ");
                string input = ReadLine();

                filePath = Regex.Replace(input, "\"", "");
            }

            return filePath;
        }

        public static void Process(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException(nameof(filePath));

            Write("Initializing...");

            using (var parser = new StudentsCSVParser(filePath))
            {
                IEnumerable<Student> students;
                if (!parser.TryParse(out students))
                {
                    WriteLine("\rFailed to process file");
                    return;
                }

                var studentRepository = new StudentRepository();
                studentRepository.Clear();
                studentRepository.AddRange(students);

                WriteLine($"\rProcessed {studentRepository.Count()} rows.");
                WriteLine("Completed.");
            }
        }
    }
}
