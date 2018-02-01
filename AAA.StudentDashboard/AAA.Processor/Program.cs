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
    public class Program
    {
        public static void Main(string[] args)
        {
            string filePath = FileProcessor.GetFilePath(args);

            Process(filePath);

            StudentHandler.AddStudent(filePath);

            Process(filePath, addDirectoriesOnly: true);

            Write("Press ENTER to exit.");
            Read();
        }

        private static void Process(string filePath, bool addDirectoriesOnly = false)
        {
            if (!addDirectoriesOnly)
                FileProcessor.Process(filePath);

            DirectoryManager.Instance.CleanDirectory(Constants.MainFolder);
            DirectoryManager.Instance.CreateDirectories(new StudentRepository().GetAll());
        }
    }
}
