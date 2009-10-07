using System.Configuration;

namespace Horn.Core.Config
{
    public class HornConfig : ConfigurationSection
    {
        private static readonly HornConfig settings;

        [ConfigurationProperty("basedirectory", IsRequired = false)]
        public string BaseDirectory
        {
            get { return (string) this["basedirectory"]; }
            set { this["basedirectory"] = value; }
        }

        public static HornConfig Settings
        {
            get
            {
                if (settings == null)
                    return null;

                return settings;
            }
        }

        static HornConfig()
        {
            settings = ConfigurationManager.GetSection("horn") as HornConfig;
        }        
    }
}