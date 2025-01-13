namespace Helpdesk.Models.Navigation;

public partial class NavigationTree : BaseObject
{
    public string Name { get; set; }
    public NavigationNode Root { get; set; }
    public NavigationNode ActiveNode { get; set; }
    public int StaticNodeLevel { get; set; }
    public bool AllowCyclicTree { get; set; }

    public NavigationTree(ApplicationOptions options)
    {
        options.NavigationOptions.StaticNodeLevel = StaticNodeLevel;
        options.NavigationOptions.AllowCyclicTree = AllowCyclicTree;
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

    private bool IsNodeStatic(NavigationNode node) => node.Level <= StaticNodeLevel;
}
