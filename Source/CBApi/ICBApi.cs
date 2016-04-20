using System;
using System.Collections.Generic;
using CBApi.Models;
using CBApi.Models.Responses;
using CBApi.Framework.Events;
using System.Collections.Specialized;

namespace CBApi
{
    public interface ICBApi
    {
        event BeforeRequestEvent OnBeforeRequest;
        event AfterRequestEvent OnAfterRequest;
        string CobrandCode { get; set; }
        string DevKey { get; set; }
        string SiteId { get; set; }
        int TimeoutMS { get; set; }
        string GetApplicationForm(string jobDid);
        AccessToken GetAccessToken(string clientId, string clientSecret, string code, string redirectUri);
        Uri GetOAuthRedirectUri(string clientId, string redirectUri, string permissions);
        BlankApplication GetBlankApplication(string jobDid);
        ICategoryRequest GetCategories();
        IEducationCodesRequest GetEducationCodes();
        IEmployeeTypesRequest GetEmployeeTypes();
        Job GetJob(string jobDid);
        List<RecommendJobResult> GetRecommendationsForJob(string jobDid);
        List<RecommendJobResult> GetRecommendationsForJobWithUserPreferences(string jobDid, string userDid);
        List<RecommendJobResult> GetRecommendationsForUser(string externalId);
        IJobSearch JobSearch();
        ResponseJobReport JobReport(string jobDid);
        ResponseApplication SubmitApplication(Application app);
        ResponseApplication SubmitApplication(RequestApplication app);

        SavedSearchCreateResponse CreateSavedSearch(SavedSearchCreate app);
        SavedSearchRetrieveResponseModel RetrieveSavedSearch(SavedSearchRetrieveRequestModel app);
        SavedSearchListResponseModel ListSavedSearches(SavedSearchListRequestModel app);
        SavedSearchUpdateResponseModel UpdateSavedSearch(SavedSearchUpdateRequestModel app);

        SavedSearchDeleteResponse DeleteSavedSearchXML(RequestSavedSearchDelete search);
        AnonymousApplicationResponse AnonymousApplication(AnonymousApplicationRequest request);

        String ApplyLink(NameValueCollection request);
        String ApplyLink(ApplyLink request);
    }
}
