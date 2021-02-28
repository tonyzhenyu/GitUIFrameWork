
namespace CustomUIFactory.TypeConvert
{
    using UnityEngine;
    public class Value_Vec2 : IValueParser
    {
        public object Parsing(string value)
        {
            string[] vs = value.Split(',');

            return new Vector2(float.Parse(vs[0]), float.Parse(vs[1]));
        }
    }
}