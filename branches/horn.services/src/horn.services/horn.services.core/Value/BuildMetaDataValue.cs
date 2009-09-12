using System.Collections.Generic;
using Horn.Core.Dsl;

namespace horn.services.core.Value
{
    public class BuildMetaDataValue
    {
        public Dictionary<string, object> MetaData { get; set; }

        public string Name { get; set; }

        public string Version { get; set; }

        public BuildMetaDataValue(IBuildMetaData buildMetaData, string version)
        {
            Name = buildMetaData.InstallName;

            Version = version;

            MetaData = buildMetaData.ProjectInfo;
        }
    }
}