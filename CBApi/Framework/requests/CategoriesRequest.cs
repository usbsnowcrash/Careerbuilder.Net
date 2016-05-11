using System.Collections.Generic;
using RestSharp;
using CBApi.Models;
using CBApi.Models.Service;

namespace CBApi.Framework.Requests
{
    internal class CategoriesRequest : GetRequest, ICategoryRequest
    {
        protected string _countryCode = "US";

        public CategoriesRequest(APISettings settings)
            : base(settings)
        {
        }

        public override string BaseUrl
        {
            get { return "/v1/categories"; }
        }

        #region ICategoryRequest Members

        public ICategoryRequest WhereCountryCode(CountryCode value)
        {
            _countryCode = value.ToString();
            return this;
        }

        public ICategoryRequest WhereHostSite(HostSite value)
        {
            _countryCode = value.ToString();
            return this;
        }

        public List<Category> ListAll()
        {
            _request.AddParameter("CountryCode", _countryCode);
            _request.RootElement = "Categories";
            base.BeforeRequest();
            IRestResponse<List<Category>> response = _client.Execute<List<Category>>(_request);
            _AfterRequestEvent(new RequestEventData(_client, _request, response));
            CheckForErrors(response);
            return response.Data;
        }

        #endregion
    }
}