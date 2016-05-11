using CBApi.Models;
using NUnit.Framework;
using Moq;
using RestSharp;
using System;
using Tests.CBApi.models.requests;

namespace Tests.CBApi.framework.requests
{
    [TestFixture]
    public class JobRequestTest
    {
        [Test]
        public void Constructor_ThrowsException_WhenPassedBlankJobDID()
        {
            try
            {
                var request = new JobRequestStub("", "DevKey", "api.careerbuilder.com", "", "");
                Assert.Fail();
            }
            catch (ArgumentNullException ex)
            {
                Assert.IsInstanceOf<ArgumentNullException>(ex);
            }
        }

        [Test]
        public void Constructor_ThrowsException_WhenPassedNullJobDID()
        {
            try
            {
                var request = new JobRequestStub(null, "DevKey", "api.careerbuilder.com", "", "");
                Assert.Fail();
            }
            catch (ArgumentNullException ex)
            {
                Assert.IsInstanceOf<ArgumentNullException>(ex);
            }
        }

        [Test]
        public void Constructor_ThrowsException_WhenPassedShortJobDID()
        {
            try
            {
                var request = new JobRequestStub("J12345678910", "DevKey", "api.careerbuilder.com", "", "");
                Assert.Fail();
            }
            catch (ArgumentException ex)
            {
                Assert.IsInstanceOf<ArgumentException>(ex);
            }
        }

        [Test]
        public void Constructor_ThrowsException_WhenPassedLongJobDID()
        {
            try
            {
                var request = new JobRequestStub("J12345678901234567890123456789", "DevKey", "api.careerbuilder.com", "",
                                                 "");
                Assert.Fail();
            }
            catch (ArgumentException ex)
            {
                Assert.IsInstanceOf<ArgumentException>(ex);
            }
        }

        [Test]
        public void Constructor_ThrowsException_WhenPassedBadJobDID()
        {
            try
            {
                var request = new JobRequestStub("W3T1SK6PN85V725Z6Q3", "DevKey", "api.careerbuilder.com", "", "");
                Assert.Fail();
            }
            catch (ArgumentException ex)
            {
                Assert.IsInstanceOf<ArgumentException>(ex);
            }
        }

        [Test]
        public void Constructor_SetsJobDID()
        {
            try
            {
                var request = new JobRequestStub("J3T1SK6PN85V725Z6Q3", "DevKey", "api.careerbuilder.com", "", "");
                Assert.AreEqual("J3T1SK6PN85V725Z6Q3", request.JobDID);
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }


        [Test]
        public void Retrieve_PerformsCorrectRequest()
        {
            //Setup
            var request = new JobRequestStub("J3T1SK6PN85V725Z6Q3", "DevKey", "api.careerbuilder.com", "", "");

            //Mock crap
            var response = new RestResponse<Job> {Data = new Job {JobTitle = "Jeff's crazy job imporieum"}};

            var restReq = new Mock<IRestRequest>();
            restReq.Setup(x => x.AddParameter("DeveloperKey", "DevKey"));
            restReq.Setup(x => x.AddParameter("DID", "J3T1SK6PN85V725Z6Q3"));
            restReq.SetupSet(x => x.RootElement = "Job");

            var restClient = new Mock<IRestClient>();
            restClient.SetupSet(x => x.BaseUrl = "https://api.careerbuilder.com/v1/job");
            restClient.Setup(x => x.Execute<Job>(It.IsAny<IRestRequest>())).Returns(response);

            request.Request = restReq.Object;
            request.Client = restClient.Object;

            //Assert
            Job myJob = request.Retrieve();
            Assert.AreSame(response.Data, myJob);
            restReq.VerifyAll();
            restClient.VerifyAll();
        }
    }
}