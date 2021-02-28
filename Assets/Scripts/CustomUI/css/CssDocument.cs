using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
namespace CustomUIFactory.Css
{
    public class CssDocument
    {
        public List<CssNode> cssNodes;

        public CssDocument()
        {
            cssNodes = new List<CssNode>();
        }
        public CssDocument(string path)
        {
            cssNodes = new List<CssNode>();
            Load(path);
        }
        public void Load(string path)
        {
            string content = File.ReadAllText(path);

            string[] subcontent = content.Split('}');
            string[] tempcontent;
            string[] attricontent;

            foreach (var item in subcontent)
            {
                if (item.Equals(""))
                {
                    continue;
                }
                tempcontent = item.TrimStart().Split('\r');
                CssNode cssNode = new CssNode();
                foreach (var bstr in tempcontent)
                {
                    if (bstr.Contains("{"))
                    {
                        cssNode.nodeName = bstr.TrimEnd('{');
                        continue;
                    }
                    if (bstr.TrimStart().Equals(""))
                    {
                        continue;
                    }
                    if (bstr.TrimStart().Contains("/*"))
                    {
                        cssNode.comments.Add(bstr.TrimStart().TrimStart('/').TrimStart('*').TrimEnd('*').TrimEnd('/'));
                        continue;
                    }
                    attricontent = bstr.TrimStart().Split(':');
                    CssAttributeNode cssAttribute = new CssAttributeNode();
                    cssAttribute.asName = attricontent[0];
                    cssAttribute.value = attricontent[1].TrimStart('"').TrimEnd(';').TrimEnd('"');
                    cssNode.cssAttributeNodes.Add(cssAttribute);
                }
                cssNodes.Add(cssNode);
            }
        }
    }

}
