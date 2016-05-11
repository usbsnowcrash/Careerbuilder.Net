using CBApi.Models;
using NUnit.Framework;
using Moq;
using RestSharp;
using Tests.CBApi.models.requests;

namespace Tests.CBApi.framework.requests
{
    [TestFixture]
    public class SavedSearchListRequestTest
    {
        [Test]
        public void Submit_PerformsCorrectRequest()
        {
            //setup
            var request = new SavedSearchListRequestStub("DevKey", "api.careerbuilder.com", "", "", 12345);
            var dummyApp = new SavedSearchListRequestModel();

            //Mock
            var response = new RestResponse<SavedSearchListResponseModel> { Data = new SavedSearchListResponseModel(), ResponseStatus = ResponseStatus.Completed };
            var restReq = new Mock<IRestRequest>();
            
            var restClient = new Mock<IRestClient>();
            restClient.SetupSet(x => x.BaseUrl = "https://api.careerbuilder.com/v1/SavedSearch/List");
            restClient.Setup(x => x.Execute<SavedSearchListResponseModel>(It.IsAny<IRestRequest>())).Returns(response);

            request.Request = restReq.Object;
            request.Client = restClient.Object;

            //Assertions
            SavedSearchListResponseModel rest = request.Submit(dummyApp);
            restReq.VerifyAll();
            restClient.VerifyAll();
        }
    }
}
