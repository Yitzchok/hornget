using System.IO;
using Horn.Core.PackageStructure;
using Horn.Core.SCM;
using Horn.Framework.helpers;
using Horn.Spec.Framework.helpers;
using Horn.Spec.Framework.Stubs;
using Xunit;
using scm = Horn.Core.SCM;
namespace Horn.Core.Spec.SCM
{
    public class When_the_package_source_revision_does_not_exist : Specification
    {
        private SourceControlDouble sourceControl;

        protected override void Because()
        {
            SourceControl.ClearDownLoadedPackages();

            var packageTree = TreeHelper.GetTempEmptyPackageTree();

            sourceControl = new SourceControlDouble("http://someurl.com/");

            sourceControl.RetrieveSource(packageTree);
        }

        [Fact]
        public void Then_the_a_source_control_export_is_performed()
        {
            Assert.True(sourceControl.CheckOutWasCalled);
        }
    }

    public class When_the_scm_revision_is_greater_than_the_package_revision : Specification
    {
        private SourceControlDouble sourceControl;

        protected override void Because()
        {
            SourceControl.ClearDownLoadedPackages();

            var packageTree = TreeHelper.GetTempPackageTree().RetrievePackage(PackageTreeHelper.PackageWithRevision);

            sourceControl = new SourceControlDouble("http://someurl.com/");

            sourceControl.RetrieveSource(packageTree);
        }

        [Fact]
        public void Then_the_a_source_control_export_is_performed()
        {
            Assert.True(sourceControl.UpdateWasCalled);
        }
    }

    public class When_the_scm_revision_is_less_than_the_package_revision : Specification
    {
        private SourceControlDouble sourceControl;

        protected override void Because()
        {
            var packageTree = TreeHelper.GetTempPackageTree().RetrievePackage(PackageTreeHelper.PackageWithRevision);

            sourceControl = new SourceControlDoubleWitholdRevision("http://someurl.com/");

            sourceControl.RetrieveSource(packageTree);
        }

        [Fact]
        public void Then_the_a_source_control_export_is_not_performed()
        {
            Assert.False(sourceControl.ExportWasCalled);
        }
    }

    public class When_the_source_code_has_downloaded : DirectorySpecificationBase
    {
        private SourceControl sourceControl;
        private IPackageTree packageTree;

        protected override void Before_each_spec()
        {
            sourceControl = new SourceControlDouble("http://somesvnuri.com/Svn");

            packageTree = TreeHelper.GetTempPackageTree().RetrievePackage(PackageTreeHelper.PackageWithoutRevision);
        }

        protected override void Because()
        {
            sourceControl.RetrieveSource(packageTree);
        }


        [Fact]
        public void Then_the_svn_revision_is_recorded()
        {
            var revisionFile = Path.Combine(packageTree.CurrentDirectory.FullName, "revision.horn");

            Assert.True(File.Exists(revisionFile));
        }
    }
}