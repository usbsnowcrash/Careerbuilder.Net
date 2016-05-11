using System;
using System.IO;
using System.Xml.Linq;
using CBApi.Models;
using NUnit.Framework;
using RestSharp;
using RestSharp.Deserializers;

namespace Tests.com.careerbuilder.CBApi.models {
    [TestFixture]
    public class SavedSearchListResponseModelTest {

        [Test]
        public void DeserializationWorks_WhenPassedRightXML() {
            var xmlPath = Path.Combine(Environment.CurrentDirectory, "SavedSearchListResponseModel.xml");
            var doc = XDocument.Load(xmlPath);

            var xml = new XmlDeserializer();
            var response = xml.Deserialize<SavedSearchListResponseModel>(new RestResponse() { Content = doc.ToString() });

            Assert.IsNotNull(response);
            Assert.IsNotNull(response.SavedSearches);
            Assert.AreEqual(2, response.SavedSearches.Count, "The count of saved searches in the xml file should be 2");

            SavedSearch search1 = response.SavedSearches[0];
            Assert.AreEqual("software engineer", search1.SearchName);
            Assert.AreEqual("US", search1.HostSite);
            Assert.AreEqual("ea6c190017576af21724885aa7bde734130a06bbf32fd5e30f0ba814e2a68b2d", search1.ExternalID);

            SavedSearch search2 = response.SavedSearches[1];
            Assert.AreEqual("engineer in 30071", search2.SearchName);
            Assert.AreEqual("US", search2.HostSite);
            Assert.AreEqual("f28273259cf31cbd1c0d07bf727deb8447a54d7e1c59d32399b937d469cf0a54", search2.ExternalID);
        }

    }
}