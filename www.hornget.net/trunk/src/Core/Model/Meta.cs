using System.Xml.Serialization;

namespace Core.Model
{
    [XmlRoot(ElementName = "Meta")]
    public class Meta
    {
        public string Forum { get; set; }
        public string Homepage { get; set; }
        public string HomepageUrl { get; set; }
        public string Notes { get; set; }
    }
}