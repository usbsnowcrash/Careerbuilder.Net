using System;
using System.Collections.Generic;

namespace CBApi.Models.Facets {
    [Serializable]
    public class Facet {

        public string JobSearchRequestParameter { get; set; }
        public List<Item> Items { get; set; }

    }
}