using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Web.MVC.Model
{
    [XmlRoot(ElementName = "Category", Namespace = "http://hornget.com/services")]
    public class Category
    {
        public string Name { get; set; }
        
        [XmlArray(ElementName = "Categories")]
        [XmlArrayItem("Category")]
        public List<Category> Categories { get; set; }

        [XmlArray(ElementName = "Packages")]
        [XmlArrayItem("Package")]
        public List<Package> Packages { get; set; }

        public string Url { get; set;}
    }
}