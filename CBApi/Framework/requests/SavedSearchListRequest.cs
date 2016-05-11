using CBApi.Models;
using RestSharp;

namespace CBApi.Framework.Requests {
    internal class SavedSearchListRequest : GetRequest {

        public SavedSearchListRequest(APISettings settings) : base(settings) { }

        public override string BaseUrl {
            get { return "/v1/SavedSearch/List"; }
        }

        public SavedSearchListResponseModel Submit(SavedSearchListRequestModel search) {
            search.DeveloperKey = _Settings.DevKey;
            AddParametersToRequest(search);
            base.BeforeRequest();
            IRestResponse<SavedSearchListResponseModel> response = _client.Execute<SavedSearchListResponseModel>(_request);
            CheckForErrors(response);
            return response.Data;
        }

        protected virtual void AddParametersToRequest(SavedSearchListRequestModel search) {
            _request.AddParameter("externaluserid", search.ExternalUserID);
        }

    }
}