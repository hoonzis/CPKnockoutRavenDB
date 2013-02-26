using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssetManager.Model;

namespace AssetManager.Tests.Utils
{
    public static class DataCreator
    {
        public static IEnumerable<Payment> GetPayments()
        {
            var payments = new List<Payment> { 
                new Payment { Amount = 100, PaymentTime = new PaymentTime {Year = 2012, Month =2}},
                new Payment { Amount = 200, PaymentTime = new PaymentTime { Year = 2012, Month = 4 } },
                new Payment {Amount = 300, PaymentTime = new PaymentTime { Year = 2012, Month = 5}}
            };
            return payments;

        }

        

        public static IEnumerable<Asset> GetAssets()
        {
            var asset = new Asset
            {
                Id = 2,
                OwnerId = 1,
                Address = "Test",
                Charges = new List<Charge>() { 
                    new Charge { AccountNumber = "111", Id = 1}
                }
            };

            return new List<Asset>{asset};
        }
    }
}
