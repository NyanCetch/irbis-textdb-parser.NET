using System;
using System.Collections.Generic;

namespace IrbisTextDbParser
{
    public class Field
    {
        public string Name { get; }
        public string Value { get; }

        private Dictionary<string, string> _subFields;
        public Dictionary<string, string> SubFields
        {
            get
            {
                if (_subFields == null)
                {
                    _subFields = new Dictionary<string, string>();
                    ExtractSubFields();
                }

                return _subFields;
            }
        }

        public Field(string name, string value)
        {
            Name = name;
            Value = value;
        }
        
        
        private void ExtractSubFields()
        {
            if (Value.IndexOf(Constants.SUB_FIELD_SEPARATOR) < 0)
                return;

            var parts = Value.Split(new []{Constants.SUB_FIELD_SEPARATOR}, StringSplitOptions.RemoveEmptyEntries);
            foreach (var rawSubField in parts)
            {
                var key = rawSubField.Substring(0, 1);
                var value = rawSubField.Substring(1).Trim();
                _subFields[key] = value;
            }
        }
    }
}