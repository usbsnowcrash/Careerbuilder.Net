using System.Collections.Generic;
using CBApi;
using CBApi.Framework.Requests;
using RestSharp;
using Tests.CBApi.models.service;

namespace Tests.CBApi.models.requests {
    internal class UserReqStub : UserRecommendationsRequest {
        public UserReqStub(string externalID, string key, string domain, string cobrand, string siteid)
            : base(new ExternalID(externalID), new APISettings() { DevKey = key, CobrandCode = cobrand, SiteId = siteid, TargetSite = new TargetSiteMock(domain) }) {
        }

        public UserReqStub(List<QsParam> qsList, string key, string domain, string cobrand, string siteid)
            : base(qsList, new APISettings() { DevKey = key, CobrandCode = cobrand, SiteId = siteid, TargetSite = new TargetSiteMock(domain) }) {
        }

        public string ExternalID {
            get {
                return this._QsParams.Find(x => x.GetType() == typeof(ExternalID)).value;
            }
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
