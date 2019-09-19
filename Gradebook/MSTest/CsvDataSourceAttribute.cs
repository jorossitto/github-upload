using Microsoft.VisualStudio.TestTools.UnitTesting;
using Fundamentals;
using System.Text.RegularExpressions;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.IO;

namespace Fundamentals.Test
{
    public class CsvDataSourceAttribute : Attribute, ITestDataSource
    {
        public string FileName { get; }

        public CsvDataSourceAttribute(string fileName)
        {
            FileName = fileName;
        }

        public IEnumerable<object[]> GetData(MethodInfo methodInfo)
        {

            string[] csvLines = File.ReadAllLines(FileName);
            var testCases = new List<object[]>();
            foreach (var csvLine in csvLines)
            {
                IEnumerable<int> values = csvLine.Split(',').Select(int.Parse);

                object[] testCase = values.Cast<object>().ToArray();
                testCases.Add(testCase);
            }
            return testCases;
        }

        public string GetDisplayName(MethodInfo methodInfo, object[] data)
        {
            if (data == null)
            {
                return null;
            }

            return $"{methodInfo.Name} ({string.Join(",", data)})";
        }
    }
}
