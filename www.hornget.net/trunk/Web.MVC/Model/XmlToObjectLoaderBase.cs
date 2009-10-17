using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Web.MVC.Model
{
    public class XmlToObjectLoaderBase<T> where T : class
    {
        public T Load(string filePath)
        {
            var xmlSerializer = new XmlSerializer(typeof(T));
            object result;
            using (var fileStream = new FileStream(filePath, FileMode.Open))
            {
                var xmlReader = new XmlTextReader(fileStream);
                result = xmlSerializer.Deserialize(xmlReader);
            }
            return (T)result;
        }
    }
}