using System.Text;
using LagrangeInterpolationSolver.Logic;
using LagrangeInterpolationSolver.Validation;
using LagrangeInterpolationSolver.View;

class Program
{
    // Значення x за варіантом:
    // - вручну:
    // x: 1,0000 1,1000 1,2320 1,4796 1,9383 1,9577 2,0380
    // y: 0,9689 1,0587 1,1740 1,3796 1,7152 1,7279 1,7791
    // E: 1,3
    // - з файлу:
    // x: 1.0000 1.1000 1.2320 1.4796 1.9383 1.9577 2.0380
    // y: 0.9689 1.0587 1.1740 1.3796 1.7152 1.7279 1.7791
    // E: 1.3

    static void Main()
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.OutputEncoding = Encoding.Unicode;

        while (true)
        {
            Console.Clear();

            Console.WriteLine("Оберіть дію:");
            Console.WriteLine("1. Виконати інтерполяцію");
            Console.WriteLine("2. Вийти");
            if (!int.TryParse(Console.ReadLine(), out int menuChoice))
            {
                TextViewer.ChangeColor("\nПОМИЛКА: Некоректне введення. Повторіть спробу, будь-ласка.", "red");
                TextViewer.ChangeColor("\nНатисніть \"Enter\" для продовження.", "blue");
                Console.ReadLine();
                continue;
            }

            switch (menuChoice)
            {
                case 1:
                    PerformInterpolation();
                    break;
                case 2:
                    Environment.Exit(0);
                    break;
                default:
                    TextViewer.ChangeColor("ПОМИЛКА: Опції не існує. Повторіть спробу, будь-ласка.", "red");
                    TextViewer.ChangeColor("\nНатисніть \"Enter\" для продовження.", "blue");
                    Console.ReadLine();
                    break;
            }
        };
    }

    static void PerformInterpolation()
    {
        while (true)
        {
            Console.Clear();

            Console.WriteLine("Оберіть метод вводу даних:");
            Console.WriteLine("1. Вручну");
            Console.WriteLine("2. З файлу");
            Console.WriteLine("3. Назад до меню");
            if (!int.TryParse(Console.ReadLine(), out int inputMethod))
            {
                TextViewer.ChangeColor("\nПОМИЛКА: Опції не існує. Повторіть спробу, будь-ласка.", "red");
                TextViewer.ChangeColor("\nНатисніть \"Enter\" для продовження.", "blue");
                Console.ReadLine();
                continue;
            }

            IDataInput dataInput;
            if (inputMethod == 1)
            {
                Console.Clear();
                dataInput = new ManualDataInput();
            }
            else if (inputMethod == 2)
            {
                Console.Clear();
                Console.WriteLine("Введіть шлях до файлу:");
                string filePath = Console.ReadLine();
                try
                {
                    dataInput = new FileDataInput(filePath);
                    TextViewer.ChangeColor("\nРозв'язання", "blue");
                }
                catch (Exception ex)
                {
                    TextViewer.ChangeColor($"ПОМИЛКА: {ex.Message}", "red");
                    TextViewer.ChangeColor("\nНатисніть \"Enter\" для продовження.", "blue");
                    Console.ReadLine();
                    return;
                }
            }
            else if (inputMethod == 3)
            {
                return;
            }
            else
            {
                TextViewer.ChangeColor("ПОМИЛКА: Опції не існує. Повторіть спробу, будь-ласка.", "red");
                TextViewer.ChangeColor("\nНатисніть \"Enter\" для продовження.", "blue");
                Console.ReadLine();
                continue;
            }

            try
            {
                double[] xValues = dataInput.GetXValues();
                double[] yValues = dataInput.GetYValues();
                double epsilon = dataInput.GetEpsilon();

                if (DataValidator.ValidateData(xValues, yValues, epsilon))
                {
                    double result = LagrangeInterpolation.Interpolate(xValues, yValues, epsilon);
                    TextViewer.ChangeColor($"Результат інтерполяції: {result}", "yellow");

                    Console.WriteLine("\nБажаєте зберегти результати у файл? (Y/N):");
                    string saveChoice = Console.ReadLine();
                    if (saveChoice.ToLower() == "y")
                    {
                        SaveCalculationsToFile(xValues, yValues, epsilon, result);
                        Console.WriteLine("Результати збережено до текстового файлу.");
                        Console.WriteLine($"Шлях: {GetProjectDirectory()}");
                        Console.WriteLine("Натисніть \"Enter\" для виходу до меню...");
                        Console.ReadLine();
                    }
                    else
                    {
                        Console.WriteLine("\nНатисніть \"Enter\" для виходу до меню...");
                        Console.ReadLine();
                    }
                    break;
                }
                else
                {
                    TextViewer.ChangeColor("ПОМИЛКА: Некоректні дані для інтерполяції.", "red");
                    TextViewer.ChangeColor("\nНатисніть \"Enter\" для продовження.", "blue");
                    Console.ReadLine();
                }
            }
            catch (Exception ex)
            {
                TextViewer.ChangeColor($"ПОМИЛКА: {ex.Message}", "red");
            }
        }
    }


    static void SaveCalculationsToFile(double[] xValues, double[] yValues, double epsilon, double result)
    {
        // Збереження початкових даних та результату до текстового файлу
        string filePath = "Calculations.txt";
        using (StreamWriter writer = new(filePath))
        {
            writer.WriteLine($"Масив X: {string.Join(" ", xValues)}");
            writer.WriteLine($"Масив Y: {string.Join(" ", yValues)}");
            writer.WriteLine($"E: {epsilon}");

            writer.WriteLine("\nПроміжкові значення на кожному кроці:");
            for (int i = 0; i < xValues.Length; i++)
            {
                writer.WriteLine($"Крок {i + 1}: {LagrangeInterpolation.Interpolate(xValues[..(i + 1)], yValues[..(i + 1)], epsilon)}");
            }

            writer.WriteLine($"Результат інтерполяції: {result}");
        }
        Console.Clear();
    }

    static string GetProjectDirectory()
    {
        string assemblyLocation = System.Reflection.Assembly.GetExecutingAssembly().Location;
        string projectDirectory = Path.GetDirectoryName(Path.GetDirectoryName(assemblyLocation));
        return projectDirectory;
    }
}
