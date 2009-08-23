using System.IO;
using Horn.Core.PackageStructure;

namespace Horn.Core.Dsl
{
    public interface IBuildConfigReader
    {
        IBuildMetaData GetBuildMetaData(string packageName);
        IBuildConfigReader SetDslFactory(IPackageTree packageTree);
    }
}