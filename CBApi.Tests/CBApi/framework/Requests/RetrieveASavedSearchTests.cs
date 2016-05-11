using CBApi.Models.WebAPIs.SavedSearch;
using NUnit.Framework;
using Moq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tests.com.careerbuilder.CBApi.framework.stubrequests;

namespace Tests.com.careerbuilder.CBApi.framework.requests
{
    [TestFixture]
    public class RetrieveASavedSearchTests
    {
        [Test]
        public void Submit_PerformsCorrectRequest()
        {
            //setup
            var request = new RetrieveASavedSearchStub("DevKey", "api.careerbuilder.com", "", "", 12345, "savedSearchDID");

            //mock
            var response = new RestResponse<SavedSearches>
            {
                Data = new SavedSearches(),
                ResponseStatus = ResponseStatus.Completed
            };
            var restReq = new Mock<IRestRequest>();

            var restClient = new Mock<IRestClient>();
            restClient.SetupSet(x => x.BaseUrl = "https://api.careerbuilder.com/CBAPI/SavedSearches/savedSearchDID");
            restClient.Setup(x=> x.Execute<SavedSearches>(It.IsAny<IRestRequest>())).Returns(response);

            request.Request = restReq.Object;
            request.Client = restClient.Object;

            //assertions

            SavedSearches rest = request.Submit("userOAuthToken");
            restReq.VerifyAll();
            restClient.VerifyAll();
        }
    }
}
