using CBApi;
using CBApi.Framework.Requests;
using RestSharp;
using Tests.CBApi.models.service;

namespace Tests.CBApi.framework.requests {
    internal class BlankAppStub : BlankApplicationRequest {
        public BlankAppStub(string jobDID, string key, string domain, string cobrand, string siteid)
            : base(jobDID, new APISettings() { DevKey = key, CobrandCode = cobrand, SiteId = siteid, TargetSite = new TargetSiteMock(domain) }) {
        }

        public BlankAppStub(string jobDID, string key, string cobrand, string siteid, TargetSiteMock site)
            : base(jobDID, new APISettings() { DevKey = key, CobrandCode = cobrand, SiteId = siteid, TargetSite = site }) {
        }



        public string JobDID {
            get { return JobDid; }
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

        protected override void CheckForErrors(IRestResponse response) {

        }
    }
}
