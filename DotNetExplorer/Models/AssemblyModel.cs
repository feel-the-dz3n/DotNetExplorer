using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DotNetExplorer.Models
{
    public class AssemblyModel
    {
        public Assembly Assembly { get; private set; }
        public string Name { get; private set; }
        public ObservableCollection<TypeModel> Types { get; } 
            = new ObservableCollection<TypeModel>();

        public AssemblyModel() { }
        public AssemblyModel(Assembly asm)
        {
            Assembly = asm;

            if (asm == null)
            {
                Name = "Unknown Assembly";
                Types.Clear();
            }
            else
            {
                Name = asm.FullName;
                Types = new ObservableCollection<TypeModel>(asm.GetTypes().Select(x => new TypeModel(x)).ToList());
            }
        }
    }
}
