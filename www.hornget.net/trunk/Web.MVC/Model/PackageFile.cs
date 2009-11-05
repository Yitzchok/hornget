using System.Xml.Serialization;

namespace Web.MVC.Model
{
    [XmlRoot(ElementName = "ZipFileName", Namespace = "http://hornget.com/services")]
    public class PackageFile
    {
        public string Name { get; set; }
    }
}