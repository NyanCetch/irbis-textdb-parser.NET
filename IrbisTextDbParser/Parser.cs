using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace IrbisTextDbParser
{
    public class Parser
    {
        public static bool EnableDebug { get; set; }
        public static int RecordLimit { get; set; } = 1000;
        
        public IEnumerable<BookRecord> Parse(string filePath, string[] filters = null)
        {
            var fields = new List<Field>();
            var recordCount = 0;
            foreach (var line in File.ReadLines(filePath))
            {
                if (EnableDebug && recordCount >= RecordLimit)
                    break;

                var clearLine = line.ToLower().TrimEnd('\r', '\n');
                
                if (line.Equals(Constants.RECORD_SEPARATOR))
                {
                    if (fields.Count > 0)
                    {
                        recordCount += 1;
                        
                        var fieldGroups = fields.GroupBy(f => f.Name)
                            .ToDictionary(g => g.Key, g => g.Select(f => f).ToList());
                        var newRecord = new BookRecord{Fields = fieldGroups};
                        fields = new List<Field>();
                        
                        yield return newRecord;
                    }

                    continue;
                }

                var parts = clearLine.Split(Constants.FIELD_VALUE_SEPARATOR);
                var name = parts[0].TrimStart(Constants.FIELD_SEPARATOR);
                var value = parts[1].Trim();
                
                if (filters != null && !filters.Contains(name))
                    continue;
                
                var newField = new Field(name, value);
                fields.Add(newField);
            }
        }
    }
}