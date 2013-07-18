using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp;
using RestSharp.Deserializers;
using System.Xml;
using System.Xml.Linq;
using CBApi.Models.Responses;
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
            var output = xml.Deserialize<SavedSearchCreate>(new RestResponse() { Content = doc.ToString() });
            Assert.IsNotNull(output);
            Assert.IsNotNull(output.SavedSearchParameters);
            Assert.AreEqual("lotsloc", output.SearchName);
            Assert.AreEqual("none", output.IsDailyEmail.ToLower());
            Assert.AreEqual("Chicago, Il, Atlanta, Ga, New York, Ny", output.SavedSearchParameters.Location);
            Assert.AreEqual(false,output.SavedSearchParameters.ExcludeNational);
            Assert.AreEqual("DRNS",output.SavedSearchParameters.EducationCode);
            Assert.AreEqual("AND", output.SavedSearchParameters.BooleanOperator);
            Assert.AreEqual("Pay", output.SavedSearchParameters.OrderBy);
            Assert.AreEqual(70,output.SavedSearchParameters.PayHigh);
            Assert.AreEqual(40,output.SavedSearchParameters.PayLow);
            Assert.AreEqual(30,output.SavedSearchParameters.PostedWithin);
            Assert.AreEqual(30, output.SavedSearchParameters.Radius);
            Assert.AreEqual(false,output.SavedSearchParameters.PayInfoOnly);
            Assert.AreEqual(false,output.SavedSearchParameters.SpecificEducation);
            Assert.AreEqual("ascending", output.SavedSearchParameters.OrderDirection);
        }
    }
}
