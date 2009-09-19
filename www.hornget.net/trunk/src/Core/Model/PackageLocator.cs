namespace Core.Model
{
    public class PackageLocator
    {
        readonly string _dataDirectory;

        public PackageLocator(string dataDirectory)
        {
            _dataDirectory = dataDirectory;
        }

        public PackageLocatorResult FromUrl(string url)
        {
            var filePath = url.ToPackageDescriptorFilePath(_dataDirectory);
            var locatedPackage = false;

            if (System.IO.File.Exists(filePath)) locatedPackage = true;

            return new PackageLocatorResult(filePath, locatedPackage);
        }
    }
}