namespace Helpdesk.Models.Navigation
{
    public interface INodeData
    {
        // Navigation node payload
        public string StaticName { get; set; }
        public string DynamicName { get; set; }
        public string Icon { get; set; }
    }
}
