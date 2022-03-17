using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterConsumedLogger
{
    public class Log
    {
        private double _waterMl = 1000.0;
        public string Name { get; set; }
        public List<Water> Waters { get; set; } = new List<Water>();
        public int Length { get; set; } = 0;
        public double WaterConsumed { get; set; } = 0;

        public int WaterConsumedLast24 { get; set; } = 0;
        public int WaterConsumedLastWeek { get; set; } = 0;
        public int WaterConsumedLastMonth { get; set; } = 0;
        public Log(string name)
        {
            Name = name;
        }
        public Log()
        {
        }
        public void AddWater(int amountOfWater)
        {
            Console.WriteLine(amountOfWater/ _waterMl + " liters of water were added!");
            Water water = new Water(amountOfWater,Length);
            Waters.Insert(0, water);
            Length++;
            WaterConsumed += amountOfWater / _waterMl;
        }   

        private void CheckEntries()
        {
            bool isOnDailyCheck = true;
            bool isOnWeeklyCheck = true;
            bool isOnMonthlyCheck = true;
            DateTime today = DateTime.Now;
            DateTime yesterday = today.AddDays(-1);
            DateTime weekAgo = today.AddDays(-7);
            DateTime monthAgo = today.AddMonths(-1);
            foreach (Water water in Waters)
            {






                if(isOnDailyCheck && DateTime.Compare(yesterday,water.Time) <= 0)
                {
                    WaterConsumedLast24+= water.WaterAmount;
                    WaterConsumedLastWeek += water.WaterAmount;
                    WaterConsumedLastMonth += water.WaterAmount;
                }
                else if(isOnWeeklyCheck && DateTime.Compare(weekAgo, water.Time) <= 0)
                {
                    isOnDailyCheck = false;
                    WaterConsumedLastWeek += water.WaterAmount;
                    WaterConsumedLastMonth += water.WaterAmount;
                }
                else if(isOnMonthlyCheck && DateTime.Compare(monthAgo, water.Time) <= 0)
                {
                    isOnWeeklyCheck = false;
                    WaterConsumedLastMonth += water.WaterAmount;

                }
                else
                {
                    break;
                }

            }
        }
        public void Show()
        {
            CheckEntries();
            Console.WriteLine("Your Water consume Report:");
            Console.WriteLine("Name: " + Name);
            Console.WriteLine("Water Log entries: " + Length);
            Console.WriteLine("Water Consumed in total (Liters): " + WaterConsumed);
            Console.WriteLine("Water Consumed in the last 24 hours (Liters): " + WaterConsumedLast24 / _waterMl);
            Console.WriteLine("Water Consumed in the last Week (Liters): " + WaterConsumedLastWeek / _waterMl);
            Console.WriteLine("Water Consumed in the last Month (Liters): " + WaterConsumedLastMonth / _waterMl);

            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }
    }
}
