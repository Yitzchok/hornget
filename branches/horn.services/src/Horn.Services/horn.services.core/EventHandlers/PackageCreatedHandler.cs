using System;
using System.Collections.Generic;
using System.IO;
using Horn.Core.PackageStructure;
using horn.services.core.Value;

namespace Horn.Services.Core.EventHandlers
{
    public class PackageCreatedHandler
    {
        private readonly IPackageTree _packageTree;

        public List<Package> MetaData { get; set; }

        private void PackageTree_PackageTreeCreated(IPackageTree packagetree)
        {
            var buildMetaDataList = packagetree.GetAllPackageMetaData();

            foreach (var metaData in buildMetaDataList)
            {
                MetaData.Add(new Package(metaData));
            }
        }

        public PackageCreatedHandler(DirectoryInfo hornDirectory)
        {
            MetaData = new List<Package>();

            _packageTree = new PackageTree();

            _packageTree.BuildNodeCreated += PackageTree_PackageTreeCreated;

            _packageTree.BuildTree(null, hornDirectory);
        }
    }
}