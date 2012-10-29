﻿using com.careerbuilder.api;
using com.careerbuilder.api.framework.requests;
using RestSharp;
using Tests.com.careerbuilder.api.models.service;

namespace Tests.com.careerbuilder.api.framework.requests {
    internal class BlankAppStub : BlankApplicationRequest {
        public BlankAppStub(string jobDID, string key, string domain, string cobrand, string siteid)
            : base(jobDID, new APISettings() { DevKey = key, CobrandCode = cobrand, SiteId = siteid, TargetSite = new TargetSiteMock(domain) }) {
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
