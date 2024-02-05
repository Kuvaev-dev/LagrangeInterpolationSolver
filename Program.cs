using LagrangeInterpolationSolver.Logic;
using LagrangeInterpolationSolver.Validation;
using LagrangeInterpolationSolver.View;
using System.Text;

class Program
{
    // Значення x за варіантом:
    // - вручну:
    // x: 0,9689 1,0587 1,1740 1,3796 1,7152 1,7279 1,7791
    // y: 1,0 1,0 1,0 1,0 1,0 1,0 1,0
    // E: 1,3
    // - з файлу:
    // x: 0.9689 1.0587 1.1740 1.3796 1.7152 1.7279 1.7791
    // y: 1.0 1.0 1.0 1.0 1.0 1.0 1.0
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
                    }

                    TextViewer.ChangeColor("\nНатисніть \"Enter\" для виходу до меню...", "blue");
                    Console.ReadLine();
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
            writer.WriteLine($"Результат інтерполяції: {result}");
        }
        TextViewer.ChangeColor($"\nРезультати збережено до текстового файлу.\nШлях: {GetProjectDirectory()}", "blue");
    }

    static string GetProjectDirectory()
    {
        string assemblyLocation = System.Reflection.Assembly.GetExecutingAssembly().Location;
        string projectDirectory = Path.GetDirectoryName(Path.GetDirectoryName(assemblyLocation));
        return projectDirectory;
    }
}