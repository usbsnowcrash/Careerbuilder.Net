using RestSharp;
using CBApi.Models.Responses;

namespace CBApi.Framework.Requests
{
    internal class JobReportRequest : GetRequest
    {
        private readonly string _jobDid = "";

        public JobReportRequest(string jobDid, APISettings settings)
            : base(settings)
        {
            _jobDid = jobDid;
        }

        public override string BaseUrl {
            get { return "/v1/jobreport"; }
        }

        public ResponseJobReport GetReport()
        {
            _request.AddParameter("JobDID", _jobDid);
            base.BeforeRequest();
            IRestResponse<ResponseJobReport> response = _client.Execute<ResponseJobReport>(_request);
            CheckForErrors(response);
            return response.Data;
        }
    }
}