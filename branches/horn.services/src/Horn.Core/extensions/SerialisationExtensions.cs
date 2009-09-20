using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;

namespace Horn.Core.extensions
{
    public static class SerialisationExtensions
    {
        public static string ToDataContractXml<T>(this object objectToSerialise)
        {
            var memoryStream = new MemoryStream();
            var writer = new XmlTextWriter(memoryStream, Encoding.UTF8);
            var serializer = new DataContractSerializer(typeof(T));
            serializer.WriteObject(writer, objectToSerialise);
            writer.Flush();
            memoryStream = (MemoryStream)writer.BaseStream;
            memoryStream.Flush();
            return (new UTF8Encoding()).GetString(memoryStream.ToArray());
        }        
    }
}