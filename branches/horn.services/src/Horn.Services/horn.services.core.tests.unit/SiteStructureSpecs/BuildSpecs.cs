using System.Collections.Generic;
using System.IO;
using Horn.Core.Dsl;
using Horn.Core.extensions;
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

namespace Horn.Services.Core.Tests.Unit.CategorySpecs
{
    public class When_the_package_tree_is_scanned : ContextSpecification
    {
        private DirectoryInfo hornDirectory;
        private IMetaDataSynchroniser metaDataSynchroniser;
        private SiteStructureBuilder _siteStructureBuilder;
        private IFileSystemProvider fileSystemProvider;

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
        }

        protected override void establish_context()
        {
            hornDirectory = FileSystemHelper.GetFakeDummyHornDirectory();
        }

        protected override void because()
        {
            _siteStructureBuilder = new SiteStructureBuilder(metaDataSynchroniser, fileSystemProvider,  hornDirectory);

            _siteStructureBuilder.Initialise();

            _siteStructureBuilder.Build();
        }

        [Test]
        public void Then_the_package_xml_is_built()
        {
            Category category = _siteStructureBuilder.Categories[0].Categories[0];

            Assert.That(category.Name, Is.EqualTo("loggers"));

            Assert.That(category.Categories.Count, Is.EqualTo(1));

            var log4net = category.Categories[0];

            Assert.That(log4net.Packages.Count, Is.EqualTo(2));

            Assert.That(log4net.Packages[0].Name, Is.EqualTo("log4net"));

            Assert.That(log4net.Packages[0].Version, Is.EqualTo("1.2.10"));

            var xml = _siteStructureBuilder.Categories.ToDataContractXml<List<Category>>();
        }
    }
}