using CBApi.Models;
using NUnit.Framework;
using Moq;
using RestSharp;
using Tests.CBApi.models.requests;

namespace Tests.CBApi.framework.requests
{
    [TestFixture]
    public class SavedSearchRetrieveRequestTest
    {
        [Test]
        public void Submit_PerformsCorrectRequest()
        {
            //setup
            var request = new SavedSearchRetrieveRequestStub("DevKey", "api.careerbuilder.com", "", "", 12345);
            var dummyApp = new SavedSearchRetrieveRequestModel();

            //Mock
            var response = new RestResponse<SavedSearchRetrieveResponseModel> { Data = new SavedSearchRetrieveResponseModel(), ResponseStatus = ResponseStatus.Completed };
            var restReq = new Mock<IRestRequest>();

            var restClient = new Mock<IRestClient>();
            restClient.SetupSet(x => x.BaseUrl = "https://api.careerbuilder.com/v1/SavedSearch/retrieve");
            restClient.Setup(x => x.Execute<SavedSearchRetrieveResponseModel>(It.IsAny<IRestRequest>())).Returns(response);

            request.Request = restReq.Object;
            request.Client = restClient.Object;

            //Assertions
            SavedSearchRetrieveResponseModel rest = request.Submit(dummyApp);
            restReq.VerifyAll();
            restClient.VerifyAll();
        }
    }
}
