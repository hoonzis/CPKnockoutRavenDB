using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace AssetManager.Technical
{
    public class LanguageFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.RouteData.Values["language"] != null)
            {
                
                var lang = filterContext.RouteData.Values["language"].ToString();
                var culture = CultureInfo.GetCultureInfoByIetfLanguageTag(lang);

                if (culture != null)
                    Thread.CurrentThread.CurrentUICulture = culture;
            }
            
            base.OnActionExecuting(filterContext);
        }
    }
}