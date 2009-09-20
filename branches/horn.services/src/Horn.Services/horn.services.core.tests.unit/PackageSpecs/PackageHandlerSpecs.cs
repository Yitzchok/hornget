using System;
using System.IO;
using Horn.Core.Dsl;
using Horn.Core.SCM;
using Horn.Services.Core.EventHandlers;
using Horn.Spec.Framework;
using Horn.Spec.Framework.Stubs;
using NUnit.Framework;
using Rhino.Mocks;

namespace Horn.Services.Core.Tests.Unit.PackageSpecs
{
    public class When_package_tree_is_scanned : ContextSpecification
    {
        private PackageCreatedHandler _packageCreatedHandler;

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
            _packageCreatedHandler = new PackageCreatedHandler(_hornDirectory);
        }

        [Test]
        public void Then_all_the_package_data_is_recorded()
        {
            Assert.That(_packageCreatedHandler.MetaData.Count, Is.EqualTo(4));

            Assert.That(_packageCreatedHandler.MetaData[0].Version, Is.EqualTo("1.2.10"));

            var log4netTrunk = _packageCreatedHandler.MetaData[1];

            Assert.That(log4netTrunk.Name, Is.EqualTo("log4net"));
            Assert.That(log4netTrunk.IsTrunk, Is.True);            
        }
    }
}