using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Raven.Client.Document;
using Raven.Abstractions.Data;
using Raven.Client.Indexes;
using System.Reflection;
using System.Threading;
using System.Globalization;

namespace AssetManager
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //The new MVC 4 stuff for routes filters and registration
            AreaRegistration.RegisterAllAreas();

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            RavenDBInit();
        }

        private static void RavenDBInit()
        {
            var parser = ConnectionStringParser<RavenConnectionStringOptions>.FromConnectionStringName("RavenDB");
            parser.Parse();

            Store = new DocumentStore
            {
                ApiKey = parser.ConnectionStringOptions.ApiKey,
                Url = parser.ConnectionStringOptions.Url,
            };

            Store.Initialize();
            

            IndexCreation.CreateIndexes(Assembly.GetCallingAssembly(), Store);

            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;
        }

        public static DocumentStore Store;

        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        protected void Application_BeginRequest()
        {
            //TODO: MVC takes the culture of the browser as the default. This culture is later used in 
            //order to validate the model -> issue while javascript will use english culture on client side...
            Thread.CurrentThread.CurrentCulture
              = CultureInfo.InvariantCulture;
        }
    }
}