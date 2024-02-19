using Newtonsoft.Json;
using P90Ez.Twitch.API.Endpoints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P90Ez.ChannelpointPlayer
{
    public class Redemtions : JsonStructure<Redemtions>
    {
        [JsonProperty(PropertyName = "Redemtions")]
        public List<Redemtion> RedemtionList { get; set; } = new List<Redemtion>();

        /// <summary>
        /// Defines the scopes based on the redemtion settings.
        /// </summary>
        /// <returns>A list of required scopes.</returns>
        public List<string> GetScopes()
        {
            List<string> scopes = new List<string>() { Scopes.ReadCPRedemtions }; //base scope, always required to listen to cp redemtions
            
            foreach(var redemtion in RedemtionList)
            {
                if(!((redemtion.ForceEnable || redemtion.EnableWhileStreaming) && scopes.Contains(Scopes.ManageCPRedemtions)))
                {
                    scopes.Add(Scopes.ManageCPRedemtions);
                }
            }

            return scopes;
        }
    }
}
