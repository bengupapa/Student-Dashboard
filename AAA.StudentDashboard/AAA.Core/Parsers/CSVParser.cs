using AAA.Shared;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AAA.Core
{
    public sealed partial class CSVParser : ICollectionFileParser
    {
        private const string COMMA = ",";
        private TextFieldParser _TextParser { get; }
        private string _FilePath { get; }
        private string[] _ExclusionList { get; set; }

        public CSVParser(string filePath)
        {
            _TextParser = new TextFieldParser(filePath)
            {
                TextFieldType = FieldType.Delimited
            };

            _FilePath = filePath;
            _TextParser.SetDelimiters(COMMA);
            _ExclusionList = new string[] { };
        }

        public bool TryParse<T>(out IEnumerable<T> listOfT) where T : new()
        {
            try
            {
                if (!File.Exists(_FilePath))
                    throw new FileNotFoundException(nameof(_FilePath));

                string[] headerFields = null;
                Type objType = typeof(T);
                List<T> objList = new List<T>();

                while (!_TextParser.EndOfData)
                {
                    string[] fields = _TextParser.ReadFields();

                    if (_TextParser.LineNumber == 2)
                    {
                        if (!IsValidType<T>(typeof(T), fields, out headerFields))
                            throw new InvalidOperationException($"{typeof(T)} is not compatible with the csv file.");
                    }
                    else
                    {
                        T obj = CreateObj<T>(headerFields, objType, fields);
                        objList.Add(obj);
                    }
                }

                listOfT = objList;
                return true;
            }
            catch(Exception e)
            {
                //NB: Log to exception logger
                listOfT = null;
                return false;
            }
            finally
            {
                _TextParser.Close();
            }
        }

        private T CreateObj<T>(string[] headerFields, Type objType, string[] fields) where T : new()
        {
            T obj = new T();

            for (int i = 0; i < fields.Length; i++)
            {
                var prop = objType.GetProperty(headerFields[i]);

                if (prop != null && _ExclusionList.Contains(prop.Name, StringComparer.OrdinalIgnoreCase)) continue;

                Mapper.MapProperties(fields[i], obj, prop);
            }

            return obj;
        }

        private bool IsValidType<T>(Type type, string[] fields, out string[] headerFields) where T : new()
        {
            var transformedFields = fields.Select(field => field.TrimAll()).ToArray();
            var typeProperties = type.GetProperties()
                .Where(p => !_ExclusionList.Contains(p.Name))
                .Select(p => p.Name);
            var result = !typeProperties.Except(transformedFields, StringComparer.OrdinalIgnoreCase).Any();

            headerFields = result ? transformedFields : null;

            return result;
        }

        public void Dispose()
        {
            _TextParser.Dispose();
        }

        public void SetExcludedFields(params string[] excludedFields)
        {
            _ExclusionList = excludedFields;
        }
    }
}
