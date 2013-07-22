using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using RestSharp;
using RestSharp.Deserializers;
using System.Xml.Linq;
using System.IO;
using CBApi.Models;
using System.Collections.Generic;

namespace Tests.CBApi.models
{
    [TestClass]
    public class SavedSearchListTest
    {
        [TestMethod]
        [DeploymentItem("testdata\\SavedSearchListData.xml")]
        public void DeserializationWorks_WhenPassedRightXML()
        {
            var xmlPath = Path.Combine(Environment.CurrentDirectory, "SavedSearchListData.xml");
            var doc = XDocument.Load(xmlPath);

            var xml = new XmlDeserializer();
            var output = xml.Deserialize<SavedSearchListResponseModel>(new RestResponse() { Content = doc.ToString() });

            Assert.IsNotNull(output, "no deserialization worked");
            Assert.IsNull(output.Errors, "deserialized errors wrong");
            Assert.IsNotNull(output.SavedSearches, "SavedSearched did not Deserialize");
            Assert.IsNotNull(output.SavedSearches.SavedSearchList, "SavedSearchList did not Deserialize");
            //make a SavedSearchList to compare to
            //this will simplify the Asserts
            List<SavedSearch> searches = new List<SavedSearch>();
            SavedSearch viewRetrieve = new SavedSearch();
            viewRetrieve.SearchName = "viewRetrieve";
            viewRetrieve.HostSite = "US";
            viewRetrieve.ExternalID = "366f62b16ac491fed0ed848baa0fbc109128e67b30e75d9614d60ef1fe207d75";
            searches.Add(viewRetrieve);
            SavedSearch lister = new SavedSearch();
            lister.SearchName = "lister";
            lister.HostSite = "US";
            lister.ExternalID = "67f87348bf0da271924400d9197eeba7a291c76362a9398ab1699659c60c585f";
            searches.Add(lister);
            Assert.AreEqual(searches[0].SearchName, output.SavedSearches.SavedSearchList[0].SearchName, "first in searchList searchname did not desearialize");
            Assert.AreEqual(searches[0].HostSite, output.SavedSearches.SavedSearchList[0].HostSite, "first in searchList hostsite did not desearialize");
            Assert.AreEqual(searches[0].ExternalID, output.SavedSearches.SavedSearchList[0].ExternalID, "first in searchList externalID did not desearialize");
            Assert.AreEqual(searches[1].SearchName, output.SavedSearches.SavedSearchList[1].SearchName, "second in searchList searchName did not desearialize");
            Assert.AreEqual(searches[1].HostSite, output.SavedSearches.SavedSearchList[1].HostSite, "second in searchList hostsite did not desearialize");
            Assert.AreEqual(searches[1].ExternalID, output.SavedSearches.SavedSearchList[1].ExternalID, "second in searchList ExternalID did not desearialize");
        }
    }
}
