using CBApi;
using CBApi.Framework.Requests;
using CBApi.Models;
using NUnit.Framework;
using Moq;
using RestSharp;
using System;
using Tests.CBApi.models.requests;
using Tests.CBApi.models.service;

namespace Tests.CBApi.framework.requests
{
    [TestFixture]
    public class TargetSiteTest
    {
        

        [Test]
        public void GetRequestURL_BuildsCorrectEndpointAddress()
        {
            var site = new TargetSiteMock("10.0.0.1") { SetHost = "api.koolkid.com", SetSecure = true };
            var request = new BlankAppStub("JXXXXXXXXXXXXXXXXXX", "DevKey","","",site);
            Assert.AreEqual("https://10.0.0.1/v1/application/blank", request.RequestURL);
        }

        [Test]
        public void Retrieve_AddsProperHeaders()
        {
            //Setup
            var site = new TargetSiteMock("10.0.0.1") { SetHost = "api.koolkid.com", SetSecure = true };
            site.SetHeaders.Add("ILikeHeaders", "true");

            var request = new BlankAppStub("JXXXXXXXXXXXXXXXXXX", "DevKey", "api.careerbuilder.com", "",site);

            //Mock crap
            var response = new RestResponse<BlankApplication> {Data = new BlankApplication()};

            var restReq = new Mock<IRestRequest>();
            restReq.Setup(x => x.AddHeader("Host", "api.koolkid.com"));
            restReq.Setup(x => x.AddHeader("ILikeHeaders", "true"));
            restReq.Setup(x => x.AddParameter("DeveloperKey", "DevKey"));
            restReq.Setup(x => x.AddParameter("JobDID", "JXXXXXXXXXXXXXXXXXX"));
            restReq.SetupSet(x => x.RootElement = "BlankApplication");

            var restClient = new Mock<IRestClient>();
            restClient.SetupSet(x => x.BaseUrl = "https://10.0.0.1/v1/application/blank");
            restClient.Setup(x => x.Execute<BlankApplication>(It.IsAny<IRestRequest>())).Returns(response);

            request.Request = restReq.Object;
            request.Client = restClient.Object;

            //Assert
            BlankApplication resp = request.Retrieve();
            restReq.VerifyAll();
            restClient.VerifyAll();
        }
    }
}