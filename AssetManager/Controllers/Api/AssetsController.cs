using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AssetManager.Model;

namespace AssetManager.Controllers
{
    public class AssetsController : RavenAPIController
    {
        
         // GET api/assets
        [Authorize]
        public IEnumerable<Object> Get()
        {
            var owner = ObtainCurrentOwner();
            return GetAssets(owner.Id);
        }

        // GET api/assets/5
        public Asset Get(int id)
        {
            var owner = ObtainCurrentOwner();
            return GetAsset(id, owner);
        }

        // POST api/assets
        public Object Post(Asset value)
        {
            var owner = ObtainCurrentOwner();
            
            value.OwnerId = owner.Id;
            RavenSession.Store(value);

            return GetReturnSaveReturnDto(value);
        }

        // PUT api/assets/5
        public Object Put(int id, Asset value)
        {
            var owner = ObtainCurrentOwner();
            var asset = GetAsset(id, owner);

            asset.Name = value.Name;
            asset.ZipCode = value.ZipCode;
            asset.City = value.City;
            asset.Address = value.Address;
            asset.Size = value.Size;
            asset.PMS = value.PMS;
            asset.Price = value.Price;
            asset.ZipCode = value.ZipCode;
            asset.Country = value.Country;
            asset.IncomeTax = value.IncomeTax;
            asset.InterestRate = value.InterestRate;
            asset.Latitude = value.Latitude;
            asset.Longitude = value.Longitude;

            return GetReturnSaveReturnDto(asset);
        }

        // DELETE api/assets/5
        public void Delete(int id)
        {
            var owner = ObtainCurrentOwner();
            var asset = GetAsset(id,owner);
            RavenSession.Delete<Asset>(asset);
        }

        private Object GetReturnSaveReturnDto(Asset asset)
        {
            var returnValue = new
            {
                asset = asset,
                message = "Saved OK"
            };
            return returnValue;
        }
    }
}
