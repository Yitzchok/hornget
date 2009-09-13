using System.Collections.Generic;
using System.IO;
using Horn.Core.PackageStructure;
using horn.services.core.Value;

namespace Horn.Services.Core.Spiders
{
    public class PackageTreeSpider
    {
        private IPackageTree _packageTree;

        public List<BuildMetaDataValue> MetaData { get; set; }

        public PackageTreeSpider(DirectoryInfo hornDirectory)
        {
            MetaData = new List<BuildMetaDataValue>();

            _packageTree = new PackageTree(hornDirectory, null);
        }
    }
}