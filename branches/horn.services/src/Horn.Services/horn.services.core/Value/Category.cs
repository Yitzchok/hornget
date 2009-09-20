using System.Collections.Generic;
using Horn.Core.PackageStructure;

namespace horn.services.core.Value
{
    public class Category
    {
        public string Name { get; private set; }

        public List<Category> Categories { get; set; }

        public List<BuildMetaDataValue> Packages { get; set; }

        public Category(IPackageTree packageTree)
        {
            Categories = new List<Category>();

            Packages = new List<BuildMetaDataValue>();

            Name = packageTree.Name;
        }
    }
}