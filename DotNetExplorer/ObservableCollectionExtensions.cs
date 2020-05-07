using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace DotNetExplorer
{
    public static class ObservableCollectionExtensions
    {
        public static void AddRange<T>(this ObservableCollection<T> collection, IEnumerable<T> range)
        {
            foreach (var i in range) collection.Add(i);
        }
    }
}
