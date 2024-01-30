using LagrangeInterpolationSolver.View;

namespace LagrangeInterpolationSolver.Logic
{
    // Клас для інтерполяції поліному Лагранжа
    class LagrangeInterpolation
    {
        public static double Interpolate(double[] xValues, double[] yValues, double epsilon)
        {
            if (xValues.Length != yValues.Length || xValues.Length == 0)
            {
                TextViewer.ChangeColor("ПОМИЛКА: Введені дані є некоректними. Повторіть спробу, будь-ласка.", "red");
                TextViewer.ChangeColor("\nНатисніть \"Enter\" для продовження.", "blue");
                Console.ReadLine();
                return 0.0;
            }

            double result = 0.0;

            for (int i = 0; i < xValues.Length; i++)
            {
                double term = yValues[i];
                TextViewer.ChangeColor($"\nКрок {i + 1}: term {i} = {term}\n", "red");

                for (int j = 0; j < xValues.Length; j++)
                {
                    if (j != i)
                    {
                        term = term * (epsilon - xValues[j]) / (xValues[i] - xValues[j]);
                        TextViewer.ChangeColor($"\tПідкрок {j + 1}: term = term * ({epsilon} - {xValues[j]}) / ({xValues[i]} - {xValues[j]}) = {term}", "blue");
                    }
                }

                result += term;
                TextViewer.ChangeColor($"\t\nРезультат виконання кроку {i + 1}: {result}\n", "magenta");
            }

            return result;
        }
    }
}
