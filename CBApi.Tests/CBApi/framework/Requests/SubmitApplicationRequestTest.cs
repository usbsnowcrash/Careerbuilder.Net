using CBApi;
using CBApi.Framework.Requests;
using CBApi.Models;
using NUnit.Framework;
using Moq;
using RestSharp;
using System;
using Tests.CBApi.models.requests;
using Tests.CBApi.models.service;

namespace Tests.CBApi.framework.requests {
    [TestFixture]
    public class SubmitApplicationRequestTest {
        
        [Test]
        public void Submit_PerformsCorrectRequest() {
            //Setup
            var request = new SubmitApplicationRequestStub("DevKey", "api.careerbuilder.com", "", "", 12345);
            var dummyApp = new RequestApplication();

            //Mock crap
            var response = new RestResponse<ResponseApplication> { Data = new ResponseApplication(),ResponseStatus = ResponseStatus.Completed };

            var restReq = new Mock<IRestRequest>();
            restReq.Setup(x => x.AddBody(dummyApp));

            var restClient = new Mock<IRestClient>();
            restClient.SetupSet(x => x.BaseUrl = "https://api.careerbuilder.com/v1/application/submit");
            restClient.Setup(x => x.Execute<ResponseApplication>(It.IsAny<IRestRequest>())).Returns(response);

            request.Request = restReq.Object;
            request.Client = restClient.Object;

            //Assert
            ResponseApplication resp = request.Submit(dummyApp);
            restReq.VerifyAll();
            restClient.VerifyAll();
        }
    }
}