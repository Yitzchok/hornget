using System;
using System.Collections.Generic;
using Horn.Core.Dsl;

namespace horn.services.core.Value
{
    public class BuildMetaDataValue
    {
        public Dictionary<string, object> MetaData { get; set; }

        public string Name { get; set; }

        public string Version { get; set; }

        public bool IsTrunk
        {
            get { return Version == "trunk"; }
        }

        public BuildMetaDataValue(IBuildMetaData buildMetaData)
        {
            Name = buildMetaData.InstallName;

            Version = buildMetaData.Version;

            MetaData = new Dictionary<string, object>();

            foreach (var projectInfo in buildMetaData.ProjectInfo)
            {
                MetaData.Add(projectInfo.Key, projectInfo.Value);   
            }
        }
    }
}