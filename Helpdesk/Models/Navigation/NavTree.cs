namespace Helpdesk.Models.Navigation
{
    public partial class NavTree : BaseObject
    {
        public string Name { get; set; }
        public NavNode Root { get; set; }
        public NavNode ActiveNode { get; set; }

        public NavTree(IApplicationSettings settings)
        {
            
        }

        public NavTree()
        {
            
        }
    }
}
