using System.Collections.Generic;
using RestSharp;
using CBApi.Models;
using CBApi.Models.Responses;
using CBApi.Models.Service;

namespace CBApi.Framework.Requests {
    internal class JobSearchRequest : GetRequest, IJobSearch {

        protected bool ExcludeNationwideJobs = true;
        protected BooleanOperator _BooleanOperator = BooleanOperator.AND;
        protected List<string> _CategoryCodes = new List<string>();
        protected List<string> _IndustryCodes = new List<string>();
        protected List<string> _CompanyDids = new List<string>();
        protected string _CompanyName = "";
        protected string _CountryCode = "US";
        protected string _EducationCode = "";
        protected List<string> _EmployeeTypes = new List<string>();
        protected string _Keywords = "";
        protected string _Location = "";
        protected int _MaxPay = -1;
        protected int _MinPay = -1;
        protected int _OffSet = 1;
        protected OrderByType _OrderBy = OrderByType.Relevance;
        protected OrderDirection _OrderDirection = OrderDirection.Descending;
        protected int _PerPage = 25;
        protected int _PostedWithin = 30;
        protected int _Radius = 30;
        protected string _Soccode = "";
        protected bool _SpecificEducation = false;
        protected bool _ExcludeNationWideJobs = true;
        protected bool _ExcludeNonTraditionalJobs = true;
        protected string _SiteEntity = "";
        protected bool _ShowFacets;
        protected Dictionary<FacetField, string> _Facets = new Dictionary<FacetField,string>();

        public JobSearchRequest(APISettings settings) : base(settings) { }

        public override string BaseUrl {
            get { return "/v1/jobsearch"; }
        }

        #region IJobSearch Members

        public ResponseJobSearch Search() {
            AddParametersToRequest();
            base.BeforeRequest();
            IRestResponse<ResponseJobSearch> response = _client.Execute<ResponseJobSearch>(_request);
            _AfterRequestEvent(new RequestEventData(_client, _request, response));
            CheckForErrors(response);
            return response.Data;
        }

        #endregion

        #region setup request

        protected void AddParametersToRequest() {
            AddKeywordsToRequest();
            AddCompanyNameToRequest();
            AddLocationToRequest();
            AddRadiusToRequest();
            AddCountryCodeToRequest();
            AddCategoriesToRequest();
            AddIndustriesToRequest();
            AddCompanyDIDsToRequest();
            AddSOCCodeToRequest();
            AddEducationToRequest();
            AddPostedWithinToRequest();
            AddEmployeeTypesToRequest();
            AddPerPageToRequest();
            AddSiteEntityToRequest();
            AddFacets();
        }

        private void AddEmployeeTypesToRequest() {
            if (_EmployeeTypes.Count > 0) {
                string emps = string.Join(",", _EmployeeTypes);
                _request.AddParameter("EmpType", emps);
            }
        }

        private void AddPostedWithinToRequest() {
            if (_PostedWithin >= 1 && _PostedWithin <= 30) {
                _request.AddParameter("PostedWithin", _PostedWithin.ToString());
            }
        }

        private void AddEducationToRequest() {
            if (!string.IsNullOrEmpty(_EducationCode)) {
                _request.AddParameter("EducationCode", _EducationCode);
                _request.AddParameter("SpecificEducation", _SpecificEducation.ToString());
            }
        }

        private void AddSOCCodeToRequest() {
            if (!string.IsNullOrEmpty(_Soccode)) {
                _request.AddParameter("SOCCode", _Soccode);
            }
        }

        private void AddCategoriesToRequest() {
            if (_CategoryCodes.Count > 0 && _CategoryCodes.Count <= 10) {
                string cats = string.Join(",", _CategoryCodes);
                _request.AddParameter("Category", cats);
            }
        }

        private void AddIndustriesToRequest() {
            if (_IndustryCodes.Count > 0 && _IndustryCodes.Count <= 10) {
                string industries = string.Join(",", _IndustryCodes);
                _request.AddParameter("IndustryCodes", industries);
            }
        }

        private void AddCompanyDIDsToRequest() {
            if (_CompanyDids.Count > 0) {
                string comps = string.Join(",", _CompanyDids);
                _request.AddParameter("CompanyDIDCSV", comps);
            }
        }

        private void AddCountryCodeToRequest() {
            if (!string.IsNullOrEmpty(_CountryCode)) {
                _request.AddParameter("CountryCode", _CountryCode);
            }
        }

        private void AddRadiusToRequest() {
            if (_Radius >= 5 && _Radius <= 150) {
                _request.AddParameter("Radius", _Radius.ToString());
            }
        }

        private void AddLocationToRequest() {
            if (!string.IsNullOrEmpty(_Location)) {
                _request.AddParameter("Location", _Location);
            }
        }

        private void AddCompanyNameToRequest() {
            if (!string.IsNullOrEmpty(_CompanyName)) {
                _request.AddParameter("CompanyName", _CompanyName);
            }
        }

        private void AddKeywordsToRequest() {
            if (!string.IsNullOrEmpty(_Keywords)) {
                _request.AddParameter("Keywords", _Keywords);
            }
        }

        private void AddPerPageToRequest() {
            _request.AddParameter("PerPage", _PerPage);
        }

        private void AddSiteEntityToRequest() {
            if (!string.IsNullOrEmpty(_SiteEntity))
                _request.AddParameter("SiteEntity", _SiteEntity);
        }
        
        private void AddFacets() {
            if (_ShowFacets || _Facets.Count > 0) {
                _request.AddParameter("UseFacets", "true");
            }

            foreach (var facet in _Facets) {
                _request.AddParameter(facet.Key.ToString(), facet.Value);
            }
        }

        #endregion

        #region IJobSearch Methods

        public IJobSearch WhereCountryCode(CountryCode value) {
            _CountryCode = value.ToString();
            return this;
        }

        public IJobSearch WhereIndustry(params string[] industries) {
            foreach (var item in industries) {
                _IndustryCodes.Add(item);
            }
            return this;
        }

        public IJobSearch WhereHostSite(HostSite value) {
            _CountryCode = value.ToString();
            return this;
        }

        public IJobSearch WhereKeywords(string value) {
            _Keywords = value;
            return this;
        }

        public IJobSearch WhereCompanyName(string value) {
            _CompanyName = value;
            return this;
        }

        public IJobSearch WhereLocation(string value) {
            _Location = value;
            return this;
        }

        public IJobSearch WhereLocation(float latitude, float longitude) {
            _Location = latitude.ToString() + "::" + longitude.ToString();
            return this;
        }

        public IJobSearch Radius(int value) {
            _Radius = value;
            return this;
        }

        public IJobSearch WhereSOCCode(string value) {
            _Soccode = value;
            return this;
        }

        public IJobSearch WherePayGreaterThan(int value) {
            _MinPay = value;
            return this;
        }

        public IJobSearch WherePayLessThan(int value) {
            _MaxPay = value;
            return this;
        }

        public IJobSearch OrderBy(OrderByType value) {
            _OrderBy = value;
            return this;
        }

        public IJobSearch Ascending() {
            _OrderDirection = OrderDirection.Ascending;
            return this;
        }

        public IJobSearch Descending() {
            _OrderDirection = OrderDirection.Descending;
            return this;
        }

        public IJobSearch SelectTop(int value) {
            _PerPage = value;
            return this;
        }

        public IJobSearch Limit(int value) {
            _PerPage = value;
            return this;
        }

        public IJobSearch Offset(int value) {
            _OffSet = value;
            return this;
        }

        public IJobSearch WhereCategories(params Category[] codes) {
            foreach (var item in codes) {
                _CategoryCodes.Add(item.Code);
            }
            return this;
        }

        public IJobSearch WhereCompanyDIDs(params string[] companies) {
            foreach (var item in companies) {
                _CompanyDids.Add(item);
            }
            return this;
        }

        public IJobSearch WhereSiteEntity(string value) {
            _SiteEntity = value;
            return this;
        }

        public IJobSearch WhereEmployeeTypes(params string[] employmentTypes) {
            foreach (var item in employmentTypes) {
                _EmployeeTypes.Add(item);
            }
            return this;
        }

        public IJobSearch WhereFacets(params KeyValuePair<FacetField, string>[] facets) {
            if (facets == null) { return this; }

            foreach (var newFacet in facets) {
                if (!string.IsNullOrWhiteSpace(newFacet.Value)) {
                    _ShowFacets = true;

                    if (_Facets.ContainsKey(newFacet.Key)) {
                        _Facets[newFacet.Key] = newFacet.Value;
                    } else {
                        _Facets.Add(newFacet.Key, newFacet.Value);
                    }
                }
            }

            return this;
        }

        public IJobSearch ShowFacets() {
            _ShowFacets = true;
            return this;
        }
        #endregion
    }
}