using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CBApi;
using CBApi.Framework.Requests;
using CBApi.Models;
using CBApi.Models.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RestSharp;
using Tests.CBApi.models.requests;
using Tests.CBApi.models.service;

namespace Tests.CBApi.framework.requests
{
    [TestClass]
    public class EducationCodesRequestTest
    {
        [TestMethod]
        public void Constructor_DefaultsToUSCountryCode()
        {
            EducationCodesRequestStub target = new EducationCodesRequestStub("DevKey", "api.careerbuilder.com", "", "");
            Assert.AreEqual("US", target.CountryCode);
        }

        [TestMethod]
        public void GetRequestURL_BuildsCorrectEndpointAddress()
        {
            EducationCodesRequestStub target = new EducationCodesRequestStub("DevKey", "api.careerbuilder.com", "", "");
            Assert.AreEqual("https://api.careerbuilder.com/v1/educationcodes", target.RequestURL);
        }

        [TestMethod]
        public void WhereCountryCode_IsFluent()
        {
            IEducationCodesRequest target = new EducationCodesRequestStub("DevKey", "api.careerbuilder.com", "", "");

            IEducationCodesRequest actual = target.WhereCountryCode(CountryCode.SE);

            Assert.AreEqual(actual, target);
        }

        [TestMethod]
        public void WhereCountryCode_SetsCountryCode()
        {
            EducationCodesRequestStub request = new EducationCodesRequestStub("DevKey", "api.careerbuilder.com", "", "");

            request.WhereCountryCode(CountryCode.SE);

            Assert.AreEqual("SE", request.CountryCode);
        }

        [TestMethod]
        public void ListAll_PerformsCorrectRequest()
        {
            EducationCodesRequestStub target = new EducationCodesRequestStub("DevKey", "api.careerbuilder.com", "", "");
            RestResponse<List<Education>> response = new RestResponse<List<Education>> { Data = new List<Education>() };
            Mock<IRestRequest> request = new Mock<IRestRequest>();
            request.Setup(x => x.AddParameter("DeveloperKey", "DevKey"));
            request.Setup(x => x.AddParameter("CountryCode", "NL"));
            request.SetupSet(x => x.RootElement = "EducationCodes");
            Mock<IRestClient> restClient = new Mock<IRestClient>();
            restClient.SetupSet(x => x.BaseUrl = "https://api.careerbuilder.com/v1/educationcodes");
            restClient.Setup(x => x.Execute<List<Education>>(It.IsAny<IRestRequest>())).Returns(response);
            target.Request = request.Object;
            target.Client = restClient.Object;

            List<Education> educationCodes = target.WhereCountryCode(CountryCode.NL).ListAll();

            Assert.IsTrue(educationCodes.Count == 0);
            request.VerifyAll();
            restClient.VerifyAll();
        }
    }
}
