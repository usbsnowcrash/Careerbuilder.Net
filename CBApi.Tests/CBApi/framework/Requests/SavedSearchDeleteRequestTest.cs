using System;
using NUnit.Framework;
using CBApi;
using CBApi.Framework.Requests;
using CBApi.Models;
using Moq;
using RestSharp;
using Tests.CBApi.models.requests;
using Tests.CBApi.models.service;

namespace Tests.CBApi.framework.requests {
    
    [TestFixture]
    public class SavedSearchDeleteRequestTest
    {
        [Test]
        public void Submit_DeleteSavedSearchRequest() {

            var request = new SavedSearchDeleteRequestStub("DevKey", "api.careerbuilder.com", "", "", 12345);
            var dummyDelete = new RequestSavedSearchDelete();
            dummyDelete.HostSite = "US";
            dummyDelete.ExternalUserID = "Nicholas.Busby.Test@CareerBuilder.com";
            dummyDelete.DeveloperKey = "WDJ16BN6CQB69FP18Y8F";
            dummyDelete.ExternalID = "test";

            var response = new RestResponse<SavedSearchDeleteResponse> { Data = new SavedSearchDeleteResponse(), ResponseStatus = ResponseStatus.Completed };

            var restReq = new Mock<IRestRequest>();
            restReq.Setup(x => x.AddBody(dummyDelete));

            var restClient = new Mock<IRestClient>();
            restClient.SetupSet(x => x.BaseUrl = "https://api.careerbuilder.com/v1/savedsearch/delete.xml");
            restClient.Setup(x => x.Execute<SavedSearchDeleteResponse>(It.IsAny<IRestRequest>())).Returns(response);

            request.Request = restReq.Object;
            request.Client = restClient.Object;

            SavedSearchDeleteResponse resp = request.Submit(dummyDelete);
            restReq.VerifyAll();
            restClient.VerifyAll();
        }
    }
}
