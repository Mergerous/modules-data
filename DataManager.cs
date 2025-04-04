using System;
using System.Text;
using JetBrains.Annotations;
using Newtonsoft.Json;
using UnityEngine;

namespace Modules.Data
{
    [UsedImplicitly]
    public sealed class DataManager
    {
        private readonly DataSettings dataSettings;

        public DataManager(DataSettings dataSettings)
        {
            this.dataSettings = dataSettings;
        }

        public void Save(string key, object data)
        {
            string json = default;

            switch (dataSettings.JsonType)
            {
                case JsonType.Newtonsoft:
                    json = JsonConvert.SerializeObject(data);
                    break;
                case JsonType.Unity:
                    json = JsonUtility.ToJson(data);
                    break;
            }
          
            if (dataSettings.ConvertToByteCode)
            {
                byte[] bytes = Encoding.Unicode.GetBytes(json);
                PlayerPrefs.SetString(key, Convert.ToBase64String(bytes));
                return;
            }

            if (dataSettings.LogSaveEvent)
            {
                Debug.Log($"SAVE {json}");
            }

            PlayerPrefs.SetString(key, json);
        }

        public void Clear()
        {
            PlayerPrefs.DeleteAll();
        }
        
        public T Load<T>(string key, T def = default) => TryLoad(key, out T data) ? data : def;

        public object Load(string key, Type type, object def) => TryLoad(key, type, out object data) ? data : def;

        public bool TryLoad<T>(string key, out T data)
        {
            if (TryLoad(key, typeof(T), out object objectData))
            {
                data = (T)objectData;
                return true;
            }

            data = default;
            return false;
        }

        public bool TryLoad(string key, Type type, out object data)
        {
            data = default;
            if (PlayerPrefs.HasKey(key))
            {
                string json;
                if (dataSettings.ConvertToByteCode)
                {
                    try
                    {
                        byte[] bytes = Convert.FromBase64String(PlayerPrefs.GetString(key));
                        json = Encoding.Unicode.GetString(bytes);
                    }
                    catch
                    {
                        return false;
                    }
                }
                else
                {
                    json = PlayerPrefs.GetString(key);
                }
                
                switch (dataSettings.JsonType)
                {
                    case JsonType.Newtonsoft:
                        data = JsonConvert.DeserializeObject(json, type);
                        break;
                    case JsonType.Unity:
                        data = JsonUtility.FromJson(json, type);
                        break;
                }
                
                if (dataSettings.LogSaveEvent)
                {
                    Debug.Log(json);
                }
                return true;
            }

            return false;
        }
    }
}
