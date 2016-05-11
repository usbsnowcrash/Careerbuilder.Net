using System;
using System.Collections.Generic;

namespace CBApi.Models {
    [Serializable]
    public class Category {
        public string Code { get; set; }
        public List<Name> Names { get; set; }
    }
    [Serializable]
    public class Name {
        public string Language { get; set; }
        public string Value { get; set; }
    }
}