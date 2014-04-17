using System;
using System.Collections.Generic;
using RestSharp;
using CBApi.Models;

namespace CBApi.Framework.Requests
{
    internal class UserRecommendationsRequest : GetRequest
    {
        protected List<QsParam> QsParams = new List<QsParam>();

        public UserRecommendationsRequest(QsParam QsParam, APISettings settings)
            : base(settings) {
            this.QsParams.Add(QsParam);
        }

        public UserRecommendationsRequest(List<QsParam> QsParams, APISettings settings)
            : base(settings) {
                this.QsParams = QsParams;
        }

        public override string BaseUrl
        {
            get { return "/v1/recommendations/foruser"; }
        }

        public List<RecommendJobResult> GetRecommendations()
        {
            addQsParams();     
            _request.RootElement = "RecommendJobResults";
            base.BeforeRequest();
            IRestResponse<List<RecommendJobResult>> response = _client.Execute<List<RecommendJobResult>>(_request);
            CheckForErrors(response);
            return response.Data;
        }

        public void addQsParams() {
            if(QsParams.Count>0)
                QsParams.ForEach(param => param.addIDParam(_request)); 
        }

        public Boolean containsQS(QsParam qs) {
            Boolean contains = false;
            QsParams.ForEach(param => contains = contains || param.compare(qs));
            return contains;
        }

    }

    internal abstract class QsParam {
        public string paramName;
        public string value;
        public QsParam(string paramName, string value) {
            if (string.IsNullOrEmpty(value)) {
                throw new ArgumentNullException(this.GetType().ToString(), this.GetType().ToString() +" value is required");
            }
            this.paramName = paramName;
            this.value = value;
        }

        public void addIDParam(IRestRequest request) {
            request.AddParameter(paramName, value);
        }

        public Boolean compare(QsParam qs) {
            return (qs.paramName == this.paramName && qs.value == this.value);
        }

    }

    internal class VisitorID : QsParam {
        public VisitorID(string id) :base("VisitorID", id){}
    }

    internal class ExternalID : QsParam {
        public ExternalID(string id) : base("ExternalID", id) { }
    }

    internal class GenericParam :QsParam{
        public GenericParam(string name, string value):base(name, value){}
    }

}