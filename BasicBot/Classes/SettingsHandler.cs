using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace BasicBot.Classes
{
    public static class SettingsHandler
    {
        private static BotSettings BotSettings;
        private static readonly string SettingsFile = Path.Combine(Environment.CurrentDirectory, "settings.json");

        private static BotSettings LoadSettings()
        {
            if (File.Exists(SettingsFile))
            {
                var jsonText = File.ReadAllText(SettingsFile);
                if (!string.IsNullOrWhiteSpace(jsonText))
                {
                    return JsonConvert.DeserializeObject<BotSettings>(jsonText);
                }
            }
            return null;
        }

        public static BotSettings GetSettings()
        {
            if (BotSettings == null)
            {
                BotSettings = LoadSettings();
            }
            return BotSettings;
        }

        public static bool SaveSettings()
        {
            if (BotSettings != null)
            {
                var jsonText = JsonConvert.SerializeObject(BotSettings, Formatting.Indented);
                if (!string.IsNullOrWhiteSpace(jsonText))
                {
                    File.WriteAllText(SettingsFile, jsonText);
                }
            }
            return false;
        }
    }
}
