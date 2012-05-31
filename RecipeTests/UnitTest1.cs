using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RecipeService;
using DataLayer;

namespace RecipeTests
{
    [TestClass]
    public class UnitTest1
    {
        static string baseAddress = @"http://localhost:8000/WebServiceTests";
        static ServiceManager<IRecipeService, RecipeService.RecipeService> serviceManager = new ServiceManager<IRecipeService, RecipeService.RecipeService>(baseAddress);

        [ClassInitialize()]
        public static void ClassInitialize(TestContext testContext)
        {
            serviceManager.StartService();
        }
        
        [ClassCleanup()]
        public static void ClassCleanup()
        {
            serviceManager.StopService();
        }

        [TestMethod]
        public void TestGetRecipes()
        {
            RecipeResult ret = new RecipeResult();
            using (var factory = serviceManager.GetChannelFactory())
            {
                IRecipeService client = factory.CreateChannel();
                ret = client.GetRecipes();

                Assert.IsTrue(ret.RecipeNames.Count > 0);
            }
        }

        [TestMethod]
        public void TestGetRecipeById_Valid()
        {
            RecipeResult ret = new RecipeResult();
            using (var factory = serviceManager.GetChannelFactory())
            {
                IRecipeService client = factory.CreateChannel();
                ret = client.GetRecipeById(1);

                Assert.IsTrue(ret.RecipeNames.Count > 0);
            }
        }

        [TestMethod]
        public void TestGetRecipeById_NotValid()
        {
            RecipeResult ret = new RecipeResult();
            using (var factory = serviceManager.GetChannelFactory())
            {
                IRecipeService client = factory.CreateChannel();
                ret = client.GetRecipeById(92837);

                Assert.IsFalse(ret.RecipeNames.Count > 0);
            }
        }

        [TestMethod]
        public void TestGetRecipeByName_Valid()
        {
            RecipeResult ret = new RecipeResult();
            using (var factory = serviceManager.GetChannelFactory())
            {
                IRecipeService client = factory.CreateChannel();
                ret = client.SearchForRecipeByName("Chocolate");

                Assert.IsTrue(ret.RecipeNames.Count > 0);
            }
        }

        [TestMethod]
        public void TestGetRecipeByName_NotValid()
        {
            RecipeResult ret = new RecipeResult();
            using (var factory = serviceManager.GetChannelFactory())
            {
                IRecipeService client = factory.CreateChannel();
                ret = client.SearchForRecipeByName("Nothing");

                Assert.IsFalse(ret.RecipeNames.Count > 0);
            }
        }
    }
}
