using Newtonsoft.Json;
using P90Ez.Twitch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P90Ez.ChannelpointPlayer
{
    public class Config : JsonStructure<Config>
    {
        public double SoundVolume { get; set; }

        public bool EnableWebsocketServer { get; set; } = false;

        public int WebsocketPort { get; set; } = 6969;

        public string Channelname { get; set; } = string.Empty;
        public string ClientID { get; set; } = string.Empty;

        [JsonIgnore]
        internal Twitch.Login.Credentials? Credentials { get; private set; }

        internal bool RequestTokens(List<string> Scopes, ILogger Logger)
        {
            Credentials = Twitch.Login.ImplicitGrantFlow(ClientID, Scopes, Logger);
            return Credentials.IsSuccess;
        }
    }
}
