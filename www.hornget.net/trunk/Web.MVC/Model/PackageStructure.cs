using System.Collections.Generic;
using System.Xml.Serialization;

namespace Web.MVC.Model
{
    [XmlRoot(ElementName = "ArrayOfCategory", Namespace = "http://hornget.com/services")]
    public class PackageStructure : List<Category> 
    {
    }
}