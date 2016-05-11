using System;
using RestSharp;

namespace CBApi.Framework.Requests
{
    internal class ApplicationFormRequest : GetRequest
    {
        protected string JobDid = "";

        public ApplicationFormRequest(string jobDid, APISettings settings)
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
            get { return "/v1/application/form"; }
        }

        public string Retrieve()
        {
            _request.AddParameter("JobDID", JobDid);
            base.BeforeRequest();
            IRestResponse response = _client.Execute(_request);
            _AfterRequestEvent(new RequestEventData(_client, _request, response));
            CheckForErrors(response);
            return response.Content;
        }
    }
}