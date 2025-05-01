using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MAUIPos.Application.Services
{
    public class CacheSystemService
    {
        public string? GetCacheString(string key)
        {
            return Preferences.Get(key, string.Empty);
        }

        public int? GetCacheInt(string key)
        {
            return Preferences.Get(key, 0);
        }

        public bool GetCacheBool(string key)
        {
            return Preferences.Get(key, false);
        }

        public T? GetCacheObject<T>(string key)
        {
            var jsonData = Preferences.Get(key, string.Empty);
            var cacheData = JsonSerializer.Deserialize<T>(jsonData);
            return cacheData;
        }

        public void SetCache(string key, string value)
        {
            Preferences.Set(key, value);
        }

        public void SetCache(string key, int value)
        {
            Preferences.Set(key, value);
        }

        public void SetCache(string key, bool value)
        {
            Preferences.Set(key, value);
        }

        public void SetCache<T>(string key, T value)
        {
            var valueData = JsonSerializer.Serialize(value);
            Preferences.Set(key, valueData);
        }   
        
        public void SetCache(string key, object value)
        {
            var valueData = JsonSerializer.Serialize(value);
            Preferences.Set(key, valueData);
        }
        
        public void CacheClearAll()
        {
            Preferences.Clear();
        }
    }
}
