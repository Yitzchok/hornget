using System.Collections.Generic;
using System.IO;
using Horn.Core.PackageStructure;
using horn.services.core.Value;

namespace horn.services.core.Crawlers
{
    public class PackageTreeCrawler
    {
        private IPackageTree _packageTree;

        public List<BuildMetaDataValue> MetaData { get; set; }

        public PackageTreeCrawler(DirectoryInfo hornDirectory)
        {
            MetaData = new List<BuildMetaDataValue>();
        }
    }
}