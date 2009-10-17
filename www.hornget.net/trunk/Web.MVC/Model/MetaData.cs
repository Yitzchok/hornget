using System.Xml.Serialization;

namespace Web.MVC.Model
{
    [XmlRoot(ElementName = "MetaData", Namespace = "http://hornget.com/services")]
    public class MetaData
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}