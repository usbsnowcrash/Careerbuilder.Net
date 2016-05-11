using System.Collections.Generic;
using CBApi.Models.Service;

namespace CBApi.Models
{
    public interface IEmployeeTypesRequest
    {
        IEmployeeTypesRequest WhereCountryCode(CountryCode value);
        IEmployeeTypesRequest WhereHostSite(HostSite value);
        List<EmployeeType> ListAll();
    }
}