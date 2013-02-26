using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AssetManager.Model;

namespace AssetManager.Controllers
{
    public class ChargesController : RavenAPIController
    {
        
        /// <summary>
        /// Create a new charge and store in the DB
        /// POST api/charges
        /// </summary>
        /// <param name="value"></param>
        /// <param name="assetID"></param>
        /// <returns></returns>
        public Object Post(Charge value, int assetID)
        {
            var owner = ObtainCurrentOwner();
            var asset = GetAsset(assetID,owner);
            value.Id = asset.GenerateChargeId();
            
            if (asset.Charges == null)
            {
                asset.Charges = new List<Charge>();
            }

            asset.Charges.Add(value);

            return GetResponse(value, asset, true);
        }

        
        /// <summary>
        /// Update existing charge in the database
        /// PUT api/charges/5
        /// </summary>
        /// <param name="value"></param>
        /// <param name="assetID"></param>
        /// <returns></returns>
        public Object Put(Charge value, int assetID)
        {
            var owner = ObtainCurrentOwner();
            var asset = GetAsset(assetID, owner);
            var charge = asset.Charges.SingleOrDefault(x => x.Id == value.Id);

            if (charge == null)
            {
                return new
                {
                    status = "KO",
                    message = "Could not load the charge"
                };
            }

            charge.Amount = value.Amount;
            charge.Automatic = value.Automatic;
            charge.Name = value.Name;
            charge.Notes = value.Notes;
            charge.Counterparty = value.Counterparty;
            charge.PaymentDay = value.PaymentDay;
            charge.AccountNumber = value.AccountNumber;
            charge.Unit = value.Unit;
            charge.Start = value.Start;
            charge.End = value.End;

            return GetResponse(charge, asset, false);
        }

        // DELETE api/charges/5
        public object Delete(int id, int assetID)
        {
            var owner = ObtainCurrentOwner();
            var asset = GetAsset(assetID, owner);

            var charge = asset.Charges.SingleOrDefault(x => x.Id == id);

            if (charge != null) 
            {
                asset.Charges.Remove(charge);
            }

            return new
            {
                Status = "OK"
            };
        }

        #region Helper methods not exposed by Rest interface
        public Object GetResponse(Charge charge, Asset asset, bool creation)
        {
            return new
            {
                dto = charge,
                message = creation ? "Charge added":"Charge updated"
            };
        }
        #endregion
    }
}
