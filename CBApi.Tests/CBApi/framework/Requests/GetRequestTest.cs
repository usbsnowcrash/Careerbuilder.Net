using CBApi;
using NUnit.Framework;
using Moq;
using RestSharp;
using System;
using Tests.CBApi.models.requests;
using Tests.CBApi.models.service;

namespace Tests.CBApi.framework.requests {
    [TestFixture]
    public class GetRequestTest {
        private APISettings _Settings = new APISettings(){DevKey="DevKey", CobrandCode="this is a cobrand",SiteId="this is a siteid",TimeoutMS=12345};
        private bool _HasEventFired = false;

        [Test]
        public void Constructor_ThrowsException_OnEmptyDevKey() {
            try {
                var request = new GetRequestStub("", "api.careerbuilder.com", "", "");
                Assert.Fail();
            } catch (ArgumentNullException ex) {
                Assert.IsInstanceOf<ArgumentNullException>(ex);
            }
        }

        [Test]
        public void Constructor_ThrowsException_OnNullDevKey() {
            try {
                var request = new GetRequestStub(null, "api.careerbuilder.com", "", "");
                Assert.Fail();
            } catch (ArgumentNullException ex) {
                Assert.IsInstanceOf<ArgumentNullException>(ex);
            }
        }

        [Test]
        public void Constructor_ThrowsException_OnEmptyDomain() {
            try {
                var request = new GetRequestStub("DevKey", "", "", "");
                Assert.Fail();
            } catch (ArgumentNullException ex) {
                Assert.IsInstanceOf<ArgumentNullException>(ex);
            }
        }

        [Test]
        public void Constructor_ThrowsException_OnNullDomain() {
            try {
                var request = new GetRequestStub("DevKey", null, "", "");
                Assert.Fail();
            } catch (ArgumentNullException ex) {
                Assert.IsInstanceOf<ArgumentNullException>(ex);
            }
        }

        [Test]
        public void Constructor_SetsDevKey() {
            var request = new GetRequestStub("DevKey", "api.careerbuilder.com", "", "");
            Assert.AreEqual("DevKey", request.DevKey);
        }

        [Test]
        public void Constructor_SetsDomain() {
            var request = new GetRequestStub("DevKey", "api.careerbuilder.com", "", "");
            Assert.AreEqual("api.careerbuilder.com", request.Domain);
        }

        [Test]
        public void Constructor_SetsCobrand() {
            var request = new GetRequestStub("DevKey", "api.careerbuilder.com", "cobrandcode", "");
            Assert.AreEqual("cobrandcode", request.CobrandCode);
        }

        [Test]
        public void Constructor_SetsSiteID() {
            var request = new GetRequestStub("DevKey", "api.careerbuilder.com", "", "SiteID");
            Assert.AreEqual("SiteID", request.SiteID);
        }

        [Test]
        public void BaseURL_IsNotSecure_WhenTargetSiteIsntSecure() {
            //Setup
            var newSite = new TargetSiteMock("127.0.0.1") { SetHost = "www.google.com", SetSecure=false };
            var settings = new APISettings() { DevKey = "DevKey", CobrandCode = "this is a cobrand", SiteId = "this is a siteid", TimeoutMS = 12345, TargetSite = newSite };
            var request = new GetRequestStub(settings);
            Assert.AreEqual("http://127.0.0.1/Exammple", request.GetRequestURL);
        }

        [Test]
        public void BaseURL_IsSecure_WhenTargetSiteIsSecure() {
            //Setup
            var newSite = new TargetSiteMock("127.0.0.1") { SetHost = "www.google.com", SetSecure = true };
            var settings = new APISettings() { DevKey = "DevKey", CobrandCode = "this is a cobrand", SiteId = "this is a siteid", TimeoutMS = 12345, TargetSite = newSite };
            var request = new GetRequestStub(settings);
            Assert.AreEqual("https://127.0.0.1/Exammple", request.GetRequestURL);
        }

        [Test]
        public void BeforeRequest_SetsDevKey_AndDomain_AndCobrand_AndSiteID_AndTimeout() {
            //Setup
            var request = new GetRequestStub(_Settings);

            //Mock crap
            var restReq = new Mock<IRestRequest>();
            restReq.Setup(x => x.AddParameter("DeveloperKey", "DevKey"));
            restReq.Setup(x => x.AddParameter("CoBrand", "this is a cobrand"));
            restReq.Setup(x => x.AddParameter("SiteID", "this is a siteid"));
            restReq.SetupSet(x => x.Timeout = 12345);

            var restClient = new Mock<IRestClient>();
            request.Request = restReq.Object;
            request.Client = restClient.Object;

            //Assert
            request.RunBeforeGet();
            restReq.VerifyAll();
        }

        [Test]
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

        [Test]
        public void BeforeRequest_RaisesBeforeRequestEvent() {
            //Setup
            var newSite = new TargetSiteMock("127.0.0.1") { SetHost = "www.google.com" };
            var settings = new APISettings() { DevKey = "DevKey", CobrandCode = "this is a cobrand", SiteId = "this is a siteid", TimeoutMS = 12345, TargetSite = newSite };
            var request = new GetRequestStub(settings);

            //Mock crap
            var restReq = new Mock<IRestRequest>();
            var restClient = new Mock<IRestClient>();
            restClient.Setup(x => x.BaseUrl).Returns("https://127.0.0.1/Exammple");
            request.Request = restReq.Object;
            request.Client = restClient.Object;

            //Assert
            request.OnBeforeRequest += (HandleBeforeRequest);
            request.RunBeforeGet();
            Assert.AreEqual(true, _HasEventFired);
            request.OnBeforeRequest -= (HandleBeforeRequest);
        }

        private void HandleBeforeRequest(IRequestEventData data) {
            _HasEventFired = true;
            Assert.AreEqual("https://127.0.0.1/Exammple", data.BaseURL);
        }

    }
}