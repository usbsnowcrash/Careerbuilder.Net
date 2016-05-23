using CBApi.Models;
using CBApi.Models.Responses;
using CBApi.Models.Service;
using NUnit.Framework;
using Moq;
using RestSharp;
using Tests.CBApi.models.requests;

namespace Tests.CBApi.framework.requests {
    [TestFixture]
    public class JobSearchRequestTest {

        [Test]
        public void Constructor_DoesNotDefaultUSCountryCode() {
            var request = new JobSearchStub("DevKey", "api.careerbuilder.com", "", "");
            Assert.AreEqual("", request.CountryCode);
        }

        [Test]
        public void GetRequestURL_BuildsCorrectEndpointAddress() {
            var request = new JobSearchStub("DevKey", "api.careerbuilder.com", "", "");
            Assert.AreEqual("https://api.careerbuilder.com/v1/jobsearch", request.RequestURL);
        }

        [Test]
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
            ResponseJobSearch resp = request.WhereCountryCode("NL").Search();
            restReq.Verify();
            restClient.VerifyAll();
        }

        [Test]
        public void WhereClause_OverridesExistingParam() {
            //Setup
            var request = new JobSearchStub("DevKey", "api.careerbuilder.com", "", "");

            //Mock crap
            var response = new RestResponse<ResponseJobSearch> { Data = new ResponseJobSearch() };

            var restReq = new Mock<IRestRequest>();
            restReq.Setup(x => x.AddParameter("DeveloperKey", "DevKey"));
            restReq.Setup(x => x.AddParameter("CountryCode", "SE"));
            restReq.SetupSet(x => x.RootElement = "ResponseJobSearch");

            var restClient = new Mock<IRestClient>();
            restClient.SetupSet(x => x.BaseUrl = "https://api.careerbuilder.com/v1/jobsearch");
            restClient.Setup(x => x.Execute<ResponseJobSearch>(It.IsAny<IRestRequest>())).Returns(response);

            request.Request = restReq.Object;
            request.Client = restClient.Object;

            //Assert
            ResponseJobSearch resp = request.WhereCountryCode("NL").Where("CountryCode","SE").Search();
            restReq.Verify();
            restClient.VerifyAll();
        }

        [Test]
        public void WhereClause_AddsToOutgoingParams() {
            //Setup
            var request = new JobSearchStub("DevKey", "api.careerbuilder.com", "", "");

            //Mock crap
            var response = new RestResponse<ResponseJobSearch> { Data = new ResponseJobSearch() };

            var restReq = new Mock<IRestRequest>();
            restReq.Setup(x => x.AddParameter("DeveloperKey", "DevKey"));
            restReq.Setup(x => x.AddParameter("silly", "value"));
            restReq.SetupSet(x => x.RootElement = "ResponseJobSearch");

            var restClient = new Mock<IRestClient>();
            restClient.SetupSet(x => x.BaseUrl = "https://api.careerbuilder.com/v1/jobsearch");
            restClient.Setup(x => x.Execute<ResponseJobSearch>(It.IsAny<IRestRequest>())).Returns(response);

            request.Request = restReq.Object;
            request.Client = restClient.Object;

            //Assert
            ResponseJobSearch resp = request.Where("Silly", "value").Search();
            restReq.Verify();
            restClient.VerifyAll();
        }



        [Test]
        public void WhereCountryCode_ReturnsCategoryRequest() {
            var request = new JobSearchStub("DevKey", "api.careerbuilder.com", "", "");
            Assert.IsInstanceOf<IJobSearch>(request.WhereCountryCode("SE"));
        }

        [Test]
        public void WhereCountryCode_SetsCountryCode() {
            var request = new JobSearchStub("DevKey", "api.careerbuilder.com", "", "");
            request.WhereCountryCode("SE");
            Assert.AreEqual("SE", request.CountryCode);
        }

        [Test]
        public void WhereHostSite_ReturnsCategoryRequest() {
            var request = new JobSearchStub("DevKey", "api.careerbuilder.com", "", "");
            Assert.IsInstanceOf<IJobSearch>(request.WhereHostSite("EU"));
        }

        [Test]
        public void WhereHostSite_SetsCountryCode() {
            var request = new JobSearchStub("DevKey", "api.careerbuilder.com", "", "");
            request.WhereHostSite(HostSite.EU);
            Assert.AreEqual("EU", request.CountryCode);
        }

        [Test]
        public void WhereNotCompanyName_SetsCorrectParameter() {
            var jobSearch = new JobSearchStub("DevKey", "api.careerbuilder.com", "", "");
            jobSearch.WhereNotCompanyName("Coca Cola");
            jobSearch.AddParametersToRequest();
            var param = jobSearch.Request.Parameters.Find(qs => qs.Name == "excludecompanynames");
            Assert.IsNotNull(param, "ExcludeCompanyNames should exist.");
            Assert.AreEqual("Coca Cola", param.Value, "ExcludeCompanyNames value should be 'Coca Cola'");
        }

        [Test]
        public void WhereNotCompanyName_SetsCorrectParameter_Multiple() {
            var jobSearch = new JobSearchStub("DevKey", "api.careerbuilder.com", "", "");
            jobSearch.WhereNotCompanyName("Coca Cola", "Intel Rabbit Co");
            jobSearch.AddParametersToRequest();
            var param = jobSearch.Request.Parameters.Find(qs => qs.Name == "excludecompanynames");
            Assert.IsNotNull(param, "ExcludeCompanyNames should exist.");
            Assert.AreEqual("Coca Cola,Intel Rabbit Co", param.Value, "ExcludeCompanyNames value should be 'Coca Cola,Intel Rabbit Co'");
        }

        [Test]
        public void WhereNotCompanyName_SetsCorrectParameter_Empty() {
            var jobSearch = new JobSearchStub("DevKey", "api.careerbuilder.com", "", "");
            jobSearch.WhereNotCompanyName(" ");
            jobSearch.AddParametersToRequest();
            var param = jobSearch.Request.Parameters.Find(qs => qs.Name == "excludecompanynames");
            Assert.IsNull(param, "ExcludeCompanyNames should not exist.");
        }

        [Test]
        public void WhereNotJobTitle_SetsCorrectParameter() {
            var jobSearch = new JobSearchStub("DevKey", "api.careerbuilder.com", "", "");
            jobSearch.WhereNotJobTitle("Coca Cola");
            jobSearch.AddParametersToRequest();
            var param = jobSearch.Request.Parameters.Find(qs => qs.Name == "excludejobtitles");
            Assert.IsNotNull(param, "ExcludeJobTitles should exist.");
            Assert.AreEqual("Coca Cola", param.Value, "ExcludeJobTitles value should be 'Coca Cola'");
        }

        [Test]
        public void WhereNotJobTitle_SetsCorrectParameter_Multiple() {
            var jobSearch = new JobSearchStub("DevKey", "api.careerbuilder.com", "", "");
            jobSearch.WhereNotJobTitle("Coca Cola", "Intel Rabbit Co");
            jobSearch.AddParametersToRequest();
            var param = jobSearch.Request.Parameters.Find(qs => qs.Name == "excludejobtitles");
            Assert.IsNotNull(param, "ExcludeJobTitles should exist.");
            Assert.AreEqual("Coca Cola,Intel Rabbit Co", param.Value, "ExcludeJobTitles value should be 'Coca Cola,Intel Rabbit Co'");
        }

        [Test]
        public void WhereNotJobTitle_SetsCorrectParameter_Empty() {
            var jobSearch = new JobSearchStub("DevKey", "api.careerbuilder.com", "", "");
            jobSearch.WhereNotJobTitle(" ");
            jobSearch.AddParametersToRequest();
            var param = jobSearch.Request.Parameters.Find(qs => qs.Name == "excludejobtitles");
            Assert.IsNull(param, "ExcludeJobTitles should not exist.");
        }

        [Test]
        public void WhereNotKeywords_SetsCorrectParameter() {
            var jobSearch = new JobSearchStub("DevKey", "api.careerbuilder.com", "", "");
            jobSearch.WhereNotKeywords("Coca Cola");
            jobSearch.AddParametersToRequest();
            var param = jobSearch.Request.Parameters.Find(qs => qs.Name == "excludekeywords");
            Assert.IsNotNull(param, "ExcludeKeywords should exist.");
            Assert.AreEqual("Coca Cola", param.Value, "ExcludeKeywords value should be 'Coca Cola'");
        }

        [Test]
        public void WhereNotKeywords_SetsCorrectParameter_Multiple() {
            var jobSearch = new JobSearchStub("DevKey", "api.careerbuilder.com", "", "");
            jobSearch.WhereNotKeywords("Coca Cola", "Intel Rabbit Co");
            jobSearch.AddParametersToRequest();
            var param = jobSearch.Request.Parameters.Find(qs => qs.Name == "excludekeywords");
            Assert.IsNotNull(param, "ExcludeKeywords should exist.");
            Assert.AreEqual("Coca Cola,Intel Rabbit Co", param.Value, "ExcludeKeywords value should be 'Coca Cola,Intel Rabbit Co'");
        }

        [Test]
        public void WhereNotKeywords_SetsCorrectParameter_Empty() {
            var jobSearch = new JobSearchStub("DevKey", "api.careerbuilder.com", "", "");
            jobSearch.WhereNotKeywords(" ");
            jobSearch.AddParametersToRequest();
            var param = jobSearch.Request.Parameters.Find(qs => qs.Name == "excludekeywords");
            Assert.IsNull(param, "ExcludeKeywords should not exist.");
        }

        [Test]
        public void WhereGroupValue_SetsCorrectParameters() {
            var jobSearch = new JobSearchStub("DevKey", "api.careerbuilder.com", "", "");
            jobSearch.WhereGroupingValue("grouping value");
            jobSearch.AddParametersToRequest();
            var param_grouping_value = jobSearch.Request.Parameters.Find(qs => qs.Name == "groupingvalue").Value;
            var param_advanced_grouping = jobSearch.Request.Parameters.Find(qs => qs.Name == "advancedgroupingmode").Value;
            var param_enable_company_job_title_collapse = jobSearch.Request.Parameters.Find(qs => qs.Name == "enablecompanyjobtitlecollapse").Value;
            Assert.AreEqual("grouping value", param_grouping_value);
            Assert.AreEqual(false, param_advanced_grouping);
            Assert.AreEqual(false, param_enable_company_job_title_collapse);
        }

        [Test]
        public void WhereGroupValue_SetsCorrectParameters_WhenNotSet() {
            var jobSearch = new JobSearchStub("DevKey", "api.careerbuilder.com", "", "");
            jobSearch.AddParametersToRequest();
            var param_grouping_value = jobSearch.Request.Parameters.Find(qs => qs.Name == "groupingvalue");
            var param_advanced_grouping = jobSearch.Request.Parameters.Find(qs => qs.Name == "advancedgroupingmode").Value;
            var param_enable_company_job_title_collapse = jobSearch.Request.Parameters.Find(qs => qs.Name == "enablecompanyjobtitlecollapse").Value;
            Assert.IsNull(param_grouping_value);
            Assert.AreEqual(false,param_advanced_grouping);
            Assert.AreEqual(false,param_enable_company_job_title_collapse);
        }

        [Test]
        public void SetRecordsPerGroup() {
            var jobSearch = new JobSearchStub("DevKey", "api.careerbuilder.com", "", "");
            jobSearch.SetRecordsPerGroup(2); 
            jobSearch.AddParametersToRequest();
            var param_records_per_group = jobSearch.Request.Parameters.Find(qs => qs.Name == "recordspergroup").Value;
            Assert.AreEqual(2,param_records_per_group );
        }

        [Test]
        public void SetRecordsPerGroup_NotSet() {
            var jobSearch = new JobSearchStub("DevKey", "api.careerbuilder.com", "", "");
            jobSearch.AddParametersToRequest();
            var param_records_per_group = jobSearch.Request.Parameters.Find(qs => qs.Name == "recordspergroup");
            Assert.IsNull(param_records_per_group);
        }

    }
}