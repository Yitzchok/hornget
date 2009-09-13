using System;
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

        private void PackageTree_PackageTreeCreated(IPackageTree packagetree)
        {
            var buildMetaData = packagetree.GetBuildMetaData();

            //HACK: REMOVE HARDCODED TRUNK FLAG
            MetaData.Add(new BuildMetaDataValue(buildMetaData, "trunk"));
        }

        public PackageTreeSpider(DirectoryInfo hornDirectory)
        {
            MetaData = new List<BuildMetaDataValue>();

            _packageTree = new PackageTree();

            _packageTree.BuildNodeCreated += PackageTree_PackageTreeCreated;

            _packageTree.BuildTree(null, hornDirectory);
        }
    }
}
