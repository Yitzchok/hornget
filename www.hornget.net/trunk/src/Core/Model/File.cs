using System.Xml.Serialization;

namespace Core.Model
{
    [XmlRoot(ElementName = "File")]
    public class File
    {
        public string Name { get; set; }
    }
}