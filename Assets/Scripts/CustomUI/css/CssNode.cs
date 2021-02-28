using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CustomUIFactory.Css
{
    public class CssNode
    {
        public string nodeName { get; set; }
        public List<string> comments { get; set; }
        public List<CssAttributeNode> cssAttributeNodes { get; set; }
        public CssNode()
        {
            cssAttributeNodes = new List<CssAttributeNode>();
            comments = new List<string>();
        }
    }
    public class CssAttributeNode
    {
        public string asName { get; set; }
        public string value { get; set; }
    }
}
