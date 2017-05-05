using ConsoleAddon;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace MultiConsole
{
    class Program
    {
        private static Type pluginType = typeof(IConsoleAddon);
        private static Dictionary<string, ConsoleFunc> addons = new Dictionary<string, ConsoleFunc>();
        private static bool workBy = true;
        static void Main(string[] args)
        {
            addons.Add("help", () => { Console.WriteLine("You can execute: {0}", String.Join(", ", addons.Keys)); });
            addons.Add("exit", () => { workBy = false; });

            LoadPlugins();
            ActivatePlugins();
            while (workBy)
            {
                Console.Write("\n>>>");
                string action = Console.ReadLine();
                if (addons.ContainsKey(action))
                {
                    addons[action]();
                }
                else
                {
                    Console.WriteLine("No Such function, use help to see possible");
                }
            }

        }

        private static void LoadPlugins()
        {
            while (true)
            {
                Console.WriteLine("Write a path to a .dll plug-in file:");
                string path = Console.ReadLine();
                try
                {
                    Assembly.LoadFrom(path);
                    Console.WriteLine("Loaded");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error, try again, {0}", ex.Message);
                }
                Console.WriteLine("More? y/n");
                string c = Console.ReadLine();
                if (c != "y")
                {
                    return;
                }
            }
        }

        private static void ActivatePlugins()
        {
            foreach (Assembly item in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (Type elem in item.GetTypes())
                {
                    if (elem.GetInterface(pluginType.FullName) != null)
                    {
                        IConsoleAddon p = Activator.CreateInstance(elem) as IConsoleAddon;
                        addons.Add(p.AddonName, p.Execute);
                    }
                }
            }
        }
    }
}
