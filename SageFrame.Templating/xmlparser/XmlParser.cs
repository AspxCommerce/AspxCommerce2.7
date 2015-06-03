#region "Copyright"
/*
FOR FURTHER DETAILS ABOUT LICENSING, PLEASE VISIT "LICENSE.txt" INSIDE THE SAGEFRAME FOLDER
*/
#endregion

#region References
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Text.RegularExpressions;
#endregion

namespace SageFrame.Templating.xmlparser
{
    /// <summary>
    /// Contains class that helps in parsing XML.
    /// </summary>
    public class XmlParser
    {
        /// <summary>
        /// Returns node's attributes from XML file for a provided node.
        /// </summary>
        /// <param name="xmlFile">XML file path.</param>
        /// <param name="startParseNode">Node for which details are to be extracted.</param>
        /// <returns>List of XmlTag class object containing node details.</returns>
        public List<XmlTag> GetXmlTags(string xmlFile, string startParseNode)
        {
            List<XmlTag> lstSectionNodes = new List<XmlTag>();
            XmlDocument doc = XmlHelper.LoadXMLDocument(xmlFile);
            XmlNodeList sectionList = doc.SelectNodes(startParseNode);
            foreach (XmlNode section in sectionList)
            {
                XmlTag tag = new XmlTag();
                tag.TagName = section.Name;
                tag.TagType = GetXmlTagType(section);
                tag.ChildNodeCount = GetChildNodeCount(section);
                tag.AttributeCount = GetAttributeCount(section);
                tag.LSTAttributes = GetAttributesCollection(section);
                tag.LSTChildNodes = section.ChildNodes.Count > 0 ? GetChildNodeCollection(section) : new List<XmlTag>();
                tag.CompleteTag = BuildCompleteTag(tag);
                tag.PchArr = GetPlaceholders(section);
                tag.Placeholders = string.Join(",", tag.PchArr);
                if (ValidateTagType(tag))
                {
                    lstSectionNodes.Add(tag);
                }
            }
            //doc.Save(xmlFile);
            return lstSectionNodes;
        }

        /// <summary>
        /// Returns layout positions.
        /// </summary>
        /// <param name="xmlFile">XML file path.</param>
        /// <param name="startParseNode">Node to be parse. </param>
        /// <returns>List of string  containing the layout position.</returns>
        public List<string> GetLayoutPositions(string xmlFile, string startParseNode)
        {
            XmlDocument doc = XmlHelper.LoadXMLDocument(xmlFile);
            XmlNodeList sectionList = doc.SelectNodes(startParseNode);
            List<string> lstPositions = new List<string>();
            foreach (XmlNode section in sectionList)
            {
                foreach (string pos in GetPositions(section))
                {
                    lstPositions.Add(pos);
                }
            }
            doc.Save(xmlFile);
            return lstPositions;
        }

        /// <summary>
        /// Returns  XmlTagTypes object containing xml tag type.
        /// </summary>
        /// <param name="xmlNode">XmlNode object  from where the xml tag type is to be retrive.</param>
        /// <returns>XmlTagTypes object containing xml type tag value.</returns>
        public XmlTagTypes GetXmlTagType(XmlNode xmlNode)
        {
            XmlTagTypes tagType = new XmlTagTypes();
            switch (xmlNode.Name)
            {
                case "layout":
                    tagType = XmlTagTypes.Layout;
                    break;
                case "section":
                    tagType = XmlTagTypes.Section;
                    break;
                case "placeholder":
                    tagType = XmlTagTypes.Placeholder;
                    break;
                case "template":
                    tagType = XmlTagTypes.TEMPLATE;
                    break;
                case "name":
                    tagType = XmlTagTypes.NAME;
                    break;
                case "author":
                    tagType = XmlTagTypes.AUTHOR;
                    break;
                case "description":
                    tagType = XmlTagTypes.DESCRIPTION;
                    break;
                case "website":
                    tagType = XmlTagTypes.WEBSITE;
                    break;
                case "wrappers":
                    tagType = XmlTagTypes.WRAPPERS;
                    break;
                case "wrap":
                    tagType = XmlTagTypes.WRAP;
                    break;
                case "sfleft":
                    tagType = XmlTagTypes.SFLEFT;
                    break;
                case "sfmiddle":
                    tagType = XmlTagTypes.SFMIDDLE;
                    break;
                case "sfright":
                    tagType = XmlTagTypes.SFRIGHT;
                    break;

            }
            return tagType;
        }

        /// <summary>
        /// Returns the count of child node in any node.
        /// </summary>
        /// <param name="xmlNode">Node of which the child node is to be count.</param>
        /// <returns>Child node counts.</returns>
        public int GetChildNodeCount(XmlNode xmlNode)
        {
            return (xmlNode.ChildNodes.Count);
        }

        /// <summary>
        /// Counts the attributes inside a XML node.
        /// </summary>
        /// <param name="xmlNode">Node of which the attributes is to be count. </param>
        /// <returns>Count of attributes.</returns>
        public int GetAttributeCount(XmlNode xmlNode)
        {
            return (xmlNode.Attributes.Count);
        }

        /// <summary>
        /// Returns list of LayoutAttribute class object containing list of attributes of each node provided.
        /// </summary>
        /// <param name="xn">Node of which the attributes are to be extracted.</param>
        /// <returns>List of attributes.</returns>
        public List<LayoutAttribute> GetAttributesCollection(XmlNode xn)
        {
            List<LayoutAttribute> lstXmlAttr = new List<LayoutAttribute>();
            int attrCount = xn.Attributes.Count;
            while (attrCount > 0)
            {
                attrCount--;
                LayoutAttribute xa = new LayoutAttribute();
                xa.Name = Utils.CleanString(xn.Attributes[attrCount].Name);
                xa.Value = Utils.CleanString(xn.Attributes[attrCount].Value);
                xa.Type = GetXmlAttributeType(xa);
                lstXmlAttr.Add(xa);

            }
            return lstXmlAttr;
        }

        /// <summary>
        /// Returns  placeholder in a string array.
        /// </summary>
        /// <param name="node">Node containg the placeholder name.</param>
        /// <returns>Array of strings of placehoder names.</returns>
        public string[] GetPlaceholders(XmlNode node)
        {
            List<string> lstPch = new List<string>();
            List<XmlTag> lstChildNodes = new List<XmlTag>();
            string childNode = "";
            if (node.ChildNodes.Count > 0)
            {
                childNode = node.ChildNodes[0].Name;
            }
            XmlNodeList xnList = node.SelectNodes(childNode);
            for (int i = 0; i < node.ChildNodes.Count; i++)
            {
                if (node.ChildNodes[i].Attributes["name"] != null)
                    lstPch.Add(node.ChildNodes[i].Attributes["name"].Value);
            }
            return lstPch.ToArray();
        }

        /// <summary>
        /// Retutrns positions of any node.
        /// </summary>
        /// <param name="node">Node containing positions.</param>
        /// <returns>Array of string containing positions.</returns>
        public string[] GetPositions(XmlNode node)
        {
            List<string> lstPch = new List<string>();

            List<XmlTag> lstChildNodes = new List<XmlTag>();
            string childNode = "";
            if (node.ChildNodes.Count > 0)
            {
                childNode = node.ChildNodes[0].Name;
            }
            XmlNodeList xnList = node.SelectNodes(childNode);

            for (int i = 0; i < node.ChildNodes.Count; i++)
            {
                foreach (string pos in node.ChildNodes[i].InnerText.Split(','))
                {
                    lstPch.Add(pos);
                }

            }
            return lstPch.ToArray();
        }

        /// <summary>
        /// Returns child node of any node provided.
        /// </summary>
        /// <param name="node">Node of which the child node are to be  list.</param>
        /// <returns>List of XmlTag class object containing child nodes. </returns>
        public List<XmlTag> GetChildNodeCollection(XmlNode node)
        {
            List<XmlTag> lstChildNodes = new List<XmlTag>();
            string childNode = "";
            if (node.ChildNodes.Count > 0)
            {
                childNode = node.ChildNodes[0].Name;
            }
            XmlNodeList xnList = node.SelectNodes(childNode);

            for (int i = 0; i < node.ChildNodes.Count; i++)
            {
                XmlTag tag = new XmlTag();
                tag.TagName = node.ChildNodes[i].Name;
                tag.TagType = GetXmlTagType(node.ChildNodes[i]);
                tag.InnerHtml = Utils.CleanString(node.ChildNodes[i].InnerText);
                tag.AttributeCount = GetAttributeCount(node.ChildNodes[i]);
                tag.LSTAttributes = GetAttributesCollection(node.ChildNodes[i]);
                tag.PositionsArr = tag.InnerHtml.Split(',');
                tag.CompleteTag = BuildCompleteTag(tag);
                if (ValidateTagType(tag))
                {
                    lstChildNodes.Add(tag);
                }
            }
            return lstChildNodes;
        }

        /// <summary>
        /// Buils complete tags from XML tag detail provided.
        /// </summary>
        /// <param name="tag">XmlTag class object containing XML details.</param>
        /// <returns></returns>
        public string BuildCompleteTag(XmlTag tag)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(GetStartTag(tag));
            sb.Append(GetEndTag(tag));
            return sb.ToString();
        }

        /// <summary>
        /// Returns start tag.
        /// </summary>
        /// <param name="tag">XmlTag object containing node details.</param>
        /// <returns>Start tag.</returns>
        public string GetStartTag(XmlTag tag)
        {
            bool hasAttribute = tag.LSTAttributes.Count > 0 ? true : false;
            StringBuilder sb = new StringBuilder();
            if (hasAttribute)
            {
                sb.Append("<");
                sb.Append(Utils.CleanString(tag.TagName));
                sb.Append(GetAttributeString(tag.LSTAttributes));
                sb.Append(">");
            }
            else
            {
                sb.Append("<");
                sb.Append(Utils.CleanString(tag.TagName));
                sb.Append(">");
            }
            return sb.ToString();
        }

        /// <summary>
        /// Returns xml end tag.
        /// </summary>
        /// <param name="tag">XmlTag tag containing tag name.</param>
        /// <returns>XML end tag.</returns>
        public string GetEndTag(XmlTag tag)
        {
            return ("</" + tag.TagName + ">");
        }


        /// <summary>
        /// Returns attributes  from list of layout.
        /// </summary>
        /// <param name="lstAttr">List of layout attributes.</param>
        /// <returns></returns>
        public string GetAttributeString(List<LayoutAttribute> lstAttr)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" ");
            foreach (LayoutAttribute attr in lstAttr)
            {
                sb.Append(attr.Name);
                sb.Append("=");
                sb.Append("'" + attr.Value + "'");
                sb.Append(" ");
            }
            return sb.ToString();
        }

        /// <summary>
        /// Validates the tag types.
        /// </summary>
        /// <param name="tag">Tag type.</param>
        /// <returns>True if the tag type is valid.</returns>
        public bool ValidateTagType(XmlTag tag)
        {
            bool exists;
            exists = Enum.IsDefined(typeof(XmlTagTypes), tag.TagType);
            return exists;

        }

        /// <summary>
        /// Returns XML attribute type.
        /// </summary>
        /// <param name="xmlAttribute">LayoutAttribute object containing attribute name.</param>
        /// <returns>Attribute types.</returns>
        public XmlAttributeTypes GetXmlAttributeType(LayoutAttribute xmlAttribute)
        {
            XmlAttributeTypes attrTypes = new XmlAttributeTypes();
            switch (xmlAttribute.Name)
            {
                case "name":
                    attrTypes = XmlAttributeTypes.NAME;
                    break;
                case "style":
                    attrTypes = XmlAttributeTypes.STYLE;
                    break;
                case "width":
                    attrTypes = XmlAttributeTypes.WIDTH;
                    break;
                case "type":
                    attrTypes = XmlAttributeTypes.TYPE;
                    break;
                case "wrapinner":
                    attrTypes = XmlAttributeTypes.WRAPINNER;
                    break;
                case "wrapouter":
                    attrTypes = XmlAttributeTypes.WRAPOUTER;
                    break;
                case "custom":
                    attrTypes = XmlAttributeTypes.CUSTOM;
                    break;
                case "mode":
                    attrTypes = XmlAttributeTypes.MODE;
                    break;
                case "class":
                    attrTypes = XmlAttributeTypes.CLASS;
                    break;
                case "depth":
                    attrTypes = XmlAttributeTypes.DEPTH;
                    break;
                case "layout":
                    attrTypes = XmlAttributeTypes.LAYOUT;
                    break;
            }
            return attrTypes;
        }

    }
}
