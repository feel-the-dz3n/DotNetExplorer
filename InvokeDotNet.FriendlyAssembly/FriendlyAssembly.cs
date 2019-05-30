using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace InvokeDotNet
{
    [Serializable]
    public class FriendlyAssembly
    {
        public delegate void BasicHandler(FriendlyAssembly sender);
        public delegate void BasicExceptionHandler(FriendlyAssembly sender, Exception exception);

        public event BasicHandler LoadBegin;
        public event BasicExceptionHandler LoadEnd;

        public event BasicHandler UnloadBegin;
        public event BasicExceptionHandler UnloadEnd;

        public FileInfo AssemblyFile { get; private set; }
        public Assembly NetAssembly { get; private set; }
        public AppDomain Domain { get; private set; }

        public bool IsAssemblyLoaded => NetAssembly != null;

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
            if (IsAssemblyLoaded) return;

            // Create AssemblyLoader domain to load assembly
            Domain = AppDomain.CreateDomain(AssemblyFile.Name + "_domain");

            // Events
            Domain.AssemblyLoad += Domain_AssemblyLoad;
            Domain.AssemblyResolve += Domain_AssemblyResolve;
            Domain.UnhandledException += Domain_UnhandledException;

            // Parameters for loader
            string loaderPath = Domain.BaseDirectory + "InvokeDotNet.AssemblyLoader.exe";
            string[] loaderArgs = new string[] { AssemblyFile.FullName };

            // Execute loader
            Domain.ExecuteAssembly(loaderPath, loaderArgs);

            // Get assembly instance
            NetAssembly = Domain.GetAssemblies().First(x => x.Location == AssemblyFile.FullName);

            // Assembly not loaded for some reason
            if (NetAssembly == null)
            {
                Unload();
                throw new Exception();
            }
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
            UnloadBegin?.Invoke(this);

            var dmn = Domain;

            Domain = null;
            NetAssembly = null;

            if (Domain != null)
                AppDomain.Unload(Domain);
        }
    }
}
