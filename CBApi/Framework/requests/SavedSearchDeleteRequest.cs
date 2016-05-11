using RestSharp;
using CBApi.Models;

namespace CBApi.Framework.Requests
{
    internal class SavedSearchDeleteRequest : PostRequest
    {
        private string DeveloperKey { get; set; }
        public SavedSearchDeleteRequest(APISettings settings)
            : base(settings) {

                DeveloperKey = settings.DevKey;
        }

        public override string BaseUrl {
            get { return "/v1/savedsearch/delete.xml"; }
        }

        public SavedSearchDeleteResponse Submit(RequestSavedSearchDelete deleteMsg) {
            deleteMsg.DeveloperKey = DeveloperKey;
            _request.AddBody(deleteMsg);
            base.BeforeRequest();
            IRestResponse<SavedSearchDeleteResponse> response = _client.Execute<SavedSearchDeleteResponse>(_request);
            CheckForErrors(response);
            return response.Data;
        }

    }
}
