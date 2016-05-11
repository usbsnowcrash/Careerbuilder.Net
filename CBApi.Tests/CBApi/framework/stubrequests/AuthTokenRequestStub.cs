using CBApi;
using CBApi.Framework.Requests;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tests.CBApi.models.service;

namespace Tests.CBApi.models.requests {
    internal class AuthTokenRequestStub : AuthTokenRequest {
        public AuthTokenRequestStub(string clientId, string clientSecret, string code, string redirectUri, string key, string domain, string cobrand, string siteid)
            : base(clientId, clientSecret, code, redirectUri, new APISettings() { DevKey = key, CobrandCode = cobrand, SiteId = siteid, TargetSite = new TargetSiteMock(domain) }) {
        }

        public string ClientId {
            get { return _ClientId; }
            set { _ClientId = value; }
        }

        public string ClientSecret {
            get { return _ClientSecret; }
            set { _ClientSecret = value; }
        }


        public string Code {
            get { return _Code; }
            set { _Code = value; }
        }


        public string RedirectUri {
            get { return _RedirectUri; }
            set { _RedirectUri = value; }
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
