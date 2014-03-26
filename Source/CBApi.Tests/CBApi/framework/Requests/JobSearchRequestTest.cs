using CBApi.Models;
using CBApi.Models.Responses;
using CBApi.Models.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RestSharp;
using Tests.CBApi.models.requests;

namespace Tests.CBApi.framework.requests {
    [TestClass]
    public class JobSearchRequestTest {

        [TestMethod]
        public void Constructor_DoesNotDefaultUSCountryCode() {
            var request = new JobSearchStub("DevKey", "api.careerbuilder.com", "", "");
            Assert.AreEqual("", request.CountryCode);
        }

        [TestMethod]
        public void GetRequestURL_BuildsCorrectEndpointAddress() {
            var request = new JobSearchStub("DevKey", "api.careerbuilder.com", "", "");
            Assert.AreEqual("https://api.careerbuilder.com/v1/jobsearch", request.RequestURL);
        }

        [TestMethod]
        public void Search_PerformsCorrectRequest() {
            //Setup
            var request = new JobSearchStub("DevKey", "api.careerbuilder.com", "", "");

            //Mock crap
            var response = new RestResponse<ResponseJobSearch> { Data = new ResponseJobSearch() };

            var restReq = new Mock<IRestRequest>();
            restReq.Setup(x => x.AddParameter("DeveloperKey", "DevKey"));
            restReq.Setup(x => x.AddParameter("CountryCode", "NL"));
            restReq.SetupSet(x => x.RootElement = "ResponseJobSearch");

            var restClient = new Mock<IRestClient>();
            restClient.SetupSet(x => x.BaseUrl = "https://api.careerbuilder.com/v1/jobsearch");
            restClient.Setup(x => x.Execute<ResponseJobSearch>(It.IsAny<IRestRequest>())).Returns(response);

            request.Request = restReq.Object;
            request.Client = restClient.Object;

            //Assert
            ResponseJobSearch resp = request.WhereCountryCode(CountryCode.NL).Search();
            restReq.Verify();
            restClient.VerifyAll();
        }

        [TestMethod]
        public void WhereCountryCode_ReturnsCategoryRequest() {
            var request = new JobSearchStub("DevKey", "api.careerbuilder.com", "", "");
            Assert.IsInstanceOfType(request.WhereCountryCode(CountryCode.SE), typeof(IJobSearch));
        }

        [TestMethod]
        public void WhereCountryCode_SetsCountryCode() {
            var request = new JobSearchStub("DevKey", "api.careerbuilder.com", "", "");
            request.WhereCountryCode(CountryCode.SE);
            Assert.AreEqual("SE", request.CountryCode);
        }

        [TestMethod]
        public void WhereHostSite_ReturnsCategoryRequest() {
            var request = new JobSearchStub("DevKey", "api.careerbuilder.com", "", "");
            Assert.IsInstanceOfType(request.WhereHostSite(HostSite.EU), typeof(IJobSearch));
        }

        [TestMethod]
        public void WhereHostSite_SetsCountryCode() {
            var request = new JobSearchStub("DevKey", "api.careerbuilder.com", "", "");
            request.WhereHostSite(HostSite.EU);
            Assert.AreEqual("EU", request.CountryCode);
        }

        [TestMethod]
        public void WhereNotCompanyName_SetsCorrectParameter() {
            var jobSearch = new JobSearchStub("DevKey", "api.careerbuilder.com", "", "");
            jobSearch.WhereNotCompanyName("Coca Cola");
            jobSearch.AddParametersToRequest();
            var param = jobSearch.Request.Parameters.Find(qs => qs.Name == "ExcludeCompanyNames");
            Assert.IsNotNull(param, "ExcludeCompanyNames should exist.");
            Assert.AreEqual("Coca Cola", param.Value, "ExcludeCompanyNames value should be 'Coca Cola'");
        }

        [TestMethod]
        public void WhereNotCompanyName_SetsCorrectParameter_Multiple() {
            var jobSearch = new JobSearchStub("DevKey", "api.careerbuilder.com", "", "");
            jobSearch.WhereNotCompanyName("Coca Cola", "Intel Rabbit Co");
            jobSearch.AddParametersToRequest();
            var param = jobSearch.Request.Parameters.Find(qs => qs.Name == "ExcludeCompanyNames");
            Assert.IsNotNull(param, "ExcludeCompanyNames should exist.");
            Assert.AreEqual("Coca Cola,Intel Rabbit Co", param.Value, "ExcludeCompanyNames value should be 'Coca Cola,Intel Rabbit Co'");
        }

        [TestMethod]
        public void WhereNotCompanyName_SetsCorrectParameter_Empty() {
            var jobSearch = new JobSearchStub("DevKey", "api.careerbuilder.com", "", "");
            jobSearch.WhereNotCompanyName(" ");
            jobSearch.AddParametersToRequest();
            var param = jobSearch.Request.Parameters.Find(qs => qs.Name == "ExcludeCompanyNames");
            Assert.IsNull(param, "ExcludeCompanyNames should not exist.");
        }

        [TestMethod]
        public void WhereNotJobTitle_SetsCorrectParameter() {
            var jobSearch = new JobSearchStub("DevKey", "api.careerbuilder.com", "", "");
            jobSearch.WhereNotJobTitle("Coca Cola");
            jobSearch.AddParametersToRequest();
            var param = jobSearch.Request.Parameters.Find(qs => qs.Name == "ExcludeJobTitles");
            Assert.IsNotNull(param, "ExcludeJobTitles should exist.");
            Assert.AreEqual("Coca Cola", param.Value, "ExcludeJobTitles value should be 'Coca Cola'");
        }

        [TestMethod]
        public void WhereNotJobTitle_SetsCorrectParameter_Multiple() {
            var jobSearch = new JobSearchStub("DevKey", "api.careerbuilder.com", "", "");
            jobSearch.WhereNotJobTitle("Coca Cola", "Intel Rabbit Co");
            jobSearch.AddParametersToRequest();
            var param = jobSearch.Request.Parameters.Find(qs => qs.Name == "ExcludeJobTitles");
            Assert.IsNotNull(param, "ExcludeJobTitles should exist.");
            Assert.AreEqual("Coca Cola,Intel Rabbit Co", param.Value, "ExcludeJobTitles value should be 'Coca Cola,Intel Rabbit Co'");
        }

        [TestMethod]
        public void WhereNotJobTitle_SetsCorrectParameter_Empty() {
            var jobSearch = new JobSearchStub("DevKey", "api.careerbuilder.com", "", "");
            jobSearch.WhereNotJobTitle(" ");
            jobSearch.AddParametersToRequest();
            var param = jobSearch.Request.Parameters.Find(qs => qs.Name == "ExcludeJobTitles");
            Assert.IsNull(param, "ExcludeJobTitles should not exist.");
        }

        [TestMethod]
        public void WhereNotKeywords_SetsCorrectParameter() {
            var jobSearch = new JobSearchStub("DevKey", "api.careerbuilder.com", "", "");
            jobSearch.WhereNotKeywords("Coca Cola");
            jobSearch.AddParametersToRequest();
            var param = jobSearch.Request.Parameters.Find(qs => qs.Name == "ExcludeKeywords");
            Assert.IsNotNull(param, "ExcludeKeywords should exist.");
            Assert.AreEqual("Coca Cola", param.Value, "ExcludeKeywords value should be 'Coca Cola'");
        }

        [TestMethod]
        public void WhereNotKeywords_SetsCorrectParameter_Multiple() {
            var jobSearch = new JobSearchStub("DevKey", "api.careerbuilder.com", "", "");
            jobSearch.WhereNotKeywords("Coca Cola", "Intel Rabbit Co");
            jobSearch.AddParametersToRequest();
            var param = jobSearch.Request.Parameters.Find(qs => qs.Name == "ExcludeKeywords");
            Assert.IsNotNull(param, "ExcludeKeywords should exist.");
            Assert.AreEqual("Coca Cola,Intel Rabbit Co", param.Value, "ExcludeKeywords value should be 'Coca Cola,Intel Rabbit Co'");
        }

        [TestMethod]
        public void WhereNotKeywords_SetsCorrectParameter_Empty() {
            var jobSearch = new JobSearchStub("DevKey", "api.careerbuilder.com", "", "");
            jobSearch.WhereNotKeywords(" ");
            jobSearch.AddParametersToRequest();
            var param = jobSearch.Request.Parameters.Find(qs => qs.Name == "ExcludeKeywords");
            Assert.IsNull(param, "ExcludeKeywords should not exist.");
        }

        [TestMethod]
        public void WhereGroupValue_SetsCorrectParameters() {
            var jobSearch = new JobSearchStub("DevKey", "api.careerbuilder.com", "", "");
            jobSearch.WhereGroupingValue("grouping value");
            jobSearch.AddParametersToRequest();
            var param_grouping_value = jobSearch.Request.Parameters.Find(qs => qs.Name == "GroupingValue").Value;
            var param_advanced_grouping = jobSearch.Request.Parameters.Find(qs => qs.Name == "AdvancedGroupingMode").Value;
            var param_enable_company_job_title_collapse = jobSearch.Request.Parameters.Find(qs => qs.Name == "EnableCompanyJobTitleCollapse").Value;
            Assert.AreEqual("grouping value", param_grouping_value);
            Assert.AreEqual(false, param_advanced_grouping);
            Assert.AreEqual(false, param_enable_company_job_title_collapse);
        }

        [TestMethod]
        public void WhereGroupValue_SetsCorrectParameters_WhenNotSet() {
            var jobSearch = new JobSearchStub("DevKey", "api.careerbuilder.com", "", "");
            jobSearch.AddParametersToRequest();
            var param_grouping_value = jobSearch.Request.Parameters.Find(qs => qs.Name == "GroupingValue");
            var param_advanced_grouping = jobSearch.Request.Parameters.Find(qs => qs.Name == "AdvancedGroupingMode");
            var param_enable_company_job_title_collapse = jobSearch.Request.Parameters.Find(qs => qs.Name == "EnableCompanyJobTitleCollapse");
            Assert.IsNull(param_grouping_value);
            Assert.IsNull(param_advanced_grouping);
            Assert.IsNull(param_enable_company_job_title_collapse);
        }

    }
}