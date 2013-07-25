using CBApi.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CBApi.Framework.Requests
{
    internal class SavedSearchListRequest : GetRequest
    {
        private string DeveloperKey { get; set; }

        public SavedSearchListRequest(APISettings settings)
            : base(settings)
        {
            DeveloperKey = settings.DevKey;
        }

        public override string BaseUrl
        {
            get { return "/v1/SavedSearch/List"; }
        }

        public SavedSearchListResponseModel Submit(SavedSearchListRequestModel search)
        {
            _request.AddBody(search);
            base.BeforeRequest();
            search.DeveloperKey = DeveloperKey;
            IRestResponse<SavedSearchListResponseModel> response = _client.Execute<SavedSearchListResponseModel>(_request);
            CheckForErrors(response);
            return response.Data;
        }
    }
}
