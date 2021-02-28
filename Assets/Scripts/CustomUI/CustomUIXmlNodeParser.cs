using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CustomUIFactory.TypeConvert;
using CustomUIFactory.Css;

namespace CustomUIFactory
{
    public class CustomUIXmlNodeParser : IDisposable
    {
        public GameObject obj;
        public CssDocument cssdoc;

        private string fullname;
        private string mynamespace;
        private Type type;
        private string typenamespace = "CustomUIFactory.TypeConvert";
        public void PrefixParsing(string prefix)
        {

        }
        public void InnerTextParsing(string innertext)
        {
            innertext = innertext.TrimEnd();
            string[] strSplit = innertext.Split('.');
            string typeName = strSplit[strSplit.Length - 1];

            mynamespace = $"{ innertext.Replace("." + typeName, "")}";
            type = Type.GetType($"{innertext},{mynamespace}");

            obj.AddComponent<RectTransform>();
            if (type != typeof(RectTransform))
                obj.AddComponent(type);

            fullname = obj.name;
        }
        public void AttributeParsing(string name, string value)
        {
            string[] strsplit = name.Split('.');
            string localtype = strsplit[0];
            string flag;
            string member;

            flag = strsplit[strsplit.Length - 1];
            if (strsplit.Length > 2)
                member = strsplit[strsplit.Length - 2];
            else
                member = null;

            Component mycomponent = obj.GetComponent(localtype);
            if (mycomponent == null)
                mycomponent = obj.AddComponent(Type.GetType(mynamespace + "." + localtype + "," + mynamespace));

            PropertyInfo propertyInfo = default;

            //----------member--------
            try
            {
                //Debug.Log(mycomponent.GetType().GetProperty(member).MemberType.ToString());

                string convertType = TypeConvertor.Typebook[mycomponent.GetType().GetProperty(member).MemberType.ToString()];
                Type t = Type.GetType($"{typenamespace}.Member_{convertType}");

                IMemberParser memberParser = Activator.CreateInstance(t) as IMemberParser;
                memberParser?.Parsing(member, flag, out propertyInfo, mycomponent);
            }
            catch (Exception)
            {
                //Debug.LogError("UIGenerator: Member Type Missed .");
                propertyInfo = mycomponent.GetType().GetProperty(flag);
            }
            //----------value---------
            try
            {
                //Debug.Log(propertyInfo.PropertyType.ToString());

                string convertType = TypeConvertor.Typebook[propertyInfo.PropertyType.ToString()];
                Type t = Type.GetType($"{typenamespace}.Value_{convertType}");

                IValueParser valueParser = Activator.CreateInstance(t) as IValueParser;
                propertyInfo.SetValue(mycomponent, valueParser?.Parsing(value));

            }
            catch (Exception e)
            {
                Debug.LogError(e.ToString());
                //Debug.LogError("UIGenerator: Value Type Missed .");
            }
            //Debug.Log($"{name}:{value}");
        }
        public void NameParsing(string name)
        {
            //parsing css
            foreach (var item in cssdoc.cssNodes)
            {
                if (name.Equals(item.nodeName))
                {
                    foreach (var btemp in item.cssAttributeNodes)
                    {
                        AttributeParsing(btemp.asName, btemp.value);
                    }
                    break;
                }
            }
        }
        public void Dispose()
        {
            obj = null;
            cssdoc = null;
        }
    }
}


public class Member_A : IMemberParser
{
    public void Parsing(string member,string flag, out PropertyInfo propertyInfo,Component component)
    {
        propertyInfo = component.GetType().GetProperty(member).PropertyType.GetProperty(flag);
        
        /*propertyInfo = mycomponent.GetType().GetProperty(member).PropertyType.GetProperty(flag);

        object v = Convert.ChangeType(value, propertyInfo.PropertyType);
        object a = mycomponent.GetType().GetProperty(member).GetValue(mycomponent);
        a.GetType().GetProperty(flag).SetValue(a, v);*/
    }
}
