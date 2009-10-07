using System;
using System.Collections.Generic;
using System.IO;
using Horn.Core.PackageStructure;
using Horn.Core.Tree.MetaDataSynchroniser;
using Horn.Core.Utils;
using horn.services.core.Value;

namespace Horn.Services.Core.Builder
{
    public class SiteStructureBuilder : ISiteStructureBuilder
    {
        private readonly IMetaDataSynchroniser metaDataSynchroniser;
        private readonly IFileSystemProvider fileSystemProvider;
        private readonly DirectoryInfo rootDirectory;
        private IPackageTree packageTree;

        public virtual List<Category> Categories { get; private set; }

        public virtual void Initialise()
        {
            metaDataSynchroniser.SynchronisePackageTree(new PackageTree(rootDirectory, null));

            packageTree = new PackageTree(rootDirectory, null);
        }

        public virtual void Build()
        {
            var root = new Category(null, packageTree);

            BuildCategories(packageTree, root);

            Categories.Add(root);
        }

        private void BuildCategories(IPackageTree packageTree, Category parent)
        {
            foreach (var childTree in packageTree.Children)
            {
                var childCategory = new Category(parent, childTree);

                BuildCategories(childTree, childCategory);

                parent.Categories.Add(childCategory);

                //TODO: Decide the structure of the xml
                //if(childTree.IsBuildNode)
                //    parent.Packages.AddRange(childCategory.Packages);
                //else
                //    parent.Categories.Add(childCategory);
            }
        }

        public SiteStructureBuilder(IMetaDataSynchroniser metaDataSynchroniser, IFileSystemProvider fileSystemProvider, DirectoryInfo rootDirectory)
        {
            this.metaDataSynchroniser = metaDataSynchroniser;
            this.fileSystemProvider = fileSystemProvider;
            this.rootDirectory = rootDirectory;
            Categories = new List<Category>();
        }
    }
}