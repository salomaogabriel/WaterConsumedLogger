using System;

namespace WaterConsumedLogger
{
    class Program
    {
        static void Main(string[] args)
        {
            bool continueTheProgram = true;
            // Root myDeserializedClass = JsonConvert.DeserializeObject<RootObject>();
            Console.WriteLine("Water Consumed Logger!");
            Console.WriteLine("To more commands, press -help or -h");
            Commands command = new Commands();
            while(continueTheProgram)
            {
                command.ReadLine(ref continueTheProgram);

            }
        }
    }
}
