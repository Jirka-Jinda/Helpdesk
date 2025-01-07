namespace Helpdesk.Models.Cache.ScopeCache
{
    public class ScopeCache : IScopeStorage
    {
        private MemoryCache scopeCache { get; set; }
        private MemoryCacheEntryOptions cacheEntryOptions { get; set; }

        public ScopeCache(IApplicationSettings settings)
        {
            // IApplicationSettings values
            scopeCache = new MemoryCache(settings.ScopeMemoryCacheOptions);
            cacheEntryOptions = settings.ScopeMemoryCacheEntryOptions;
        }
        public ScopeCache()
        {
            // Default values
            scopeCache = new MemoryCache(new MemoryCacheOptions() 
            {

            });
            cacheEntryOptions = new MemoryCacheEntryOptions() 
            {

            };         
        }

        public bool Get<T>(out T? data)
        {
            return GetKeyed("", out data);
        }
        public bool GetKeyed<T>(string key, out T? data)
        {
            string objectKey = key + GetObjectKey<T>();
            
            if (scopeCache.TryGetValue(objectKey, out object? res) && res != null)
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
                scopeCache.CreateEntry(objectKey);
                scopeCache.Set(objectKey, data, cacheEntryOptions);
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
                scopeCache.Clear();
                return true;
            }
            catch { return false; }
        }
    }
}
