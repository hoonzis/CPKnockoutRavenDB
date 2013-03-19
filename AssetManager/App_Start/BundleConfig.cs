using System.Web;
using System.Web.Optimization;

namespace AssetManager
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/base")
                .Include("~/Scripts/jquery-1.9.0.min.js")
                .Include("~/Scripts/path.min.js")
                .Include("~/Scripts/bootstrap.min.js")
                .Include("~/Scripts/knockout-2.2.1.js")
                .Include("~/Scripts/knockout.validation.js"));

            bundles.Add(new ScriptBundle("~/bundles/vms").IncludeDirectory("~/ViewModels/", "*.js"));
            
            bundles.Add(new ScriptBundle("~/bundles/tech")
                .Include("~/Scripts/tools.js")
                .IncludeDirectory("~/Scripts/KoExtensions/", "*.js"));

            //the css has to be bundled to resamble "Content" directory,
            //because Bootstrap uses relative urls to search the images
            bundles.Add(new StyleBundle("~/Content/styles")
                .Include("~/Content/bootstrap.min.css")
                .Include("~/Content/bootstrap-responsive.min.css")
                .Include("~/Content/rickshaw.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/charting")
                .Include("~/Scripts/d3.v2.min.js")  
                .Include("~/Scripts/rickshaw.js"));

            //this is a crazy fix for loading bootstrap.css in bundle
            //http://stackoverflow.com/questions/12533591/why-are-my-style-bundles-not-rendering-correctly-in-asp-net-mvc-4
            bundles.IgnoreList.Clear();
        }
    }
}