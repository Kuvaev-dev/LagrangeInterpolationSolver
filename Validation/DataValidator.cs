using LagrangeInterpolationSolver.View;

namespace LagrangeInterpolationSolver.Validation
{
    // Клас для валідації даних
    class DataValidator
    {
        private const double Tolerance = 1e-8;

        public static bool ValidateData(double[] xValues, double[] yValues, double epsilon)
        {
            // Перевірка на однакову довжину масивів
            if (xValues.Length != yValues.Length)
            {
                TextViewer.ChangeColor("\nПОМИЛКА: Масиви x та y повинні мати однакову довжину.\n", "red");
                TextViewer.ChangeColor("\nНатисніть \"Enter\" для продовження.", "blue");
                Console.ReadLine();
                return false;
            }

            // Перевірка на порожні масиви
            if (xValues.Length == 0)
            {
                TextViewer.ChangeColor("\nПОМИЛКА: Масиви не мають бути пустими.\n", "red");
                TextViewer.ChangeColor("\nНатисніть \"Enter\" для продовження.", "blue");
                Console.ReadLine();
                return false;
            }

            // Перевірка, чи всі значення x унікальні
            if (!AreAllUnique(xValues))
            {
                TextViewer.ChangeColor("\nПОМИЛКА: Усі значення масиву х мають бути унікальними.", "red");
                TextViewer.ChangeColor("\nНатисніть \"Enter\" для продовження.", "blue");
                Console.ReadLine();
                return false;
            }

            // Перевірка, чи значення epsilon знаходиться в межах значень x
            if (epsilon <= xValues.Min() || epsilon >= xValues.Max())
            {
                TextViewer.ChangeColor("ПОМИЛКА: Значення Е має бути включено в межі значень масиву х.", "red");
                TextViewer.ChangeColor("\nНатисніть \"Enter\" для продовження.", "blue");
                Console.ReadLine();
                return false;
            }

            return true;
        }

        private static bool AreAllUnique(double[] values)
        {
            for (int i = 0; i < values.Length - 1; i++)
            {
                for (int j = i + 1; j < values.Length; j++)
                {
                    if (Math.Abs(values[i] - values[j]) < Tolerance)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
