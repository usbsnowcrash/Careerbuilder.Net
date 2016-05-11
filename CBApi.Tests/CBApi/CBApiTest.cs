using NUnit.Framework;
using System.Collections.Specialized;
using CBApi;
using CBApi.Models;
using CBApi.Models.Service;

namespace Tests.CBApi
{
    [TestFixture]
    public class CbApiTest
    {
        [Test]
        public void Constructor_DefaultsToCareerbuilderCom()
        {
            var svc = new CBApiStub();
            Assert.IsInstanceOfType(svc.Site, typeof (CareerBuilderCom));
        }

        [Test]
        public void GetCategories_ReturnsCategoriesRequest()
        {
            var svc = new CbApi();
            Assert.IsInstanceOfType(svc.GetCategories(), typeof (ICategoryRequest));
        }

        [Test]
        public void GetEmployeeTypes_ReturnsEmpRequest()
        {
            var svc = new CbApi();
            Assert.IsInstanceOfType(svc.GetEmployeeTypes(), typeof (IEmployeeTypesRequest));
        }

        //[Test]
        //public void GetBlankApplication_ReturnsBlankApplication() {
        //    ICBApi svc = new CbApi();
        //    Assert.IsInstanceOf<BlankApplication>(svc.GetBlankApplication(""));
        //}

        [Test]
        public void Application_Link_Request_With_NameValueCollection_Returns_String()
        {
            var svc = new CbApi();
            Assert.IsInstanceOf<string>(svc.ApplyLink(new NameValueCollection()));
        }

        [Test]
        public void Application_Link_Request_With_ApplyLink_Returns_String() {
            var svc = new CbApi();
            Assert.IsInstanceOf<string>(svc.ApplyLink(new ApplyLink()));
        }

        [Test]
        public void JobSearch_ReturnsJobSearchRequest()
        {
            var svc = new CbApi();
            Assert.IsInstanceOfType(svc.JobSearch(), typeof (IJobSearch));
        }
    }

    public class CBApiStub : CbApi
    {
        public TargetSite Site
        {
            get { return _Settings.TargetSite; }
        }
    }
}