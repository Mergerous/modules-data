using System;

namespace Modules.Data
{
    public sealed class DataHandle
    {
        private event Action<string, object> onSaved; 
        private readonly string key;
        
        private object data;

        public DataHandle(string key, object data, Action<string, object> onSaved)
        {
            this.key = key;
            this.data = data;
            this.onSaved = onSaved;
        }

        public void Save()
        {
            onSaved?.Invoke(key, data);
        }
        
        public T GetData<T>(T fallback = default) where T : class => (data ??= fallback) as T;
    }
}