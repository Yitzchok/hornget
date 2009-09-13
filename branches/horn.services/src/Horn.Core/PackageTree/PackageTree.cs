using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Horn.Core.BuildEngines;
using Horn.Core.Dsl;
using Horn.Core.extensions;
using Horn.Core.Tree.MetaDataSynchroniser;
using Horn.Core.Utils.CmdLine;

namespace Horn.Core.PackageStructure
{
    public class PackageTree : IPackageTree
    {
        public event BuildNodeCreatedHandler BuildNodeCreated;

        private readonly IMetaDataSynchroniser metaDataSynchroniser;
        private DirectoryInfo result;
        public const string RootPackageTreeName = ".horn";
        private IList<IPackageTree> children;
        private DirectoryInfo patchDirectory;
        private DirectoryInfo workingDirectory;
        private readonly static string[] reservedDirectoryNames = new[]{"working", "build_root_dir"};
        private static readonly string[] libraryNodes = new[] { "lib", "debug", "buildengines", "output"};

        public virtual string BuildFile{ get; set; }

        public virtual IBuildMetaData BuildMetaData { get; private set; }

        public virtual IPackageTree[] Children
        {
            get { return children.ToArray(); }
        }

        public virtual void BuildTree(IPackageTree parent, DirectoryInfo directory)
        {
            Parent = parent;

            children = new List<IPackageTree>();

            Name = directory.Name;

            CurrentDirectory = directory;

            IsBuildNode = DirectoryIsBuildNode(directory);

            if (IsBuildNode)
            {
                CreateRequiredDirectories();
            }

            foreach (var child in directory.GetDirectories())
            {
                if (IsReservedDirectory(child))
                    return;

                var newNode = CreatePackageTreeNode(child);

                children.Add(newNode);
            }

            if (parent == null)
            {
                Result.Delete(true);
            }
        }

        public virtual DirectoryInfo CurrentDirectory { get; private set; }

        public virtual bool Exists
        {
            get
            {
                if (!CurrentDirectory.Exists)
                    return false;

                return RootDirectoryContainsBuildFiles() > 0;
            }
        }

        public virtual string FullName
        {
            get
            {
                if (!IsAversionRequest)
                    return Name;

                return string.Format("{0}-{1}", Name, Version);
            }
        }

        public virtual bool IsAversionRequest
        {
            get { return string.IsNullOrEmpty(Version) == false; }
        }

        public virtual bool IsBuildNode { get; private set; }

        public virtual bool IsRoot
        {
            get { return (Parent == null); }
        }

        public virtual string Name { get; private set; }

        public virtual FileInfo Nant
        {
            get
            {
                //TODO: Find a less explicit way to find the nant.exe
                var path = Path.Combine(Root.CurrentDirectory.FullName, "buildengines");
                path = Path.Combine(path, "Nant");
                path = Path.Combine(path, "Nant");
                path = Path.Combine(path, "NAnt.exe");

                return new FileInfo(path);
            }
        }

        public virtual DirectoryInfo OutputDirectory { get; private set; }

        public virtual DirectoryInfo PatchDirectory
        {
            get
            {
                if (patchDirectory != null)
                    return patchDirectory;

                patchDirectory = new DirectoryInfo(Path.Combine(CurrentDirectory.FullName, "patch"));

                return patchDirectory;
            }
        }

        public virtual bool PatchExists
        {
            get { return PatchDirectory.Exists; }
        }

        public virtual IPackageTree Parent { get; set; }

        public virtual DirectoryInfo Result
        {
            get
            {
                result = new DirectoryInfo(Path.Combine(Root.CurrentDirectory.FullName, "result"));

                if(!result.Exists)
                    result.Create();

                return result;
            }
        }

        public virtual IPackageTree Root
        {
            get
            {
                if (IsRoot)
                    return this;

                IPackageTree parent = Parent;

                while (!parent.IsRoot)
                {
                    parent = parent.Parent;
                }

                return parent;
            }
        }

        public virtual FileInfo Sn
        {
            get
            {
                //TODO: Find a less explicit way to find the sn.exe
                var path = Path.Combine(Root.CurrentDirectory.FullName, "buildengines");
                path = Path.Combine(path, "Sn");
                path = Path.Combine(path, "sn.exe");

                return new FileInfo(path);                
            }
        }

        public virtual string Version { get; set; }

        public virtual DirectoryInfo WorkingDirectory
        {
            get
            {
                if (IsRoot)
                    return CurrentDirectory;

                if(string.IsNullOrEmpty(Version)) 
                    return workingDirectory;

                var versionedDirectoryPath = string.Format("{0}-{1}", workingDirectory.FullName, Version);

                return new DirectoryInfo(versionedDirectoryPath);
            }

            private set { workingDirectory = value; }
        }

        public virtual void Add(IPackageTree item)
        {
            item.Parent = this;

            children.Add(item);
        }

        public virtual List<IPackageTree> BuildNodes()
        {
            var result = Root.GetAllPackages()
                .Where(c => c.IsBuildNode).ToList();

            return result;
        }

        public virtual void CreateRequiredDirectories()
        {
            WorkingDirectory = new DirectoryInfo(Path.Combine(CurrentDirectory.FullName, "Working"));
            WorkingDirectory.Create();

            OutputDirectory = new DirectoryInfo(Path.Combine(CurrentDirectory.FullName, "Output"));
            OutputDirectory.Create();
        }

        public virtual void DeleteWorkingDirectory()
        {
            if (!Root.Name.StartsWith(RootPackageTreeName))
                return;

            if(!WorkingDirectory.Exists)
                return;

            WorkingDirectory.Delete(true);
        }

        public virtual IBuildMetaData GetBuildMetaData(string packageName)
        {
            IPackageTree packageTree = RetrievePackage(packageName);

            return GetBuildMetaData(packageTree);
        }

        public virtual IRevisionData GetRevisionData()
        {
            return new RevisionData(this);
        }

        public virtual IPackageTree GetRootPackageTree(DirectoryInfo rootFolder)
        {
            IPackageTree root = new PackageTree(rootFolder, null);

            metaDataSynchroniser.SynchronisePackageTree(root);

            return new PackageTree(rootFolder, null);            
        }

        public virtual void PatchPackage()
        {
            if (!PatchExists)
                return;

            PatchDirectory.CopyToDirectory(WorkingDirectory, false);
        }

        public virtual void Remove(IPackageTree item)
        {
            children.Remove(item);

            item.Parent = null;
        }

        public virtual IPackageTree RetrievePackage(Dependency dependency)
        {
            return RetrievePackage(dependency.PackageName, dependency.Version);
        }

        public virtual IPackageTree RetrievePackage(ICommandArgs commandArgs)
        {
            var packageName = commandArgs.PackageName;

            var version = commandArgs.Version;

            return RetrievePackage(packageName, version);
        }

        public virtual IPackageTree RetrievePackage(string packageName)
        {
            return RetrievePackage(packageName, null);
        }

        protected virtual void OnBuildNodeCreated(IPackageTree packageTree)
        {
            if (BuildNodeCreated != null)
                BuildNodeCreated(packageTree);
        }

        private IPackageTree RetrievePackage(string packageName, string version)
        {
            var nodes = Root.GetAllPackages()
                .Where(c => c.Name == packageName).ToList();

            if (nodes.Count() == 0)
                return new NullPackageTree();

            var result = nodes.First();

            //HACK: Need a better way of initialising the package tree with the version information
            if (!string.IsNullOrEmpty(version))
                result.Version = version;

            return result;
        }

        private int RootDirectoryContainsBuildFiles()
        {
            return (WorkingDirectory.GetFiles("horn.*", SearchOption.AllDirectories).Length);
        }

        private IBuildMetaData GetBuildMetaData(IPackageTree packageTree)
        {
            if (BuildMetaData != null)
                return BuildMetaData;

            var buildFileResolver = new BuildFileResolver().Resolve(packageTree.CurrentDirectory, packageTree.FullName);

            var reader = IoC.Resolve<IBuildConfigReader>();

            BuildMetaData = reader.SetDslFactory(packageTree).GetBuildMetaData(packageTree, Path.GetFileNameWithoutExtension(buildFileResolver.BuildFile));

            return BuildMetaData;
        }

        private PackageTree CreatePackageTreeNode(DirectoryInfo child)
        {
            var newNode = new PackageTree();

            newNode.BuildNodeCreated += NewNode_BuildNodeCreated;

            newNode.BuildTree(this, child);

            if(DirectoryIsBuildNode(child))
                OnBuildNodeCreated(newNode);

            return new PackageTree(child, this);
        }

        private void NewNode_BuildNodeCreated(IPackageTree packagetree)
        {
            OnBuildNodeCreated(packagetree);
        }

        private bool IsReservedDirectory(DirectoryInfo child)
        {
            return reservedDirectoryNames.Contains(child.Name.ToLower());
        }

        private bool DirectoryIsBuildNode(DirectoryInfo directory)
        {
            if (IsRoot)
                return false;

            return (directory.GetFiles("*.boo").Length > 0) &&
                   (!libraryNodes.Contains(directory.Name.ToLower()));
        }

        public PackageTree()
        {            
        }

        public PackageTree(IMetaDataSynchroniser metaDataSynchroniser)
        {
            this.metaDataSynchroniser = metaDataSynchroniser;
        }

        public PackageTree(DirectoryInfo directory, IPackageTree parent)
        {
            BuildTree(parent, directory);
        }
    }
}