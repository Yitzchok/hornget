using Horn.Core.PackageStructure;
using Horn.Core.Tree.MetaDataSynchroniser;
using Horn.Core.Utils;
using Horn.Services.Core.Builder;
using Horn.Services.Core.Tests.Unit.Helpers;
using NUnit.Framework;
using Rhino.Mocks;

namespace Horn.Services.Core.Tests.Unit.PackageTreeBuilderSpecs
{
    public class When_the_package_tree_builder_is_initialised : BuilderSpecBase
    {
        private ISiteStructureBuilder siteStructureBuilder;
        private IMetaDataSynchroniser metaDataSynchroniser;
        private IFileSystemProvider fileSystemProvider;

        protected override void establish_context()
        {
            hornDirectory = FileSystemHelper.GetFakeDummyHornDirectory();

            metaDataSynchroniser = MockRepository.GenerateStub<IMetaDataSynchroniser>();

            fileSystemProvider = MockRepository.GenerateStub<IFileSystemProvider>();

            fileSystemProvider.Stub(x => x.Exists(hornDirectory.FullName)).Return(false);

            dependencyResolver.Stub(x => x.Resolve<IPackageTree>());

            siteStructureBuilder = new SiteStructureBuilder(metaDataSynchroniser, fileSystemProvider, hornDirectory);
        }

        protected override void because()
        {
            siteStructureBuilder.Initialise();
        }

        [Test]
        public void Then_the_package_tree_is_downloaded()
        {
            metaDataSynchroniser.AssertWasCalled(x => x.SynchronisePackageTree(Arg<IPackageTree>.Is.TypeOf));
        }
    }
}