using Newtonsoft.Json;
using P90Ez.Twitch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P90Ez.ChannelpointPlayer
{
    public abstract class JsonStructure<T>
    {
        /// <summary>
        /// Returns a string in json format representing this object.
        /// </summary>
        /// <returns>This object in a json format.</returns>
        public override string ToString()
        {
            return Serialize();
        }

        /// <summary>
        /// Serializes object to a Json string.
        /// </summary>
        /// <param name="Prettyprint">Formatting</param>
        /// <returns>This object in a json format.</returns>
        public string Serialize(bool Prettyprint = false)
        {
            string result = string.Empty;
            try
            {
                result = JsonConvert.SerializeObject(this, (Prettyprint ? Formatting.Indented : Formatting.None));
            }catch { }

            return result;
        }

        /// <summary>
        /// Serializes and writes this object as a Json string to a file.
        /// </summary>
        /// <param name="Path">Path to write to</param>
        /// <param name="Prettyprint">Formatting</param>
        /// <returns></returns>
        public bool SerializeToFile(string Path, bool Prettyprint = false)
        {
            try
            {
                File.WriteAllText(Path, Serialize(Prettyprint));
            } 
            catch
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Trys to deserialize a given string to an object of T.
        /// </summary>
        /// <param name="JsonString"></param>
        /// <returns>An object of T when successful, otherwise <code>default(T?)</code> or null.</returns>
        public static T? TryDeserialize(string JsonString)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(JsonString);
            }
            catch
            {
                return default(T?);
            }
        }

        /// <summary>
        /// Trys to read and deserialize the content of a file to an object of T.
        /// </summary>
        /// <param name="Path">Path to Json file.</param>
        /// <returns>An object of T when successful, otherwise <code>default(T?)</code> or null.</returns>
        public static T? TryDeserializeFromFile(string Path, ILogger? Logger = null)
        {
            T? result = default(T?);

            if (!File.Exists(Path))
            {
                if (Logger != null)
                    Logger.Log($"File \"{Path}\" not found!", ILogger.Severety.Warning);
                return result;
            }

            try
            {
                string FileContent = File.ReadAllText(Path);
                result = TryDeserialize(FileContent);
            }catch(Exception ex)
            {
                if (Logger != null)
                    Logger.Log(ex.Message, ILogger.Severety.Warning);
            }

            return result;
        }
    }
}
