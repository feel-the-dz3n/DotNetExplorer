using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;

namespace DotNetExplorer.Models
{
    public class TypeModel
    {
        public Type Type { get; private set; }
        public string TypeName { get; private set; }

        public ObservableCollection<ContainterModel> Models { get; }
            = new ObservableCollection<ContainterModel>();

        public TypeModel() { }
        public TypeModel(Type type)
        {
            Type = type;
            TypeName = "Unknown Type";
            Models.Clear();


            if (type != null)
            {
                TypeName = type.GetFriendlyName();

                var fields = new ObservableCollection<TypeMemberModel>(
                    type.GetFields().Select(x => new TypeMemberModel(x)).ToList());
                var cnt = new ContainterModel() { Name = $"Fields ({fields.Count})" };
                cnt.Models.AddRange(fields);
                Models.Add(cnt);

                var props = new ObservableCollection<TypeMemberModel>(
                    type.GetProperties().Select(x => new TypeMemberModel(x)).ToList());
                cnt = new ContainterModel() { Name = $"Properties ({props.Count})" };
                cnt.Models.AddRange(props);
                Models.Add(cnt);

                var methods = new ObservableCollection<TypeMemberModel>(
                    type.GetMethods().Select(x => new TypeMemberModel(x)).ToList());
                cnt = new ContainterModel() { Name = $"Methods ({methods.Count})" };
                cnt.Models.AddRange(methods);
                Models.Add(cnt);
            }
        }
    }
}
