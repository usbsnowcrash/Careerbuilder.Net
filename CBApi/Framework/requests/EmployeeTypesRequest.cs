using System.Collections.Generic;
using RestSharp;
using CBApi.Models;
using CBApi.Models.Service;

namespace CBApi.Framework.Requests
{
    internal class EmployeeTypesRequest : GetRequest, IEmployeeTypesRequest
    {
        protected string _countryCode = "US";

        public EmployeeTypesRequest(APISettings settings)
            : base(settings)
        {
        }

        public override string BaseUrl
        {
            get { return "/v1/employeetypes"; }
        }

        #region IEmployeeTypesRequest Members

        public IEmployeeTypesRequest WhereCountryCode(CountryCode value)
        {
            _countryCode = value.ToString();
            return this;
        }

        public IEmployeeTypesRequest WhereHostSite(HostSite value)
        {
            _countryCode = value.ToString();
            return this;
        }

        public List<EmployeeType> ListAll()
        {
            _request.AddParameter("CountryCode", _countryCode);
            _request.RootElement = "EmployeeTypes";
            base.BeforeRequest();
            IRestResponse<List<EmployeeType>> response = _client.Execute<List<EmployeeType>>(_request);
            CheckForErrors(response);
            return response.Data;
        }

        #endregion
    }
}