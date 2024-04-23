namespace Helpdesk.Models.Navigation
{
    public class NavigationNode
    {
        public string DynamicName { get; set; }
        public string StaticName { get; set; }
        public bool ReplaceWithSameControllerNodes { get; set; }
        public string Icon { get; set; }
        public IEnumerable<NavigationNode> Children { get; set; }
        public NavigationNode? Parent { get; set; }
        public NavigationRoute Route { get; set; }

        public NavigationNode(string dynamicName = "", string staticName = "", string icon = "", IEnumerable<NavigationNode> children = null, NavigationNode parent = null, NavigationRoute route = null, bool replaceWithSameControllerNodes = false)
        {
            // TODO: problem nullovani atributu, asi predelat konstruktor
            DynamicName = dynamicName;
            StaticName = staticName;
            Icon = icon;
            if (children == null)
                Enumerable.Empty<NavigationNode>();
            else
                Children = children;
            Parent = parent;
            Route = route;
            ReplaceWithSameControllerNodes = replaceWithSameControllerNodes;
        }

        public bool HasChildren()
        {
            if (Children != null && Children.Any())
                return true;
            else return false;
        }

        public void CutChildren()
        {
            Children = Enumerable.Empty<NavigationNode>();
        }
    }
}
