using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssetManager.Controllers;
using AssetManager.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Raven.Client.Embedded;

namespace AssetManager.Tests.Controllers
{
    [TestClass]
    public class ChargesControllerTest : ParentControllerTest
    {
        [TestInitialize]
        public void Initialize()
        {
            SetUp();
        }

        [TestCleanup]
        public void Clean()
        {
            CleanUp();
        }

        [TestMethod]
        public void Post_Test_NewCharge()
        {
            ChargesController controler = new ChargesController();
            controler.RavenSession = RavenSession;

            RavenSession.Store(new Owner { Email = "tester@tester.com", Id = 1, Name = "Tester", UserName = "username" });
            RavenSession.Store(new Asset { OwnerId = 1, Id = 1, Name = "Asset1" });

            RavenSession.SaveChanges();

            var newCharge = new Charge
            {
                AccountNumber = "1234",
                Amount = 500,
                Automatic = false,
                Counterparty = "Counterparty",
                Name = "Name",
                Notes = "Notes",
                PaymentDay = 1,
                Unit = 2,
                
            };
            controler.RavenSession = RavenSession;
            var result = controler.Post(newCharge, 1);

            RavenSession.SaveChanges();

            var charge = RavenSession.Load<Asset>(1).Charges.First();
            Assert.AreEqual(charge.Id, 1);
            Assert.AreEqual(charge.Name, "Name");
            Assert.AreEqual(charge.Notes, "Notes");
            Assert.AreEqual(charge.PaymentDay, 1);
            Assert.AreEqual(charge.Payments.Count(), 0);
            Assert.AreEqual(charge.Unit, 2);
            Assert.AreEqual(charge.Amount, 500);
        }
    }
}
