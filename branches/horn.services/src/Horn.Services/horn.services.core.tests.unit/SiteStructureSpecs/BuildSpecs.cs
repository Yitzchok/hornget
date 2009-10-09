using Horn.Services.Core.Tests.Unit.SiteStructureSpecs;
using horn.services.core.Value;
using NUnit.Framework;
using Rhino.Mocks;

namespace Horn.Services.Core.Tests.Unit.CategorySpecs
{
    public class When_the_package_tree_is_scanned : BuilderIniitialiseBase
    {
        [Test]
        public void Then_the_horn_object_graph_is_created()
        {
            Category category = siteStructureBuilder.Categories[0].Categories[0];

            AssertCategoryIntegrity(category);
        }

        [Test]
        public void Then_the_horn_xml_is_saved_to_the_file_system()
        {
            fileSystemProvider.AssertWasCalled(x => x.WriteTextFile(Arg<string>.Is.TypeOf, Arg<string>.Is.TypeOf));
        }

        [Test]
        public void Then_the_new_package_directory_is_copied_to_the_drop_directory()
        {
            fileSystemProvider.AssertWasCalled(x => x.CopyDirectory(Arg<string>.Is.TypeOf, Arg<string>.Is.TypeOf));
        }
    }
}