using AAA.Shared;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AAA.Core
{
    public class DirectoryManager
    {
        private string _BasePath { get; }
        private DirectoryInfo _BaseDir { get; }
        private string _GradesDir { get; set; }

        private DirectoryManager()
        {
            _BasePath = PathHelper.ResolvePath(AppDomain.CurrentDomain.BaseDirectory, PathHelper.ReversePath(backSteps: 4));
            _BaseDir = GetDirecoty(_BasePath);
            _GradesDir = $@"{Constants.MainFolder}\{Constants.GradesFolder}";
            _BaseDir.CreateSubdirectory(_GradesDir);
        }

        private DirectoryInfo GetDirecoty(string dirPath)
        {
            return Directory.Exists(dirPath) ? new DirectoryInfo(dirPath) : null;
        }

        private DirectoryInfo CreateDirectory(string grade, string subDirectory = null)
        {
            var subDir = string.IsNullOrEmpty(subDirectory) ? string.Empty : $@"\{subDirectory}";
            return _BaseDir.CreateSubdirectory($@"{_GradesDir}\{grade}{subDir}");
        }

        public void CreateDirectories(IEnumerable<Student> students)
        {
            var groupedStudents = students.GroupBy(s => s.Grade);

            foreach(var group in groupedStudents)
            {
                var studentsInGrade = group.ToArray();
                var grade = $"Grade {(int)group.Key}";

                CreateDirectory(grade);
                DirectoryInfo allStudentsDir = CreateDirectory(grade, Constants.AllStudentsFolder);
                DirectoryInfo topAchieverDir = CreateDirectory(grade, Constants.TopAchieverFolder);
                DirectoryInfo worstAchieverDir = CreateDirectory(grade, Constants.WorstAchieverFolder);

                var builder = new StudentsCSVBuilder();

                //All Students
                builder.SetOutputDirectory(allStudentsDir.FullName);
                builder.Build(studentsInGrade);

                //High peformer
                builder.SetOutputDirectory(topAchieverDir.FullName);
                builder.Build(ScoreManager.GetTopAchiever(studentsInGrade));

                //Worst performer
                builder.SetOutputDirectory(worstAchieverDir.FullName);
                builder.Build(ScoreManager.GetWorstAchiever(studentsInGrade));
            }
        }

        public void CleanDirectory(string dir)
        {
            Directory.Delete(PathHelper.ResolvePath(_BaseDir.FullName, dir), true);
        }

        private static DirectoryManager _DirectoryManager = null;
        private static object _LockObj = new object();
        public static DirectoryManager Instance
        {
            get
            {
                lock (_LockObj)
                {
                    return _DirectoryManager ?? (_DirectoryManager = new DirectoryManager());
                }
            }
        }
    }
}
