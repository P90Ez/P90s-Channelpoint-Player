using P90Ez.Twitch;
using P90Ez.Twitch.EventSub;
using static P90Ez.Twitch.Login;

namespace P90Ez.ChannelpointPlayer
{
    internal class Program
    {
        public static Logger logger = new Logger();
        static void Main(string[] args)
        {
            Console.Title = "P90s Channelpoint Player";
            Directory.CreateDirectory("logs");

            logger.WriteToConsole(true);
            logger.WriteToFile($"logs\\{DateTime.Now.ToShortDateString().Replace(".", "")}_{DateTime.Now.ToShortTimeString().Replace(":", "")}.txt", true);

            logger.Log("Reading credentials...", ILogger.Severety.Message);
            Credentials? creds = Credentials.TryDeserializeFromFile("Credentials.json");

            if(creds == null)
            {
                logger.Log("Could not read credentials! Press any key to exit...", ILogger.Severety.Critical);
                Console.ReadLine();
                return;
            }

            logger.Log("Requesting auth token...", ILogger.Severety.Message);

            if(!creds.RequestTokens())
            {
                logger.Log("Request to get an auth token failed! Press any key to exit...", ILogger.Severety.Critical);
                Console.ReadLine();
                return;
            }

            logger.Log("Successfully obtained auth token.", ILogger.Severety.Message);
            logger.Log("Starting listening to Channelpoint redemtions...", ILogger.Severety.Message);

            //TODO
            //EventSubInstance EventSub = new EventSubInstance(creds.TwitchTokens, logger);
            //EventSub.Add_ChannelPoints()

            Console.ReadLine();
        }
    }
}
