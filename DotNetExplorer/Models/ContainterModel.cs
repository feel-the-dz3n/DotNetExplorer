using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace DotNetExplorer.Models
{
    public class ContainterModel
    {
        public string Name { get; set; }
        public ObservableCollection<TypeMemberModel> Models { get; }
            = new ObservableCollection<TypeMemberModel>();
    }
}
