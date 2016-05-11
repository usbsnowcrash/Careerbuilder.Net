using RestSharp;
using CBApi.Models;

namespace CBApi.Framework.Requests
{
    internal class SubmitApplicationRequest : PostRequest
    {
        public SubmitApplicationRequest(APISettings settings) : base(settings)
        {
        }

        public override string BaseUrl
        {
            get { return "/v1/application/submit"; }
        }

        public ResponseApplication Submit(RequestApplication app)
        {
            _request.AddBody(app);
            base.BeforeRequest();
            IRestResponse<ResponseApplication> response = _client.Execute<ResponseApplication>(_request);
            CheckForErrors(response);
            return response.Data;
        }
    }
}