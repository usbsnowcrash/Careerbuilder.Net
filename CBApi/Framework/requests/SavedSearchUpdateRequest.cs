using CBApi.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CBApi.Framework.Requests
{
    internal class SavedSearchUpdateRequest : PostRequest
    {
        private string DeveloperKey { get; set; }

        public SavedSearchUpdateRequest(APISettings settings)
            : base(settings)
        {
            DeveloperKey = settings.DevKey;
        }

        public override string BaseUrl
        {
            get { return "/v2/SavedSearch/Update"; }
        }

        public SavedSearchUpdateResponseModel Submit(SavedSearchUpdateRequestModel search)
        {
            _request.AddBody(search);
            base.BeforeRequest();
            search.DeveloperKey = DeveloperKey;
            IRestResponse<SavedSearchUpdateResponseModel> response = _client.Execute<SavedSearchUpdateResponseModel>(_request);
            CheckForErrors(response);
            return response.Data;
        }
    }
}
