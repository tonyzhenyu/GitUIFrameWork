
namespace CustomUIFactory.TypeConvert
{
    using UnityEngine;
    public class Value_Vec3 : IValueParser
    {
        public object Parsing(string value)
        {
            string[] vs = value.Split(',');

            return new Vector3(float.Parse(vs[0]), float.Parse(vs[1]), float.Parse(vs[2]));
        }
    }
}