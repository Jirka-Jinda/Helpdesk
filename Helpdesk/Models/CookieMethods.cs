namespace Helpdesk.Models
{
    public static class CookieMethods
    {
        public static bool SetToResponseCookie<T>(HttpResponse response, T value)
        {
            string key = GetObjectKey<T>();
            var cookie = response.Cookies;
            if (cookie != null)
            {
                cookie.Append(key, JsonSerializer.Serialize(value), new CookieOptions() { IsEssential = true, Expires = DateTimeOffset.Now.AddDays(360) });
                return true;
            }
            return false;
        }

        public static bool GetFromRequestCookie<T>(HttpRequest request, out T value)
        {
            string key = GetObjectKey<T>();
            var cookie = request.Cookies;
            if (cookie.ContainsKey(key))
            {
                if (cookie.TryGetValue(key, out string temp))
                {
                    // TODO: ISerializable, implementovat na UserSettings
                    value = JsonSerializer.Deserialize<T>(temp);
                    return true;
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
}
