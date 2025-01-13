namespace Helpdesk.Models.Navigation
{
    public static class DummyNav
    {
        public static NavigationTree CreateDummyNav()
        {
            var navigation = new NavigationTree(new ApplicationOptions())
            {
                StaticNodeLevel = 2,
                AllowCyclicTree = false,
                Name = "TestNavigation",
                Root = new NavigationNode(new NodeData() { StaticName = "TestNode1Static", DynamicName = "TestNode1Dynamic", Icon = "chevron" },
                new NavigationRoute("", "Layout", "Index")),
            };
            navigation.ActiveNode = navigation.Root;
            navigation.Root.Level = 0;

            var n1 = new NavigationNode(new NodeData() { StaticName = "Node1", DynamicName = "Node1", Icon = "arrow-right-square" }, new NavigationRoute("", "", ""));
            var n2 = new NavigationNode(new NodeData() { StaticName = "Node2", DynamicName = "Node2", Icon = "arrow-right-square" }, new NavigationRoute("", "", ""));
            var n3 = new NavigationNode(new NodeData() { StaticName = "Node3", DynamicName = "Node3", Icon = "arrow-right-square" }, new NavigationRoute("", "", ""));
            var n4 = new NavigationNode(new NodeData() { StaticName = "Node4", DynamicName = "Node4", Icon = "arrow-right-square" }, new NavigationRoute("", "", ""));
            var n5 = new NavigationNode(new NodeData() { StaticName = "Node5", DynamicName = "Node5", Icon = "arrow-right-square" }, new NavigationRoute("", "", ""));
            var n6 = new NavigationNode(new NodeData() { StaticName = "Node6", DynamicName = "Node6", Icon = "arrow-right-square" }, new NavigationRoute("", "", ""));
            var n7 = new NavigationNode(new NodeData() { StaticName = "Node7", DynamicName = "Node7", Icon = "arrow-right-square" }, new NavigationRoute("", "", ""));

            var s1 = new NavigationNode(new NodeData() { StaticName = "Test", DynamicName = "Test", Icon = "arrow-right-square" }, new NavigationRoute("", "TicketCreation", "Index"));
            var s2 = new NavigationNode(new NodeData() { StaticName = "SubNode2", DynamicName = "SubNode2", Icon = "arrow-right-square" }, new NavigationRoute("", "", ""));

            navigation.Root.AppendChildren(new List<NavigationNode>() { n1, n2, n3, n4, n5, n6, n7 });

            n1.AppendChildren(new List<NavigationNode>() { s1, s2 });

            return navigation;
        }
    }
}
