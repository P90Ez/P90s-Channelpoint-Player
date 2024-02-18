using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using P90Ez.Twitch;

namespace P90Ez.ChannelpointPlayer
{
    public class Credentials : JsonStructure<Credentials>
    {
        public string Channelname { get; set; } = string.Empty;
        public string ClientID { get; set; } = string.Empty;

        [JsonIgnore]
        internal Login.Credentials? TwitchTokens { get; private set; }

        private static readonly List<string> Scopes = new List<string>() { "channel:read:redemptions" };
        internal bool RequestTokens()
        {
            TwitchTokens = Login.ImplicitGrantFlow(ClientID, Scopes);
            return TwitchTokens.IsSuccess;
        }
    }
}
