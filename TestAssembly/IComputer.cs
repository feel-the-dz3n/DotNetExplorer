using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TestAssembly
{
    public interface IComputer
    {
        void Shutdown();
        List<FileInfo> GetFiles();
        bool LogIn(string user, string password);
    }
}
