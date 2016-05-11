using CBApi.Models;
using NUnit.Framework;
using Moq;
using RestSharp;
using System;
using Tests.CBApi.models.requests;

namespace Tests.CBApi.framework.requests {
    [TestFixture]
    public class AuthTokenRequestTest {
        [Test]
        public void Constructor_SetsClientID() {
            var request = new AuthTokenRequestStub("ClientID", "ClientSecret", "Code","redirectURI", "DevKey", "api.careerbuilder.com", "", "");
            Assert.AreEqual("ClientID", request.ClientId);
        }

        [Test]
        public void Constructor_SetsClientSecret() {
            var request = new AuthTokenRequestStub("ClientID", "ClientSecret", "Code", "redirectURI", "DevKey", "api.careerbuilder.com", "", "");
            Assert.AreEqual("ClientSecret", request.ClientSecret);
        }

        [Test]
        public void Constructor_SetsCode() {
            var request = new AuthTokenRequestStub("ClientID", "ClientSecret", "Code", "redirectURI", "DevKey", "api.careerbuilder.com", "", "");
            Assert.AreEqual("Code", request.Code);
        }

        [Test]
        public void Constructor_RedirectURI() {
            var request = new AuthTokenRequestStub("ClientID", "ClientSecret", "Code", "redirectURI", "DevKey", "api.careerbuilder.com", "", "");
            Assert.AreEqual("redirectURI", request.RedirectUri);
        }

        [Test]
        public void Constructor_ThrowsException_WhenPassedNullOrEmptyClientID() {
            try {
                var request = new AuthTokenRequestStub("", "ClientSecret", "Code", "redirectURI", "DevKey", "api.careerbuilder.com", "", "");
                Assert.Fail("Should have thrown exception");
            } catch (ArgumentNullException ex) {
                Assert.IsInstanceOf<ArgumentNullException>(ex);
            }

            try {
                var request = new AuthTokenRequestStub(null, "ClientSecret", "Code", "redirectURI", "DevKey", "api.careerbuilder.com", "", "");
                Assert.Fail("Should have thrown exception");
            } catch (ArgumentNullException ex) {
                Assert.IsInstanceOf<ArgumentNullException>(ex);
            }
        }

        [Test]
        public void Constructor_ThrowsException_WhenPassedNullOrEmptyClientSecret() {
            try {
                var request = new AuthTokenRequestStub("ClientID", "", "Code", "redirectURI", "DevKey", "api.careerbuilder.com", "", "");
                Assert.Fail("Should have thrown exception");
            } catch (ArgumentNullException ex) {
                Assert.IsInstanceOf<ArgumentNullException>(ex);
            }

            try {
                var request = new AuthTokenRequestStub("ClientID", null, "Code", "redirectURI", "DevKey", "api.careerbuilder.com", "", "");
                Assert.Fail("Should have thrown exception");
            } catch (ArgumentNullException ex) {
                Assert.IsInstanceOf<ArgumentNullException>(ex);
            }
        }

        [Test]
        public void Constructor_ThrowsException_WhenPassedNullOrEmptyCode() {
            try {
                var request = new AuthTokenRequestStub("ClientID", "asdas", "", "redirectURI", "DevKey", "api.careerbuilder.com", "", "");
                Assert.Fail("Should have thrown exception");
            } catch (ArgumentNullException ex) {
                Assert.IsInstanceOf<ArgumentNullException>(ex);
            }

            try {
                var request = new AuthTokenRequestStub("ClientID", "asdas", null, "redirectURI", "DevKey", "api.careerbuilder.com", "", "");
                Assert.Fail("Should have thrown exception");
            } catch (ArgumentNullException ex) {
                Assert.IsInstanceOf<ArgumentNullException>(ex);
            }
        }

        [Test]
        public void Constructor_ThrowsException_WhenPassedNullOrEmptyRedirectUri() {
            try {
                var request = new AuthTokenRequestStub("ClientID", "asdas", "asdfasd", "", "DevKey", "api.careerbuilder.com", "", "");
                Assert.Fail("Should have thrown exception");
            } catch (ArgumentNullException ex) {
                Assert.IsInstanceOf<ArgumentNullException>(ex);
            }

            try {
                var request = new AuthTokenRequestStub("ClientID", "asdas", "asdfasd", null, "DevKey", "api.careerbuilder.com", "", "");
                Assert.Fail("Should have thrown exception");
            } catch (ArgumentNullException ex) {
                Assert.IsInstanceOf<ArgumentNullException>(ex);
            }
        }

        [Test]
        public void GetRequestURL_BuildsCorrectEndpointAddress() {
            var request = new AuthTokenRequestStub("ClientID", "ClientSecret", "Code", "redirectURI", "DevKey", "api.careerbuilder.com", "", "");
            Assert.AreEqual("https://api.careerbuilder.com/auth/token", request.RequestURL);
        }

        [Test]
        public void Retrieve_PerformsCorrectRequest() {
            //Setup
            var request = new AuthTokenRequestStub("ClientID", "ClientSecret", "Code", "redirectURI", "DevKey", "api.careerbuilder.com", "", ""); 

            //Mock crap
            var response = new RestResponse<AccessToken> { Data = new AccessToken() };

            var restReq = new Mock<IRestRequest>();
            restReq.Setup(x => x.AddParameter("DeveloperKey", "DevKey"));
            restReq.Setup(x => x.AddParameter("client_id", "ClientID"));
            restReq.Setup(x => x.AddParameter("client_secret", "ClientSecret"));
            restReq.Setup(x => x.AddParameter("redirect_uri", "redirectURI"));
            restReq.Setup(x => x.AddParameter("code", "Code"));

            var restClient = new Mock<IRestClient>();
            restClient.SetupSet(x => x.BaseUrl = "https://api.careerbuilder.com/auth/token");
            restClient.Setup(x => x.Execute<AccessToken>(It.IsAny<IRestRequest>())).Returns(response);

            request.Request = restReq.Object;
            request.Client = restClient.Object;

            //Assert
            AccessToken resp = request.GetAccessToken();
            restReq.VerifyAll();
            restClient.VerifyAll();
        }
    }
}