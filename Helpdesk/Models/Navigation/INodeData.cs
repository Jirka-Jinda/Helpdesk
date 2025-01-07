namespace Helpdesk.Models.Navigation
{
    /// <summary>
    /// Navigation node data payload. Contains everything usable. Can be changed or updated.
    /// </summary>
    public interface INodeData
    {
        public string StaticName { get; set; }
        public string DynamicName { get; set; }
        public string Icon { get; set; }
    }
}
