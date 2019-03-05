using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BasicBot.Classes
{
    public class BotSettings
    {
        [JsonProperty]
        public string BotToken { get; internal set; }
        [JsonProperty]
        public string BotPrefix { get; internal set; }
    }
}
