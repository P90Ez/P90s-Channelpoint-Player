using P90Ez.Twitch;
using P90Ez.Twitch.EventSub;

namespace P90Ez.ChannelpointPlayer
{
    internal class Program
    {
        public static readonly string RedemtionsFile = "Redemtions.json";
        public static readonly string ConfigFile = "Config.json";

        public static Logger Logger = new Logger();

        static void Main(string[] args)
        {
            Console.Title = "P90s Channelpoint Player";
            Directory.CreateDirectory("logs");

            Logger.WriteToConsole(true);
            Logger.WriteToFile($"logs\\{DateTime.Now.ToShortDateString().Replace(".", "")}_{DateTime.Now.ToLongTimeString().Replace(":", "")}.txt", true);

            #region Redemtions
            Logger.Log("Reading redemtions...", ILogger.Severety.Message);
            Redemtions? redemtions = Redemtions.TryDeserializeFromFile(RedemtionsFile, Logger);

            if(redemtions == null)
            {
                Logger.Log("Failed to read redemtion!", ILogger.Severety.Warning);
                redemtions = new Redemtions();
            }
            #endregion
            #region Config
            Logger.Log("Reading login...", ILogger.Severety.Message);
            Config? Config = Config.TryDeserializeFromFile(ConfigFile);

            if(Config == null)
            {
                Logger.Log("Failed to read login! Press any key to exit...", ILogger.Severety.Critical);
                Console.ReadLine();
                return;
            }
            #endregion
            #region Credentials
            Logger.Log("Requesting auth token...", ILogger.Severety.Message);

            if(!Config.RequestTokens(redemtions.GetScopes(), Logger) || Config.Credentials == null)
            {
                Logger.Log("Request to get an auth token failed! Press any key to exit...", ILogger.Severety.Critical);
                Console.ReadLine();
                return;
            }

            Logger.Log("Successfully obtained auth token.", ILogger.Severety.Message);
            #endregion

            Logger.Log($"Obtaining channel id for channel {Config.Channelname}...", ILogger.Severety.Message);

            var simpleRequests = new Twitch.API.SimplifiedRequests(Config.Credentials, Logger);
            long ChannelID = simpleRequests.GetBroadcasterID(Config.Channelname);

            Logger.Log($"Channel id is {ChannelID}.", ILogger.Severety.Message);

            Logger.Log("Starting listening to Channelpoint redemtions...", ILogger.Severety.Message);

            EventSubInstance EventSub = new EventSubInstance(Config.Credentials, Logger);
            EventSub.Add_ChannelPoints(ChannelID.ToString()).RewardRedeemed += CP_RewardRedeemed;

            Console.ReadLine();
        }

        private static void CP_RewardRedeemed(object? sender, Twitch.EventSub.Events.ChannelPoints.ChannelPoints_RedemptionEvent e)
        {
            Logger.Log($"{e.UserName} redeemed {e.Reward.Title}!", ILogger.Severety.Message);
        }
    }
}
