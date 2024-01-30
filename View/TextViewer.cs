namespace LagrangeInterpolationSolver.View
{
    public class TextViewer
    {
        public static void ChangeColor(string text,string option)
        {
            Console.ResetColor();
            switch (option)
            {
                case "green":
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case "red":
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case "blue":
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    break;
                case "yellow":
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case "magenta":
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    break;
            }
            Console.WriteLine(text);
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Green;
        }
    }
}
