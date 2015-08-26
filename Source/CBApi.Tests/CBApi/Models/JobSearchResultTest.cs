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

namespace Tests.CBApi.models {
    [TestClass]
    public class JobSearchResultTest {

        [TestMethod]
        [DeploymentItem("testdata\\ResponseJobSearch.xml")]
        public void DeserializationWorks_WhenPassedRightXML() {
            var xmlpath = Path.Combine(Environment.CurrentDirectory,"ResponseJobSearch.xml");
            var doc = XDocument.Load(xmlpath);

            var xml = new XmlDeserializer();
            var output = xml.Deserialize<ResponseJobSearch>(new RestResponse() { Content = doc.ToString() });

            Assert.IsNotNull(output.Results);
            Assert.AreEqual(25, output.Results.Count);
            Assert.AreEqual("Direct Sales Recruiting, LLC", output.Results[0].Company);
            Assert.AreEqual("c35x165rs82x1bnbbc", output.Results[0].CompanyDID);
            Assert.AreEqual("http://www.careerbuilder.com/Jobs/Company/C35X165RS82X1BNBBC/Direct-Sales-Recruiting-LLC/?sc_cmp1=13_JobRes_ComDet", output.Results[0].CompanyDetailsURL);
            Assert.AreEqual("JHM0LH6LPCGLF3FR3ZB", output.Results[0].DID);
            Assert.AreEqual("Mars", output.Results[0].DisplayCity);
            Assert.AreEqual("41-4011.00", output.Results[0].OnetCode);
            Assert.AreEqual("Full-Time", output.Results[0].EmploymentType);
            Assert.AreEqual("http://api.careerbuilder.com/v1/joblink?TrackingID=DLR0T71H&DID=JHM0LH6LPCGLF3FR3ZB", output.Results[0].JobDetailsURL);
            Assert.AreEqual("http://api.careerbuilder.com/v1/job?DID=JHM0LH6LPCGLF3FR3ZB&DeveloperKey=WXXXXXXXXXXXXXXXXXX", output.Results[0].JobServiceURL);
            Assert.AreEqual("TN - Chattanooga", output.Results[0].Location);
            Assert.AreEqual(35.04644f, output.Results[0].LocationLatitude);
            Assert.AreEqual(-85.30946f, output.Results[0].LocationLongitude);
            Assert.AreEqual("10/28/2012", output.Results[0].PostedDate);
            Assert.AreEqual("5/15/2014 7:19:36 AM", output.Results[0].PostedTime);
            Assert.AreEqual("$70k - $80k/year", output.Results[0].Pay);
            Assert.AreEqual("http://www.careerbuilder.com/jobseeker/jobs/recommendedjobs.aspx?ipath=JELO&job_did=JHM0LH6LPCGLF3FR3ZB", output.Results[0].SimilarJobsURL);
            Assert.AreEqual("Medical Sales-Tissue Graft/Biologics-Wound Care", output.Results[0].JobTitle);
            Assert.AreEqual("http://emj.icbdr.com/MediaManagement/QS/I8F50C63D7B6SGPDXQS.gif", output.Results[0].CompanyImageURL);
            Assert.IsNotNull(output.Facets);
            Assert.AreEqual(0, output.Facets.Count);
        }

        [TestMethod]
        [DeploymentItem("testdata\\ResponseJobSearchWithFacets.xml")]
        public void DeserializationWorks_WhenPassedRightXML_WithFacets() {
            var xmlpath = Path.Combine(Environment.CurrentDirectory,"ResponseJobSearchWithFacets.xml");
            var doc = XDocument.Load(xmlpath);

            var xml = new XmlDeserializer();
            var output = xml.Deserialize<ResponseJobSearch>(new RestResponse() { Content = doc.ToString() });

            Assert.IsNotNull(output.Results);
            Assert.AreEqual(9, output.Results.Count);

            var job = output.Results[2];
            Assert.AreEqual("Bartech Group", job.Company);
            Assert.AreEqual("c8g5xl6rqnqy6pmmtkb", job.CompanyDID);
            Assert.AreEqual("http://www.careerbuilder.com/Jobs/Company/C8G5XL6RQNQY6PMMTKB/Bartech-Group/?sc_cmp1=13_JobRes_ComDet", job.CompanyDetailsURL);
            Assert.AreEqual("JB71S364NQ0X8VY51WD", job.DID);
            Assert.AreEqual("15-1071.00", job.OnetCode);
            Assert.AreEqual("Network and Computer Systems Administrators", job.ONetFriendlyTitle);
            //DescriptionTeaser left out intentionally tl;dr
            Assert.AreEqual("Nearby", job.Distance);
            Assert.AreEqual("Full-Time", job.EmploymentType);
            Assert.AreEqual("http://api.careerbuilder.com/v1/joblink?TrackingID=CBWG7KSY&DID=JB71S364NQ0X8VY51WD", job.JobDetailsURL);
            Assert.AreEqual("https://api.careerbuilder.com/v1/job?DID=JB71S364NQ0X8VY51WD&DeveloperKey=WDRF81661JM5WPB63DGJ", job.JobServiceURL);
            Assert.AreEqual("GA - Various US Locations", job.Location);
            Assert.AreEqual(33.74831f, job.LocationLatitude);
            Assert.AreEqual(-84.39111f, job.LocationLongitude);
            Assert.AreEqual("N/A", job.Pay);
            Assert.AreEqual("http://www.careerbuilder.com/Jobs/SimilarJobs.aspx?ipath=JELO&job_did=JB71S364NQ0X8VY51WD", job.SimilarJobsURL);
            Assert.AreEqual("IT Network Engineer (Network Administrator)", job.JobTitle);
            Assert.AreEqual("http://emj.icbdr.com/MediaManagement/7P/MVJ81466WSNX4T1SG7P.gif", job.CompanyImageURL);
            
            Assert.IsNotNull(output.Facets);
            Assert.AreEqual(7, output.Facets.Count);

            var facetNoItems = output.Facets[6];
            Assert.AreEqual("FacetNormalizedCompanyDID", facetNoItems.JobSearchRequestParameter);
            Assert.IsNotNull(facetNoItems.Items);
            Assert.AreEqual(0, facetNoItems.Items.Count);

            var facetOneItem = output.Facets[3];
            Assert.AreEqual("FacetState", facetOneItem.JobSearchRequestParameter);
            Assert.IsNotNull(facetOneItem.Items);
            Assert.AreEqual(1, facetOneItem.Items.Count);
            Assert.AreEqual("\"GA\"", facetOneItem.Items[0].JobSearchRequestValue);
            Assert.AreEqual("Georgia", facetOneItem.Items[0].DisplayValue);
            Assert.AreEqual(1868, facetOneItem.Items[0].Count);
        }

    }
}
