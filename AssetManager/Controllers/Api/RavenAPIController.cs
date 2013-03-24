using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Raven.Client;
using AssetManager.Model;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.Web;

namespace AssetManager.Controllers
{
    [RequireHttps]
    public class RavenAPIController : ApiController
    {
        public IDocumentSession RavenSession { get; set; }

        public RavenAPIController()
        {
             
        }

        protected Owner ObtainCurrentOwner()
        {
            return RavenSession.Query<Owner>().SingleOrDefault(x => x.UserName == HttpContext.Current.User.Identity.Name);
        }

        public Asset GetAsset(int id, Owner owner)
        {
            if (owner == null)
                return null;
            return RavenSession.Query<Asset>().SingleOrDefault(x => x.Id == id && x.OwnerId == owner.Id);
        }

        public IEnumerable<Asset> GetAssets(int ownerID)
        {
            return RavenSession.Query<Asset>().Where(x => x.OwnerId == ownerID);
        }

        protected override void Initialize(System.Web.Http.Controllers.HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            if(RavenSession == null)
                RavenSession = WebApiApplication.Store.OpenSession();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            using (RavenSession)
            {
                if (RavenSession != null)
                    RavenSession.SaveChanges();
            }
        }

        /*
        public override Task<HttpResponseMessage> ExecuteAsync(System.Web.Http.Controllers.HttpControllerContext controllerContext, System.Threading.CancellationToken cancellationToken)
        {
            return base.ExecuteAsync(controllerContext,cancellationToken)
               .ContinueWith(task =>
               {
                   OnActionExecuted();
                   return task;
               }).Unwrap();
        }*/

        

    }
}
