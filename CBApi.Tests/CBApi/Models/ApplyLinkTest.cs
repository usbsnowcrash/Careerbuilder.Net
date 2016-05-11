using CBApi.Models;
using NUnit.Framework;
using System.Collections.Specialized;

namespace Tests.com.careerbuilder.CBApi.models
{
    [TestFixture]
    public class ApplyLinkFromNameValueCollectionTest
    {
        private static NameValueCollection args;
        private static ApplyLink applyLink;

        [SetUp]
        public static void Before()
        {
            args = new NameValueCollection() {
                {"JobDID", "Fake_DID"},
                {"SiteID", "Fake_Site_ID"},
                {"HostSite", "US"},
                {"JApply", "Fake_JApply"},
                {"ApplicationEmail", "bob@test.org"},
                {"TrackingID", "Fake_TrackingID"},
                {"Cobrand", "Fake_Cobrand"}
            };
            applyLink = new ApplyLink(args);
        }

        [Test]
        public void ApplyLink_Gets_JobDID_From_NameValueCollection()
        {
            Assert.AreEqual("Fake_DID", applyLink.JobDID);
        }

        [Test]
        public void ApplyLink_Gets_SiteID_From_NameValueCollection()
        {
            Assert.AreEqual("Fake_Site_ID", applyLink.SiteID);
        }

        [Test]
        public void ApplyLink_Gets_HostStie_From_NameValueCollection()
        {
            Assert.AreEqual("US", applyLink.HostSite);
        }

        [Test]
        public void ApplyLink_Gets_JApply_From_NameValueCollection()
        {
            Assert.AreEqual("Fake_JApply", applyLink.JApply);
        }

        [Test]
        public void ApplyLink_Gets_ApplicationEmail_From_NameValueCollection()
        {
            Assert.AreEqual("bob@test.org", applyLink.ApplicationEmail);
        }

        [Test]
        public void ApplyLink_Gets_TrackingID_From_NameValueCollection()
        {
            Assert.AreEqual("Fake_TrackingID", applyLink.TrackingID);
        }

        [Test]
        public void ApplyLink_Gets_Cobrand_From_NameValueCollection()
        {
            Assert.AreEqual("Fake_Cobrand", applyLink.Cobrand);
        }
    }
}
