using CustomUIFactory.Css;
using System;
using System.IO;
using System.Reflection;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;
namespace CustomUIFactory
{
    public sealed class CustomUIGenerator : MonoBehaviour
    {
        public string path;
        public string convertorPath;
        public string cssPath;

        private GameObject root;
        private CssDocument cssdoc;
        private void Awake()
        {
            TypeConvertor.Init(convertorPath);
            GenCssDoc(cssPath);
        }
        private void Start()
        {
            GenerateXMLNodes(path);
            RootObjectInit();
        }
        #region InitRootObject
        private void RootObjectInit()
        {
            GameObject canvas = GameObject.Find("Canvas");
            foreach (var item in FindObjectsOfType<GameObject>())
            {
                if (item.name.Contains("root"))
                {
                    root = item;
                    root.name = "root";
                    break;
                }
            }
            root.transform.SetParent(canvas.transform);

            root.transform.position = new Vector3(Screen.width / 2, Screen.height / 2, 0);
            RectTransform rr = root.GetComponent<RectTransform>();

            rr.anchorMax = Vector2.one;
            rr.anchorMin = Vector2.zero;
            rr.offsetMax = Vector2.zero;
            rr.offsetMin = Vector2.zero;
            
        }
        #endregion
          
        public void GenCssDoc(string path)
        {
            cssdoc = new CssDocument(path);
        }
        #region GenerateXMLNodes
        public void GenerateXMLNodes(string path)
        {
            XmlDocument xmldoc = new XmlDocument();
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                xmldoc.Load(fs);
            }
            foreach (XmlNode root in xmldoc.ChildNodes)
            {
                XmlNodeTravel(root);
            }
        }
        #endregion
        #region TravelNodes
        private void XmlNodeTravel(XmlNode node)
        {
            if (node.HasChildNodes)
            {
                using (CustomUIXmlNodeParser parser = new CustomUIXmlNodeParser())
                {
                    //generate
                    
                    GameObject obj = new GameObject(node.Name + node.GetHashCode());
                    parser.obj = obj;
                    parser.cssdoc = cssdoc;

                    foreach (XmlNode item in node)
                    {
                        XmlNodeTravel(item);
                        item.RemoveAll();
                    }
                    parser.PrefixParsing(node.Prefix);
                    if (node.InnerText.Equals(""))
                    {
                        return;
                    }
                    parser.InnerTextParsing(node.InnerText);
                    for (int i = 0; i < node.Attributes.Count; i++)
                    {
                        parser.AttributeParsing(node.Attributes.Item(i).Name, node.Attributes.Item(i).Value);
                    }
                    parser.NameParsing(node.Name);
                    if (node.ParentNode.Name.Contains(@"#"))
                    {
                        return;
                    }
                    obj.transform.SetParent(GameObject.Find(node.ParentNode.Name + node.ParentNode.GetHashCode()).transform);
                    obj.name = node.Name;
                }
            }
        }
        #endregion
    }
}
