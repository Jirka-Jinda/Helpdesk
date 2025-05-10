namespace Helpdesk.Filters;

/// <summary>
/// Handles operating the navigation between every new view.
/// Is responsible for synchronization of views and navigation from both ends.
/// Result can be altered via controller's NavigationAttribute.
/// Is Resolved after all other logic and filters and before rendering view.
/// </summary>
public class NavigationFilter : ActionFilterAttribute
{
    private IStorageManager storageManager;

    public NavigationFilter(IStorageManager sm)
    {
        storageManager = sm;
    }

    // Called before action mathod after model binding
    // Use for shortcutting or redirecting
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        
    }

    // Called after action method before rendering view
    public override void OnResultExecuting(ResultExecutingContext context)
    {
        NodeData newNode = new();
        NavigationRoute newRoute = new(context.RouteData);
        bool skipForward = false;

        if (!storageManager.GetSessioned(out NavigationTree? navigation))
        {
            // Get new navigation and save into 'navigation'
             navigation = DummyNav.CreateDummyNav();
        }
        if (NavigationAttribute.GetAttribute((Controller)context.Controller, newRoute, out NavigationAttribute attrs))
        {
            if (attrs.BackwardMove)
            {
                navigation?.MoveBackward();
                skipForward = true;
            }
            if (attrs.NavigationName != null) newNode.StaticName = newNode.DynamicName = attrs.NavigationName;
            if (attrs.IgnoreMove) skipForward = true;
        }
        if (!skipForward)
            navigation?.MoveForward(newRoute, newNode);

        storageManager.PutSessioned(navigation);
    }
}