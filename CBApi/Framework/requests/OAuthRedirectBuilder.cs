using System;
using RestSharp;
using CBApi.Models;

namespace CBApi.Framework.Requests {
    internal class OAuthRedirectBuilder {
        protected string _RedirectURI = "";
        protected string _Domain = "";
        protected string _ClientId = "";
        protected string _Resources = null;

        internal OAuthRedirectBuilder(string clientId, string redirectUri, string additionalPermissions, string domain) {
            if (string.IsNullOrEmpty(clientId)) {
                throw new ArgumentNullException("clientId");
            }

            if (string.IsNullOrEmpty(redirectUri)) {
                throw new ArgumentNullException("redirectUri");
            }

            if (string.IsNullOrEmpty(domain)) {
                throw new ArgumentNullException("domain");
            }

            if (!string.IsNullOrEmpty(additionalPermissions)) {
                _Resources = additionalPermissions;
            }

            _ClientId = clientId;
            _RedirectURI = redirectUri;
            _Domain = domain;
        }

        public Uri OAuthUri() {
            Uri redirect;
            if (_Resources == null) {
                redirect = new Uri(string.Format("https://{0}/auth/prompt?client_id={1}&redirect_uri={2}", _Domain, _ClientId, _RedirectURI));
            } else {
                redirect = new Uri(string.Format("https://{0}/auth/prompt?client_id={1}&redirect_uri={2}&resources={3}", _Domain, _ClientId, _RedirectURI, _Resources));
            }
            
            return redirect;
        }
    }
}