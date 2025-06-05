namespace Helpdesk.Models.Navigation
{
    public class NavigationRoute
    {
        public string Action { get; set; }
        public string Controller { get; set; }
        public string Area { get; set; }
        public string[]? Arguments { get; set; } = null;

        public NavigationRoute(RouteData routeData)
        {
            if (routeData.Values.TryGetValue("area", out object? ar) && ar != null)
                Area = ar.ToString()!;
            else Area = "";

            if (routeData.Values.TryGetValue("controller", out object? con) && con != null)
                Controller ??= con.ToString()!;
            else Controller = "";

            if (routeData.Values.TryGetValue("action", out object? act) && act != null)
                Action = act.ToString()!;
            else Action = "";

            Arguments = null;
        }

        public NavigationRoute(string area, string controller, string action, string[]? arguments = null)
        {
            Area = area;
            Controller = controller;
            Action = action;
            Arguments = arguments;
        }

        public RouteData GetRouteData()
        {
            throw new NotImplementedException();
        }

        public static bool operator ==(NavigationRoute route1, NavigationRoute route2)
        {
            if (route1.Area == route2.Area &&
                route1.Controller == route2.Controller &&
                route1.Action == route2.Action) return true;
            else return false;
        }

        public static bool operator !=(NavigationRoute route1, NavigationRoute route2) => !(route1 == route2);

        public override bool Equals(object? obj) => base.Equals(obj);

        public override int GetHashCode() => base.GetHashCode();
    }
}

