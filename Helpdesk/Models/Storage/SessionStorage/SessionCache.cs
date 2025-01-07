namespace Helpdesk.Models.Cache.SessionCache
{
    public class SessionCache : ISessionStorage
    {
        private MemoryCache sessionCache { get; init; }
        private MemoryCacheEntryOptions cacheOptions { get; init; }

        public SessionCache(IApplicationSettings settings)
        {
            // IApplication Settings
            sessionCache = new MemoryCache(settings.SessionMemoryCacheOptions);
            cacheOptions = settings.SessionMemoryCacheEntryOptions;
        }

        public SessionCache()
        {
            // Default Settings
            sessionCache = new MemoryCache(new MemoryCacheOptions());
            cacheOptions = new MemoryCacheEntryOptions();
        }

        public bool Get(Guid key, out object? data)
        {
            return Get(key.ToString(), out data);
        }

        public bool Get<T>(Guid key, out T? data)
        {
            return Get<T>(key.ToString(), out data);
        }

        public bool Get(string key, out object? data)
        {
            return sessionCache.TryGetValue(key, out data);
        }

        public bool Get<T>(string key, out T? data)
        {
            bool fetched;
            if (fetched = sessionCache.TryGetValue(key, out object? temp))
            {
                try
                {
                    data = (T?)temp;
                    return fetched;
                }
                catch (InvalidCastException)
                {
                    data = default;
                    return false;
                }
            }
            else
            {
                data = default;
                return fetched;
            }
        }

        public bool Put(Guid key, object data)
        {
            return Put(key.ToString(), data);
        }

        public bool Put(string key, object data)
        {
            try
            {
                sessionCache.CreateEntry(key);
                sessionCache.Set(key, data, cacheOptions);
                return true;
            }
            catch { return false; }
        }

        public bool Delete(Guid key) 
        {
            return Delete(key.ToString());
        }

        public bool Delete(string key) 
        {
            try
            {
                sessionCache.Remove(key);
                return true;
            }
            catch { return false; }
        }

        public bool Restart()
        {
            try
            {
                sessionCache.Clear();
                return true;
            }
            catch { return false; }
        }
    }
}
