using System.Collections.Generic;
using CBApi.Models.Service;

namespace CBApi.Models
{
    public interface IEducationCodesRequest
    {
        IEducationCodesRequest WhereCountryCode(CountryCode value);
        List<Education> ListAll();
    }
}