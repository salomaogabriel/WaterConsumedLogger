using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterConsumedLogger
{


 
    public class Water
    {
        //Position in the list
        public int Id { get; set; }
        //When the water was logged
        public DateTime Time { get; set; }

        //Amount of water consumed in ml
        public int WaterAmount { get; set; }

        public Water(int waterAmount,int id)
        {
            Time = DateTime.Now;
            WaterAmount = waterAmount;
            Id = id;
        }
        //Multiply the amount of water in a bottle per the number of bottles consumed
        public static int CalculateTotalWaterAmountInBottles(int waterAmountPerBottle, int numberOfBottles)
        {
            return waterAmountPerBottle * numberOfBottles;
        }
    }
}
