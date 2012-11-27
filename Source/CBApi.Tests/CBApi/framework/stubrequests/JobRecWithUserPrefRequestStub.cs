using CBApi;
using CBApi.Framework.Requests;
using RestSharp;
using Tests.CBApi.models.service;

namespace Tests.CBApi.models.requests {
    internal class JobRecWithUserPrefRequestStub : JobRecommendationsWithUserPreferencesRequest {
        public JobRecWithUserPrefRequestStub(string jobDid, string userDid, string key, string domain, string cobrand, string siteid)
            : base(jobDid, userDid, new APISettings() { DevKey = key, CobrandCode = cobrand, SiteId = siteid, TargetSite = new TargetSiteMock(domain) }) {
        }

        public string JobDid {
            get { return _jobDid; }
        }

        public string UserDid {
            get { return _userDid; }
        }

        public string DevKey {
            get { return _Settings.DevKey; }
        }

        public string Domain {
            get { return _Settings.TargetSite.Domain; }
        }

        public string RequestURL {
            get { return base.GetRequestURL(); }
        }

        public IRestClient Client {
            get { return _client; }
            set { _client = value; }
        }

        public IRestRequest Request {
            get { return _request; }
            set { _request = value; }
        }

        protected override void CheckForErrors(IRestResponse response) { }
    }
}
