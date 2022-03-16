using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterConsumedLogger
{
    public class Config
    {
        public string LoadedFileName { get; set; }
        //Amount of water in a bottle in mL
        public int DefaultAmountWaterInBottle { get; set; } = 500;

        public Config()
        {

        }
        public Config(string fileName)
        {
            LoadedFileName = fileName;
        }
        public Config(string fileName,int amountWater)
        {
            LoadedFileName = fileName;
            DefaultAmountWaterInBottle = amountWater;
        }
    }
}
