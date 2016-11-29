using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace JK.Framework.Core.Config
{
    public class JKConfigHandler : IConfigurationSectionHandler
    {
        private static Dictionary<string, string> _dictionary;
        public object Create(object parent, object configContext, XmlNode section)
        {
            Dictionary<string, string> myDictionary = new Dictionary<string, string>();
            if (section != null)
            {
                foreach (XmlNode node in section.ChildNodes)
                {
                    foreach (XmlNode childNode in node)
                    {
                        var name = childNode.Attributes["name"].InnerText;
                        var value = childNode.Attributes["value"].InnerText;
                        var text = childNode.Attributes["text"].InnerText;
                        myDictionary.Add(name, value);
                    }
                }
            }
            _dictionary = myDictionary;
            return _dictionary;
        }

        public static string GetValue(string key)
        {
            return _dictionary[key];
        }
    }
}
