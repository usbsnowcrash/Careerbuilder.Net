using System;

namespace CBApi.Models.Facets {
    [Serializable]
    public class Item {

        public string JobSearchRequestValue { get; set; }
        public string DisplayValue { get; set; }
        public int Count { get; set; }

    }
}