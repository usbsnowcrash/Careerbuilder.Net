using System.Collections.Generic;
using CBApi.Properties;
using CBApi.Framework.Requests;
using CBApi.Models;
using CBApi.Models.Responses;
using CBApi.Models.Service;
using System;
using CBApi.Framework.Events;

namespace CBApi {
    public class CbApi : ICBApi {
        #region attributes
        protected APISettings _Settings = new APISettings();
        protected List<BeforeRequestEvent> _BeforeListeners = new List<BeforeRequestEvent>();
        protected List<AfterRequestEvent> _AfterListeners = new List<AfterRequestEvent>();

        public string DevKey { 
            get {return _Settings.DevKey;}
            set { _Settings.DevKey = value; } 
        }
        public string CobrandCode {
            get { return _Settings.CobrandCode; }
            set { _Settings.CobrandCode = value; }
        }
        public string SiteId {
            get { return _Settings.SiteId; }
            set { _Settings.SiteId = value; }
        }

        public virtual int TimeoutMS {
            get { return _Settings.TimeoutMS; }
            set { _Settings.TimeoutMS = value; }
        }

        public event BeforeRequestEvent OnBeforeRequest {
            add { _BeforeListeners.Add(value); }
            remove { _BeforeListeners.Remove(value); }
        }

        public event AfterRequestEvent OnAfterRequest {
            add { _AfterListeners.Add(value); }
            remove { _AfterListeners.Remove(value); }
        }
        #endregion

        #region construction and factories

        protected internal CbApi() {
            _Settings.TargetSite = new CareerBuilderCom();
            _Settings.DevKey = Settings.Default.DevKey;
        }

        protected internal CbApi(string key) {
            _Settings.TargetSite = new CareerBuilderCom();
            _Settings.DevKey = key;
        }

        protected internal CbApi(string key, int timeout) {
            _Settings.TargetSite = new CareerBuilderCom();
            _Settings.DevKey = key;
            _Settings.TimeoutMS = timeout;
        }

        protected internal CbApi(string key, int timeout, TargetSite site) {
            _Settings.TargetSite = site;
            _Settings.DevKey = key;
            _Settings.TimeoutMS = timeout;
        }

        protected internal CbApi(string key, string cobrandCode) {
            _Settings.TargetSite = new CareerBuilderCom();
            _Settings.DevKey = key;
            _Settings.CobrandCode = cobrandCode;
        }

        protected internal CbApi(string key, string cobrandCode, string siteid) {
            _Settings.TargetSite = new CareerBuilderCom();
            _Settings.DevKey = key;
            _Settings.CobrandCode = cobrandCode;
            _Settings.SiteId = siteid;
        }

        #endregion

        protected void WireBeforeRequestEvents(BaseRequest req) {
            foreach (var item in _BeforeListeners) {
                req.OnBeforeRequest += item;
            }
        }

        protected void WireAfterRequestEvents(BaseRequest req) {
            foreach (var item in _AfterListeners) {
                req.OnAfterRequest += item;
            }
        }

        #region api calls
        /// <summary>
        /// Make a call to /auth/token
        /// </summary>
        /// <param name="clientId">20 character long external client ID.</param>
        /// <param name="clientSecret">64 character long external client secret.</param>
        /// <param name="code">20 character long OAuth authorization grant code returned from auth/prompt redirection.</param>
        /// <param name="redirectUri">URL that was provided at the time of external client registration.</param>
        /// <returns></returns>
        public AccessToken GetAccessToken(string clientId, string clientSecret, string code, string redirectUri) {
            var req = new AuthTokenRequest(clientId, clientSecret, code, redirectUri, _Settings);
            WireBeforeRequestEvents(req);
            WireAfterRequestEvents(req);
            return req.GetAccessToken();
        }
        
        /// <summary>
        /// Gets the Uri to redirect to for OAuth
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="redirectUri"></param>
        /// <param name="permissions"></param>
        /// <returns></returns>
        public Uri GetOAuthRedirectUri(string clientId, string redirectUri, string permissions) {
            var req = new OAuthRedirectBuilder(clientId, redirectUri, permissions, _Settings.TargetSite.Domain);
            return req.OAuthUri();
        }

        /// <summary>
        /// Make a call to /v1/application/blank
        /// </summary>
        /// <param name="JobDID">The unique ID of the job</param>
        /// <returns>The job</returns>
        public BlankApplication GetBlankApplication(string jobDid) {
            var req = new BlankApplicationRequest(jobDid, _Settings);
            WireBeforeRequestEvents(req);
            WireAfterRequestEvents(req);
            return req.Retrieve();
        }

        /// <summary>
        /// Make a call to /v1/application/form
        /// </summary>
        /// <param name="JobDID">The unique ID of the job</param>
        /// <returns>The job</returns>
        public string GetApplicationForm(string jobDid) {
            var req = new ApplicationFormRequest(jobDid, _Settings);
            WireBeforeRequestEvents(req);
            WireAfterRequestEvents(req);
            return req.Retrieve();
        }

        /// <summary>
        /// Submit an application to /v1/application/submit
        /// </summary>
        /// <param name="app">The application being submited to careerbuilder</param>
        /// <returns></returns>
        public ResponseApplication SubmitApplication(Application app) {
            RequestApplication req = new RequestApplication();
            req.CoBrand = app.CoBrand;
            req.DeveloperKey = app.DeveloperKey;
            req.JobDID = app.JobDID;
            req.SiteID = app.SiteID;
            req.Test = app.Test;
            req.Resume = app.Resume;
            List<Response> responses = new List<Response>();
            foreach (var item in app.Questions) {
                responses.Add(new Response() { QuestionID = item.QuestionID, ResponseText = item.ResponseText });
            }
            req.Responses = responses;
            return SubmitApplication(req);
        }

        /// <summary>
        /// Submit an application to /v1/application/submit
        /// </summary>
        /// <param name="app">The application being submited to careerbuilder</param>
        /// <returns></returns>
        public ResponseApplication SubmitApplication(RequestApplication app) {
            var req = new SubmitApplicationRequest(_Settings);
            WireBeforeRequestEvents(req);
            WireAfterRequestEvents(req);
            return req.Submit(app);
        }

        /// <summary>
        /// Make a call to /v1/categories
        /// </summary>
        /// <returns>A Category Request to query against</returns>
        public ICategoryRequest GetCategories() {
            var req = new CategoriesRequest(_Settings);
            WireBeforeRequestEvents(req);
            WireAfterRequestEvents(req);
            return req;
        }

        /// <summary>
        /// Make a call to /v1/employeetypes
        /// </summary>
        /// <returns>A Employee Request to query against</returns>
        public IEmployeeTypesRequest GetEmployeeTypes() {
            var req = new EmployeeTypesRequest(_Settings);
            WireBeforeRequestEvents(req);
            WireAfterRequestEvents(req);
            return req;
        }
     
        /// <summary>
        /// Make a call to /v1/job
        /// </summary>
        /// <param name="JobDID">The unique ID of the job</param>
        /// <returns>The job</returns>
        public Job GetJob(string jobDid) {
            var req = new JobRequest(jobDid, _Settings);
            WireBeforeRequestEvents(req);
            WireAfterRequestEvents(req);
            return req.Retrieve();
        }

        /// <summary>
        /// Make a call to UserInfo service with endpoint /v2/user/checkexisting
        /// </summary>
        /// <param name="checkExisting">The email/password user info combo</param>
        /// <returns>The User's status in the system and External DID</returns>
        public ResponseUserCheck CheckExistingUser(UserCheck checkExisting) {
            checkExisting.DeveloperKey = _Settings.DevKey;

            var req = new CheckExistingUserRequest(_Settings);
            WireBeforeRequestEvents(req);
            WireAfterRequestEvents(req);
            return req.GetUserCheck(checkExisting);
        }

        /// <summary>
        /// Make a call to /v1/recommendations/forjob
        /// </summary>
        /// <param name="jobDID">The unique ID of the job</param>
        /// <returns>The list of recommended jobs, based on the given job.</returns>
        public List<RecommendJobResult> GetRecommendationsForJob(string jobDid) {
            var req = new JobRecommendationsRequest(jobDid, _Settings);
            WireBeforeRequestEvents(req);
            WireAfterRequestEvents(req);
            return req.GetRecommendations();
        }

        /// <summary>
        /// Make a call to /v1/recommendations/forjobwithuserprefs
        /// </summary>
        /// <param name="jobDID">The unique ID of the job</param>
        /// <param name="userDid">The unique ID of the user</param>
        /// <returns>The list of recommended jobs, based on the given job and taking the user's preferences into account.</returns>
        public List<RecommendJobResult> GetRecommendationsForJobWithUserPreferences(string jobDid, string userDid) {
            var req = new JobRecommendationsWithUserPreferencesRequest(jobDid, userDid, _Settings);
            WireBeforeRequestEvents(req);
            WireAfterRequestEvents(req);
            return req.GetRecommendations();
        }

        /// <summary>
        /// make a call to /v1/recommendations/foruser
        /// </summary>
        /// <param name="externalId">The ID of the user that you wish to get recs for</param>
        /// <returns></returns>
        public List<RecommendJobResult> GetRecommendationsForUser(string externalId) {
            var req = new UserRecommendationsRequest(externalId, _Settings);
            WireBeforeRequestEvents(req);
            WireAfterRequestEvents(req);
            return req.GetRecommendations();
        }

        /// <summary>
        /// Make a call to /v1/jobsearch
        /// </summary>
        /// <returns>A Job Request to query against</returns>
        public IJobSearch JobSearch() {
            var req = new JobSearchRequest(_Settings);
            WireBeforeRequestEvents(req);
            WireAfterRequestEvents(req);
            return req;
        }

        public ResponseJobReport JobReport(string jobDid) {
            var req = new JobReportRequest(jobDid, _Settings);
            WireBeforeRequestEvents(req);
            WireAfterRequestEvents(req);
            return req.GetReport();
        }

        #endregion
    }
}