using System;

namespace CBApi.Framework.Requests {
    internal class JobRecommendationsWithUserPreferencesRequest : JobRecommendationsRequest {
        protected string _userDid;

        public JobRecommendationsWithUserPreferencesRequest(string jobDid, string userDid, APISettings settings) : base(jobDid, settings) {
            if (string.IsNullOrEmpty(userDid)) {
                throw new ArgumentNullException();
            }
            if (userDid.Length >= 18 && userDid.Length <= 20 &&
                userDid.StartsWith("U", StringComparison.InvariantCultureIgnoreCase)) {
                    _userDid = userDid;
            } else {
                throw new ArgumentException("This does not look like a userDid");
            }
        }

        public override string BaseUrl {
            get { return "/v1/recommendations/forjobwithuserprefs"; }
        }

        protected override void BeforeRequest() {
            _request.AddParameter("UserDID", _userDid);
            base.BeforeRequest();
        }
    }
}
