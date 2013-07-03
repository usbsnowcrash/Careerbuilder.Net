using System.Text.RegularExpressions;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using RestSharp.Deserializers;
using Newtonsoft.Json.Linq;

namespace CBApi.Framework {
    internal class ErrorParser {
        public static void CheckForErrors(IRestResponse response) {
            if (response != null) {
                ParseResponseForErrorsNode(response);
                CheckRestSharpForErrorStatus(response);
            }
        }

        private static void ParseResponseForErrorsNode(IRestResponse response) {

            if (IsJson(response))
                ParseJSONForErrorsNode(response);
            else
                ParseXmlForErrorsNode(response);
        }

        private static bool IsJson(IRestResponse response)
        {
            if (string.IsNullOrEmpty(response.ContentType))
                return false;

            var regex = new Regex("^application/json.*");
            return regex.IsMatch(response.ContentType.ToLower());
        }

        private static void ParseXmlForErrorsNode(IRestResponse response) {
            var errors = new List<string>();
            var xml = new XmlDocument();
            try {
                xml.LoadXml(response.Content);
                foreach (XmlNode item in xml.SelectNodes("//Error")) {
                    if (!string.IsNullOrEmpty(item.InnerText)) {
                        errors.Add(item.InnerText);
                    }
                }
            } catch (XmlException) {

            }

            if (errors.Count > 0) {
                throw new APIException(errors[0], errors);
            }
        }

        private static void ParseJSONForErrorsNode(IRestResponse response) {
            if (!String.IsNullOrWhiteSpace(response.Content)) {
                var errors = new List<String>();

                JObject json = JObject.Parse(response.Content);
                foreach (string error in json["Errors"].Select(e => (String)e).ToList<String>()) {
                    if (!String.IsNullOrWhiteSpace(error))
                        errors.Add(error);
                }

                if (errors.Count > 0) {
                    throw new APIException(errors[0], errors);
                }
            }
        }

        private static void CheckRestSharpForErrorStatus(IRestResponse response) {
            if (response.ResponseStatus == ResponseStatus.TimedOut) {
                if (!string.IsNullOrEmpty(response.ErrorMessage)) {
                    throw new APITimeoutException(response.ErrorMessage);
                } else {
                    throw new APITimeoutException("An unknown error occured while making the API call");
                }

            } else if (response.ResponseStatus != ResponseStatus.Completed) {
                if (!string.IsNullOrEmpty(response.ErrorMessage)) {
                    throw new APIException(response.ErrorMessage);
                } else {
                    throw new APIException("An unknown error occured while making the API call");
                }

            }
        }
    }
}
