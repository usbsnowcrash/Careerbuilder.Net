using System.Collections.Generic;
using RestSharp;
using CBApi.Models;
using System;

namespace CBApi.Framework.Requests {
    internal class JobRecommendationsRequest : GetRequest {
        protected string _jobDid = "";

        public JobRecommendationsRequest(string jobDid, APISettings settings) : base(settings) {
            if (string.IsNullOrEmpty(jobDid)) {
                throw new ArgumentNullException();
            }
            if (jobDid.Length >= 18 && jobDid.Length <= 20 &&
                jobDid.StartsWith("J", StringComparison.InvariantCultureIgnoreCase)) {
                    _jobDid = jobDid;
            } else {
                throw new ArgumentException("This does not look like a jobDid");
            }
        }

        public override string BaseUrl {
            get { return "/v2/recommendations/forjob"; }
        }

        public List<RecommendJobResult> GetRecommendations() {
            _request.AddParameter("JobDID", _jobDid);
            _request.AddParameter("ShowMoreFields", true);
            _request.RootElement = "RecommendJobResults";
            BeforeRequest();
            IRestResponse<List<RecommendJobResult>> response = _client.Execute<List<RecommendJobResult>>(_request);
            CheckForErrors(response);
            return response.Data;
        }
    }
}