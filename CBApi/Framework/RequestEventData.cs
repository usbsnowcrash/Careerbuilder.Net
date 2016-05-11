using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp;
namespace CBApi.Framework {
    [Serializable]
    internal class RequestEventData : IRequestEventData {
        private string _BaseURL = "";
        private string _Method = "";
        private string _ResponseContent = "";
        private Dictionary<string, string> _Parameters = new Dictionary<string, string>();

        public RequestEventData(IRestClient client, IRestRequest request, IRestResponse response) {
            if (client != null) {
                _BaseURL = client.BaseUrl;
            }
            if (request != null) {
                _Method = request.Method.ToString();
                if (request.Parameters != null) {
                    foreach (var item in request.Parameters) {
                        _Parameters.Add(item.Name, item.Value.ToString());
                    }
                }

            }
            if (response != null) {
                _ResponseContent = response.Content;
            }
        }

        public string BaseURL {
            get { return _BaseURL; }
        }

        public string Method {
            get { return _Method; }
        }

        public string ResponseContent {
            get { return _ResponseContent; }
        }

        public Dictionary<string, string> Parameters {
            get { return _Parameters; }
        }
    }
}
