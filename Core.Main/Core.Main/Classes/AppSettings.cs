using System.Collections.Generic;

namespace Core.Main.Classes
{
    public class AppSettings
    {
        public static AppSettings Settings { get; set; }

        public AppSettings()
        {
            Settings = this;
        }

        public string Key1 { get; set; }
        public string Key2 { get; set; }
        public List<Plugin> Plugins { get; set; } = new List<Plugin>();
    }
}
