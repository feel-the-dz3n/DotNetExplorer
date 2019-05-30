using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace InvokeDotNet.FriendlyAssembly
{
    [Serializable]
    public class RemoteLoader : MarshalByRefObject
    {
        public Assembly NetAssembly { get; private set; }

        public MethodInfo EntryPoint => NetAssembly.EntryPoint;
        public string FullName => NetAssembly.FullName;
        public string Location => NetAssembly.Location;

        public void LoadFile(FileInfo file)
        {
            NetAssembly = Assembly.LoadFile(file.FullName);
        }

        public RemoteLoader() { }
    }
}
