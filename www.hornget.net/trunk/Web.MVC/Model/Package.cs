using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Web.MVC.Model
{
    [XmlRoot(ElementName = "Package", Namespace = "http://hornget.com/services")]
    public class Package
    {
        public Package()
        {
            Contents = new List<PackageFile>();
        }

        public string Name { get; set; }

        [XmlArrayItem("MetaData")]
        public List<MetaData> MetaData { get; set; }
        public string Version { get; set; }

        [XmlArrayItem("PackageFile")]
        public List<PackageFile> Contents { get; set; }

        public PackageFile ZipFileName { get; set; }

        public bool IsError { get; set; }

        public string ErrorMessage { get; set; }

        public string DownloadUrl()
        {
            return string.Format("/downloads/{0}/{1}", Url.Substring(0, Url.LastIndexOf('/')), ZipFileName.Name);
        }
        public string Url { get; set; }
    }
}