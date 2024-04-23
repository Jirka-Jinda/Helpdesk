namespace Helpdesk.ActionFilters.Global
{
    public class CookieFilter : ActionFilterAttribute
    {
        private IScopeStorage scopeCache { get; set; }

        public CookieFilter(IScopeStorage scopeCache)
        {
            this.scopeCache = scopeCache;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var cookie = context.HttpContext.Request.Cookies;

            if (cookie != null && CookieMethods.GetFromRequestCookie(context.HttpContext.Request, out UserSettings cookieSettings) && cookieSettings != null)
                scopeCache.Put(cookieSettings as IUserSettings);
            else
                scopeCache.Put(new UserSettings() as IUserSettings); //TODO: prepsat do interfacu, asi factory metoda nebo expose default object do DI
        }

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            if (scopeCache.Get(out IUserSettings settings))
            {
                CookieMethods.SetToResponseCookie(context.HttpContext.Response, settings as UserSettings);
            }
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var res = context.Result as ViewResult;
            Console.WriteLine(res.ViewName);

            base.OnActionExecuted(context);
        }

    }
}
