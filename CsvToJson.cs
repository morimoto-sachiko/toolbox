using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace CsvToJson
{
    class Program
    {
        static void Main()
        {
            string csvPath = @"C:\Users\Public\Documents\table_export.csv";
            string jsonPath = @"C:\Users\Public\Documents\table_export.json";

            var rows = CsvLoader.Load(csvPath);

            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            string json = JsonSerializer.Serialize(rows, options);

            File.WriteAllText(jsonPath, json);

            Console.WriteLine("JSON Export Complete");
        }
    }

    public static class CsvLoader
    {
        public static List<Dictionary<string, string>> Load(string path)
        {
            var result = new List<Dictionary<string, string>>();

            var lines = File.ReadAllLines(path);

            if (lines.Length == 0)
                return result;

            var headers = lines[0].Split(',');

            for (int i = 1; i < lines.Length; i++)
            {
                var values = lines[i].Split(',');

                var row = new Dictionary<string, string>();

                for (int j = 0; j < headers.Length; j++)
                {
                    string value = j < values.Length ? values[j] : "";

                    row[headers[j]] = value;
                }

                result.Add(row);
            }

            return result;
        }
    }
}