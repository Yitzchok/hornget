using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace horn_get
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleOutput.Header();

            Options options = new Options();

            if (!options.ParseArguments(args))
            {
                return;
            }

            if (!String.IsNullOrEmpty(options.Category))
            {
                if (String.Compare(options.Category, "all", true) == 0)
                {
                    var categories = HornHttpClient.ListCategories(options.Url);

                    ConsoleOutput.ListCategories(categories);
                }
                else
                {
                    if (!String.IsNullOrEmpty(options.PackageContents))
                    {
                        var packageContents = HornHttpClient.ListPackage(options.Url, options.Category, options.Package);
                        
                        ConsoleOutput.ListPackage(packageContents);

                    } 
                    else if (!String.IsNullOrEmpty(options.Package))
                    {
                        var package = HornHttpClient.PackageDescription(options.Url, options.Category, options.Package);

                        ConsoleOutput.PackageDescription(package);
                    }
                    else if (!String.IsNullOrEmpty(options.Download))
                    {
                        HornHttpClient.PackageDownload(options.Url, options.Category, options.Download, String.IsNullOrEmpty(options.Output) ? 
                            Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location):options.Output);
                    }
                    else
                    {
                        var category = HornHttpClient.ListCategory(options.Url, options.Category);

                        ConsoleOutput.ListCategory(category);
                    }
                }
            }
        }

    }

}

