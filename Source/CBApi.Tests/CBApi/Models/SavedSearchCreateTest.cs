using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using RestSharp;
using RestSharp.Deserializers;
using System.Xml.Linq;
using System.IO;
using CBApi.Models;

namespace Tests.CBApi.models
{
    [TestClass]
    public class SavedSearchCreateTest
    {
        [TestMethod]
        [DeploymentItem("testdata\\SaveSearchCreateData.xml")]
        public void DeserializationWorks_WhenPassedRightXML()
        {
            var xmlPath = Path.Combine(Environment.CurrentDirectory, "SaveSearchCreateData.xml");
            var doc = XDocument.Load(xmlPath);
            
            var xml = new XmlDeserializer();
            var output = xml.Deserialize<SavedSearchCreateResponse>(new RestResponse() { Content = doc.ToString() });
            Assert.IsNotNull(output, "no deserialization worked");
            Assert.IsNotNull(output.SavedSearch.SavedSearchParameters, "SavedSearchParameters deserialization Did not work");
            Assert.AreEqual("lotsloc", output.SavedSearch.SearchName, "search name did not dezerialize");
            Assert.AreEqual("none", output.SavedSearch.IsDailyEmail.ToLower(), "IsDailyEmail did not dezerialize");
            Assert.AreEqual("Chicago, Il, Atlanta, Ga, New York, Ny", output.SavedSearch.SavedSearchParameters.Location, "Location did not dezerialize");
            Assert.AreEqual(false, output.SavedSearch.SavedSearchParameters.ExcludeNational, "ExcludeNational did not dezerialize");
            Assert.AreEqual("DRNS", output.SavedSearch.SavedSearchParameters.EducationCode, "educationCode did not dezerialize");
            Assert.AreEqual("AND", output.SavedSearch.SavedSearchParameters.BooleanOperator, "booleanoperator did not dezerialize");
            Assert.AreEqual("Pay", output.SavedSearch.SavedSearchParameters.OrderBy, "OrderBy did not dezerialize");
            Assert.AreEqual(70, output.SavedSearch.SavedSearchParameters.PayHigh, "pay high did not dezerialize");
            Assert.AreEqual(40, output.SavedSearch.SavedSearchParameters.PayLow, "Pay Low did not dezerialize");
            Assert.AreEqual(30, output.SavedSearch.SavedSearchParameters.PostedWithin, "posted within did not dezerialize");
            Assert.AreEqual(30, output.SavedSearch.SavedSearchParameters.Radius, "radius did not dezerialize");
            Assert.AreEqual(false, output.SavedSearch.SavedSearchParameters.PayInfoOnly, "pay info only did not dezerialize");
            Assert.AreEqual(false, output.SavedSearch.SavedSearchParameters.SpecificEducation, "specific education did not dezerialize");
            Assert.AreEqual("ascending", output.SavedSearch.SavedSearchParameters.OrderDirection, "order direction did not dezerialize");
        }
    }
}
