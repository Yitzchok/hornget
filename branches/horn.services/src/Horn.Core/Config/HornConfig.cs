using System.Configuration;

namespace Horn.Core.Config
{
    public class HornConfig : ConfigurationSection
    {
        private static readonly HornConfig settings;

        [ConfigurationProperty("dropdirectory", IsRequired = false)]
        public string DropDirectory
        {
            get { return (string) this["dropdirectory"]; }
            set { this["dropdirectory"] = value; }
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