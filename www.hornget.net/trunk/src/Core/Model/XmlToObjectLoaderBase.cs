using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Core.Model
{
    public class XmlToObjectLoaderBase<T> where T : class
    {
        protected XmlToObjectLoaderBase(string dataDirectory)
        {
            DataDirectory = dataDirectory;
        }

        public string DataDirectory { get; private set; }

        public T Load(string fileName)
        {
            var xmlSerializer = new XmlSerializer(typeof(T));
            var fileStream = new FileStream(BuildPath(DataDirectory, fileName), FileMode.Open);
            var xmlReader = new XmlTextReader(fileStream);
            return (T) xmlSerializer.Deserialize(xmlReader);
        }

        static string BuildPath(string dataDirectory, string fileName)
        {
            return Path.Combine(dataDirectory, fileName);
        }
    }
}