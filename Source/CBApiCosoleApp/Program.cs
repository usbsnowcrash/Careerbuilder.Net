using System;
using System.Collections.Generic;
using CBApi;
using CBApi.Models;
using CBApi.Models.Responses;
using CBApi.Models.Service;
using RestSharp;
using CBApi.Framework.Events;

namespace CBApiCosoleApp {
    internal class Program {

        private static void HandleBeforeRequest(IRequestEventData data){
            Console.WriteLine("caught before request: " + data.BaseURL);
        }

        private static void HandleAfterRequest(IRequestEventData data) {
            Console.WriteLine("caught after request: " + data.ResponseContent);
        }

        private static void Main(string[] args) {
            ICBApi svc = API.GetInstance("EnterDevKey", 20000);
            svc.OnBeforeRequest += new BeforeRequestEvent(HandleBeforeRequest);
            svc.OnAfterRequest += new AfterRequestEvent(HandleAfterRequest);

            Console.WriteLine(svc.ApplyLink(new System.Collections.Specialized.NameValueCollection() { { "HostSite", "US" }, { "JobDID", "J3H2GZ67DPGRQV8287H" } }));

            //////ResponseJobReport jobReport = svc.JobReport("J3J67S75826K34DRBMB");


            ////Make a call to https://api.careerbuilder.com/v2/categories
            List<Category> codes = svc.GetCategories()
                .WhereCountryCode(CountryCode.US)
                .ListAll();
            foreach (Category code in codes) {
                Console.WriteLine(code.Code);
            }

            //Make a call to https://api.careerbuilder.com/v2/employeetypes
            List<EmployeeType> emps = svc.GetEmployeeTypes()
                .WhereCountryCode(CountryCode.US)
                .ListAll();
            foreach (EmployeeType emp in emps) {
                Console.WriteLine(emp.Code);
            }

            //Search for Jobs
            ResponseJobSearch search = svc.JobSearch()
                .WhereKeywords("Software Engineering")
                .WhereLocation("Atlanta,GA")
                .WhereCountryCode(CountryCode.US)
                .OrderBy(OrderByType.Title)
                .Ascending()
                .Search();
            List<JobSearchResult> jobs = search.Results;
            foreach (JobSearchResult item in jobs) {
                Console.WriteLine(item.JobTitle);
            }

            //Make a call to https://api.careerbuilder.com/v2/recommendations/forjob
            List<RecommendJobResult> jobRecs = svc.GetRecommendationsForJob(jobs[0].DID);
            foreach (RecommendJobResult item in jobRecs) {
                Console.WriteLine(item.Title);
            }

            //Make a call to https://api.careerbuilder.com/v2/job
            Job myJob = svc.GetJob(jobs[0].DID);
            Console.WriteLine(myJob.JobTitle);

            //Make a call to https://api.careerbuilder.com/v2/application/blank
            BlankApplication myApp = svc.GetBlankApplication("JHQ7G55WH9YLR8T1N78");

            //Make a call to https://api.careerbuilder.com/v2/application/submit in test mode
            myApp.Test = true;
            foreach (var item in myApp.Questions) {
                item.ResponseText = "true";
            }
            ResponseApplication tempResponse = svc.SubmitApplication(myApp);
            Console.WriteLine(tempResponse.ApplicationStatus);

            //Make a call to https://api.careerbuilder.com/v2/application/form
            string form = svc.GetApplicationForm("JHQ7G55WH9YLR8T1N78");
            Console.WriteLine(form);
        }
    }
}