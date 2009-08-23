using System;
using System.IO;
using Horn.Core.PackageStructure;
using Horn.Core.Spec.Unit.dsl;
using Xunit;

namespace Horn.Core.Spec.Unit.HornTree
{
    public class When_resolving_a_build_file_with_no_other_versions : BaseDSLSpecification
    {
        private IBuildFileResolver fileResolver;
        private DirectoryInfo buildFolder;

        protected override void Before_each_spec()
        {
            fileResolver = new BuildFileResolver();
        }

        protected override void Because()
        {
            buildFolder = GetTestBuildConfigsFolder();
        }

        [Fact]
        public void Then_the_boo_file_is_returned()
        {
            Assert.Equal("boo", fileResolver.Resolve(buildFolder, "horn").Extension);
        }
    }
}