using System;
using System.Collections.Generic;
using CBApi.Models;
using CBApi.Models.Responses;
using CBApi.Framework.Events;

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
        IEmployeeTypesRequest GetEmployeeTypes();
        Job GetJob(string jobDid);
        List<RecommendJobResult> GetRecommendationsForJob(string jobDid);
        List<RecommendJobResult> GetRecommendationsForJobWithUserPreferences(string jobDid, string userDid);
        List<RecommendJobResult> GetRecommendationsForUser(string externalId);
        IJobSearch JobSearch();
        ResponseJobReport JobReport(string jobDid);
        ResponseApplication SubmitApplication(Application app);
        ResponseApplication SubmitApplication(RequestApplication app);
    }
}