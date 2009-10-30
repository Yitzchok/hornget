using System.Collections.Generic;

namespace Web.MVC.Model
{
    public static class PackageStructureExtensionMethods
    {
        public static IEnumerable<Package> GetAllPackages(this IEnumerable<Category> categories)
        {
            foreach (var cat in categories)
            {
                foreach (var package in cat.Packages)
                {
                    yield return package;
                }
            }
        }

        //public static IEnumerable<Category> GetAllCategories(this PackageStructure packageStructure)
        //{
        //    return packageStructure.GetAllCategories();
        //}

        public static IEnumerable<Category> GetAllCategories(this Category category)
        {
            foreach (var child in category.Categories)
            {
                yield return child;

                foreach (var childCategory in child.Categories)
                {
                    foreach (var c in childCategory.GetAllCategories())
                    {
                        yield return c;
                    }
                }
            }

            yield return category;
        }
    }
}