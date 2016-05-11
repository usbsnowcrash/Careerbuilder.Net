using System;
namespace CBApi.Models {
    [Serializable]
    public class Item {
        public string Key { get; set; }
        public int Count { get; set; }
    }
}