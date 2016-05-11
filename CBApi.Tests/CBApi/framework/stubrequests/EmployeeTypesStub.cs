using CBApi;
using CBApi.Framework.Requests;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tests.CBApi.models.service;

namespace Tests.CBApi.models.requests {
    internal class EmployeeTypesStub : EmployeeTypesRequest {
        public EmployeeTypesStub(string key, string domain)
            : base(new APISettings() { DevKey = key, CobrandCode = "", SiteId = "", TargetSite = new TargetSiteMock(domain) }) {
        }

        public string DevKey {
            get { return _Settings.DevKey; }
        }

        public string Domain {
            get { return _Settings.TargetSite.Domain; }
        }

        public string CountryCode {
            get { return _countryCode; }
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