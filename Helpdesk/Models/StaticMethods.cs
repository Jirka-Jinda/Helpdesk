using Microsoft.AspNetCore.Mvc.ViewFeatures;
namespace Helpdesk.Models
{
    public static class StaticMethods
    {
        public static bool SetToViewData<T>(ViewDataDictionary viewData, T value)
        {
            string key = GetObjectKey<T>();
            if (viewData.ContainsKey(key))
                return false;
            else
            {
                viewData.Add(key, value);
                return true;
            }
        }
        public static bool GetFromViewData<T>(ViewDataDictionary viewData, out T value)
        {
            string key = GetObjectKey<T>();
            if (viewData.ContainsKey(key))
            {
                object temp;
                if (viewData.TryGetValue(key, out temp))
                {
                    value = (T)temp;
                    return true;
                }
            }
            value = default;
            return false;
        }

        public static string GetAttributeKey(Type objectType, string attributeName)
        {
            return objectType.ToString() + "." + attributeName;
        }
        public static string GetAttributeKey<T>(string attributeName)
        {
            return typeof(T).Name + "." + attributeName;
        }
        public static string GetObjectKey(Type objectType)
        {
            return objectType.ToString();
        }
        public static string GetObjectKey<T>()
        {
            return typeof(T).Name;
        }
    }
}
