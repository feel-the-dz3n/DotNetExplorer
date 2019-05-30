using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace InvokeDotNet.AssemblyLoader
{
    public class Program
    {
        public static Assembly LoadedAssembly;

        static int Main(string[] args)
        {
            if (args.Length == 0) return 0;

            Console.WriteLine("Loading assembly: " + args[0]);
            LoadedAssembly = Assembly.LoadFile(args[0]);
            Console.WriteLine("OK");

            return 0;
        }
    }
}
