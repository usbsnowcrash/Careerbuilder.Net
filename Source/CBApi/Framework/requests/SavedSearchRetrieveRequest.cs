using CBApi.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CBApi.Framework.Requests
{
    internal class SavedSearchRetrieveRequest : GetRequest
    {
        private string DeveloperKey { get; set; }

        public SavedSearchRetrieveRequest(APISettings settings)
            : base(settings)
        {
            DeveloperKey = settings.DevKey;
        }

        public override string BaseUrl
        {
            get { return "/v1/SavedSearch/retrieve"; }
        }

        public SavedSearchRetrieveResponseModel Submit(SavedSearchRetrieveRequestModel search)
        {
            _request.AddBody(search);
            base.BeforeRequest();
            search.DeveloperKey = DeveloperKey;
            IRestResponse<SavedSearchRetrieveResponseModel> response = _client.Execute<SavedSearchRetrieveResponseModel>(_request);
            CheckForErrors(response);
            return response.Data;
        }
    }
}
