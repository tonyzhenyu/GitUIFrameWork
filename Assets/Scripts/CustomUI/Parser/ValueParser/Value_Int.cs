
namespace CustomUIFactory.TypeConvert
{
    public class Value_Int : IValueParser
    {
        public object Parsing(string value)
        {
            return int.Parse(value);
        }
    }
}