
namespace CustomUIFactory.TypeConvert
{
    using UnityEngine;
    using System;
    using System.IO;
    using System.Drawing;
    using UnityEngine.U2D;

    public class Value_Sprite : IValueParser
    {
        public object Parsing(string value)
        {
            using (FileStream fs = new FileStream(value, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                byte[] bytes = new byte[fs.Length];
                fs.Read(bytes, 0, (int)fs.Length);
                FileInfo fi = new FileInfo(value);
                
                int width = 1024;
                int height = 1024;
                
                Texture2D texture2D = new Texture2D(width, height);
                texture2D.LoadImage(bytes);
                
                Sprite sprite = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), Vector2.zero);
                sprite.name = fi.Name;
                fi = null;

                return sprite;
            }
        }
    }
}