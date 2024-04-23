namespace Helpdesk.Models.Navigation
{
    public class NavigationStatic : Navigation
    {
        public IEnumerable<NavigationNode> Shared { get; set; } = Enumerable.Empty<NavigationNode>();
        public NavigationStatic(IApplicationSettings settings)
        {
            Nodes = CreateNavigation();
            Shared = CreateShared();
        }
        public NavigationStatic()
        {
            Console.WriteLine("Error while accessing navigation DI, services not present.");
        }

        public bool FindShared(NavigationRoute route, out NavigationNode match, bool onlyController = false)
        {
            Queue<NavigationNode> queue = new Queue<NavigationNode>();
            foreach (NavigationNode node in Shared)
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

        private static IEnumerable<NavigationNode> CreateShared()
        {
            return new List<NavigationNode>() {
                new NavigationNode(dynamicName: "Uživatelé", staticName: "Uživatelé", icon: "people", route: new NavigationRoute("", "Layout", "Users"), replaceWithSameControllerNodes: true),
                new NavigationNode(dynamicName: "Nastavení", staticName: "Nastavení", icon: "gear", route: new NavigationRoute("", "Layout", "Settings"), replaceWithSameControllerNodes: true),
            };
        }

        private static IEnumerable<NavigationNode> CreateNavigation()
        {
            var node1 = new NavigationNode(dynamicName: "Node1", staticName: "Node1", icon: "dash", route: new NavigationRoute("", "", ""), replaceWithSameControllerNodes: true);
            var node2 = new NavigationNode(dynamicName: "Node2", staticName: "Node2", icon: "dash", route: new NavigationRoute("", "", ""), replaceWithSameControllerNodes: true);
            var node3 = new NavigationNode(dynamicName: "Node3", staticName: "Node3", icon: "dash", route: new NavigationRoute("", "", ""), replaceWithSameControllerNodes: true);
            var node4 = new NavigationNode(dynamicName: "Node4", staticName: "Node4", icon: "dash", route: new NavigationRoute("", "", ""), replaceWithSameControllerNodes: true);
            var node5 = new NavigationNode(dynamicName: "Node5", staticName: "Node5", icon: "dash", route: new NavigationRoute("", "", ""), replaceWithSameControllerNodes: true);
            var node6 = new NavigationNode(dynamicName: "Node6", staticName: "Node6", icon: "dash", route: new NavigationRoute("", "", ""), replaceWithSameControllerNodes: true);
            var node7 = new NavigationNode(dynamicName: "Node7", staticName: "Node7", icon: "dash", route: new NavigationRoute("", "", ""), replaceWithSameControllerNodes: true);
            var node8 = new NavigationNode(dynamicName: "Node8", staticName: "Node8", icon: "dash", route: new NavigationRoute("", "", ""), replaceWithSameControllerNodes: true);

            node1.Children = new List<NavigationNode>() { node2, node3 };
            node2.Parent = node3.Parent = node1;

            node4.Children = new List<NavigationNode>() { node5, node6 };
            node5.Parent = node6.Parent = node4;

            node7.Children = new List<NavigationNode>() { node8 };
            node8.Parent = node7;

            return new List<NavigationNode>() { node1, node4, node7 };
        }
    }
}
