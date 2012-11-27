﻿using System;
using System.Collections.Generic;
using CBApi.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RestSharp;
using Tests.CBApi.models.requests;

namespace Tests.CBApi.framework.requests {
    [TestClass]
    public class JobRecommendationsRequestTest {
        [TestMethod]
        public void Constructor_SetsExternalID() {
            var request = new JobRecRequestStub("J1234567890123456789", "DevKey", "api.careerbuilder.com", "", "");
            Assert.AreEqual("J1234567890123456789", request.JobDid);
        }

        [TestMethod]
        public void Constructor_ThrowsException_WhenPassedNullOrEmpty() {
            try {
                var request = new JobRecRequestStub(null, "DevKey", "api.careerbuilder.com", "", "");
                Assert.Fail("Should have thrown exception");
            } catch (ArgumentNullException ex) {
                Assert.IsInstanceOfType(ex, typeof(ArgumentNullException));
            }

            try {
                var request2 = new JobRecRequestStub("", "DevKey", "api.careerbuilder.com", "", "");
                Assert.Fail("Should have thrown exception");
            } catch (ArgumentNullException ex) {
                Assert.IsInstanceOfType(ex, typeof(ArgumentNullException));
            }

            try {
                var request3 = new JobRecRequestStub("NotAValidJobDid", "DevKey", "api.careerbuilder.com", "", "");
                Assert.Fail("Should have thrown exception");
            } catch (ArgumentException ex) {
                Assert.IsInstanceOfType(ex, typeof(ArgumentException));
            }
        }

        [TestMethod]
        public void GetRequestURL_BuildsCorrectEndpointAddress() {
            var request = new JobRecRequestStub("J1234567890123456789", "DevKey", "api.careerbuilder.com", "", "");
            Assert.AreEqual("https://api.careerbuilder.com/v1/recommendations/forjob", request.RequestURL);
        }

        [TestMethod]
        public void GetRecommendations_PerformsCorrectRequest() {
            //Setup
            var request = new JobRecRequestStub("J1234567890123456789", "DevKey", "api.careerbuilder.com", "", "");

            //Mock crap
            var response = new RestResponse<List<RecommendJobResult>> { Data = new List<RecommendJobResult>() };

            var restReq = new Mock<IRestRequest>();
            restReq.Setup(x => x.AddParameter("DeveloperKey", "DevKey"));
            restReq.Setup(x => x.AddParameter("JobDID", "J1234567890123456789"));
            restReq.SetupSet(x => x.RootElement = "RecommendJobResults");

            var restClient = new Mock<IRestClient>();
            restClient.SetupSet(x => x.BaseUrl = "https://api.careerbuilder.com/v1/recommendations/forjob");
            restClient.Setup(x => x.Execute<List<RecommendJobResult>>(It.IsAny<IRestRequest>())).Returns(response);

            request.Request = restReq.Object;
            request.Client = restClient.Object;

            //Assert
            List<RecommendJobResult> resp = request.GetRecommendations();
            restReq.VerifyAll();
            restClient.VerifyAll();
        }
    }
}
