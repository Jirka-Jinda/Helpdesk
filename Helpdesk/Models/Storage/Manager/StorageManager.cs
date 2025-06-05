using Helpdesk.Models.Cookies;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Helpdesk.Models.Storage.Manager;

/// <summary>
/// Implementation of IStorageManager, methods overview in interface definition.
/// </summary>
public class StorageManager : IStorageManager
{
    private readonly IScopeStorage ScopeStorage;
    private readonly ISessionStorage SessionStorage;

    public StorageManager()
    {
        throw new Exception("Session or scope storage not aviable, functionality will not be complete");
    }

    public StorageManager(IScopeStorage scopeStorage, ISessionStorage sessionStorage)
    {
        ScopeStorage = scopeStorage;
        SessionStorage = sessionStorage;
    }

    public bool GetScoped<T>(out T? data)
    {
        return ScopeStorage.Get<T>(out data);
    }
    public bool GetScopedKeyed<T>(string key, out T? data)
    {
        return ScopeStorage.GetKeyed(key, out data);
    }
    public bool GetSessioned<T>(out T? data)
    {
        ScopeStorage.Get(out IUserSettings? settings);
        if (settings != null)
            return SessionStorage.Get(settings.UserId, out data);
        else
        {
            data = default;
            return false;
        }
    }

    public bool PutScoped<T>(T data)
    {
        return ScopeStorage.Put(data);
    }
    public bool PutScopedKeyed<T>(string key, T data)
    {
        return ScopeStorage.PutKeyed(key, data);
    }
    public bool PutSessioned<T>(T data)
    {
        ScopeStorage.Get(out IUserSettings? settings);
        if (settings != null)
            return SessionStorage.Put(settings.UserId, data!);
        else return false;
    }

    public bool DeleteSessioned<T>(T data)
    {
        ScopeStorage.Get(out IUserSettings? settings);
        if (settings != null)
            return SessionStorage.Delete(settings.UserId);
        else return false;
    }

    public bool Restart()
    {
        return ScopeStorage.Restart() && SessionStorage.Restart();
    }
}
