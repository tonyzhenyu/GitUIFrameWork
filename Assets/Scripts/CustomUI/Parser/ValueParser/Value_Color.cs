
namespace CustomUIFactory.TypeConvert
{
    using UnityEngine;
    public class Value_Color : IValueParser
    {
        public object Parsing(string value)
        {
            string[] vs = value.Split(',');

            if (vs.Length<=3)
            {
                return new Color(float.Parse(vs[0]) / 255, float.Parse(vs[1]) / 255, float.Parse(vs[2]) / 255);
            }
            else
            {
                return new Color(float.Parse(vs[0]) / 255, float.Parse(vs[1]) / 255, float.Parse(vs[2]) / 255, float.Parse(vs[3]) / 255);
            }
        }
    }
}