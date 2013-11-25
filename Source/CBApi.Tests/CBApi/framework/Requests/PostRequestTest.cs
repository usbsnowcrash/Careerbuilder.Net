using CBApi;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RestSharp;
using System;
using Tests.CBApi.models.requests;
using Tests.CBApi.models.service;

namespace Tests.CBApi.framework.requests {
    [TestClass]
    public class PostRequestTest {
        private bool _HasEventFired = false;

        [TestMethod]
        public void BaseURL_IsNotSecure_WhenTargetSiteIsntSecure() {
            //Setup
            var newSite = new TargetSiteMock("127.0.0.1") { SetHost = "www.google.com", SetSecure = false };
            var settings = new APISettings() { DevKey = "DevKey", CobrandCode = "this is a cobrand", SiteId = "this is a siteid", TimeoutMS = 12345, TargetSite = newSite };
            var request = new PostRequestStub(settings);
            Assert.AreEqual("http://127.0.0.1/Exammple", request.GetRequestURL);
        }

        [TestMethod]
        public void BaseURL_IsSecure_WhenTargetSiteIsSecure() {
            //Setup
            var newSite = new TargetSiteMock("127.0.0.1") { SetHost = "www.google.com", SetSecure = true };
            var settings = new APISettings() { DevKey = "DevKey", CobrandCode = "this is a cobrand", SiteId = "this is a siteid", TimeoutMS = 12345, TargetSite = newSite };
            var request = new PostRequestStub(settings);
            Assert.AreEqual("https://127.0.0.1/Exammple", request.GetRequestURL);
        }

        [TestMethod]
        public void BeforeRequest_SetsURL_SetsFormat_SetsTimeout() {
            //Setup
            var request = new PostRequestStub("DevKey", "api.careerbuilder.com", "this is a cobrand", "this is a siteid",12345);

            //Mock crap
            var restClient = new Mock<IRestClient>();
            var restReq = new Mock<IRestRequest>();
            restClient.SetupSet(x => x.BaseUrl = "https://api.careerbuilder.com/Exammple");

            restReq.SetupSet(x => x.RequestFormat = DataFormat.Xml);
            restReq.SetupSet(x => x.Timeout = 12345);

            
            request.Request = restReq.Object;
            request.Client = restClient.Object;

            //Assert
            request.RunBeforeRequest();
            restReq.VerifyAll();
        }

        [TestMethod]
        public void BeforeRequest_AddsHostParameter() {
            //Setup
            var newSite = new TargetSiteMock("127.0.0.1") { SetHost = "www.google.com" };
            var settings = new APISettings() { DevKey = "DevKey", CobrandCode = "this is a cobrand", SiteId = "this is a siteid", TimeoutMS = 12345, TargetSite = newSite };
            var request = new GetRequestStub(settings);

            //Mock crap
            var restReq = new Mock<IRestRequest>();
            restReq.Setup(x => x.AddHeader("Host", "www.google.com"));

            var restClient = new Mock<IRestClient>();
            request.Request = restReq.Object;
            request.Client = restClient.Object;

            //Assert
            request.RunBeforeGet();
            restReq.VerifyAll();
        }

        [TestMethod]
        public void BeforeRequest_AddsDeveloperKeyParameter() {
            //Setup
            var newSite = new TargetSiteMock("127.0.0.1") { SetHost = "www.google.com" };
            var settings = new APISettings() { DevKey = "DevKey", CobrandCode = "this is a cobrand", SiteId = "this is a siteid", TimeoutMS = 12345, TargetSite = newSite };
            var request = new GetRequestStub(settings);

            //Mock crap
            var restReq = new Mock<IRestRequest>();
            restReq.Setup(x => x.AddParameter("DeveloperKey", settings.DevKey));
            restReq.Setup(x => x.AddHeader("Host", "www.google.com"));

            var restClient = new Mock<IRestClient>();
            request.Request = restReq.Object;
            request.Client = restClient.Object;

            //Assert
            request.RunBeforeGet();
            restReq.VerifyAll();
        }

        [TestMethod]
        public void BeforeRequest_RaisesBeforeRequestEvent() {
            //Setup
            var newSite = new TargetSiteMock("127.0.0.1") { SetHost = "www.google.com" };
            var settings = new APISettings() { DevKey = "DevKey", CobrandCode = "this is a cobrand", SiteId = "this is a siteid", TimeoutMS = 12345, TargetSite = newSite };
            var request = new PostRequestStub(settings);

            //Mock crap
            var restReq = new Mock<IRestRequest>();
            var restClient = new Mock<IRestClient>();
            restClient.Setup(x => x.BaseUrl).Returns("https://127.0.0.1/Exammple");
            request.Request = restReq.Object;
            request.Client = restClient.Object;

            //Assert
            request.OnBeforeRequest += (HandleAfterRequest);
            request.RunBeforeRequest();
            Assert.AreEqual(true, _HasEventFired);
            request.OnBeforeRequest -= (HandleAfterRequest);
        }

        private void HandleAfterRequest(IRequestEventData data) {
            _HasEventFired = true;
            Assert.AreEqual("https://127.0.0.1/Exammple", data.BaseURL);
        }
    }
}