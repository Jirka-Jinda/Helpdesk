namespace Helpdesk.Models.Navigation
{
    public class NavigationNode : BaseObject
    {
        public int Level { get; set; }
        public NavigationNode Parent { get; set; }
        public ICollection<NavigationNode> Children { get; set; }
        public NavigationRoute Route { get; set; }
        public INodeData Data { get; set; }

        public bool IsRoot => Level == 0;
        public bool HasParent => Parent != null && Parent != this;
        public bool HasChildren => Children != null && Children.Count() != 0;

        public NavigationNode(INodeData data, NavigationRoute route)
        {
            Parent = this;
            Children = new List<NavigationNode>();
            Route = route;
            Data = data;
        }

        /// <summary>
        /// Removes all children from node
        /// </summary>
        /// <returns>True if any children were removed, else false</returns>
        public bool RemoveChildren()
        {
            if (Children.Count == 0)
                return false;
            else
            {
                foreach (var child in Children)
                    RemoveChild(child);
                return true;
            }
        }

        /// <summary>
        /// Appends collection of nodes as children to the parent
        /// </summary>
        /// <param name="children"></param>
        public void AppendChildren(ICollection<NavigationNode> children)
        {
            foreach (var child in children)           
                AppendChild(child);
        }

        /// <summary>
        /// Removes given child from the parent
        /// </summary>
        /// <param name="node"></param>
        /// <returns>True if parent had given child, else false</returns>
        public bool RemoveChild(NavigationNode node)
        {
            if (Children.Contains(node))
            {
                Children.Remove(node);
                return true;
            }
            else return false;
        }

        /// <summary>
        /// Appends given node as child to the parent
        /// </summary>
        /// <param name="node"></param>
        public void AppendChild(NavigationNode node)
        {
            Children.Add(node);
            node.Parent = this;
            node.Level = Level + 1;
        }
    }
}
