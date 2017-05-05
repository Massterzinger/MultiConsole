using CalcFunctions;

namespace SMathOperations
{
    public class MyAddPlugin : ICalcFunction
    {
        public string FuncName { get { return "+"; } }

        public double Calculation(params double[] args)
        {
            return args[0] + args[1];
        }
    }

    public class MySubPlugin : ICalcFunction
    {
        public string FuncName { get { return "-"; } }

        public double Calculation(params double[] args)
        {
            return args[0] - args[1];
        }
    }

    public class MyMulPlugin : ICalcFunction
    {
        public string FuncName { get { return "*"; } }

        public double Calculation(params double[] args)
        {
            return args[0] * args[1];
        }
    }

    public class MyDivPlugin : ICalcFunction
    {
        public string FuncName { get { return "/"; } }

        public double Calculation(params double[] args)
        {
            return args[0] / args[1];
        }
    }
}
