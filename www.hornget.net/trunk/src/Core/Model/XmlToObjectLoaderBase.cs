using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Core.Model
{
    public class XmlToObjectLoaderBase<T> where T : class
    {
        public T Load(string filePath)
        {
            var xmlSerializer = new XmlSerializer(typeof(T));
            var fileStream = new FileStream(filePath, FileMode.Open);
            var xmlReader = new XmlTextReader(fileStream);
            return (T) xmlSerializer.Deserialize(xmlReader);
        }
    }
}