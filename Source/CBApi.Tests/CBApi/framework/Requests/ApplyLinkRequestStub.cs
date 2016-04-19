using CBApi.Framework.Requests;
using CBApi;
using CBApi.Models;
using System.Collections.Specialized;
using RestSharp;
using Tests.CBApi.models.service;

namespace Tests.com.careerbuilder.CBApi.framework.requests {
    internal class ApplyLinkRequestStub : ApplyLinkRequest {

        public ApplyLinkRequestStub(NameValueCollection args, APISettings settings) :
            base(new ApplyLink(args), settings) { }

        public ApplyLinkRequestStub(ApplyLink args, APISettings settings) :
            base(args, settings) { }

        public IRestClient Client {
            get { return _client; }
            set { _client = value; }
        }

        public ApplyLink Model { get { return model; } }
    }
}
