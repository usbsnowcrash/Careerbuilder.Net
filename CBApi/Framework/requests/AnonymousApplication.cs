using CBApi.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CBApi.Framework.Requests
{
    internal class AnonymousApplication : PostRequest
    {
        private string DeveloperKey { get; set; }
        public AnonymousApplication(APISettings settings)
            : base(settings)
        {
            DeveloperKey = settings.DevKey;
        }

        public override string BaseUrl
        {
            get { return "/v2/Application/submit"; }
        }

        public AnonymousApplicationResponse Submit(AnonymousApplicationRequest request)
        {
            _request.AddBody(request);
            base.BeforeRequest();
            request.DeveloperKey=DeveloperKey;
            IRestResponse<AnonymousApplicationResponse> response = _client.Execute<AnonymousApplicationResponse>(_request);
            CheckForErrors(response);
            return response.Data;
        }
    }
}
