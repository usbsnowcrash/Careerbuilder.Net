using CBApi.Models.Service;

namespace CBApi {
    public class APISettings {
        public TargetSite TargetSite { get; set; }
        public string DevKey { get; set; }
        public string CobrandCode { get; set; } //If you are a careerbuilder partner you can set these tracking codes
        public string SiteId { get; set; } //Otherwise leave these two parameters alone
        public int TimeoutMS { get; set; }

        public APISettings() {
            TargetSite = new CareerBuilderCom();
            DevKey = "";
            CobrandCode = "";
            SiteId = "";
            TimeoutMS = 30000;
        }
    }
}
