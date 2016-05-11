using System.Xml.Serialization;

namespace CBApi.Models
{
    public class Bucket
    {
        [XmlAttribute("Type")]
        public string Type { get; set; }

        //public Item item { get; set; }
    }
}