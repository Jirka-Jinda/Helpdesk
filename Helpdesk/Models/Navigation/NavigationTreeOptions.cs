using System.ComponentModel.DataAnnotations;

namespace Helpdesk.Models.Navigation
{
    public sealed class NavigationTreeOptions
    {
        [Required]
        public NavigationRoute? HomePage { get; set; }
        public int StaticNodeLevel { get; set; }
        public bool AllowCyclicTree { get; set; }
    }
}
