using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CBApi.Models {
    public class SearchLocation {

        public string City { get; set; }
        public string StateCode { get; set; }
        public string CountryCode { get; set; }
        public string PostalCode { get; set; }

    }
}