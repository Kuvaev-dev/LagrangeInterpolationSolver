using System.Globalization;

namespace LagrangeInterpolationSolver.Validation
{
    // Клас для введення даних вручну
    public class ManualDataInput : IDataInput
    {
        public double[] GetXValues()
        {
            Console.WriteLine("Введіть значення масиву х через пробіл:");
            string input = Console.ReadLine();
            return ConvertToDoubleArray(input);
        }

        public double[] GetYValues()
        {
            Console.WriteLine("Введіть значення масиву у через пробіл:");
            string input = Console.ReadLine();
            return ConvertToDoubleArray(input);
        }

        public double GetEpsilon()
        {
            Console.WriteLine("Введіть значення Е:");
            string input = Console.ReadLine();
            return Convert.ToDouble(input);
        }

        private static double[] ConvertToDoubleArray(string input)
        {
            string[] stringValues = input.Split(' ');
            double[] doubleValues = new double[stringValues.Length];

            for (int i = 0; i < stringValues.Length; i++)
            {
                doubleValues[i] = double.Parse(stringValues[i].Replace(',', '.'), CultureInfo.InvariantCulture);
            }

            return doubleValues;
        }
    }
}
