using System;
using System.IO;
using System.Threading;
using Horn.Domain.PackageStructure;
using Horn.Domain.SCM;
using Horn.Framework.helpers;

namespace Horn.Domain.Spec
{
    public class SourceControlDouble : SVNSourceControl
    {
        public bool ExportWasCalled;

        protected override Thread StartMonitoring()
        {
            Console.WriteLine("Source control download monitoring started.");

            return null;
        }

        protected override void StopMonitoring(Thread thread)
        {
            Console.WriteLine("Source control download monitoring stopped.");
        }

        protected override void Initialise(IPackageTree packageTree)
        {
            Console.WriteLine("In initialise");
        }

        public override string Revision
        {
            get
            {
                return long.MaxValue.ToString();
            }
        }

        protected override string Download(DirectoryInfo destination)
        {
            Console.WriteLine("In Download");

            if (!destination.Exists)
                destination.Create();

            FileHelper.CreateFileWithRandomData(Path.Combine(destination.FullName, "horn.boo"));

            ExportWasCalled = true;

            return long.MaxValue.ToString();
        }

        public SourceControlDouble(string url)
            : base(url)
        {
        }
    }

    public class SourceControlDoubleWithFakeFileSystem : SourceControlDouble
    {
        protected override void RecordCurrentRevision(IPackageTree tree, string revision)
        {
            Console.WriteLine(revision);
        }

        public SourceControlDoubleWithFakeFileSystem(string url)
            : base(url)
        {
        }
    }

    public class SourceControlDoubleWitholdRevision : SourceControlDouble
    {
        public override string Revision
        {
            get
            {
                return "0";
            }
        }

        public SourceControlDoubleWitholdRevision(string url)
            : base(url)
        {
        }
    }
}