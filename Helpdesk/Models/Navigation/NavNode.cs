namespace Helpdesk.Models.Navigation
{
    public class NavNode : BaseObject
    {
        public int Level { get; set; }
        public NavNode Parent { get; set; }
        public IEnumerable<NavNode> Children { get; set; }
        public NavRoute Route { get; set; }
        public INodeData Data { get; set; }

        public NavNode()
        {
            
        }

        public bool IsRoot()
        {
            return Level == 0;
        }

        public bool HasChildren()
        {
            if (Children == null || Children == Enumerable.Empty<NavNode>())
                return false;
            else return true;
        }
    }
}
