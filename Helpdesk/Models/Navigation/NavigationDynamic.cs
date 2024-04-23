namespace Helpdesk.Models.Navigation
{
    public class NavigationDynamic : Navigation
    {
        public NavigationNode Root => Nodes.Last();

        public NavigationDynamic(IApplicationSettings settings)
        {
            AllowDynamicNavigationCycles = settings.AllowDynamicNavigationCycles;
            MaxDynamicNavigationLength = settings.MaxDynamicNavigationLength;
        }
        public NavigationDynamic()
        {
            
        }

        public bool Forward(NavigationNode node, string name = "")
        {
            // Make sure it has name to display
            if (name != "")
                node.StaticName = node.DynamicName = name;
            else if (node.DynamicName == "" || node.StaticName == "")
                node.StaticName = node.DynamicName = node.Route.Controller + "|" + node.Route.Action;

            // Lets try to append new node to existing navigation
            try
            {
                // Are there too many nodes in nav?
                if (Nodes.Count() > MaxDynamicNavigationLength)
                {
                    return false;
                }
                // If there is a node with same controller and is to be replaced
                else if (Find(node, out var replaceNode, true) && node.ReplaceWithSameControllerNodes)
                {
                    if (replaceNode.Parent != null)
                        replaceNode.Parent.Children = new List<NavigationNode>() { node };
                    else
                        Nodes = new List<NavigationNode>() { node };
                }
                // Is there already a node with same route?
                else if (Find(node, out var findNode) && !AllowDynamicNavigationCycles)
                    findNode.CutChildren();
                else
                {
                    List<NavigationNode> newChild = new List<NavigationNode>() { node };

                    if (Nodes.Count() > 0 && Last() != null)
                        Last().Children = newChild;
                    else
                        Nodes = newChild;
                }
                    return true;
            }
            catch { return false; }
        }

        public bool Forward(RouteData routeData)
        {
            return Forward(new NavigationRoute(routeData));
        }

        public bool Forward(NavigationRoute routeData)
        {
            return Forward(new NavigationNode(route: routeData));
        }

        public bool Backward(out NavigationRoute routeData)
        {
            var last = Last();
            if (last != null && last.Parent != null)
            {
                routeData = last.Parent.Route;
                last.Parent.Children = null;
                return true;
            }
            else
            {
                routeData = default;
                return false;
            }
        }

        private NavigationNode? Last()
        {
            var count = Nodes.Count();
            switch (count)
            {
                case 0:
                    Console.WriteLine("No nodes in dynamic navigation.");
                    return null;
                case 1:
                    var n = Nodes.First();
                    while (n.HasChildren() && n.Children.Count() == 1)
                        n = n.Children.First();
                    return n;
                default:
                    Console.WriteLine("There is more than one root in dynamic navigation, repair.");
                    return null;
            }
        }
    }
}
