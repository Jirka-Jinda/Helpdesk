namespace Helpdesk.Models.Navigation
{
    /// <summary>
    /// Methods for saving and operating the tree data structure as a navigation.
    /// Encapsulation over implementation, use these methods only or Sectumsempra(prog).
    /// </summary>
    public partial class NavigationTree
    {
        /// <summary>
        /// Moves navigation one step forward to a provided node, sets new active node
        /// </summary>
        /// <param name="node"></param>
        /// <returns>True if move was possible and executed, else false</returns>
        public bool MoveForward(NavigationNode newNode)
        {
            return MoveForward(newNode.Route, newNode.Data);
        }

        /// <summary>
        /// Moves navigation one step forward to a provided route, sets new active node. Base method to be called in normal circumstances.
        /// </summary>
        /// <param name="node"></param>
        /// <returns>True if move was possible and executed, else false</returns>
        public bool MoveForward(NavigationRoute newRoute, INodeData data)
        {
            try
            {
                if (Find(newRoute, out NavigationNode? nodeFound) && nodeFound != null)
                {
                    // Node found:
                    if (nodeFound.IsRoot)
                    {
                        // Homepage
                        MoveHome();
                    }
                    else if (nodeFound.HasParent)
                    {
                        // Node is already in tree structure
                        ExecuteMoveToNode(nodeFound);
                    }
                    else
                        throw new Exception("Node found but it's data is corrupted.");
                }
                else
                {
                    // Node not found:
                    NavigationNode newNode = new(data, newRoute);
                    ActiveNode.AppendChild(newNode);
                    newNode.Parent = ActiveNode;
                    newNode.Level = ActiveNode.Level + 1;
                    ActiveNode = newNode;
                }
                return true;
            }
            catch
            {
                // This really should be an exceptional state, but we dont want the app to crash, better display broken navigation and schedule restart.
                return false;
            }
        }

        /// <summary>
        /// Moves navigation one step backwards to the previously visited node, sets new active node
        /// </summary>
        /// <param name="node"></param>
        /// <returns>True if move was possible and executed</returns>
        public bool MoveBackward()
        {
            if (ActiveNode.Parent != null && ActiveNode.Parent != Root)
            {
                ExecuteMoveToNode(ActiveNode.Parent);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Moves navigation to a specific node provided
        /// </summary>
        /// <param name="node"></param>
        /// <returns>True if node was found and move executed, else false</returns>
        public bool MoveToNode(NavigationNode NavigationNode)
        {
            return MoveToNode(NavigationNode.Route);
        }

        /// <summary>
        /// Moves navigation to a specific node given it's navigation route
        /// </summary>
        /// <param name="route"></param>
        /// <returns>True if node was found and move executed, else false</returns>
        public bool MoveToNode(NavigationRoute NodeRoute)
        {
            if (Find(NodeRoute, out NavigationNode? nodeFound) && nodeFound != null)
            {
                ExecuteMoveToNode(nodeFound);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Executes move to existing node, support method.
        /// </summary>
        /// <param name="NodeRoute"></param>
        private void ExecuteMoveToNode(NavigationNode Node)
        {
            ActiveNode = Node;
            RemoveNonStaticChildren(Node);
        }
        private void RemoveNonStaticChildren(NavigationNode node)
        {
            if (node.HasChildren)
            {
                for (int k = 0; k < node.Children.Count; k++)
                {
                    var actualNode = node.Children.ElementAt(k);
                    if (IsNodeStatic(actualNode))
                        RemoveNonStaticChildren(actualNode);
                    else
                    {
                        node.RemoveChild(actualNode);
                        k--;
                    }
                }
            }
        }

        /// <summary>
        /// Resets navigation and returns home node
        /// </summary>
        /// <returns>True if succeded, home node in out parameter; else false</returns>
        public bool MoveHome()
        {
            ExecuteMoveToNode(Root);
            return true;
        }

        /// <summary>
        /// Get all nodes on direct path from currently active node to home node 
        /// </summary>
        /// <returns>Readonly collection of nodes</returns>
        public IReadOnlyCollection<NavigationNode> GetPathNodes()
        {
            var result = new List<NavigationNode>();
            var node = ActiveNode;

            while(!node.IsRoot)
            {
                result.Add(node);
                node = node.Parent;
            }

            result.Reverse();
            return result;
        }

        /// <summary>
        /// Get all nodes from root up to certain level
        /// </summary>
        /// <returns>Readonly collection of nodes in main navigation, default root and it's children</returns>
        public IReadOnlyCollection<NavigationNode> GetMenuNodes(int upToLevel = 1)
        {
            var result = new List<NavigationNode>();

            // just optimalization for base use case, no need to initialize queue and variable
            if (upToLevel <= 1)
                foreach (var child in Root.Children)
                    result.Add(child);
            else
            {
                NavigationNode node;
                var queue = new Queue<NavigationNode>();
                queue.Enqueue(Root);

                while (queue.Count > 0)
                {
                    node = queue.Dequeue();
                    foreach (var child in node.Children)
                    {
                        if (!node.IsRoot)
                            result.Add(node);
                        if (child.Level < upToLevel)
                            queue.Enqueue(child);
                    }
                }
            }
            
            return result;
        }
    }
}
