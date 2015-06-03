#region "Copyright"
/*
FOR FURTHER DETAILS ABOUT LICENSING, PLEASE VISIT "LICENSE.txt" INSIDE THE SAGEFRAME FOLDER
*/
#endregion

#region "References"
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;
#endregion


namespace SageFrame.Web
{
    /// <summary>
    /// Summary description for XmlHelper
    /// </summary>
    /// 
    public class XmlHelper
    {
        #region Methods
        /// <summary>
        /// XML Encode
        /// </summary>
        /// <param name="s">String</param>
        /// <returns>Encoded string</returns>
        public static string XmlEncode(string s)
        {
            if (s == null)
                return null;
            s = Regex.Replace(s, @"[^\u0009\u000A\u000D\u0020-\uD7FF\uE000-\uFFFD]", "", RegexOptions.Compiled);
            return XmlEncodeAsIs(s);
        }

        /// <summary>
        /// XML Encode as is
        /// </summary>
        /// <param name="s">String</param>
        /// <returns>Encoded string</returns>
        public static string XmlEncodeAsIs(string s)
        {
            if (s == null)
                return null;
            using (StringWriter sw = new StringWriter())
            using (XmlTextWriter xwr = new XmlTextWriter(sw))
            {
                xwr.WriteString(s);
                String sTmp = sw.ToString();
                return sTmp;
            }
        }

        /// <summary>
        /// Encodes an attribute
        /// </summary>
        /// <param name="s">Attribute</param>
        /// <returns>Encoded attribute</returns>
        public static string XmlEncodeAttribute(string s)
        {
            if (s == null)
                return null;
            s = Regex.Replace(s, @"[^\u0009\u000A\u000D\u0020-\uD7FF\uE000-\uFFFD]", "", RegexOptions.Compiled);
            return XmlEncodeAttributeAsIs(s);
        }

        /// <summary>
        /// Encodes an attribute as is
        /// </summary>
        /// <param name="s">Attribute</param>
        /// <returns>Encoded attribute</returns>
        public static string XmlEncodeAttributeAsIs(string s)
        {
            return XmlEncodeAsIs(s).Replace("\"", "&quot;");
        }

        /// <summary>
        /// Decodes an attribute
        /// </summary>
        /// <param name="s">Attribute</param>
        /// <returns>Decoded attribute</returns>
        public static string XmlDecode(string s)
        {
            StringBuilder sb = new StringBuilder(s);
            return sb.Replace("&quot;", "\"").Replace("&apos;", "'").Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&").ToString();
        }

        /// <summary>
        /// Serializes a datetime
        /// </summary>
        /// <param name="dateTime">Datetime</param>
        /// <returns>Serialized datetime</returns>
        public static string SerializeDateTime(DateTime dateTime)
        {
            XmlSerializer xmlS = new XmlSerializer(typeof(DateTime));
            StringBuilder sb = new StringBuilder();
            using (StringWriter sw = new StringWriter(sb))
            {
                xmlS.Serialize(sw, dateTime);
                return sb.ToString();
            }
        }

        /// <summary>
        /// Deserializes a datetime
        /// </summary>
        /// <param name="dateTime">Datetime</param>
        /// <returns>Deserialized datetime</returns>
        public static DateTime DeserializeDateTime(string dateTime)
        {
            XmlSerializer xmlS = new XmlSerializer(typeof(DateTime));
            StringBuilder sb = new StringBuilder();
            using (StringReader sr = new StringReader(dateTime))
            {
                object objDate = xmlS.Deserialize(sr);
                return (DateTime)objDate;
            }
        }
        /// <summary>
        /// Delete XML nodes.
        /// </summary>
        /// <param name="target_path">File path.</param>
        /// <param name="nodeName">Node name.</param>
        public static void DeleteNodes(string target_path, string nodeName)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(target_path);
            XmlNode node = doc.SelectSingleNode("resourcemaps");
            node.RemoveAll();
            doc.Save(target_path);
        }
        #endregion
    }
}
