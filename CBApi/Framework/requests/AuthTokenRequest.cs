using System;
using RestSharp;
using CBApi.Models;

namespace CBApi.Framework.Requests {
    internal class AuthTokenRequest : GetRequest {
        protected string _ClientId = "";
        protected string _ClientSecret = "";
        protected string _Code = "";
        protected string _RedirectUri = "";

        internal AuthTokenRequest(string clientId, string clientSecret, string code, string redirectUri, APISettings settings)
            : base(settings) {
                
            if (string.IsNullOrEmpty(clientId)) {
                throw new ArgumentNullException();
            }

            if (string.IsNullOrEmpty(clientSecret)) {
                throw new ArgumentNullException();
            }

            if (string.IsNullOrEmpty(code)) {
                throw new ArgumentNullException();
            }

            if (string.IsNullOrEmpty(redirectUri)) {
                throw new ArgumentNullException();
            }

            _ClientId = clientId;
            _ClientSecret = clientSecret;
            _Code = code;
            _RedirectUri = redirectUri;
            
        }

        public override string BaseUrl {
            get { return "/auth/token"; }
        }

        public AccessToken GetAccessToken() {
            _request.AddParameter("client_id", _ClientId);
            _request.AddParameter("client_secret", _ClientSecret);
            _request.AddParameter("redirect_uri", _RedirectUri);
            _request.AddParameter("code", _Code);
            base.BeforeRequest();
            IRestResponse<AccessToken> response = _client.Execute<AccessToken>(_request);
            _AfterRequestEvent(new RequestEventData(_client, _request, response));
            CheckForErrors(response);
            return response.Data;
        }
    }
}