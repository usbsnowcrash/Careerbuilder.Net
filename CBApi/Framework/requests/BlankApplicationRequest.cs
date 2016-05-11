using System;
using RestSharp;
using CBApi.Models;

namespace CBApi.Framework.Requests
{
    internal class BlankApplicationRequest : GetRequest
    {
        protected string JobDid = "";

        public BlankApplicationRequest(string jobDid, APISettings settings)
            : base(settings)
        {
            if (string.IsNullOrEmpty(jobDid))
            {
                throw new ArgumentNullException();
            }
            
            if (jobDid.Length >= 18 && jobDid.Length <= 20 &&
                     jobDid.StartsWith("J", StringComparison.InvariantCultureIgnoreCase))
            {
                JobDid = jobDid;
            }
            else
            {
                throw new ArgumentException("This does not look like a job did");
            }
        }

        public override string BaseUrl
        {
            get { return "/v1/application/blank"; }
        }

        public BlankApplication Retrieve()
        {
            _request.AddParameter("JobDID", JobDid);
            _request.RootElement = "BlankApplication";
            base.BeforeRequest();
            IRestResponse<BlankApplication> response = _client.Execute<BlankApplication>(_request);
            _AfterRequestEvent(new RequestEventData(_client, _request, response));
            CheckForErrors(response);
            BlankApplication app = response.Data;
            if (app != null)
            {
                app.SiteID = _Settings.SiteId;
                app.CoBrand = _Settings.CobrandCode;
                app.DeveloperKey = _Settings.DevKey;
            }
            return app;
        }
    }
}