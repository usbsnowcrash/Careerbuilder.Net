using System;
using System.Collections.Generic;
using RestSharp;
using CBApi.Models;

namespace CBApi.Framework.Requests
{
    internal class UserRecommendationsRequest : GetRequest
    {
        protected List<QsParam> _QsParams = new List<QsParam>();

        public UserRecommendationsRequest(QsParam QsParam, APISettings settings)
            : base(settings) {
            this._QsParams.Add(QsParam);
        }

        public UserRecommendationsRequest(List<QsParam> QsParams, APISettings settings)
            : base(settings) {
                this._QsParams = QsParams;
        }

        public override string BaseUrl
        {
            get { return "/v1/recommendations/foruser"; }
        }

        public List<RecommendJobResult> GetRecommendations()
        {
            AddQueryStrings();     
            _request.RootElement = "RecommendJobResults";
            base.BeforeRequest();
            IRestResponse<List<RecommendJobResult>> response = _client.Execute<List<RecommendJobResult>>(_request);
            CheckForErrors(response);
            return response.Data;
        }

        public void AddQueryStrings() {
            if(_QsParams.Count > 0)
                _QsParams.ForEach(param => param.addIDParam(_request)); 
        }

    }
}