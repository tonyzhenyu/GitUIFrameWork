
namespace CustomUIFactory.TypeConvert
{
    using System.Reflection;
    using UnityEngine;

    public interface IMemberParser
    {
        void Parsing(string member, string flag, out PropertyInfo propertyInfo, Component component);
    }
}