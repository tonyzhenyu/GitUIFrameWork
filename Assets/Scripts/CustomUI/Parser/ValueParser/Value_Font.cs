
namespace CustomUIFactory.TypeConvert
{
    using System.IO;
    using UnityEngine;
    using UnityEngine.TextCore.LowLevel;
    public class Value_Font : IValueParser
    {
        public object Parsing(string value)
        {
            //UnityEditor.TrueTypeFontImporter.GetAtPath
            Font a = new Font();
            
            if (value.Contains(@"/"))
            {
                Debug.Log("Import Font Assets Not Support Yet");
                /*
                FontEngineError fontEngineError = FontEngine.LoadFontFace(value);
                
                using (FileStream fs = new FileStream(value, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    
                    //a = Font
                }*/
            }
            else
            {
                a = Font.CreateDynamicFontFromOSFont(value, 12);
            }
            return a;
        }
    }
}