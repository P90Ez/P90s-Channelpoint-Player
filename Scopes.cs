using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P90Ez.ChannelpointPlayer
{
    public static class Scopes
    {
        public static readonly string ReadCPRedemtions = Twitch.EventSub.Events.ChannelPoints.RequiredScopes;
        public static readonly string ManageCPRedemtions = Twitch.API.Endpoints.UpdateCustomReward.RequieredScopes;
    }
}
