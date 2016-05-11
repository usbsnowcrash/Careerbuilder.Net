using CBApi;
using CBApi.Framework.Requests;
using RestSharp;
using Tests.CBApi.models.service;

namespace Tests.CBApi.models.requests {
    internal class JobSearchStub : JobSearchRequest {

        public IRestClient Client {
            get { return _client; }
            set { _client = value; }
        }

        public string CountryCode {
            get { return _CountryCode; }
        }

        public string DevKey {
            get { return _Settings.DevKey; }
        }

        public string Domain {
            get { return _Settings.TargetSite.Domain; }
        }

        public IRestRequest Request {
            get { return _request; }
            set { _request = value; }
        }

        public string RequestURL {
            get { return base.GetRequestURL(); }
        }

        public JobSearchStub(string key, string domain, string cobrand, string siteid)
            : base(new APISettings() { DevKey = key, CobrandCode = cobrand, SiteId = siteid, TargetSite = new TargetSiteMock(domain) }) {
        }

        protected override void CheckForErrors(IRestResponse response) {
        }

        public new void AddParametersToRequest() {
            base.AddParametersToRequest();
        }

    }
}