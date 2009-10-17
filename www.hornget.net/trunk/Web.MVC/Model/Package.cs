using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Web.MVC.Model
{
    [XmlRoot(ElementName = "Package", Namespace = "http://hornget.com/services")]
    public class Package
    {
        public string Name { get; set; }

        [XmlArray(ElementName = "MetaData")]
        [XmlArrayItem("MetaData")]
        public List<MetaData> MetaData { get; set; }
        public string Version { get; set; }
        public Type Type
        {
            get { return typeof (Package); }
        }

        public string Url { get; set; }
    }
}