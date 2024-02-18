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

        public List<WebsocketConnection> WebsocketConnections { get; set; } = new List<WebsocketConnection>();
    }
}
