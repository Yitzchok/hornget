
using System;
using System.Globalization;
using System.Text;
using CommandLine;

namespace horn_get
{
    public class Options
    {
        [Option("u", "url", HelpText = "The Horn Server URL", Required = true)]
        public string Url = String.Empty;

        [Option("c", "category", HelpText = "Category Name", Required = true)]
        public string Category = String.Empty;

        [Option("pc", "packagecontents", HelpText = "Package Name", Required = false)]
        public string PackageContents = String.Empty;

        [Option("p", "package", HelpText = "Package Description", Required = false)]
        public string Package = String.Empty;

        [Option("d", "download", HelpText = "Download package", Required = false)]
        public string Download = String.Empty;

        [Option("o", "output", HelpText = "Output path", Required = false)]
        public string Output = String.Empty;

        [HelpOption]
        public string GetUsage()
        {
            var help = new StringBuilder();
            help.AppendLine("Options:");
            help.AppendLine("  -u url, --url                       The Horn Server URL");
            help.AppendLine("  -c categoryName --category          Lists category(ies) ('all' lists all categories)");
            help.AppendLine("  -pc packageName, --packagecontents  Shows package contents");
            help.AppendLine("  -p packageName, --package           Shows package information");
            help.AppendLine("  -d packageName, --download          Downloads Package");
            help.AppendLine("  -o outputFolder,  --output          Optional Output destination for download");
            help.AppendLine("  -h, --help                          Shows this help message");
            help.AppendLine();
            return help.ToString();
        }

        public virtual bool ParseArguments(string[] args)
        {
            return Parser.ParseArguments(args, this, Console.Out);
        }


    }
}