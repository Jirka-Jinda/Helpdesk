namespace Helpdesk.Models.Cache.SessionCache
{
    public interface ISessionStorage
    {
        bool Get(Guid key, out object? data);
        bool Get<T>(Guid key, out T? data);
        bool Get(string key, out object? data);
        bool Get<T>(string key, out T? data);
        bool Put(Guid key, object data);
        bool Put(string key, object data);
        bool Delete(Guid key);
        bool Delete(string key);
        bool Restart();
    }
}
