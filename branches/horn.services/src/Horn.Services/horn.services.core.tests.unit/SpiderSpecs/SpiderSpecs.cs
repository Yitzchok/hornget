using System;
using System.IO;
using Horn.Core.Dsl;
using Horn.Core.SCM;
using Horn.Services.Core.Spiders;
using Horn.Spec.Framework;
using Horn.Spec.Framework.Stubs;
using NUnit.Framework;
using Rhino.Mocks;

namespace Horn.Services.Core.Tests.Unit.SpiderSpecs
{
    public class When_horn_scans_one_package_folder : ContextSpecification
    {
        private PackageTreeSpider _packageTreeSpider;

        private DirectoryInfo _hornDirectory;

        public override void before_each_spec()
        {
            var dependencyResolver = MockRepository.GenerateStub<IDependencyResolver>();

            var configReader = new BooBuildConfigReader();

            dependencyResolver.Stub(x => x.Resolve<IBuildConfigReader>()).Return(configReader);

            dependencyResolver.Stub(x => x.Resolve<SVNSourceControl>()).Return(
                new SourceControlDouble("http://someurl.com/"));

            IoC.InitializeWith(dependencyResolver);
        }

        protected override void establish_context()
        {
            var hornDirectoryPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ".horn");

            _hornDirectory = new DirectoryInfo(hornDirectoryPath);
        }

        protected override void because()
        {
            _packageTreeSpider = new PackageTreeSpider(_hornDirectory);
        }

        [Test]
        public void Then_the_package_meta_is_recorded()
        {
            Assert.That(_packageTreeSpider.MetaData.Count, Is.GreaterThan(0));

            Assert.That(_packageTreeSpider.MetaData[0].MetaData.Count, Is.GreaterThan(0));
        }
    }
}