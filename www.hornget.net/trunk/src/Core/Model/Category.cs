using System.Xml.Serialization;

namespace Core.Model
{
    [XmlRoot(ElementName = "Category")]
    public class Category
    {
        public string Name { get; set; }   
        public string Url { get; set; }   
        public string Description { get; set; }   
        public string Image { get; set; }   
    }
}