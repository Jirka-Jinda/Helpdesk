namespace Helpdesk.Models.Cache.ScopeCache
{
    public interface IScopeStorage
    {
        bool Get<T>(out T? data);
        bool GetKeyed<T>(string key, out T? data);
        bool Put<T>(T data);
        bool PutKeyed<T>(string key, T data);
        bool Restart();
    }
}
