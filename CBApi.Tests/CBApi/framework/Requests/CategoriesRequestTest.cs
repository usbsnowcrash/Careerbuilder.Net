using CBApi.Models;
using CBApi.Models.Service;
using NUnit.Framework;
using Moq;
using RestSharp;
using System.Collections.Generic;
using Tests.CBApi.models.requests;

namespace Tests.CBApi.framework.requests
{
    [TestFixture]
    public class CategoriesTest
    {
        [Test]
        public void Constructor_DefaultsToUSCountryCode()
        {
            var request = new CategoriesStub("DevKey", "api.careerbuilder.com", "", "");
            Assert.AreEqual("US", request.CountryCode);
        }

        [Test]
        public void GetRequestURL_BuildsCorrectEndpointAddress()
        {
            var request = new CategoriesStub("DevKey", "api.careerbuilder.com", "", "");
            Assert.AreEqual("https://api.careerbuilder.com/v1/categories", request.RequestURL);
        }

        [Test]
        public void WhereCountryCode_ReturnsCategoryRequest()
        {
            var request = new CategoriesStub("DevKey", "api.careerbuilder.com", "", "");
            Assert.IsInstanceOf<ICategoryRequest>(request.WhereCountryCode(CountryCode.SE));
        }

        [Test]
        public void WhereCountryCode_SetsCountryCode()
        {
            var request = new CategoriesStub("DevKey", "api.careerbuilder.com", "", "");
            request.WhereCountryCode(CountryCode.SE);
            Assert.AreEqual("SE", request.CountryCode);
        }

        [Test]
        public void WhereHostSite_ReturnsCategoryRequest()
        {
            var request = new CategoriesStub("DevKey", "api.careerbuilder.com", "", "");
            Assert.IsInstanceOf<ICategoryRequest>(request.WhereHostSite(HostSite.EU));
        }

        [Test]
        public void WhereHostSite_SetsCountryCode()
        {
            var request = new CategoriesStub("DevKey", "api.careerbuilder.com", "", "");
            request.WhereHostSite(HostSite.EU);
            Assert.AreEqual("EU", request.CountryCode);
        }

        [Test]
        public void ListAll_PerformsCorrectRequest()
        {
            //Setup
            var request = new CategoriesStub("DevKey", "api.careerbuilder.com", "", "");

            //Mock crap
            var response = new RestResponse<List<Category>> {Data = new List<Category>()};

            var restReq = new Mock<IRestRequest>();
            restReq.Setup(x => x.AddParameter("DeveloperKey", "DevKey"));
            restReq.Setup(x => x.AddParameter("CountryCode", "NL"));
            restReq.SetupSet(x => x.RootElement = "Categories");

            var restClient = new Mock<IRestClient>();
            restClient.SetupSet(x => x.BaseUrl = "https://api.careerbuilder.com/v1/categories");
            restClient.Setup(x => x.Execute<List<Category>>(It.IsAny<IRestRequest>())).Returns(response);

            request.Request = restReq.Object;
            request.Client = restClient.Object;

            //Assert
            List<Category> cats = request.WhereCountryCode(CountryCode.NL).ListAll();
            Assert.IsTrue(cats.Count == 0);
            restReq.VerifyAll();
            restClient.VerifyAll();
        }
    }
}