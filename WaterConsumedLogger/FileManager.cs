using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;
using Newtonsoft.Json;
using System.Net;
using System;
using System.Collections.Generic;

namespace WaterConsumedLogger
{
    static class FileManager
    {
        private static string _logsFolder = "Logs\\";
        public static string CurrentDirectory => Directory.GetCurrentDirectory();
        public static DirectoryInfo CurrentDirectoryInfo => new DirectoryInfo(CurrentDirectory);
        public static string WaterLogJson => Path.Combine(CurrentDirectoryInfo.FullName, "Waterlog.json");
        public static string ConfigJson => Path.Combine(CurrentDirectoryInfo.FullName, "config.json");
        public static void CreateJsonLog(string fileName,ref bool continueTheProgram, Log log)
        {
            //Creates a file with the name 

            string fileNameWithFolder = _logsFolder + fileName + ".json";
            string path = Path.Combine(CurrentDirectoryInfo.FullName, fileNameWithFolder);
            try
            {
                JsonSerializer serializer = new JsonSerializer();

                using (StreamWriter writer = new StreamWriter(path))
                using (JsonTextWriter jsonWriter = new JsonTextWriter(writer))
                {
                    serializer.Serialize(jsonWriter, log);

                }
            }
            catch(Exception)
            {
                Console.WriteLine("Unable to Create the file!");
                Console.WriteLine("Press any key to leave the program");
                Console.ReadKey();
                continueTheProgram = false;
            }
            
        }
        public static void ChangeConfigFile(ref bool continueTheProgram,Config config)
        {
            try
            {
                JsonSerializer serializer = new JsonSerializer();

                using (StreamWriter writer = new StreamWriter(ConfigJson))
                using (JsonTextWriter jsonWriter = new JsonTextWriter(writer))
                {
                    serializer.Serialize(jsonWriter, config);

                    Console.WriteLine("Configuration file Changed!");
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Unable to Create the file!");
                Console.WriteLine("Press any key to leave the program");
                Console.ReadKey();
                continueTheProgram = false;
            }
        }
        public static Log LoadJson(string fileName, ref bool continueTheProgram)
        {
            //Loads a json 
            string fileNameWithFolder = _logsFolder + fileName + ".json";
            string path = Path.Combine(CurrentDirectoryInfo.FullName, fileNameWithFolder);
            try
            {
                JsonSerializer serializer = new JsonSerializer();
                Log log = new Log("Loading");
                using (StreamReader reader = new StreamReader(path))
                using (JsonTextReader jsonReader = new JsonTextReader(reader))
                {
                    log = serializer.Deserialize<Log>(jsonReader);

                    Console.WriteLine("File Loaded!!");
                }
                return log;
            }
            catch (Exception)
                {
                    Console.WriteLine("You need to Create a File!");
                    return new Log("unnamed");
                }
        }
        public static Config LoadConfig()
        {
            try
            {
                JsonSerializer serializer = new JsonSerializer();
                Config config= new Config("Loading");
                using (StreamReader reader = new StreamReader(ConfigJson))
                using (JsonTextReader jsonReader = new JsonTextReader(reader))
                {
                    config = serializer.Deserialize<Config>(jsonReader);

                }
                return config;
            }
            catch (Exception e)
            {
                
                return new Config(e.Message);
            }
        }
    }
  
}
