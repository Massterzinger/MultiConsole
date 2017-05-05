namespace CalcFunctions
{
    public interface ICalcFunction
    {
        string FuncName { get; }
        double Calculation(params double[] args);
    }
}
