using CalcFunctions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace PostfixCalc
{
    public delegate double AriphmeticOperFunc(params double[] args);

    public class CalcEntry : ConsoleAddon.IConsoleAddon
    {
        public static Dictionary<string, AriphmeticOperFunc> Funcs { get; set; }

        public string AddonName { get { return "PostfixCalc"; } }

        public static double Evaluate(string expression)
        {
            Stack<double> operands = new Stack<double>();
            foreach (var token in expression.Split(' '))
            {
                if (Funcs.ContainsKey(token))
                {
                    double a = operands.Pop(), b = operands.Pop();
                    operands.Push(Funcs[token](a, b));
                }
                else
                {
                    operands.Push(Convert.ToDouble(token));
                }
            }
            return operands.Pop();
        }

        public CalcEntry()
        {
            try
            {
                string[] pathArr = Directory.GetFiles(Directory.GetCurrentDirectory() + "/Dlls", "*.dll");
                Funcs = new Dictionary<string, AriphmeticOperFunc>();
                foreach (var plug in pathArr)
                {
                    Assembly.LoadFrom(plug);
                }
                Console.WriteLine("Loaded set of operations");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error when loading operations, {0}", ex.Message);
            }

            foreach (Assembly item in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (Type elem in item.GetTypes())
                {
                    if (elem.GetInterface(typeof(ICalcFunction).FullName) != null)
                    {
                        ICalcFunction p = Activator.CreateInstance(elem) as ICalcFunction;
                        Funcs.Add(p.FuncName, p.Calculation);
                    }
                }
            }
        }
        public void Execute()
        {
            Console.WriteLine(Evaluate(Console.ReadLine()));
        }
    }
}
