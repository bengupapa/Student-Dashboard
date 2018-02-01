using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AAA.Shared
{
    public interface IRepository<T>
    {
        void AddRange(IEnumerable<T> objs);
        int Count();
        IEnumerable<T> GetAll();
        void Clear();
    }
}
