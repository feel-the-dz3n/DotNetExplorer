using System;
using System.Reflection;

namespace TestAssembly
{
    public class Fetch
    {
        public static Assembly Get() => Assembly.GetExecutingAssembly();
    }
}
