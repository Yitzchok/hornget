using System.Collections.Generic;
using System.Xml.Serialization;

namespace Web.MVC.Model
{
    [XmlRoot(ElementName = "Category", Namespace = "http://hornget.com/services")]
    public class PackageStructure : Category 
    {
    }
}