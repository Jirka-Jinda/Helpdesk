namespace Helpdesk.Models.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class NavigationAttribute : Attribute
    {
        public string? NavigationName { get; set; } = null;
        public bool IgnoreActionInNavigation { get; set; } = false;
        public bool ReplaceWithSameControllerNodes { get; set; } = false;

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
            attr = default;
            return false;
        }
    }
}
