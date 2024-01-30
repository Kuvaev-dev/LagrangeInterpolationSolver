namespace LagrangeInterpolationSolver.Validation
{
    // Інтерфейс для введення даних
    interface IDataInput
    {
        double[] GetXValues();
        double[] GetYValues();
        double GetEpsilon();
    }
}
