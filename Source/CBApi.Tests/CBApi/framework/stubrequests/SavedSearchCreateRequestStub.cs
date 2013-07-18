using CBApi;
using CBApi.Framework.Requests;
using RestSharp;
using Tests.CBApi.models.service;


namespace Tests.CBApi.models.requests
{
    internal class SavedSearchCreateRequestStub :SavedSearchCreateRequest
    {
        public string DeveloperKey;
        public SavedSearchCreateRequestStub(string key, string domain, string cobrand, string siteid, int timeout)
            : base(new APISettings() { DevKey = key, CobrandCode = cobrand, SiteId = siteid, TargetSite = new TargetSiteMock(domain), TimeoutMS = timeout })
        {
            
        }

        public IRestClient Client { get { return _client; } set { _client = value; } }
        public IRestRequest Request { get { return _request; } set { _request = value; } }
    }
}
