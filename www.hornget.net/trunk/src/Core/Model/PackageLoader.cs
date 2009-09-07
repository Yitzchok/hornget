namespace Core.Model
{
    public class PackageLoader : XmlToObjectLoaderBase<Package>
    {
        public PackageLoader(string dataDirectory) : base(dataDirectory) { }
    }
}