using CBApi.Models;
using RestSharp;
using System;
using System.Collections.Specialized;
using System.Reflection;

namespace CBApi.Framework.Requests
{
    internal class ApplyLinkRequest : GetRequest
    {
        protected ApplyLink model;

        public ApplyLinkRequest(NameValueCollection args, APISettings settings) :
            this(new ApplyLink(args), settings) { }

        public ApplyLinkRequest(ApplyLink args, APISettings settings)
            : base(settings)
        {
            if (args == null) {
                throw new ArgumentException();
            }
            model = args;
        }

        public override string BaseUrl {
            get { return "/v2/application/applylink"; }
        }

        public string Retrieve() 
        {
            foreach (PropertyInfo property in model.GetType().GetProperties())
            {
                var value = property.GetValue(model, null);
                if (value != null)
                {
                    _request.AddParameter(property.Name, value);
                }
            }
            base.BeforeRequest();
            IRestResponse response = _client.Execute(_request);
            _AfterRequestEvent(new RequestEventData(_client, _request, response));
            return response.ResponseUri.ToString();
        }
    }
}
