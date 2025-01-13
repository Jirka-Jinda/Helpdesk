using Helpdesk.Models.Storage.Manager;

namespace Helpdesk.Models.Cache.ScopeCache
{
    public class ScopeCache : IScopeStorage
    {
        private MemoryCache _scopeCache { get; set; }
        private MemoryCacheEntryOptions _cacheEntryOptions { get; set; }

        public ScopeCache(StorageOptions options)
        {
            // IApplicationSettings values
            _scopeCache = new MemoryCache(options.ScopeMemoryCacheOptions);
            _cacheEntryOptions = options.ScopeMemoryCacheEntryOptions;
        }

        public bool Get<T>(out T? data)
        {
            return GetKeyed("", out data);
        }
        public bool GetKeyed<T>(string key, out T? data)
        {
            string objectKey = key + GetObjectKey<T>();
            
            if (_scopeCache.TryGetValue(objectKey, out object? res) && res != null)
            {
                try
                {
                    data = (T)res;
                    return true;
                }
                catch (InvalidCastException) { data = default; return false; }
            }
            else
            {
                data = default;
                return false;
            }
        }
        public bool Put<T>(T data)
        {
            return PutKeyed("", data);
        }
        public bool PutKeyed<T>(string key, T data)
        {
            try
            {
                string objectKey = key + GetObjectKey<T>();
                _scopeCache.CreateEntry(objectKey);
                _scopeCache.Set(objectKey, data, _cacheEntryOptions);
                return true;
            }
            catch { return false; }
        }

        private static string GetObjectKey<T>()
        {
            return typeof(T).Name;
        }

        public bool Restart()
        {
            try
            {
                _scopeCache.Clear();
                return true;
            }
            catch { return false; }
        }
    }
}
