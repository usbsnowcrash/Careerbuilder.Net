using CBApi.Models.Responses;
using CBApi.Models.Service;

namespace CBApi.Models
{
    public enum BooleanOperator
    {
        AND,
        OR
    }

    public enum OrderByType
    {
        Date,
        Pay,
        Title,
        Company,
        Distace,
        Location,
        Relevance
    }

    public enum OrderDirection
    {
        Ascending,
        Descending
    }

    public interface IJobSearch
    {
        IJobSearch Ascending();
        IJobSearch Descending();
        IJobSearch Limit(int value);
        IJobSearch Offset(int value);
        IJobSearch OrderBy(OrderByType value);
        IJobSearch Radius(int value);
        IJobSearch SelectTop(int value);
        IJobSearch WhereKeywords(string value);
        IJobSearch WhereCategories(params Category[] codes);
        IJobSearch WhereIndustry(params string[] industries);
        IJobSearch WhereCompanyDIDs(params string[] companies);
        IJobSearch WhereCompanyName(string value);
        IJobSearch WhereLocation(string value);
        IJobSearch WhereLocation(float latitude, float longitude);
        IJobSearch WhereCountryCode(CountryCode value);
        IJobSearch WhereHostSite(HostSite value);
        IJobSearch WhereSOCCode(string value);
        IJobSearch WherePayGreaterThan(int value);
        IJobSearch WherePayLessThan(int value);
        IJobSearch WhereSiteEntity(string value);
        
        ResponseJobSearch Search();
    }
}