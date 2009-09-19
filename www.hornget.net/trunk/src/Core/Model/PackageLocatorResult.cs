namespace Core.Model
{
    public class PackageLocatorResult
    {
        readonly bool _hasLocatedPackage;

        public PackageLocatorResult(string descriptorFilePath, bool hasLocatedPackage)
        {
            DescriptorPath = descriptorFilePath;
            _hasLocatedPackage = hasLocatedPackage;
        }

        public bool HasLocatedDescriptor()
        {
            return _hasLocatedPackage;
        }
        public string DescriptorPath { get; private set; }
    }
}