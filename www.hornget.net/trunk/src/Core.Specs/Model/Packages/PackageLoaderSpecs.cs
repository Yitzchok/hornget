using System;
using System.IO;
using Core.Model;
using Machine.Specifications;

namespace Core.Specs.Model.Packages
{
    [Subject(typeof(PackageLoader), "Loading package data")]
    public class when_loading_a_package_data_file_that_exists
    {
        static PackageLoader _packageLoader;
        static Package _package;
        static string _packageDescriptorFilePath;

        Establish context = () =>
        {
            _packageDescriptorFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "model/testdata/ValidPackageData.xml");
            _packageLoader = new PackageLoader();
        };

        Because of = () =>
        {
            _package = _packageLoader.Load(_packageDescriptorFilePath);
        };

        It should_load_the_correct_package_file = () =>
        {
            _package.Category.ShouldEqual("ORM");
            _package.Name.ShouldEqual("NHibernate");
            _package.Url.ShouldEqual("https://nhibernate.svn.sourceforge.net/svnroot/nhibernate/trunk/nhibernate/");
            _package.Description.ShouldEqual("NHibernate handles persisting plain .NET objects to and from an underlying relational database.");
            _package.Version.ShouldEqual("1.0.0.0");

            _package.Meta.Forum.ShouldEqual("http://groups.google.co.uk/group/nhusers?hl=en");
            _package.Meta.Homepage.ShouldEqual("http://www.hibernate.org/343.html");
            _package.Meta.HomepageUrl.ShouldEqual("http://www.nhforge.org");
            _package.Meta.Notes.ShouldEqual("Some notes about nhibernate");

            _package.FileName.ShouldEqual("nhibernate-2-1.zip");
            _package.Contents.Count.ShouldEqual(2);
            _package.Contents[0].Name.ShouldEqual("ABC.hbm.xml");
            _package.Contents[1].Name.ShouldEqual("antlr.runtime.dll");
        };
    }
}