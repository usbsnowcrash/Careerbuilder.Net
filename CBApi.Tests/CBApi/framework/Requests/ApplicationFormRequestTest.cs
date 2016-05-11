//using CBApi;
//using CBApi.Framework.Requests;
//using CBApi.Models;
//using NUnit.Framework;
//using Moq;
//using RestSharp;
//using System;
//using Tests.CBApi.models.requests;
//using Tests.CBApi.models.service;

//namespace Tests.CBApi.framework.requests
//{
//    [TestFixture]
//    public class ApplicationFormRequestTest
//    {
//        private APISettings _Settings = new APISettings();
//        [Test]
//        public void Constructor_SetsJobDID()
//        {
//            var request = new ApplicationFormRequest("JXXXXXXXXXXXXXXXXXX", _Settings);
//            Assert.AreEqual("JXXXXXXXXXXXXXXXXXX", request.JobDID);
//        }

//        [Test]
//        public void Constructor_ThrowsException_WhenPassedNullOrEmpty()
//        {
//            try
//            {
//                var request = new BlankAppStub(null, "DevKey", "api.careerbuilder.com", "", "");
//                Assert.Fail("Should have thrown exception");
//            }
//            catch (ArgumentNullException ex)
//            {
//                Assert.IsInstanceOfType(ex, typeof (ArgumentNullException));
//            }

//            try
//            {
//                var request = new BlankAppStub("", "DevKey", "api.careerbuilder.com", "", "");
//                Assert.Fail("Should have thrown exception");
//            }
//            catch (ArgumentNullException ex)
//            {
//                Assert.IsInstanceOfType(ex, typeof (ArgumentNullException));
//            }
//        }

//        [Test]
//        public void Constructor_ThrowsException_WhenPassedBadJobDID()
//        {
//            try
//            {
//                var request = new BlankAppStub("UXXXXXXXXXXXXXXXXXXX", "DevKey", "api.careerbuilder.com", "", "");
//                Assert.Fail("Should have thrown exception");
//            }
//            catch (ArgumentException ex)
//            {
//                Assert.IsInstanceOfType(ex, typeof (ArgumentException));
//            }

//            try
//            {
//                var request = new BlankAppStub("JXXXXXXXXXXX", "DevKey", "api.careerbuilder.com", "", "");
//                Assert.Fail("Should have thrown exception");
//            }
//            catch (ArgumentException ex)
//            {
//                Assert.IsInstanceOfType(ex, typeof (ArgumentException));
//            }
//        }

//        [Test]
//        public void GetRequestURL_BuildsCorrectEndpointAddress()
//        {
//            var request = new BlankAppStub("JXXXXXXXXXXXXXXXXXX", "DevKey", "api.careerbuilder.com", "", "");
//            Assert.AreEqual("https://api.careerbuilder.com/v1/application/blank", request.RequestURL);
//        }

//        [Test]
//        public void Retrieve_PerformsCorrectRequest()
//        {
//            //Setup
//            var request = new BlankAppStub("JXXXXXXXXXXXXXXXXXX", "DevKey", "api.careerbuilder.com", "", "");

//            //Mock crap
//            var response = new RestResponse<BlankApplication> {Data = new BlankApplication()};

//            var restReq = new Mock<IRestRequest>();
//            restReq.Setup(x => x.AddParameter("DeveloperKey", "DevKey"));
//            restReq.Setup(x => x.AddParameter("JobDID", "JXXXXXXXXXXXXXXXXXX"));
//            restReq.SetupSet(x => x.RootElement = "BlankApplication");

//            var restClient = new Mock<IRestClient>();
//            restClient.SetupSet(x => x.BaseUrl = "https://api.careerbuilder.com/v1/application/blank");
//            restClient.Setup(x => x.Execute<BlankApplication>(It.IsAny<IRestRequest>())).Returns(response);

//            request.Request = restReq.Object;
//            request.Client = restClient.Object;

//            //Assert
//            BlankApplication resp = request.Retrieve();
//            restReq.VerifyAll();
//            restClient.VerifyAll();
//        }
//    }

//    internal class BlankAppStub : BlankApplicationRequest
//    {
//        public BlankAppStub(string jobDID, string key, string domain, string cobrand, string siteid)
//            : base(jobDID, new APISettings() { DevKey = key, CobrandCode = cobrand, SiteId = siteid, TargetSite = new TargetSiteMock(domain) })
//        {
//        }

//        public string JobDID
//        {
//            get { return JobDid; }
//        }

//        public string DevKey
//        {
//            get { return _Settings.DevKey; }
//        }

//        public string Domain
//        {
//            get { return _Settings.TargetSite.Domain; }
//        }

//        public string RequestURL
//        {
//            get { return base.GetRequestURL(); }
//        }

//        public IRestClient Client
//        {
//            get { return _client; }
//            set { _client = value; }
//        }

//        public IRestRequest Request
//        {
//            get { return _request; }
//            set { _request = value; }
//        }

//        protected override void CheckForErrors(IRestResponse response) {

//        }
//    }
//}