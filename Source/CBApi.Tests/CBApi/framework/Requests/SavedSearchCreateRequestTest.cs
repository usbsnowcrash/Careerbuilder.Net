using CBApi;
using CBApi.Framework.Requests;
using CBApi.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RestSharp;
using System;
using Tests.CBApi.models.requests;
using Tests.CBApi.models.service;

namespace Tests.CBApi.framework.requests
{
    [TestClass]
    public class SavedSearchCreateRequestTest
    {
        [TestMethod]
        public void Submit_PerformsCorrectRequest()
        {
            //setup
            var request = new SavedSearchCreateRequestStub("DevKey", "api.careerbuilder.com", "", "", 12345);
            var dummyApp = new SavedSearchCreate();
            dummyApp = SetUpApp(dummyApp);

            //Mock
            var response = new RestResponse<SavedSearchCreateResponse> { Data = new SavedSearchCreateResponse(), ResponseStatus = ResponseStatus.Completed };
            var restReq = new Mock<IRestRequest>();
            restReq.Setup(x => x.AddBody(dummyApp));

            

            var restClient = new Mock<IRestClient>();
            restClient.SetupSet(x => x.BaseUrl = "https://api.careerbuilder.com/v2/SavedSearch/Create");
            restClient.Setup(x => x.Execute<SavedSearchCreateResponse>(It.IsAny<IRestRequest>())).Returns(response);

            request.Request = restReq.Object;
            request.Client = restClient.Object;

            //Assertions
            SavedSearchCreateResponse rest = request.Submit(dummyApp);
            restReq.VerifyAll();
            restClient.VerifyAll();
        }

        private SavedSearchCreate SetUpApp(SavedSearchCreate dummyApp)
        {
            dummyApp.HostSite = "US";
            dummyApp.SearchName = "lotsloc";
            dummyApp.IsDailyEmail = "none";
            dummyApp.ExternalUserID = "Nicholas.Busby.Test@CareerBuilder.com";
            dummyApp.DeveloperKey = "WDJ16BN6CQB69FP18Y8F";
            dummyApp.SearchParameters = new SearchParameters();
            dummyApp.SearchParameters.Radius = 30;
            dummyApp.SearchParameters.PayHigh = 70;
            dummyApp.SearchParameters.PayLow = 40;
            dummyApp.SearchParameters.PostedWithin = 30;
            dummyApp.SearchParameters.PayInfoOnly = false;
            dummyApp.SearchParameters.Location = "Chicago, Il, Atlanta, Ga, New York, Ny";
            dummyApp.SearchParameters.OrderDirection = "ascending";
            dummyApp.SearchParameters.SpecificEducation = false;
            dummyApp.SearchParameters.ExcludeNational = false;
            dummyApp.SearchParameters.OrderBy = "Pay";
            return dummyApp;
        }
    }
}
