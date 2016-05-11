using System;
using System.Collections.Generic;
namespace CBApi.Models {
    [Serializable]
    public class EmployeeType {
        public string Code { get; set; }
        public List<Name> Names { get; set; }
    }
}