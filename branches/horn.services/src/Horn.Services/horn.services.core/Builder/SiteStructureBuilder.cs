using System.Collections.Generic;
using System.IO;
using Horn.Core.Extensions;
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
        private readonly DirectoryInfo dropDirectory;
        private IPackageTree rootPackageTree;
        private DirectoryInfo sandBox;

        public virtual List<Category> Categories { get; private set; }

        public virtual void Initialise()
        {
            var rootDirectory = fileSystemProvider.GetHornRootDirectory();

            metaDataSynchroniser.SynchronisePackageTree(new PackageTree(rootDirectory, null));

            rootPackageTree = new PackageTree(rootDirectory, null);

            sandBox = fileSystemProvider.CreateTemporaryHornDirectory();
        }

        public virtual void Build()
        {
            var root = new Category(null, rootPackageTree);

            var parentDirectory = CreatePackageDirectory(root, sandBox);

            BuildCategories(rootPackageTree, root, parentDirectory);

            Categories.Add(root);

            var xml = root.ToDataContractXml<Category>();

            var hornFile = Path.Combine(sandBox.FullName, "horn.xml");

            fileSystemProvider.WriteTextFile(hornFile, xml);

            var destinationDirectory = Path.Combine(dropDirectory.FullName, PackageTree.RootPackageTreeName);

            fileSystemProvider.CopyDirectory(sandBox.FullName, destinationDirectory);
        }

        private void BuildCategories(IPackageTree packageTree, Category parent, DirectoryInfo parentDirectory)
        {           
            foreach (var childTree in packageTree.Children)
            {
                var childCategory = new Category(parent, childTree);

                var newDirectory = CreatePackageDirectory(childCategory, parentDirectory);

                BuildCategories(childTree, childCategory, newDirectory);

                parent.Categories.Add(childCategory);

                //TODO: Decide the structure of the xml
                //if(childTree.IsBuildNode)
                //    parent.Packages.AddRange(childCategory.Packages);
                //else
                //    parent.Categories.Add(childCategory);
            }
        }

        private DirectoryInfo CreatePackageDirectory(Category category, DirectoryInfo directory)
        {
            var newDirectory = new DirectoryInfo(Path.Combine(directory.FullName, category.Name));

            fileSystemProvider.CreateDirectory(newDirectory.FullName);

            return newDirectory;
        }

        public SiteStructureBuilder(IMetaDataSynchroniser metaDataSynchroniser, IFileSystemProvider fileSystemProvider, DirectoryInfo dropDirectory)
        {
            this.metaDataSynchroniser = metaDataSynchroniser;
            this.fileSystemProvider = fileSystemProvider;
            this.dropDirectory = dropDirectory;
            Categories = new List<Category>();
        }
    }
}