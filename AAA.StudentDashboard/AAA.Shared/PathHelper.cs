using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAA.Shared
{
    public static class PathHelper
    {
        public static string ReversePath(int backSteps)
        {
            var reverserArray = Enumerable.Repeat<string>(Constants.PathReverser, backSteps).ToArray();
            return String.Join(string.Empty, reverserArray);
        }

        public static string ResolvePath(params string[] paths)
        {
            return Path.GetFullPath(String.Join(Constants.PathJoiner, paths));
        }
    }
}
