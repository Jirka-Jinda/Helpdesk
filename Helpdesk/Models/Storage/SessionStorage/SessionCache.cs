using Helpdesk.Models.Storage.Manager;

namespace Helpdesk.Models.Cache.SessionCache
{
    public class SessionCache : ISessionStorage
    {
        private MemoryCache _sessionCache { get; init; }
        private MemoryCacheEntryOptions _cacheOptions { get; init; }

        public SessionCache(StorageOptions options)
        {
            // IApplication Settings
            _sessionCache = new MemoryCache(options.SessionMemoryCacheOptions);
            _cacheOptions = options.SessionMemoryCacheEntryOptions;
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
            return _sessionCache.TryGetValue(key, out data);
        }

        public bool Get<T>(string key, out T? data)
        {
            bool fetched;
            if (fetched = _sessionCache.TryGetValue(key, out object? temp))
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
                _sessionCache.CreateEntry(key);
                _sessionCache.Set(key, data, _cacheOptions);
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
                _sessionCache.Remove(key);
                return true;
            }
            catch { return false; }
        }

        public bool Restart()
        {
            try
            {
                _sessionCache.Clear();
                return true;
            }
            catch { return false; }
        }
    }
}
