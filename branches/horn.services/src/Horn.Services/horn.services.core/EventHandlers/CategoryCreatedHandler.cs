using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using Horn.Core.PackageStructure;
using horn.services.core.Value;

namespace Horn.Services.Core.EventHandlers
{
    public class CategoryCreatedHandler
    {
        private readonly IPackageTree _packageTree;

        public List<Category> Categories { get; private set; }

        private List<IPackageTree> orphanedCategories = new List<IPackageTree>();

        private void PackageTree_CategoryCreated(IPackageTree packageTreeNode)
        {
            Console.WriteLine(packageTreeNode.Name);

            if(CategoryHasBeenAdded(packageTreeNode))
                return;

            if(IsTopLevelNode(packageTreeNode))
            {
                Categories.Add(new Category(packageTreeNode));

                return;
            }

            var parent = GetCategory(Categories, packageTreeNode.Name);

            if(parent == null)
            {
                orphanedCategories.Add(packageTreeNode);

                return;
            }

            parent.Categories.Add(new Category(packageTreeNode));
        }

        private bool CategoryHasBeenAdded(IPackageTree packageTree)
        {
            return (Categories.Where(x => x.Name.ToLower() == packageTree.Name.ToLower()).Count() > 0);
        }

        private bool IsTopLevelNode(IPackageTree packageTree)
        {
            return ((packageTree.Parent) != null && (packageTree.Parent.Name == ".horn"));
        }

        private Category GetCategory(List<Category> categories, string name)
        {
            Category ret = null;

            foreach (var category in categories)
            {
                if (category.Name == name)
                {
                    return category;   
                }

                ret = GetCategory(category.Categories, name);

                if(ret != null)
                    return ret;
            }

            return ret;
        }

        public CategoryCreatedHandler(DirectoryInfo hornDirectory)
        {
            Categories = new List<Category>();

            _packageTree = new PackageTree();

            _packageTree.CategoryCreated += PackageTree_CategoryCreated;

            _packageTree.BuildTree(null, hornDirectory);

            while(orphanedCategories.Count > 0)
            {
                for (int i = (orphanedCategories.Count - 1); i >= 0; i--)
                {
                    var orphan = orphanedCategories[i];

                    Console.WriteLine(orphan.Name);
                    Console.WriteLine(orphan.Parent.Name);

                    var parent = GetCategory(Categories, orphan.Parent.Name);

                    if(parent == null)
                        continue;

                    Console.WriteLine(parent.Name);

                    parent.Categories.Add(new Category(orphan));

                    orphanedCategories.Remove(orphan);
                }
            }
        }
    }
}