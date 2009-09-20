using System;
using System.Collections.Generic;
using System.IO;
using Horn.Core.Dsl;
using Horn.Core.extensions;
using Horn.Core.SCM;
using Horn.Services.Core.EventHandlers;
using horn.services.core.Value;
using Horn.Spec.Framework;
using Horn.Spec.Framework.Stubs;
using NUnit.Framework;
using Rhino.Mocks;

namespace Horn.Services.Core.Tests.Unit.CategorySpecs
{
    public class When_the_package_tree_is_scanned : ContextSpecification
    {
        private DirectoryInfo _hornDirectory;

        private CategoryCreatedHandler _categoryCreatedHandler;

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
            _categoryCreatedHandler = new CategoryCreatedHandler(_hornDirectory);
        }

        [Test]
        public void Then_the_category_meta_data_should_be_recorded()
        {
            Category category = _categoryCreatedHandler.Categories[0];

            Assert.That(category.Name, Is.EqualTo("loggers"));

            Assert.That(category.Categories.Count, Is.EqualTo(1));

            var log4net = category.Categories[0];

            Assert.That(log4net.Packages.Count, Is.EqualTo(2));

            Assert.That(log4net.Packages[0].Name, Is.EqualTo("log4net"));

            Assert.That(log4net.Packages[0].Version, Is.EqualTo("1.2.10"));

            var xml = _categoryCreatedHandler.Categories.ToDataContractXml<List<Category>>();
        }
    }
}