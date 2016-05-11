using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace CBApi.Framework {
    internal class ErrorParser {

        public static void CheckForErrors(IRestResponse response) {
            if (response != null) {
                ParseResponseForErrorsNode(response);
                CheckRestSharpForErrorStatus(response);
            }
        }

        private static void ParseResponseForErrorsNode(IRestResponse response) {
            if (string.IsNullOrWhiteSpace(response.Content)) {
                return;
            }

            if ((response.ContentType ?? "").ToLower().StartsWith("application/json")) {
                ParseJSONForErrorsNode(response);
            } else {
                ParseXmlForErrorsNode(response);
            }
        }

        private static void ParseXmlForErrorsNode(IRestResponse response) {
            var errors = new List<string>();
            var xml = new XmlDocument();

            string filteredXmlContent = GetXmlContentWithoutNamespaces(response.Content);
            xml.LoadXml(filteredXmlContent);

            //xml.LoadXml(response.Content);
            foreach (XmlNode item in xml.SelectNodes("//Error")) {
                if (!string.IsNullOrEmpty(item.InnerText)) {
                    errors.Add(item.InnerText);
                }
            }

            if (errors.Count == 0) {
                XmlNode errorsNode = xml.SelectSingleNode("//Errors");
                if (errorsNode != null) {
                    foreach (XmlNode error in errorsNode.SelectNodes("//string")) {
                        errors.Add(error.InnerText);
                    }
                }
            }

            if (errors.Count > 0) {
                throw new APIException(errors[0], errors);
            }
        }

        //Implemented based on interface, not part of algorithm
        public static string GetXmlContentWithoutNamespaces(string xmlDocument) {
            XElement xmlDocumentWithoutNamespaces = RemoveAllNamespaces(XElement.Parse(xmlDocument));
            return xmlDocumentWithoutNamespaces.ToString();
        }

        //Core recursion function
        private static XElement RemoveAllNamespaces(XElement xmlDocument) {
            if (!xmlDocument.HasElements) {
                XElement xElement = new XElement(xmlDocument.Name.LocalName);
                xElement.Value = xmlDocument.Value;

                foreach (XAttribute attribute in xmlDocument.Attributes())
                    xElement.Add(attribute);

                return xElement;
            }
            return new XElement(xmlDocument.Name.LocalName, xmlDocument.Elements().Select(el => RemoveAllNamespaces(el)));
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