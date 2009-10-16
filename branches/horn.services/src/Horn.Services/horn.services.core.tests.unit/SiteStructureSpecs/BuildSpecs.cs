using System.IO;
using Horn.Services.Core.Tests.Unit.SiteStructureSpecs;
using horn.services.core.Value;
using NUnit.Framework;
using Rhino.Mocks;

namespace Horn.Services.Core.Tests.Unit.BuildSpecs
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
        public void Then_the_result_of_each_bulid_is_zipped()
        {
            fileSystemProvider.ZipFolder(Arg<DirectoryInfo>.Is.TypeOf, Arg<DirectoryInfo>.Is.TypeOf, Arg<string>.Is.TypeOf);
        }
    }
}