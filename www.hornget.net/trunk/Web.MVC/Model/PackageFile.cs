using System.Xml.Serialization;

namespace Web.MVC.Model
{
    [XmlRoot(ElementName = "PackageFile", Namespace = "http://hornget.com/services")]
    public class PackageFile
    {
        public string Name { get; set; }
    }
}