using System.Reflection;

namespace DotNetExplorer.Models
{
    public class TypeMemberModel
    {
        public object Object { get; private set; }
        public string TechnicalName { get; private set; }

        public TypeMemberModel(FieldInfo x)
        {
            TechnicalName = x.GetTechnicalName();
            Object = x;
        }

        public TypeMemberModel(MethodInfo x)
        {
            Object = x;
            TechnicalName = x.GetTechnicalName();
        }

        public TypeMemberModel(PropertyInfo x)
        {
            Object = x;
            TechnicalName = x.GetTechnicalName();
        }
    }
}
