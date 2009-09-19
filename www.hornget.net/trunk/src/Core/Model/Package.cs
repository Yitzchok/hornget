using System.Collections.Generic;
using System.Xml.Serialization;

namespace Core.Model
{
    [XmlRoot(ElementName = "Package")]
    public class Package
    {
        public Package()
        {
            Contents = new List<File>();
            Meta = new Meta();
        }

        public string Category { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public string Version { get; set; }
        public Meta Meta { get; set; }
        public string FileName { get; set; }

        [XmlArray(ElementName = "Contents")]
        [XmlArrayItem("File")]
        public List<File> Contents { get; set; }
    }
}