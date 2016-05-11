using System;
using System.Collections.Generic;

namespace CBApi.Models {
    [Serializable]
    public class Education {
        public string Code { get; set; }
        public List<Name> Names { get; set; }
    }
}