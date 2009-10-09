using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using Horn.Core.Extensions;
using Horn.Core.PackageStructure;
using Horn.Core.Tree.MetaDataSynchroniser;
using Horn.Core.Utils;
using horn.services.core.Value;
using log4net;

namespace Horn.Services.Core.Builder
{
    public class SiteStructureBuilder : ISiteStructureBuilder
    {
        private readonly IMetaDataSynchroniser metaDataSynchroniser;
        private readonly IFileSystemProvider fileSystemProvider;
        private readonly DirectoryInfo dropDirectory;
        private IPackageTree rootPackageTree;
        private DirectoryInfo sandBox;

        protected DateTime nextPollTime;
        protected TimeSpan frequency = new TimeSpan(0, 0, 20, 0);
        protected static readonly ILog log = LogManager.GetLogger(typeof(SiteStructureBuilder));

        public virtual List<Category> Categories { get; private set; }

        public virtual bool ServiceStarted { get; set; }

        public virtual bool ShouldContinueAfterException
        {
            get { return true; }
        }

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

            var parentDirectory = CreatePackageDirectory(root, sandBox, rootPackageTree);

            BuildCategories(rootPackageTree, root, parentDirectory);

            Categories.Add(root);

            var xml = root.ToDataContractXml<Category>();

            var hornFile = Path.Combine(sandBox.FullName, "horn.xml");

            fileSystemProvider.WriteTextFile(hornFile, xml);

            var destinationDirectory = Path.Combine(dropDirectory.FullName, PackageTree.RootPackageTreeName);

            fileSystemProvider.CopyDirectory(sandBox.FullName, destinationDirectory);
        }

        public virtual void Run()
        {
            Debugger.Break();

            var hasRanOnce = false;

            while (ServiceStarted)
            {
                if ((DateTime.UtcNow < nextPollTime) && hasRanOnce)
                {
                    SuspendTask();

                    if (!ServiceStarted)
                        break;

                    SetNextPollTime();
                }

                try
                {
                    Initialise();

                    Build();
                }
                catch (Exception ex)
                {
                    log.Error(ex);

                    if (!ShouldContinueAfterException)
                        break;
                }

                hasRanOnce = true;
            }
        }

        protected virtual void SuspendTask()
        {
            Thread.Sleep(frequency);
        }

        private void BuildCategories(IPackageTree packageTree, Category parent, DirectoryInfo parentDirectory)
        {           
            foreach (var childTree in packageTree.Children)
            {
                var childCategory = new Category(parent, childTree);

                var newDirectory = CreatePackageDirectory(childCategory, parentDirectory, childTree);

                BuildCategories(childTree, childCategory, newDirectory);

                parent.Categories.Add(childCategory);

                //TODO: Decide the structure of the xml
                //if(childTree.IsBuildNode)
                //    parent.Packages.AddRange(childCategory.Packages);
                //else
                //    parent.Categories.Add(childCategory);
            }
        }

        private DirectoryInfo CreatePackageDirectory(Category category, DirectoryInfo directory, IPackageTree packageTree)
        {
            var newDirectory = new DirectoryInfo(Path.Combine(directory.FullName, category.Name));

            fileSystemProvider.CreateDirectory(newDirectory.FullName);
          
            if(packageTree.IsBuildNode)
            {
                //TODO: Remove - FOR TESTING
                var tempFileName = Path.Combine(newDirectory.FullName, string.Format("{0}.txt", category.Name));
                fileSystemProvider.WriteTextFile(tempFileName, "some text");

                var zip = fileSystemProvider.ZipFolder(newDirectory, newDirectory, category.Name);

                fileSystemProvider.CopyFile(zip.FullName, zip.FullName, true);                
            }

            return newDirectory;
        }

        private void SetNextPollTime()
        {
            nextPollTime = DateTime.Now.Add(frequency);
        }

        public SiteStructureBuilder(IMetaDataSynchroniser metaDataSynchroniser, IFileSystemProvider fileSystemProvider, string dropDirectoryPath)
        {
            this.metaDataSynchroniser = metaDataSynchroniser;
            this.fileSystemProvider = fileSystemProvider;
            dropDirectory = new DirectoryInfo(dropDirectoryPath);
            Categories = new List<Category>();
        }
    }
}