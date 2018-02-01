using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static System.Console;

namespace AAA.Processor
{
    public static class DisplayHelper
    {
        public static string Underline
        {
            get
            {
                return "-----------------------";
            }
        }

        public static void UnderlineValue(string value)
        {
            WriteLine($"{value}\n{Underline}");
        }

        public static void Wrap(string value)
        {
            WriteLine($"{Underline}\n{value}\n{Underline}");
        }
    }
}
