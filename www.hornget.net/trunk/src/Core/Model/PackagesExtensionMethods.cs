using System.IO;

namespace Core.Model
{
    public static class PackagesExtensionMethods
    {
        public static string ToPackageDescriptorFilePath(this string url, string packageDataDirectory)
        {
            var filePath = url.Replace(".html", ".xml");
            filePath = filePath.Replace("/", "\\");
            return filePath.AppendPackageDataDirectory(packageDataDirectory);
        }
        static string AppendPackageDataDirectory(this string filePath, string packageDataDirectory)
        {
            return Path.Combine(packageDataDirectory, filePath);
        }
    }
}