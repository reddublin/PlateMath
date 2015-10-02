using System;
using System.Collections.Generic;
using System.Linq;

namespace PlateMath
{
    class Program
    {
        private static double[] PlateWeights = new double[] { 2.5, 5, 25, 10, 45 };
        private static int BarbellWeight = 45;

        static void Main(string[] args)
        {
            Console.Write("Enter your weight:  ");
            var UserWeight = Console.ReadLine();

            int TotalWeight;
            var IsInteger = Int32.TryParse(UserWeight, out TotalWeight);

            if (!IsInteger)
            {
                Console.WriteLine("Cannot convert your value to numeric");
                Console.ReadKey();
                return;
            }

            if (IsWeightLessThanBarbell(TotalWeight, BarbellWeight))
            {
                Console.WriteLine("Weight you entered is less than barbell ({0}lb)", BarbellWeight);
                Console.ReadKey();
                return;
            }

            // Get working weight
            var WorkingWeight = TotalWeight - BarbellWeight;

            // Sort plate weight to start with the highest
            PlateWeights = PlateWeights.OrderByDescending(c => c).ToArray();

            // Calculate plate setup
            var plateCount = GetPlateSetup(WorkingWeight);

            if (plateCount.Count == 0)
            {
                Console.WriteLine("Weight cannot be divided evenly");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Your plate setup is (each side):");
            plateCount.ToList().ForEach(x => Console.WriteLine("{0}lb: {1}", x.Key, x.Value));

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }

        /// <summary>
        /// Validates if entered weight is less than barbell weight
        /// </summary>
        /// <param name="weight">Entered weight</param>
        /// <param name="barbell">Barbell wight</param>
        /// <returns></returns>
        private static bool IsWeightLessThanBarbell(int weight, int barbell)
        {
            return weight < barbell ? true : false;
        }

        /// <summary>
        /// Returns the nearest even number of plates
        /// </summary>
        /// <param name="weight">Calculating weight</param>
        /// <param name="plateWeight">Plate weight</param>
        /// <returns></returns>
        private static int NumberOfPlates(int weight, double plateWeight)
        {
            var number = (int)Math.Truncate(weight / plateWeight);

            return (number % 2 == 0) ? number : (number - 1);
        }

        /// <summary>
        /// Calculate Plate Setup (each side)
        /// </summary>
        /// <param name="weight"></param>
        /// <returns></returns>
        private static Dictionary<double, int> GetPlateSetup(int weight)
        {
            var plateCount = new Dictionary<double, int>();

            foreach (var p in PlateWeights)
            {
                if (weight > 0)
                {
                    var number = NumberOfPlates(weight, p);

                    if (number != 0)
                    {
                        plateCount.Add(p, number / 2);
                    }

                    weight = weight - (int)(number * p);
                }
            }

            return weight != 0 ? new Dictionary<double, int>() : plateCount;
        }
    }
}
