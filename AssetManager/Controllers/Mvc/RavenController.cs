using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Raven.Client;
using AssetManager.Model;

namespace AssetManager.Controllers
{
    public class RavenController : Controller
    {
        public IDocumentSession RavenSession { get; protected set; }

        protected Owner ObtainCurrentOwner()
        {
            var owner = RavenSession.Query<Owner>().SingleOrDefault(x => x.UserName == User.Identity.Name);
            return owner;
        }

        public RavenController()
        {
            
        }

        public Owner GetCurrentOwner()
        {
            var owner = RavenSession. Query<Owner>().SingleOrDefault(x => x.UserName == User.Identity.Name);
            return owner;
        }

        public Asset GetAsset(int id, Owner owner)
        {
            var asset = RavenSession.Query<Asset>().SingleOrDefault(x => x.Id == id && x.OwnerId == owner.Id);
            return asset;
        }

        public IEnumerable<Asset> GetAssets(int ownerID)
        {
            return RavenSession.Query<Asset>().Where(x => x.Id == ownerID);
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.IsChildAction)
                return;

            using (RavenSession)
            {
                if (filterContext.Exception != null)
                    return;

                if (RavenSession != null)
                    RavenSession.SaveChanges();
            }
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            if(RavenSession == null)
                RavenSession = WebApiApplication.Store.OpenSession();
            
        }
    }
}
