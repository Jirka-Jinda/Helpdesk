using Helpdesk.Models.Attributes;

namespace Helpdesk.ActionFilters.Global
{
    public class NavigationFilter : ActionFilterAttribute
    {
        private ISessionStorage sessionCache { get; set; }
        private IScopeStorage scopeCache { get; set; }
        private IApplicationSettings appSettings { get; set; }
        private NavigationStatic staticNav { get; set; }


        public NavigationFilter(ISessionStorage sessionCache, IScopeStorage scopeCache, IApplicationSettings appSettings, NavigationStatic staticNav)
        {
            this.sessionCache = sessionCache;
            this.scopeCache = scopeCache;
            this.appSettings = appSettings;
            this.staticNav = staticNav;
        }

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            // TODO: srovnat navigaci se zobrazenim okna, pokud se otevre nove okno a bude k dispozici stara dynamicka navigace, musi se zobrazit odpovidajici okno
            // nebo se musi vynulovat navigace a zobrazit nova instance aplikace
            // navigace -> zobrazeni
            // zobrazeni -> navigace DONE

            if (scopeCache.Get(out IUserSettings settings))
            {
                NavigationNode newNode = new(route: new NavigationRoute(context.RouteData));

                // Get old or create new dynamic nav
                if (!sessionCache.Get(settings.UserId, out NavigationDynamic nav))
                    nav = new NavigationDynamic();                

                // If is static navigation node, get all information from object in static nav
                if (staticNav.Find(newNode.Route, out NavigationNode staticNode) || staticNav.FindShared(newNode.Route, out staticNode))
                    newNode = staticNode;

                // If is homepage
                if (newNode.Route == appSettings.HomePage.Route)
                    newNode = appSettings.HomePage;

                // Changes according to Navigation attributes
                if (NavigationAttribute.GetAttribute(context.Controller as Controller, new NavigationRoute(context.RouteData), out NavigationAttribute attrs))
                {
                    if (!attrs.IgnoreActionInNavigation)
                    {
                        NavigationNode node = new(route: new NavigationRoute(context.RouteData));

                        if (attrs.NavigationName != null)
                            newNode.DynamicName = newNode.StaticName = attrs.NavigationName;
                        if (attrs.ReplaceWithSameControllerNodes)
                            newNode.ReplaceWithSameControllerNodes = true;
                    }
                    else
                        return;
                }

                // Finally move forward and save
                newNode.CutChildren();
                nav.Forward(newNode);
                sessionCache.Put(settings.UserId, nav);
            }
            else
                Console.WriteLine("Couldnt retrieve user settings from scope cache.");
        }
    }
}
