
namespace CustomUIFactory.TypeConvert
{
    public class Value_Str : IValueParser
    {
        public object Parsing(string value)
        {
            return value;
        }
    }
}