using System.Collections.Generic;
using RestSharp;
using CBApi.Models;
using CBApi.Models.Service;

namespace CBApi.Framework.Requests
{
    internal class EducationCodesRequest : GetRequest, IEducationCodesRequest
    {
        protected string _countryCode = "US";

        public EducationCodesRequest(APISettings settings) : base(settings) { }

        public override string BaseUrl
        {
            get { return "/v1/educationcodes"; }
        }

        #region IEducationCodesRequest Members
        public IEducationCodesRequest WhereCountryCode(CountryCode value)
        {
            _countryCode = value.ToString();
            return this;
        }

        public List<Education> ListAll()
        {
            _request.AddParameter("CountryCode", _countryCode);
            _request.RootElement = "EducationCodes";
            base.BeforeRequest();
            IRestResponse<List<Education>> response = _client.Execute<List<Education>>(_request);
            _AfterRequestEvent(new RequestEventData(_client, _request, response));
            CheckForErrors(response);
            return response.Data;
        }
        #endregion
    }
}