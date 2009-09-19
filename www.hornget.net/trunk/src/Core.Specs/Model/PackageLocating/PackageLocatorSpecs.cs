using System;
using System.IO;
using Core.Model;
using Machine.Specifications;

namespace Core.Specs.Model.PackageLocating
{
    [Subject(typeof(PackageLocator), "Locating package descriptor")]
    public class when_locating_a_package_that_exists : as_package_descriptor_loading_context
    {
        Establish context = () =>
        {
            Url = "orm/nhibernate/nhibernate.html";
            DataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "model/testdata/packages/");
            PackageLocator = new PackageLocator(DataPath);
        };

        It should_locate_the_package_descriptor = () => LocatorResult.HasLocatedDescriptor().ShouldBeTrue();

        It should_contain_the_correct_file_path_for_loading_the_descriptor_xml = () => LocatorResult.DescriptorPath.ShouldEqual(AppendDataPath("orm\\nhibernate\\nhibernate.xml"));
    }

    [Subject(typeof(PackageLocator), "Locating package descriptor")]
    public class when_locating_a_package_of_a_different_version_that_exists_next_to_another_package_version : as_package_descriptor_loading_context
    {
        Establish context = () =>
        {
            Url = "orm/nhibernate/nhibernate-2-1.html";
            DataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "model/testdata/packages/");
            PackageLocator = new PackageLocator(DataPath);
        };

        It should_locate_the_package_descriptor = () => LocatorResult.HasLocatedDescriptor().ShouldBeTrue();

        It should_contain_the_correct_file_path_for_loading_the_descriptor_xml = () => LocatorResult.DescriptorPath.ShouldEqual(AppendDataPath("orm\\nhibernate\\nhibernate-2-1.xml"));
    }

    [Subject(typeof(PackageLocator), "Locating package descriptor")]
    public class when_locating_a_package_that_does_not_exist : as_package_descriptor_loading_context
    {
        Establish context = () =>
        {
            Url = "orm/foo/bar.html";
            DataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "model/testdata/packages/");
            PackageLocator = new PackageLocator(DataPath);
        };

        It should_locate_the_package_descriptor = () => LocatorResult.HasLocatedDescriptor().ShouldBeFalse();

        It should_contain_the_correct_file_path_for_loading_the_descriptor_xml = () => LocatorResult.DescriptorPath.ShouldEqual(AppendDataPath("orm\\foo\\bar.xml"));
    }

    public abstract class as_package_descriptor_loading_context
    {
        Because of = () =>
        {
            LocatorResult = PackageLocator.FromUrl(Url);
        };

        protected static string Url;
        protected static PackageLocator PackageLocator;
        protected static PackageLocatorResult LocatorResult;
        protected static string DataPath;

        protected static string AppendDataPath(string filePath)
        {
            return Path.Combine(DataPath, filePath);
        }
    }
}