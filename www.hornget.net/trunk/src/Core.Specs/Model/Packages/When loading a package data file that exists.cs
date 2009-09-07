using System;
using System.IO;
using Core.Model;
using Machine.Specifications;

namespace Core.Specs.Model.Packages
{
    [Subject(typeof(PackageLoader), "Loading package data")]
    public class When_loading_a_package_data_file_that_exists
    {
        static PackageLoader _packageLoader;
        static Package _package;
        static string _dataPath;

        Establish context = () =>
        {
            _dataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "model/packages/testdata/");
            _packageLoader = new PackageLoader(_dataPath);
        };

        Because of = () =>
        {
            _package = _packageLoader.Load("ValidPackageData.xml");
        };

        It should_look_in_the_correct_location_for_the_package_data_files = () => _packageLoader.DataDirectory.ShouldEqual(_dataPath);

        It should_load_the_correct_package_file = () =>
        {
            _package.Category.ShouldEqual("ORM");
            _package.Name.ShouldEqual("NHibernate");
            _package.Url.ShouldEqual("orm/nhibernate-2-1");
            _package.Description.ShouldEqual("NHibernate handles persisting plain ....");
            _package.Notes.ShouldEqual("Trunk Build");
            _package.ForumUrl.ShouldEqual("http://groups.google.co.uk/group/nhusers?hl=en");
            _package.HomepageUrl.ShouldEqual("http://www.nhforge.org");
            _package.Version.ShouldEqual("1.0.0.0");
            _package.Created.ShouldEqual(new DateTime(2009, 01, 01, 17, 55, 0));
            _package.FileName.ShouldEqual("nhibernate-2-1.zip");
            _package.Contents.Count.ShouldEqual(2);
            _package.Contents[0].Name.ShouldEqual("ABC.hbm.xml");
            _package.Contents[1].Name.ShouldEqual("antlr.runtime.dll");
        };
    }
}