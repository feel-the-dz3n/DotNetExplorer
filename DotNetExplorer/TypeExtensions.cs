using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetExplorer
{
    public static class TypeExtensions
    {
        public static string GetFriendlyName(this Type type)
        {
            return type.Name;
        }
    }
}
