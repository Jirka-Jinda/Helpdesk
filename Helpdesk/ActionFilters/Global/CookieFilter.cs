﻿using Helpdesk.Models.Cookies;

namespace Helpdesk.ActionFilters.Global
{
    /// <summary>
    /// Cookie handling from request to response.
    /// Exposes user information via IUserSettings in Scope storage.
    /// </summary>
    public class CookieFilter : ActionFilterAttribute
    {
        private IStorageManager storageManager { get; set; }

        public CookieFilter(IStorageManager storageManager)
        {
            this.storageManager = storageManager;
        }

        /// <summary>
        /// Resolves cookie handling from incoming request or creates new one, exposes IUserSettings into scope storage.
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var cookie = context.HttpContext.Request.Cookies;

            if (cookie != null && CookieMethods.GetFromRequestCookie(context.HttpContext.Request, out UserSettings? cookieSettings) && cookieSettings != null)
                storageManager.PutScoped(cookieSettings as IUserSettings);
            else
                storageManager.PutScoped(new UserSettings() as IUserSettings); //TODO: prepsat do interfacu, asi factory metoda nebo expose default object do DI
        }

        /// <summary>
        /// Retrieves cookie from scope storage and appends into response.
        /// </summary>
        /// <param name="context"></param>
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            if (storageManager.GetScoped(out IUserSettings? settings))
            {
                CookieMethods.SetToResponseCookie(context.HttpContext.Response, settings as UserSettings);
            }
        }
    }
}
