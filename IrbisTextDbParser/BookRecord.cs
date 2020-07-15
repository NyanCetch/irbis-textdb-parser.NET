using System.Collections.Generic;

namespace IrbisTextDbParser
{
    public class BookRecord
    {
        public Dictionary<string, List<Field>> Fields { get; set; }
    }
}