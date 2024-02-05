using LagrangeInterpolationSolver.View;
using System.Globalization;

namespace LagrangeInterpolationSolver.Validation
{
    // Клас для введення даних з файлу
    class FileDataInput : IDataInput
    {
        private readonly string filePath;

        public FileDataInput(string filePath)
        {
            this.filePath = filePath;
        }

        public double[] GetXValues()
        {
            string[] lines = File.ReadAllLines(filePath);
            return ConvertToDoubleArray(lines[0]);
        }

        public double[] GetYValues()
        {
            string[] lines = File.ReadAllLines(filePath);
            return ConvertToDoubleArray(lines[1]);
        }

        public double GetEpsilon()
        {
            string[] lines = File.ReadAllLines(filePath);
            return Convert.ToDouble(lines[2], CultureInfo.InvariantCulture);
        }

        private static double[] ConvertToDoubleArray(string input)
        {
            string[] stringValues = input.Split(' ');
            double[] doubleValues = new double[stringValues.Length];

            for (int i = 0; i < stringValues.Length; i++)
            {
                if (!double.TryParse(stringValues[i].Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out double result))
                {
                    TextViewer.ChangeColor($"ПОМИЛКА: Некоректний формат значення: {stringValues[i]}", "red");
                }

                doubleValues[i] = result;
            }

            return doubleValues;
        }
    }
}
