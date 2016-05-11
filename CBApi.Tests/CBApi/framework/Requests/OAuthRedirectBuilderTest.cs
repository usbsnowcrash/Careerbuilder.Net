using CBApi.Framework.Requests;
using NUnit.Framework;
using System;
using System.Web;

namespace Tests.CBApi.framework.requests {
    [TestFixture]
    public class OAuthRedirectBuilderTest {
        [Test]
        public void Constructor_SetsClientID() {
            var request = new OAuthRedirectBuilderStub("ClientID", "redirectURI","","api.careerbuilder.com");
            Assert.AreEqual("ClientID", request.ClientId);
        }

        [Test]
        public void Constructor_SetsRedirectUri() {
            var request = new OAuthRedirectBuilderStub("ClientID", "redirectURI", "", "api.careerbuilder.com");
            Assert.AreEqual("redirectURI", request.RedirectUri);
        }

        [Test]
        public void Constructor_SetsDomain() {
            var request = new OAuthRedirectBuilderStub("ClientID", "redirectURI", "", "api.careerbuilder.com");
            Assert.AreEqual("api.careerbuilder.com", request.Domain);
        }

        [Test]
        public void Constructor_ThrowsException_WhenPassedNullOrEmptyClientID() {
            try {
                var request = new OAuthRedirectBuilderStub("", "redirectURI", "", "api.careerbuilder.com");
                Assert.Fail("Should have thrown exception");
            } catch (ArgumentNullException ex) {
                Assert.IsInstanceOfType(ex, typeof(ArgumentNullException));
            }

            try {
                var request = new OAuthRedirectBuilderStub(null, "redirectURI", "", "api.careerbuilder.com");
                Assert.Fail("Should have thrown exception");
            } catch (ArgumentNullException ex) {
                Assert.IsInstanceOfType(ex, typeof(ArgumentNullException));
            }
        }

        [Test]
        public void Constructor_ThrowsException_WhenPassedNullOrEmptyRedirectUri() {
            try {
                var request = new OAuthRedirectBuilderStub("ClientID", "", "", "api.careerbuilder.com");
                Assert.Fail("Should have thrown exception");
            } catch (ArgumentNullException ex) {
                Assert.IsInstanceOfType(ex, typeof(ArgumentNullException));
            }

            try {
                var request = new OAuthRedirectBuilderStub("ClientID", null, "", "api.careerbuilder.com");
                Assert.Fail("Should have thrown exception");
            } catch (ArgumentNullException ex) {
                Assert.IsInstanceOfType(ex, typeof(ArgumentNullException));
            }
        }

        [Test]
        public void Constructor_ThrowsException_WhenPassedNullOrEmptyDomain() {
            try {
                var request = new OAuthRedirectBuilderStub("ClientID", "redirectURI", "", "");
                Assert.Fail("Should have thrown exception");
            } catch (ArgumentNullException ex) {
                Assert.IsInstanceOf<ArgumentNullException>(ex);
            }

            try {
                var request = new OAuthRedirectBuilderStub("ClientID", "redirectURI", "", null);
                Assert.Fail("Should have thrown exception");
            } catch (ArgumentNullException ex) {
                Assert.IsInstanceOf<ArgumentNullException>(ex);
            }
        }

        [Test]
        public void OAuthUri_BuildsCorrectUriWithoutResources() {
            var request = new OAuthRedirectBuilderStub("ClientID",  HttpUtility.UrlEncode("https://jobseeker.careerbuilder.com/MyCareerBuilder"), "", "api.careerbuilder.com");
            var uri = request.OAuthUri();
            Assert.AreEqual("https://api.careerbuilder.com/auth/prompt?client_id=ClientID&redirect_uri=https%3a%2f%2fjobseeker.careerbuilder.com%2fMyCareerBuilder", uri.AbsoluteUri);
        }

        [Test]
        public void OAuthUri_BuildsCorrectUriWithResources() {
            var request = new OAuthRedirectBuilderStub("ClientID", HttpUtility.UrlEncode("https://jobseeker.careerbuilder.com/MyCareerBuilder"), "Resources", "api.careerbuilder.com");
            var uri = request.OAuthUri();
            Assert.AreEqual("https://api.careerbuilder.com/auth/prompt?client_id=ClientID&redirect_uri=https%3a%2f%2fjobseeker.careerbuilder.com%2fMyCareerBuilder&resources=Resources", uri.AbsoluteUri);
        }

      
    }

    internal class OAuthRedirectBuilderStub : OAuthRedirectBuilder {
        public OAuthRedirectBuilderStub(string clientId, string redirectUri, string additionalPermissions, string domain)
            : base(clientId, redirectUri,additionalPermissions, domain) {
        }

        public string ClientId {
            get { return _ClientId; }
            set { _ClientId = value; }
        }

        public string Domain {
            get { return _Domain; }
            set { _Domain = value; }
        }

        public string RedirectUri {
            get { return _RedirectURI; }
            set { _RedirectURI = value; }
        }
    }
}