using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AssetManager.Model;

namespace AssetManager.Controllers
{
    public class RentsController : RavenAPIController
    {
     
        // POST api/renters
        public Object Post(Rent value, int assetID)
        {
            var owner = ObtainCurrentOwner();
            var asset = GetAsset(assetID, owner);

            value.Id = asset.GenerateRentId();
            if (asset.Rents == null)
            {
                asset.Rents = new List<Rent>();
            }

            asset.Rents.Add(value);

            return  new
            {
                dto = value,               
                message = "Rent saved"
            };
        }

        // PUT api/renters/5
        public Object Put(Rent value,int assetID)
        {
            var owner = ObtainCurrentOwner();
            var asset = GetAsset(assetID, owner);
            var rent = asset.Rents.SingleOrDefault(x => x.Id == value.Id);

            if (rent == null)
            {
                return new
                {
                    status = "KO",
                    message = "Could not load the rent"
                };
            }

            rent.Amount = value.Amount;
            rent.Name = value.Name;
            rent.Notes = value.Notes;
            rent.PaymentDay = value.PaymentDay;
            rent.Deposit = value.Deposit;
            rent.DepositPayed = value.DepositPayed;
            rent.Unit = value.Unit;
            rent.Start = value.Start;
            rent.End = value.End;
            rent.AccountNumber = value.AccountNumber.Replace(" ",String.Empty);

            return new
            {
                dto = value,               
                message = "Rent saved"
            };
        }

        // DELETE api/renters/5
        public object Delete(int id, int assetID)
        {
            var owner = ObtainCurrentOwner();
            var asset = GetAsset(assetID, owner);
            var rent = asset.Rents.SingleOrDefault(x => x.Id == id);
            if (rent != null)
            {
                asset.Rents.Remove(rent);
            }

            return new
            {
                Status = "OK"
            };
        }
    }
}
