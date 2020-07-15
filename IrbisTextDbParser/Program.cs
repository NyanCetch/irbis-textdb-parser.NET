using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace IrbisTextDbParser
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            /*var file = @"C:\Users\NyanCetch\Downloads\GB.TXT";

            var sw = Stopwatch.StartNew();
            var lineCount = 0;
            foreach (var line in File.ReadLines(file))
                lineCount += 1;
            Console.WriteLine($"Lines: {lineCount}");
            Console.WriteLine($"Lines iteration elapsed: {sw.Elapsed.TotalSeconds} sec.");

            Parser.EnableDebug = true;
            Parser.RecordLimit = 10000;
            var parser = new Parser();
            
            sw.Restart();
            var recordCount = 0;
            var availableBookCount = 0;
            var uniqueBookCount = 0;
            var statusSet = new List<string>();
            var storageSet = new List<string>();
            foreach (var record in parser.Parse(file, new[] {"910"}))
            {
                var copies = record.Fields["910"];

                var statuses = copies.Select(f => f.SubFields["a"]).Where(s => "01".Contains(s)).ToList();
                var newStatuses = statuses.Except(statusSet);
                statusSet.AddRange(newStatuses);

                var storageItems = copies.Where(f => f.SubFields.ContainsKey("d")).Select(f => f.SubFields["d"]).ToList();
                foreach (var storage in storageItems)
                {
                    
                }
                var newStorageItems = storageItems.Except(storageSet);
                storageSet.AddRange(newStorageItems);
                
                uniqueBookCount += copies.Count;
                availableBookCount += copies.Count(f => f.SubFields["a"].Equals("0"));
                recordCount += 1;
            }
                
            Console.WriteLine($"Records: {recordCount}");
            Console.WriteLine($"Records parsing elapsed: {sw.Elapsed.TotalSeconds} sec.");
            
            Console.WriteLine($"Unique books: {uniqueBookCount}");
            Console.WriteLine($"Available books: {availableBookCount}");
            
            Console.WriteLine($"Statuses: [{string.Join(", ", statusSet)}]");
            Console.WriteLine($"Storage items: [{string.Join(", ", storageSet)}]");*/

            var raw =
                @"абонемент гб, абонемент цб, абонемент дб, м/о гб, чит. зал дб, чит. зал гб, чит. зал цб, к/х цб, чит.зал гб, к/х гб (ф.4), к/х гб, п/ф гб, чит.зал дб, чит.зал цб, абонемент дет.биб., чит.зал дет.биб., мет. отд.,
            опи цб, пр.от. цб, пр.от.цб, опи, аб цб, б/о гб, м/к гб, чит..зал гб, к\х гб, к/х гб (ф1), чз гб]";
            var items = raw.Split(',');

            var str = "чит..   зал /  цб";
            var regex = new Regex(@"\W+");
            var newStr = regex.Replace(str, " ");
            Console.WriteLine(newStr);

            items = items.Select(s => regex.Replace(s, " ")).ToArray();
            Console.WriteLine($"{string.Join(", ", items)}\n{items.Length}\n");
            
            regex = new Regex(@"\w+");
            items = items.Select(s => regex.Replace(s, (match) =>
            {
                var v = match.Value;
                if (v.Length <= 2)
                    return v;

                return v[0].ToString();
            })).Select(s => s.Replace(" ", "")).Distinct().ToArray();
            Console.WriteLine($"{string.Join(", ", items)}\n{items.Length}");
        }
    }
}