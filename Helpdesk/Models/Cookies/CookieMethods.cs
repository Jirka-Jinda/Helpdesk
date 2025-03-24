namespace Helpdesk.Models.Cookies;

public static class CookieMethods
{
    /// <summary>
    /// Serializes given object and appends it to the http response cookie.
    /// Cookie key is object type, so only unique object will be appended.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="response"></param>
    /// <param name="value"></param>
    /// <returns>True if appended, else false</returns>
    public static bool SetToResponseCookie<T>(HttpResponse response, T cookieValue)
    {
        string cookieKey = GetObjectKey<T>();
        var cookies = response.Cookies;

        if (cookies == null)
        {
            return false;
        }

        var options = new CookieOptions
        {
            IsEssential = true,
            Expires = DateTimeOffset.Now.AddDays(360)
        };

        cookies.Append(cookieKey, JsonSerializer.Serialize(cookieValue), options);
        return true;
    }

    /// <summary>
    /// Extracts cookie from http request and deserializes it.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="request"></param>
    /// <param name="value"></param>
    /// <returns>True if succeded, else false; object in out param</returns>
    public static bool GetFromRequestCookie<T>(HttpRequest request, out T? value)
    {
        string key = GetObjectKey<T>();
        var cookie = request.Cookies;
        if (cookie.ContainsKey(key))
        {
            if (cookie.TryGetValue(key, out string? temp))
            {
                try
                {
                    value = JsonSerializer.Deserialize<T>(temp);
                    return true;
                }
                catch (Exception)
                {
                    Console.WriteLine("Failed to deserialize the cookie");
                    // TODO
                }
            }
        }
        value = default;
        return false;
    }

    private static string GetObjectKey<T>()
    {
        return typeof(T).Name;
    }
}
