using CBApi.Models.WebAPIs.SavedSearch;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CBApi.Framework.Requests
{
    public class RetrieveASavedSearch : GetRequest
    {
        protected string _savedSearchDID;
        public RetrieveASavedSearch(APISettings settings, string savedSearchDID) 
            : base(settings) 
        {
            _savedSearchDID = savedSearchDID;
        }

        public override string BaseUrl
        {
            get { return "/CBAPI/SavedSearches/" + _savedSearchDID; }
        }

        public SavedSearches Submit(string userOAuthToken)
        {
            string devkey = _Settings.DevKey;
            _request.AddParameter("userOAuthToken", userOAuthToken);
            base.BeforeRequest();
            IRestResponse<SavedSearches> response = _client.Execute<SavedSearches>(_request);
            CheckForErrors(response);
            return response.Data;
        }
    }
}
