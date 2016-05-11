using CBApi;
using CBApi.Framework.Requests;
using RestSharp;
using Tests.CBApi.models.service;

namespace Tests.CBApi.models.requests {
    internal class GetRequestStub : GetRequest {
        private string _BaseURL = "/Exammple";
        public GetRequestStub(string key, string domain, string cobrand, string siteid)
            : base(new APISettings() { DevKey = key, CobrandCode = cobrand, SiteId = siteid, TargetSite = new TargetSiteMock(domain) }) {
        }

        public GetRequestStub(APISettings settings)
            : base(settings) {

        }

        public new string GetRequestURL {
            get { return GetRequestURL(); }
        }

        public override string BaseUrl {
            get { return _BaseURL; }
        }

        public string SetBaseUrl {
            set { _BaseURL = value; }
        }


        public string DevKey {
            get { return _Settings.DevKey; }
        }

        public string Domain {
            get { return _Settings.TargetSite.Domain; }
        }

        public string CobrandCode {
            get { return _Settings.CobrandCode; }
        }

        public string SiteID {
            get { return _Settings.SiteId; }
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

        public void RunBeforeGet() {
            BeforeRequest();
        }
    }
}
