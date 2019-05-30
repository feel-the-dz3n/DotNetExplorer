using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace InvokeDotNet.FriendlyAssembly
{
    [Serializable]
    public class FriendlyAssembly : MarshalByRefObject
    {
        public delegate void BasicHandler(FriendlyAssembly sender);
        public delegate void BasicExceptionHandler(FriendlyAssembly sender, Exception exception);

        public event BasicHandler LoadBegin;
        public event BasicExceptionHandler LoadEnd;

        public event BasicHandler UnloadBegin;
        public event BasicExceptionHandler UnloadEnd;

        public FileInfo AssemblyFile { get; private set; }
        public AppDomain Domain { get; private set; }
        public RemoteLoader Loader { get; private set; }

        public bool IsLoaded => Domain != null;

        public FriendlyAssembly(string fileName)
            => AssemblyFile = new FileInfo(fileName);

        public FriendlyAssembly(FileInfo file)
            => AssemblyFile = file;

        public void LoadAsync()
        {
            new Thread(() =>
            {
                LoadBegin.Invoke(this);

                try
                {
                    Load();

                    LoadEnd?.Invoke(this, null);
                }
                catch (Exception ex)
                {
                    LoadEnd?.Invoke(this, ex);
                }
            }
            ).Start();
        }

        public void Load()
        {
            // Do not load the assembly if it's already loaded
            if (IsLoaded) return;

            // Create domain
            AppDomainSetup mySetupInfo = new AppDomainSetup();
            // mySetupInfo.ApplicationBase = AssemblyFile.Directory.FullName;
            mySetupInfo.ApplicationName = AssemblyFile.Name + "_InvokeDotNet";
            // mySetupInfo.LoaderOptimization = LoaderOptimization.MultiDomainHost;

            Domain = AppDomain.CreateDomain(AssemblyFile.Name + "_InvokeDotNet", null, mySetupInfo);

            // Create instance for remote loader
            Loader = (RemoteLoader)Domain.CreateInstanceFromAndUnwrap(
             typeof(RemoteLoader).Assembly.Location,
             typeof(RemoteLoader).FullName);
            
            // Events
            Domain.AssemblyLoad += Domain_AssemblyLoad;
            Domain.AssemblyResolve += Domain_AssemblyResolve;
            Domain.UnhandledException += Domain_UnhandledException;

            // Load assembly in remote loader
            Loader.LoadFile(AssemblyFile);// Domain.GetAssemblies().First(x => x.Location == AssemblyFile.FullName);

            // Debugger.Log(0, "", "Full name: " + Loader.NetAssembly.FullName + Environment.NewLine);
        }

        private void Domain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
        }

        private void Domain_AssemblyLoad(object sender, AssemblyLoadEventArgs args)
        {
            Debugger.Log(0, "", "Load assembly: " + args.LoadedAssembly.Location + Environment.NewLine);
        }

        private Assembly Domain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            Debugger.Log(0, "", "Resolve assembly: " + args.Name + Environment.NewLine);
            return null;
        }

        public void UnloadAsync()
        {
            new Thread(() =>
            {
                UnloadBegin.Invoke(this);

                try
                {
                    Unload();

                    UnloadEnd?.Invoke(this, null);
                }
                catch (Exception ex)
                {
                    UnloadEnd?.Invoke(this, ex);
                }
            }
            ).Start();
        }

        public void Unload()
        {
            if (IsLoaded)
            {
                AppDomain.Unload(Domain);
                Domain = null;
            }
        }

        public override string ToString()
        {
            if (!IsLoaded) return AssemblyFile.Name + " (unloaded)";
            else return Loader.FullName;
        }
    }
}
