using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace CBApi.Framework.Requests
{
    internal class ApplyLinkRequest : GetRequest
    {
        protected NameValueCollection args;

        public ApplyLinkRequest(NameValueCollection args, APISettings settings)
            : base(settings)
        {
            if (args == null) {
                throw new ArgumentException();
            }
            this.args = args;
        }

        public override string BaseUrl {
            get { return "/v2/application/applylink"; }
        }

        public string Retrieve() 
        {
            _request.AddParameter("JobDID", args["JobDID"]);
            base.BeforeRequest();
            IRestResponse response = _client.Execute(_request);
            _AfterRequestEvent(new RequestEventData(_client, _request, response));
            //CheckForErrors(response);
            return response.Content;
        }
    }
}
