namespace Helpdesk.Models.Navigation;

public partial class NavigationTree : BaseObject
{
    public string Name { get; set; }
    public NavigationNode Root { get; set; }
    public NavigationNode ActiveNode { get; set; }

    // Settings
    private int staticNodeLevel = 2;
    private bool allowCyclicTree = false;

    public NavigationTree(IApplicationSettings settings)
    {
        settings.NavigationTreeOptions.StaticNodeLevel = staticNodeLevel;
        settings.NavigationTreeOptions.AllowCyclicTree = allowCyclicTree;
    }

    public NavigationTree()
    {
        
    }

    private bool Find(NavigationRoute route, out NavigationNode? match)
    {
        Queue<NavigationNode> queue = new();
        queue.Enqueue(Root);

        while (queue.Count > 0)
        {
            NavigationNode temp = queue.Dequeue();
            if (temp.Route == route)
            {
                match = temp;
                return true;
            }
            else if (temp.HasChildren)
                foreach (NavigationNode node in temp.Children)
                    queue.Enqueue(node);
        }
        match = null;
        return false;
    }

    private bool IsNodeStatic(NavigationNode node) => node.Level <= staticNodeLevel;
}
