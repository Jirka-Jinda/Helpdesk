namespace Helpdesk.Models.Navigation;

public class NavigationOptions
{
    public NavigationRoute HomePage { get; set; } = new NavigationRoute("", "Layout", "Home");
    public int StaticNodeLevel { get; set; } = 2;
    public bool AllowCyclicTree { get; set; } = false;
}
