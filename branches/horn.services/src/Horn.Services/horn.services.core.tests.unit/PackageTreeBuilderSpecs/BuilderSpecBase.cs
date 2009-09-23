using System.IO;
using Horn.Services.Core.Tests.Unit.Helpers;
using Horn.Spec.Framework;

namespace Horn.Services.Core.Tests.Unit.PackageTreeBuilderSpecs
{
    public abstract class BuilderSpecBase : ContextSpecification 
    {
        protected DirectoryInfo hornDirectory;

        public override void before_each_spec()
        {
            hornDirectory = FileSystemHelper.GetFakeDummyHornDirectory();
        }
    }
}