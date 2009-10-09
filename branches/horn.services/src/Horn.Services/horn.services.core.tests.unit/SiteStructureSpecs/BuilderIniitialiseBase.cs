using System.IO;
using Horn.Core.Dsl;
using Horn.Core.SCM;
using Horn.Core.Tree.MetaDataSynchroniser;
using Horn.Core.Utils;
using Horn.Core.Utils.IoC;
using Horn.Services.Core.Builder;
using Horn.Services.Core.Tests.Unit.Helpers;
using horn.services.core.Value;
using Horn.Spec.Framework;
using Horn.Spec.Framework.Stubs;
using NUnit.Framework;
using Rhino.Mocks;

namespace Horn.Services.Core.Tests.Unit.SiteStructureSpecs
{
    public abstract class BuilderIniitialiseBase : ContextSpecification
    {
        protected IMetaDataSynchroniser metaDataSynchroniser;
        protected SiteStructureBuilder siteStructureBuilder;
        protected IFileSystemProvider fileSystemProvider;

        public override void before_each_spec()
        {
            var dependencyResolver = MockRepository.GenerateStub<IDependencyResolver>();

            metaDataSynchroniser = MockRepository.GenerateStub<IMetaDataSynchroniser>();

            fileSystemProvider = MockRepository.GenerateStub<IFileSystemProvider>();

            var configReader = new BooBuildConfigReader();

            dependencyResolver.Stub(x => x.Resolve<IBuildConfigReader>()).Return(configReader);

            dependencyResolver.Stub(x => x.Resolve<SVNSourceControl>()).Return(
                new SourceControlDouble("http://someurl.com/"));

            IoC.InitializeWith(dependencyResolver);

            fileSystemProvider.Stub(x => x.GetHornRootDirectory()).Return(FileSystemHelper.GetFakeDummyHornDirectory());

            fileSystemProvider.Stub(x => x.CreateTemporaryHornDirectory()).Return(new DirectoryInfo(@"C:\documents and settings\paul.cowan\AppData"));

            fileSystemProvider.Stub(x => x.ZipFolder(Arg<DirectoryInfo>.Is.TypeOf, Arg<DirectoryInfo>.Is.TypeOf, Arg<string>.Is.TypeOf)).Return(
                new FileInfo(@"C:\zip"));

            siteStructureBuilder = new SiteStructureBuilder(metaDataSynchroniser, fileSystemProvider, new DirectoryInfo(@"C:\").FullName);

            siteStructureBuilder.Initialise();

            siteStructureBuilder.Build();
        }

        protected void AssertCategoryIntegrity(Category category)
        {
            Assert.That(category.Name, Is.EqualTo("loggers"));

            Assert.That(category.Categories.Count, Is.EqualTo(1));

            var log4net = category.Categories[0];

            Assert.That(log4net.Packages.Count, Is.EqualTo(2));

            Assert.That(log4net.Packages[0].Name, Is.EqualTo("log4net"));

            Assert.That(log4net.Packages[0].Version, Is.EqualTo("1.2.10"));
        }

        protected override void because()
        {
        }

        protected override void establish_context()
        {
        }
    }
}