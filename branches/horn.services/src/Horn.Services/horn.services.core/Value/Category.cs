using System.Collections.Generic;
using System.Runtime.Serialization;
using Horn.Core.PackageStructure;

namespace horn.services.core.Value
{
    [DataContract(Name = "Category", Namespace = "http://hornget.com/services")]
    public class Category
    {
        [DataMember(Order = 1)]
        public string Name { get; private set; }

        [DataMember(Order = 2)]
        public List<Category> Categories { get; set; }

        [DataMember(Order = 3)]
        public List<Package> Packages { get; set; }

        public Category(IPackageTree packageTreeNode)
        {
            Categories = new List<Category>();

            Packages = new List<Package>();

            Name = packageTreeNode.Name;

            foreach (var buildMetaData in packageTreeNode.GetAllPackageMetaData())
            {
                Packages.Add(new Package(buildMetaData));
            }
        }
    }
}