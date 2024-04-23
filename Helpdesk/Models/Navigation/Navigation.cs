namespace Helpdesk.Models.Navigation
{
    public abstract class Navigation : BaseObject
    {
        public string Name { get; set; } = "";
        public IEnumerable<NavigationNode> Nodes { get; set; } = Enumerable.Empty<NavigationNode>();

        public static uint MaxDynamicNavigationLength = 8;
        public static bool AllowDynamicNavigationCycles = false;
         
        public bool Find(NavigationNode node, out NavigationNode match, bool onlyController = false)
        {
            return Find(node.Route, out match, onlyController);
        }

        public bool Find(RouteData route, out NavigationNode match, bool onlyController = false)
        {
            return Find(new NavigationRoute(route), out match, onlyController);
        }

        public bool Find(NavigationRoute route, out NavigationNode match, bool onlyController = false)
        {
            Queue<NavigationNode> queue = new Queue<NavigationNode>();
            foreach (NavigationNode node in Nodes)
                queue.Enqueue(node);

            while (queue.Count > 0)
            {
                NavigationNode res = queue.Dequeue();
                if ((res.Route == route) || (onlyController && res.Route.Controller == route.Controller))
                {
                    match = res;
                    return true;
                }
                else if (res.Children != null)
                    foreach (NavigationNode node in res.Children)
                        queue.Enqueue(node);
            }
            match = null;
            return false;
        }

        public IEnumerable<NavigationNode> PathTo(NavigationNode node)
        {
            List<NavigationNode> res = new() { node };
            while (node.Parent != null)
            {
                res.Add(node.Parent);
                node = node.Parent;
            }
            res.Reverse();
            return res;
        }
    }
}
