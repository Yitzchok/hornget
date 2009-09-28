using System;
using System.IO;
using Horn.Core.Tree.MetaDataSynchroniser;
using Horn.Core.Utils;

namespace Horn.Services.Core.Builder
{
    public class PackageTreeBuilder : IPackageTreeBuilder
    {
        private readonly IMetaDataSynchroniser metaDataSynchroniser;
        private readonly IFileSystemProvider fileSystemProvider;
        private readonly DirectoryInfo rootDirectory;

        public virtual void Initialise()
        {
            
        }

        public virtual void Build()
        {

        }

        public PackageTreeBuilder(IMetaDataSynchroniser metaDataSynchroniser, IFileSystemProvider fileSystemProvider, DirectoryInfo root)
        {
            this.metaDataSynchroniser = metaDataSynchroniser;
            this.fileSystemProvider = fileSystemProvider;
            rootDirectory = root;
        }
    }
}