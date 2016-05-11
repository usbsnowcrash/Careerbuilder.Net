using CBApi.Models;
using NUnit.Framework;
using Moq;
using RestSharp;
using Tests.CBApi.models.requests;

namespace Tests.CBApi.framework.requests
{
    [TestFixture]
    public class SavedSearchUpdateRequestTest
    {
        [Test]
        public void Submit_PerformsCorrectRequest()
        {
            //setup
            var request = new SavedSearchUpdateRequestStub("DevKey", "api.careerbuilder.com", "", "", 12345);
            var dummyApp = new SavedSearchUpdateRequestModel();

            //Mock
            var response = new RestResponse<SavedSearchUpdateResponseModel> { Data = new SavedSearchUpdateResponseModel(), ResponseStatus = ResponseStatus.Completed };
            var restReq = new Mock<IRestRequest>();
            restReq.Setup(x => x.AddBody(dummyApp));



            var restClient = new Mock<IRestClient>();
            restClient.SetupSet(x => x.BaseUrl = "https://api.careerbuilder.com/v2/SavedSearch/Update");
            restClient.Setup(x => x.Execute<SavedSearchUpdateResponseModel>(It.IsAny<IRestRequest>())).Returns(response);

            request.Request = restReq.Object;
            request.Client = restClient.Object;

            //Assertions
            SavedSearchUpdateResponseModel rest = request.Submit(dummyApp);
            restReq.VerifyAll();
            restClient.VerifyAll();
        }
    }
}
