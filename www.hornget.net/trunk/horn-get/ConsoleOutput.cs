using System;
using System.Collections.Generic;
using Web.MVC.Model;

namespace horn_get
{
    public static class ConsoleOutput
    {
        public static void ListCategories(IList<Category> categories)
        {
            Console.WriteLine("List of categories follows: ");
            Console.WriteLine();

            if (categories != null)
            {
                foreach (var category in categories)
                {
                    Console.WriteLine(String.Format("{0}, Url: {1}", category.Name, category.Url));
                }
            }

        }

   

        public static void ListCategory(Category category)
        {
            Console.WriteLine("List of packages follows: ");
            Console.WriteLine();

            if (category != null)
            {
                foreach (var package in category.Categories)
                {
                    Console.WriteLine(String.Format("{0},  Url: {1}", package.Name, package.Url));
                }
                
            }
        }

        public static void ListPackage(Category package)
        {
            Console.WriteLine("Contents of package follows: ");
            Console.WriteLine();

            if (package != null)
            {
                foreach (var content in package.Packages)
                {
                    Console.WriteLine(String.Format("{0}-{1}, Url: {2}", content.Name, content.Version, content.Url));
                }
            }
        }

        public static void PackageDescription(Package package)
        {
            Console.WriteLine("Package description follows: ");
            Console.WriteLine();

            if (package != null)
            {
                Console.WriteLine(String.Format("{0}: {1}", "Name", package.Name));
                Console.WriteLine(String.Format("{0}: {1}", "Version", package.Version));
                Console.WriteLine(String.Format("{0}: {1}", "Url", package.Url));
                Console.WriteLine("Files:");
                foreach (var content in package.Contents)
                {
                    Console.WriteLine(String.Format("{0}", content.Name));
                }
            }

        }

        public static void Header()
        {
            Console.WriteLine("Copyright (c) 2009 - Horn Project");
            Console.WriteLine(String.Format("{0}", Globals.VERSION));

        }

    }
}