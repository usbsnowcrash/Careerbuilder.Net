﻿using System.Collections.Generic;
using CBApi.Models;
using CBApi.Models.Responses;
using CBApi.Models.Service;
using RestSharp;

namespace CBApi.Framework.Requests {
    internal class JobSearchRequest : GetRequest, IJobSearch {

        protected BooleanOperator _BooleanOperator = BooleanOperator.AND;
        protected List<string> _CategoryCodes = new List<string>();
        protected List<string> _CompanyDids = new List<string>();
        protected string _CompanyName = "";
        protected string _CountryCode = "US";
        protected string _EducationCode = "";
        protected Dictionary<FacetField, string> _Facets = new Dictionary<FacetField, string>();
        protected List<string> _IndustryCodes = new List<string>();
        protected List<string> _EmployeeTypes = new List<string>();
        protected string _Keywords = "";
        protected string _Location = "";
        protected int _MaxPay = -1;
        protected int _MinPay = -1;
        protected int _OffSet = 1;
        protected OrderByType _OrderBy = OrderByType.Relevance;
        protected OrderDirection _OrderDirection = OrderDirection.Descending;
        protected int _PageNumber = 1;
        protected int _PerPage = 25;
        protected int _PostedWithin = 30;
        protected int _Radius = 0;
        protected bool _ShowFacets;
        protected string _SiteEntity = "";
        protected string _Soccode = "";
        protected bool _SpecificEducation = false;
        protected bool _ExcludeJobsWithoutSalary, _ExcludeNationwide, _ExcludeNontraditional;

        public override string BaseUrl {
            get { return "/v1/jobsearch"; }
        }

        public JobSearchRequest(APISettings settings)
            : base(settings) {
        }

        public IJobSearch Ascending() {
            _OrderDirection = OrderDirection.Ascending;
            return this;
        }

        public IJobSearch Descending() {
            _OrderDirection = OrderDirection.Descending;
            return this;
        }

        public IJobSearch ExcludeJobsWithoutSalary() {
            _ExcludeJobsWithoutSalary = true;
            return this;
        }

        public IJobSearch ExcludeNationwideJobs() {
            _ExcludeNationwide = true;
            return this;
        }

        public IJobSearch ExcludeNontraditionalJobs() {
            _ExcludeNontraditional = true;
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

        public IJobSearch OrderBy(OrderByType value) {
            _OrderBy = value;
            return this;
        }

        public IJobSearch Radius(int value) {
            _Radius = value;
            return this;
        }

        public ResponseJobSearch Search() {
            AddParametersToRequest();
            base.BeforeRequest();
            IRestResponse<ResponseJobSearch> response = _client.Execute<ResponseJobSearch>(_request);
            _AfterRequestEvent(new RequestEventData(_client, _request, response));
            CheckForErrors(response);
            return response.Data;
        }

        public IJobSearch SelectCount(int value) {
            _PerPage = value;
            return this;
        }

        public IJobSearch SelectPage(int value) {
            _PageNumber = value;
            return this;
        }

        public IJobSearch SelectTop(int value) {
            _PerPage = value;
            return this;
        }

        public IJobSearch ShowFacets() {
            _ShowFacets = true;
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

        public IJobSearch WhereCompanyName(string value) {
            _CompanyName = value;
            return this;
        }

        public IJobSearch WhereCountryCode(CountryCode value) {
            _CountryCode = value.ToString();
            return this;
        }

        public IJobSearch WhereEducationCodeEquals(string educationCode) {
            if (!string.IsNullOrWhiteSpace(educationCode)) {
                _EducationCode = educationCode.ToUpper();
                _SpecificEducation = true;
            }
            return this;
        }

        public IJobSearch WhereEducationCodeMaximum(string educationCode) {
            if (!string.IsNullOrWhiteSpace(educationCode)) {
                _EducationCode = educationCode.ToUpper();
                _SpecificEducation = false;
            }
            return this;
        }

        public IJobSearch WhereEmployeeTypes(params string[] employmentTypes) {
            foreach (var employmentType in employmentTypes) {
                if (!string.IsNullOrWhiteSpace(employmentType) && employmentType.ToUpper() != "ALL") {
                    _EmployeeTypes.Add(employmentType.ToUpper());
                }
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

        public IJobSearch WhereHostSite(HostSite value) {
            _CountryCode = value.ToString();
            return this;
        }

        public IJobSearch WhereIndustry(params string[] industries) {
            foreach (var industryCode in industries) {
                if (!string.IsNullOrWhiteSpace(industryCode) && industryCode.ToUpper() != "ALL") {
                    _IndustryCodes.Add(industryCode.ToUpper());
                }
            }
            return this;
        }

        public IJobSearch WhereKeywords(string value) {
            _Keywords = value;
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

        public IJobSearch WherePayGreaterThan(int minimumPay) {
            if (minimumPay > 0) {
                _MinPay = minimumPay;
            }
            return this;
        }

        public IJobSearch WherePayLessThan(int maximumPay) {
            if (maximumPay > 0) {
                _MaxPay = maximumPay;
            }
            return this;
        }

        public IJobSearch WherePerPage(int value) {
            _PerPage = value;
            return this;
        }

        public IJobSearch WherePostedWithin(int numberOfDays) {
            if (numberOfDays > 0) {
                _PostedWithin = numberOfDays;
            }
            return this;
        }

        public IJobSearch WhereSiteEntity(string value) {
            _SiteEntity = value;
            return this;
        }

        public IJobSearch WhereSOCCode(string value) {
            _Soccode = value;
            return this;
        }

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
            AddSimpleExclusionsToRequest();

            AddPerPageToRequest();
            AddPageNumberToRequest();
            AddSiteEntityToRequest();

            AddFacets();
        }

        private void AddCategoriesToRequest() {
            if (_CategoryCodes.Count > 0 && _CategoryCodes.Count <= 10) {
                string cats = string.Join(",", _CategoryCodes);
                _request.AddParameter("Category", cats);
            }
        }

        private void AddCompanyDIDsToRequest() {
            if (_CompanyDids.Count > 0) {
                string comps = string.Join(",", _CompanyDids);
                _request.AddParameter("CompanyDIDCSV", comps);
            }
        }

        private void AddCompanyNameToRequest() {
            if (!string.IsNullOrEmpty(_CompanyName)) {
                _request.AddParameter("CompanyName", _CompanyName);
            }
        }

        private void AddCountryCodeToRequest() {
            if (!string.IsNullOrEmpty(_CountryCode)) {
                _request.AddParameter("CountryCode", _CountryCode);
            }
        }

        private void AddEducationToRequest() {
            if (!string.IsNullOrWhiteSpace(_EducationCode) && _EducationCode != "DRNS") {
                _request.AddParameter("EducationCode", _EducationCode);
                if (_SpecificEducation) {
                    _request.AddParameter("SpecificEducation", _SpecificEducation.ToString());
                }
            }
        }

        private void AddEmployeeTypesToRequest() {
            if (_EmployeeTypes.Count > 0) {
                string industries = string.Join(",", _EmployeeTypes);
                _request.AddParameter("EmpType", industries);
            }
        }

        private void AddSimpleExclusionsToRequest() {
            if (_ExcludeJobsWithoutSalary) {
                _request.AddParameter("PayInfoOnly", "true");
            }
            if (_ExcludeNationwide) {
                _request.AddParameter("ExcludeNational", "true");
            }
            if (_ExcludeNontraditional) {
                _request.AddParameter("ExcludeNonTraditionalJobs", "true");
            }
        }

        private void AddFacets() {
            if (_ShowFacets || _Facets.Count > 0) {
                _request.AddParameter("UseFacets", "true");
            }

            foreach (var facet in _Facets) {
                _request.AddParameter(facet.Key.ToString(), facet.Value);
            }
        }

        private void AddIndustriesToRequest() {
            if (_IndustryCodes.Count > 0) {
                if (_IndustryCodes.Count > 10) {
                    _IndustryCodes.RemoveRange(10, _IndustryCodes.Count - 10);
                }
                string industries = string.Join(",", _IndustryCodes);
                _request.AddParameter("IndustryCodes", industries);
            }
        }

        private void AddKeywordsToRequest() {
            if (!string.IsNullOrEmpty(_Keywords)) {
                _request.AddParameter("Keywords", _Keywords);
            }
        }

        private void AddLocationToRequest() {
            if (!string.IsNullOrEmpty(_Location)) {
                _request.AddParameter("Location", _Location);
            }
        }

        private void AddPageNumberToRequest() {
            _request.AddParameter("PageNumber", _PageNumber);
        }

        private void AddPerPageToRequest() {
            _request.AddParameter("PerPage", _PerPage);
        }

        private void AddPostedWithinToRequest() {
            if (_PostedWithin >= 1 && _PostedWithin <= 30) {
                _request.AddParameter("PostedWithin", _PostedWithin.ToString());
            }
        }

        private void AddRadiusToRequest() {
            if (_Radius >= 5 && _Radius <= 150) {
                _request.AddParameter("Radius", _Radius.ToString());
            }
        }

        private void AddSiteEntityToRequest() {
            if (!string.IsNullOrEmpty(_SiteEntity))
                _request.AddParameter("SiteEntity", _SiteEntity);
        }

        private void AddSOCCodeToRequest() {
            if (!string.IsNullOrEmpty(_Soccode)) {
                _request.AddParameter("SOCCode", _Soccode);
            }
        }

    }
}