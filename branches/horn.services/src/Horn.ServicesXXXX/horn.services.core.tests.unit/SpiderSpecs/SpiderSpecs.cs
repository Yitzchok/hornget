using System;
using System.IO;
using horn.services.core.Crawlers;
using Horn.Spec.Framework;
using NUnit.Framework;

namespace horn.services.core.tests.unit.parser.specs
{
    public class When_horn_scans_one_package_folder : ContextSpecification
    {
        private PackageTreeCrawler _packageTreeCrawler;

        private DirectoryInfo _hornDirectory;

        protected override void establish_context()
        {
            var hornDirectoryPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ".horn");

            _hornDirectory = new DirectoryInfo(hornDirectoryPath);
        }

        protected override void because()
        {
            _packageTreeCrawler = new PackageTreeCrawler(_hornDirectory);
        }

        [Test]
        public void Then_the_package_meta_is_recorded()
        {
            Assert.That(_packageTreeCrawler.MetaData.Count, Is.GreaterThan(0));
        }
    }
}