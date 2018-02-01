using AAA.Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AAA.Core
{
    public class CSVBuilder : ICollectionFileBuilder
    {
        private string _OutputDir { get; }
        private StringBuilder _Builder { get; }
        private string _Delimiter { get; }
        private string _Name { get; }

        public CSVBuilder(string fileName, string outputDir, string delimiter = ",")
        {
            _Name = GetSanitizedName(fileName);
            _OutputDir = outputDir;
            _Delimiter = $"{ delimiter.Trim() } ";
            _Builder = new StringBuilder();
        }

        public void Build<T>(params T[] objects) where T : new()
        {
            Type objType = typeof(T);
            string[] headers = GetCSVHeaders<T>();
            var rowFields = new List<string>();

            AppendRow(headers);

            foreach (T obj in objects)
            {
                rowFields.Clear();
                foreach(string header in headers)
                {
                    var field = objType.GetProperty(header.TrimAll()).GetValue(obj, null).ToString();
                    rowFields.Add(field);
                }
                AppendRow(rowFields.ToArray());
            }

            WriteCSVFile();
        }

        private string GetSanitizedName(string fileName)
        {
            var nameWithoutExt = Path.HasExtension(fileName) ? Path.GetFileNameWithoutExtension(fileName) : fileName;
            return $"{nameWithoutExt.Trim()}.csv";
        }

        private void AppendRow(string[] rowColumns)
        {
            _Builder.AppendLine(String.Join(_Delimiter, rowColumns));
        }

        private void WriteCSVFile()
        {
            File.WriteAllText($@"{_OutputDir}\{_Name}", ToString(), Encoding.UTF8);
        }

        private string[] GetCSVHeaders<T>()
        {
            IEnumerable<string> properties = GetProperties<T>();

            return properties.Select(p => p.ToWords()).ToArray();
        }

        private static IEnumerable<string> GetProperties<T>()
        {
            Type objType = typeof(T);
            var properties = objType.GetProperties().Select(p => p.Name);
            return properties;
        }

        public override string ToString()
        {
            return _Builder.ToString();
        }
    }
}
