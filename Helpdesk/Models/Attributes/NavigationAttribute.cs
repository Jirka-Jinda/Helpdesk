namespace Helpdesk.Models.Attributes
{
    /// <summary>
    /// Attribute for controller methods to influence navigation behavior:
    /// NavigationName - displays name in breadcrumb and main navigation.
    /// IgnoreMove - navigation move will be ignored and active node will not change.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class NavigationAttribute : Attribute
    {
        public string? NavigationName { get; set; } = null;
        public bool IgnoreMove { get; set; } = false;

        public static bool GetAttribute(Controller controller, NavigationRoute route, out NavigationAttribute attr)
        {
            var attrType = typeof(NavigationAttribute);
            var method = controller.GetType().GetMethod(route.Action);
            if (method != null)
            {
                var attributes = method.GetCustomAttributes(attrType, false);
                if (attributes.Length == 1)
                {
                    attr = (NavigationAttribute)attributes[0];
                    return true;
                }
            }
            attr = new();
            return false;
        }
    }
}
