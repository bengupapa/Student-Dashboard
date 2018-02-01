using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AAA.Shared
{
    public interface IStudentRepository
    {
        Student GetStudentByName(string fullName);
        IEnumerable<Student> GetStudentsByGrade(Grade grade);
    }
}
