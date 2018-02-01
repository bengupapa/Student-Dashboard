using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AAA.Shared
{
    public interface ICollectionFileParser : IDisposable
    {
        void SetExcludedFields(params string[] excludedFields);
        bool TryParse<T>(out IEnumerable<T> listOfT) where T : new();
    }
}
