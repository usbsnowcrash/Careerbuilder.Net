using System.Collections.Generic;
using CBApi.Models.Service;

namespace CBApi.Models
{
    public interface ICategoryRequest
    {
        ICategoryRequest WhereCountryCode(CountryCode value);
        ICategoryRequest WhereHostSite(HostSite value);
        List<Category> ListAll();
    }
}