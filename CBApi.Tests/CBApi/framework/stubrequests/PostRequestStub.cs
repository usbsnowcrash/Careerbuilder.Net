using CBApi;
using CBApi.Framework.Requests;
using RestSharp;
using Tests.CBApi.models.service;

namespace Tests.CBApi.models.requests {
    internal class PostRequestStub : PostRequest {
        private string _BaseURL = "/Exammple";
        public PostRequestStub(string key, string domain, string cobrand, string siteid,int timeout)
            : base(new APISettings() { DevKey = key, CobrandCode = cobrand, SiteId = siteid, TargetSite = new TargetSiteMock(domain),TimeoutMS = timeout }) {
        }

        public PostRequestStub(APISettings settings)
            : base(settings) {
        }

        public override string BaseUrl {
            get { return _BaseURL; }
        }

        public string SetBaseUrl {
            set { _BaseURL = value; }
        }

        public string GetRequestURL {
            get { return PostRequestURL(); }
        }

        public IRestClient Client {
            get { return _client; }
            set { _client = value; }
        }

        public IRestRequest Request {
            get { return _request; }
            set { _request = value; }
        }

        public void RunBeforeRequest() {
            BeforeRequest();
        }
    }
}
