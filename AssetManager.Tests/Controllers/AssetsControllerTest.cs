using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Web;
using AssetManager.Controllers;
using AssetManager.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Raven.Client;
using Raven.Client.Embedded;

namespace AssetManager.Tests.Controllers
{
    [TestClass]
    public class AssetsControllerTest : ParentControllerTest
    {

        [TestInitialize]
        public void Init()
        {
            SetUp();
        }

        [TestMethod]
        public void GetAssets_Test()
        {
            
            AssetsController controler = new AssetsController();

            
            RavenSession.Store(new Owner { Email = "tester@tester.com", Id = 1, Name = "Tester", UserName="username"});
            RavenSession.Store(new Asset { OwnerId = 1, Id = 1, Name = "Asset1" });

            RavenSession.SaveChanges();

            controler.RavenSession = RavenSession;
            var result = controler.Get();
            Assert.AreEqual(result.Count(), 1);

            CleanUp();
        }

        [TestCleanup]
        public void Clean()
        {
            CleanUp();
        }
    }
}
