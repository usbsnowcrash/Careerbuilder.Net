using RestSharp;
using CBApi.Models.CheckExisting;

namespace CBApi.Framework.Requests {
    internal class CheckExistingUserRequest : PostRequest
    {
        public CheckExistingUserRequest(APISettings settings) : base(settings)
        {
        }

        public override string BaseUrl
        {
            get { return "/v2/user/checkexisting"; }
        }

        public ResponseUserCheck GetUserCheck(Request checkExisting)
        {
            _request.AddBody(checkExisting);
            base.BeforeRequest();
            IRestResponse<ResponseUserCheck> response = _client.Execute<ResponseUserCheck>(_request);
            CheckForErrors(response);
            return response.Data;
        }
    }
}