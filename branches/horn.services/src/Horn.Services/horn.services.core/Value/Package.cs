using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Horn.Core.Dsl;

namespace horn.services.core.Value
{
    [DataContract(Name = "Package", Namespace = "http://hornget.com/services")]
    public class Package
    {
        [DataMember(Order = 1)]
        public string Name { get; set; }

        [DataMember(Order = 2)]
        public List<MetaData> MetaData { get; set; }

        [DataMember(Order = 3)]
        public string Version { get; set; }

        public bool IsTrunk
        {
            get { return Version == "trunk"; }
        }

        public Package(IBuildMetaData buildMetaData)
        {
            Name = buildMetaData.InstallName;

            Version = buildMetaData.Version;

            MetaData = new List<MetaData>();

            foreach (var projectInfo in buildMetaData.ProjectInfo)
            {
                MetaData.Add(new MetaData(projectInfo.Key, projectInfo.Value.ToString()));   
            }
        }
    }
}