namespace Helpdesk.Models.Storage.StorageManager
{
    /// <summary>
    /// Methods to operate session and scoped internal application storage.
    /// Encapsulation over cache implementation, use these methods only or Sectumsempra(prog).
    /// </summary>
    public interface IStorageManager
    {
        /// <summary>
        /// Get data from scope storage with same type as out parameter.
        /// Must be unique type, else use keyed method version.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns>True if found, out stored object; else false</returns>
        bool GetScoped<T>(out T? data);

        /// <summary>
        /// Get data from scope storage with same type as out parameter, stored under given key.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="data"></param>
        /// <returns>True if found, out stored object; else false</returns>
        bool GetScopedKeyed<T>(string key, out T? data);

        /// <summary>
        /// Get data from session storage with same type as out parameter.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns>True if found, out stored object; else false</returns>
        bool GetSessioned<T>(out T? data);

        /// <summary>
        /// Saves data to scope storage with same type as out parameter.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns>True if saving was succesful, else false</returns>
        bool PutScoped<T>(T data);

        /// <summary>
        /// Saves data to scope storage under given key.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="data"></param>
        /// <returns>True if saving was succesful, else false</returns>
        bool PutScopedKeyed<T>(string key, T data);

        /// <summary>
        /// Saves data to session storage with same type as out parameter.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns>True if saving was succesful, else false</returns>
        bool PutSessioned<T>(T data);

        /// <summary>
        /// Delete data from session storage with same type as out parameter.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns>True if found and deleted, else false</returns>
        bool DeleteSessioned<T>(T data);

        /// <summary>
        /// Delete all entries from scope and session storage.
        /// </summary>
        /// <returns>True if both succeded, else false</returns>
        bool Restart();
    }
}
