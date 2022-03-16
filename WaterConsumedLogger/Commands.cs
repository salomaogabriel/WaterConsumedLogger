using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterConsumedLogger
{

    public class Commands
    {
       public Log Log{ get; set; }
        public Config Config { get; set; }
        public Commands()
        {
            bool continueTheProgram = true;
            LoadConfig();
            LoadLog(Config.LoadedFileName, ref continueTheProgram);
        }
        private void LoadConfig()
        {
            Config = FileManager.LoadConfig();
        }
        public void ReadLine(ref bool continueTheProgram)
        {
            Console.Write("New Command: ");

            string input = Console.ReadLine();
            string[] splittedInput = input.Split(" ");
            int amount;
            switch (splittedInput[0].ToLower())
            {
                case "-help":
                    Help(ref continueTheProgram);
                    break;
                case "-h":
                    Help(ref continueTheProgram);
                    break;
                case "-create":
                    CreateLog(splittedInput[1],ref continueTheProgram);
                    break;
                case "-c":
                    CreateLog(splittedInput[1],ref continueTheProgram);
                    break;
                case "-load":
                    LoadLog(splittedInput[1], ref continueTheProgram);
                    break;
                case "-l":
                    LoadLog(splittedInput[1], ref continueTheProgram);
                    break;
                case "-add":
                    if(int.TryParse(splittedInput[1], out amount))
                    {
                        AddWater(amount, ref continueTheProgram);

                    }
                    break;
                case "-a":
                    if (int.TryParse(splittedInput[1], out amount))
                    {
                        AddWater(amount, ref continueTheProgram);

                    }
                    break;
                case "-bottle":
                    DealWithBottle(splittedInput, ref continueTheProgram);
                    break;
                case "-b":
                    DealWithBottle(splittedInput, ref continueTheProgram);

                    break;
                case "-show":
                    Show(ref continueTheProgram);
                    break;
                case "-s":
                    Show(ref continueTheProgram);

                    break;


                case "-config":
                    if (int.TryParse(splittedInput[1], out amount))
                    {
                        ChangeConfig(amount, ref continueTheProgram);

                    }
                    break;
                case "-exit":
                    Exit(ref continueTheProgram);
                    break;
                case "-e":
                    Exit(ref continueTheProgram);
                    break;
                default:
                    Exit(ref continueTheProgram);
                    break;
            }
        }
        private void Show(ref bool continueTheProgram)
        {
            Log.Show();
        }
        private void DealWithBottle(string [] input, ref bool continueTheProgram)
        {
            int bottles;
            int amountInmL;
            if (int.TryParse(input[1], out bottles))
            {
                if (input.Length == 3)
                {
                    //The input has a customized amount of Water (ml)

                    if (int.TryParse(input[2], out amountInmL))
                    {
                        Log.AddWater(Water.CalculateTotalWaterAmountInBottles(amountInmL, bottles));
                    }
                    else
                    {
                        Console.WriteLine("The amount needs to be an integer number!");
                        continueTheProgram = false;

                    }
                }
                else
                {
                    amountInmL = Config.DefaultAmountWaterInBottle;
                    Log.AddWater(Water.CalculateTotalWaterAmountInBottles(amountInmL, bottles));

                    //The input only has the amount of bottles
                }
            }
            else
            {
                Console.WriteLine("The amount needs to be an integer number!");
                continueTheProgram = false;
            }
           
        }
        private void ChangeConfig(int amount,ref bool continueTheProgram)
        {
            Config.DefaultAmountWaterInBottle = amount;
            FileManager.ChangeConfigFile(ref continueTheProgram, Config);

        }
        private void AddWater(int amountInmL,ref bool continueTheProgram)
        {

            Log.AddWater(amountInmL);

            FileManager.CreateJsonLog(Config.LoadedFileName, ref continueTheProgram, Log);

        }
        private void CreateLog(string fileName,ref bool continueTheProgram)
        {
            Log = new Log(fileName);
            Config = new Config(fileName);
            FileManager.CreateJsonLog(fileName, ref continueTheProgram,Log);
            Console.WriteLine("File Created!");

            FileManager.ChangeConfigFile( ref continueTheProgram,Config);

        }
        private void LoadLog(string fileName,ref bool continueTheProgram)
        {
            Config.LoadedFileName = fileName;
            FileManager.ChangeConfigFile(ref continueTheProgram, Config);
            Log = FileManager.LoadJson(fileName, ref continueTheProgram);
        }
        private static void Exit(ref bool continueTheProgram)
        {
            Console.WriteLine("Good bye!");
            continueTheProgram = false;

        }
        private static void Help(ref bool continueTheProgram)
        {
            Console.WriteLine("Commands:");
            Console.WriteLine("");
            Console.WriteLine("\t-help/-h : Show all the commands");
            Console.WriteLine("\t-create/-c --name : Creates a new Log file with fileName as --name");
            Console.WriteLine("\t-load/-l --name : Loads a log file where the name is --name");
            Console.WriteLine("\t-add/-a --amount : Adds a new log entry with the amount of water consumed in mL");
            Console.WriteLine("\t-bottle/-b --bottles : Calculates the amount of water consumed by multiplying the amount of water per bottle" +
                " with the amount of bottles and add a new log entry with the result");
            Console.WriteLine("\t-bottle/-b --bottles -amount: Calculates the amount of water consumed with a different capacity than" +
                "the one in the config file");
            Console.WriteLine("\t-show/-s : Show the log");
            Console.WriteLine("\t-config --newAmount: Change the default amount of mL per bottle");

            Console.WriteLine("\t-exit,/-e : Leaves the Program.");

            Console.WriteLine("");
            Console.WriteLine("Insert a command or press ENTER to leave the program.");
            Console.WriteLine("");
        }
    }
}
