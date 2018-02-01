using AAA.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.IO;

namespace AAA.DataAccess
{
    public class StudentRepository : IStudentRepository, IRepository<Student>
    {
        private IList<Student> _Students { get; }
        private string _DataFilePath { get; }

        public StudentRepository()
        {
            _DataFilePath = PathHelper.ResolvePath(
                AppDomain.CurrentDomain.BaseDirectory.Replace(@"\bin\Debug\",""), 
                PathHelper.ReversePath(backSteps: 1), @"Data\Students.json");

            IList<Student> students;
            _Students = TryReadData(out students) ? students : new List<Student>();
        }

        public Student GetStudentByName(string fullName)
        {
            return _Students.FirstOrDefault(s => 
            s.GetFullName().Equals(fullName, StringComparison.InvariantCultureIgnoreCase));
        }

        public IEnumerable<Student> GetStudentsByGrade(Grade grade)
        {
            return _Students.Where(s => s.Grade == grade);
        }

        public void AddRange(IEnumerable<Student> objs)
        {
            objs.ToList().ForEach(_Students.Add);
            WriteData();
        }

        public int Count()
        {
            return _Students.Count;
        }

        public IEnumerable<Student> GetAll()
        {
            return _Students;
        }

        public void Clear()
        {
            _Students.Clear();
        }

        private void WriteData()
        {
            var studentData = JsonConvert.SerializeObject(_Students, Formatting.Indented);
            File.WriteAllText(_DataFilePath, studentData);
        }

        private bool TryReadData(out IList<Student> students)
        {
            try
            {
                var file = File.ReadAllText(_DataFilePath).Replace("\r\n", string.Empty);
                students = JsonConvert.DeserializeObject<IList<Student>>(file);
                return true;
            }
            catch(Exception e)
            {
                students = null;
                return false;
            }
        }
    }
}
