using System;

namespace Modules.Data
{
    public sealed class DataAttribute : Attribute
    {
        public string Key { get; }
        public Type Type { get; }

        public DataAttribute(string key, Type type)
        {
            Key = key;
            Type = type;
        }
    }
}
