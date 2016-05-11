using System;
using System.Text;
using System.Xml;
using System.Linq;
using RestSharp;
using System.Collections.Generic;
using CBApi.Framework.Events;

namespace CBApi.Framework.Requests {
    public abstract class GetRequest : BaseRequest {
        protected APISettings _Settings = null;

        protected IRestClient _client = new RestClient();
        protected IRestRequest _request = new RestRequest();

        protected GetRequest(APISettings settings) {
            if (settings == null) {
                throw new ArgumentNullException("settings", "You must provide valid API Settings");
            }
            _Settings = settings;

            if (string.IsNullOrEmpty(settings.DevKey)) {
                throw new ArgumentNullException("DevKey", "Please provide a valid developer key");
            }

            if (settings.TargetSite == null) {
                throw new ArgumentNullException("TargetSite", "Please provide a valid domain name");
            }

            if (settings.TargetSite != null && string.IsNullOrEmpty(settings.TargetSite.Domain)) {
                throw new ArgumentNullException("TargetSite", "Please provide a valid domain name");
            }
        }

        public abstract string BaseUrl { get; }

        protected virtual string GetRequestURL() {
            var url = new StringBuilder(20);
            if (_Settings.TargetSite.Secure) {
                url.Append("https://");
            } else {
                url.Append("http://");
            }
            url.Append(_Settings.TargetSite.Domain);
            url.Append(this.BaseUrl);
            return url.ToString();
        }

        protected virtual void BeforeRequest() {
            _client.BaseUrl = GetRequestURL();
            _request.AddParameter("DeveloperKey", _Settings.DevKey);

            if (!string.IsNullOrEmpty(_Settings.CobrandCode)) {
                _request.AddParameter("CoBrand", _Settings.CobrandCode);
            }

            if (!string.IsNullOrEmpty(_Settings.SiteId)) {
                _request.AddParameter("SiteID", _Settings.SiteId);
            }
            _request.Timeout = _Settings.TimeoutMS;
            if (!string.IsNullOrEmpty(_Settings.TargetSite.Host)) {
                _request.AddHeader("Host", _Settings.TargetSite.Host);
            }
            foreach (var item in _Settings.TargetSite.Headers) {
                _request.AddHeader(item.Key,item.Value);
            }
            _BeforeRequestEvent(new RequestEventData(_client, _request, null));
        }

        protected virtual void CheckForErrors(IRestResponse response) {
            ErrorParser.CheckForErrors(response);
       }
    }
}