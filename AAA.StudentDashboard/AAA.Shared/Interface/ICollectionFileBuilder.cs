using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AAA.Shared
{
    public interface ICollectionFileBuilder
    {
        void Build<T>(params T[] objects) where T : new();
    }
}
