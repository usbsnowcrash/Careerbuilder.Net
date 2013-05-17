using System.Collections.Generic;
using CBApi.Models.Responses;
using CBApi.Models.Service;

namespace CBApi.Models {

    public enum BooleanOperator {
        AND,
        OR
    }

    public enum FacetField {
        FacetCategory,
        FacetCompany,
        FacetCity,
        FacetState,
        FacetCityState,
        FacetPay,
        FacetNormalizedCompanyDID
    }

    public enum OrderByType {
        Date,
        Pay,
        Title,
        Company,
        Distace,
        Location,
        Relevance
    }

    public enum OrderDirection {
        Ascending,
        Descending
    }

    public interface IJobSearch {

        IJobSearch Ascending();

        IJobSearch ApplyRequirements(string value);

        IJobSearch Descending();

        IJobSearch ExcludeApplyRequirements(string value);

        IJobSearch ExcludeJobsWithoutSalary();

        IJobSearch ExcludeNationwideJobs();

        IJobSearch ExcludeNontraditionalJobs();

        IJobSearch Limit(int value);

        IJobSearch Offset(int value);

        IJobSearch OrderBy(OrderByType value);

        IJobSearch Radius(int value);

        ResponseJobSearch Search();

        IJobSearch SelectCount(int value);

        IJobSearch SelectPage(int value);

        IJobSearch SelectTop(int value);

        IJobSearch ShowFacets();

        IJobSearch WhereCategories(params Category[] codes);

        IJobSearch WhereCompanyDIDs(params string[] companies);

        IJobSearch WhereCompanyName(string value);

        IJobSearch WhereCountryCode(CountryCode value);

        IJobSearch WhereEducationCodeEquals(string educationCode);

        IJobSearch WhereEducationCodeMaximum(string educationCode);

        IJobSearch WhereEmployeeTypes(params string[] employmentTypes);

        IJobSearch WhereFacets(params KeyValuePair<FacetField, string>[] facets);

        IJobSearch WhereHostSite(HostSite value);

        IJobSearch WhereIndustry(params string[] industries);

        IJobSearch WhereKeywords(string value);

        IJobSearch WhereLocation(string value);

        IJobSearch WhereLocation(float latitude, float longitude);

        IJobSearch WhereNotCompanyName(params string[] companyNames);

        IJobSearch WhereNotJobTitle(params string[] jobTitles);

        IJobSearch WhereNotKeywords(params string[] keywords);

        IJobSearch WherePayGreaterThan(int minimumPay);

        IJobSearch WherePayLessThan(int maximumPay);

        IJobSearch WherePerPage(int value);

        IJobSearch WherePostedWithin(int numberOfDays);

        IJobSearch WhereSiteEntity(string value);

        IJobSearch WhereSOCCode(string value);
    }
}