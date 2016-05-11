using CBApi;
using NUnit.Framework;
using Moq;
using RestSharp;
using System;
using System.Collections.Specialized;

namespace Tests.com.careerbuilder.CBApi.framework.requests {
    [TestFixture]
    public class ApplyLinkRequestTest {
        [Test]
        public void Retrieve_Returns_A_Uri_String_With_JobDID()
        {
            var request = new ApplyLinkRequestStub(new NameValueCollection() { { "JobDID", "J3H2GZ67DPGRQV8287H" } },
                new APISettings() { DevKey = "DevKey" });
            var restReq = new Mock<IRestClient>();
            var response = new RestResponse<String>();
            response.ResponseUri = new Uri("http://www.careerbuilder.com/jobseeker/applyonline/applybegin.aspx?JobDID=J3H2GZ67DPGRQV8287H&Job_DID=J3H2GZ67DPGRQV8287H&_ga=1.29579389.319760682.1459515366&IPath=CJR_AB");
            restReq.Setup(x => x.Execute(It.IsAny<IRestRequest>())).Returns(response);
            request.Client = restReq.Object;

            Assert.AreEqual("http://www.careerbuilder.com/jobseeker/applyonline/applybegin.aspx?JobDID=J3H2GZ67DPGRQV8287H&Job_DID=J3H2GZ67DPGRQV8287H&_ga=1.29579389.319760682.1459515366&IPath=CJR_AB", request.Retrieve());
        }
    }
}
