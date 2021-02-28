using System.Collections.Generic;
using System.IO;

namespace CustomUIFactory
{
    public static class TypeConvertor
    {
        public static Dictionary<string, string> Typebook = new Dictionary<string, string>();
        public static void Init(string path)
        {
            string[] vs = File.ReadAllLines(path);
            Typebook.Clear();
            foreach (var item in vs)
            {
                string[] vs1 = item.Split(',');
                Typebook.Add(vs1[0], vs1[1]);
            }
        }
    }
}