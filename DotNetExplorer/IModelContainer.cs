using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetExplorer
{
    public interface IModelContainer<T>
    {
        T Model { get; set; }
    }
}
