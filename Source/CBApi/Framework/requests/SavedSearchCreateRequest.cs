using System;
using CBApi.Models;
using RestSharp;

namespace CBApi.Framework.Requests
{
    internal class SavedSearchCreateRequest : PostRequest
    {
        private string DeveloperKey { get; set; }
        public SavedSearchCreateRequest(APISettings settings)
            : base(settings)
        {
            DeveloperKey = settings.DevKey;
        }

        public override string BaseUrl
        {
            get { return "/v2/SavedSearch/Create"; }
        }

        public SavedSearchCreateResponse Submit(SavedSearchCreate search)
        {
            _request.AddBody(search);
            base.BeforeRequest();
            search.DeveloperKey = DeveloperKey;
            IRestResponse<SavedSearchCreateResponse> response = _client.Execute<SavedSearchCreateResponse>(_request);
            CheckForErrors(response);
            return response.Data;
        }
    }
}
