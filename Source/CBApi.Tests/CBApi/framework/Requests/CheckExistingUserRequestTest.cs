using CBApi;
using CBApi.Framework.Requests;
using CBApi.Models.CheckExisting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RestSharp;
using System;
using Tests.CBApi.models.requests;
using Tests.CBApi.models.service;

namespace Tests.CBApi.framework.requests {
    [TestClass]
    public class CheckExistingUserRequestTest {
        
        [TestMethod]
        public void GetUserCheck_PerformsCorrectRequest() {
            //Setup
            var request = new CheckExistingUserRequestStub("DevKey", "api.careerbuilder.com", "", "", 12345);
            var dummyApp = new Request();

            //Mock crap
            var response = new RestResponse<ResponseUserCheck> { Data = new ResponseUserCheck(), ResponseStatus = ResponseStatus.Completed };

            var restReq = new Mock<IRestRequest>();
            restReq.Setup(x => x.AddBody(dummyApp));

            var restClient = new Mock<IRestClient>();
            restClient.SetupSet(x => x.BaseUrl = "https://api.careerbuilder.com/v2/user/checkexisting");
            restClient.Setup(x => x.Execute<ResponseUserCheck>(It.IsAny<IRestRequest>())).Returns(response);

            request.Request = restReq.Object;
            request.Client = restClient.Object;

            //Assert
            ResponseUserCheck resp = request.GetUserCheck(dummyApp);
            restReq.VerifyAll();
            restClient.VerifyAll();
        }
    }
}