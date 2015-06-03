#region "Copyright"
/*
FOR FURTHER DETAILS ABOUT LICENSING, PLEASE VISIT "LICENSE.txt" INSIDE THE SAGEFRAME FOLDER
*/
#endregion

#region "References"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using SageFrame.Templating.xmlparser;
using System.IO;
using System.Web;
#endregion

namespace SageFrame.Templating
{
    /// <summary>
    /// Helper class of wireframe.
    /// </summary>
    public class WireFrameHelper
    {
        public static List<CustomWrapper> lstWrapper = new List<CustomWrapper>();
        public static int _Mode = 0;
        /// <summary>
        /// Information about template.
        /// </summary>
        /// <param name="lstXml">List of XmlTag class.</param>
        /// <returns>Object of TemplateInfo class.</returns>
        public static TemplateInfo CreateTemplateObject(List<XmlTag> lstXml)
        {
            TemplateInfo objTemp = new TemplateInfo();
            List<XmlTag> lstDetails = lstXml[0].LSTChildNodes;
            foreach (XmlTag tag in lstDetails)
            {
                if (Utils.IsValidTag(tag))
                {
                    switch (tag.TagType)
                    {
                        case XmlTagTypes.NAME:
                            objTemp.TemplateName = tag.InnerHtml;
                            break;
                        case XmlTagTypes.AUTHOR:
                            objTemp.Author = tag.InnerHtml;
                            break;
                        case XmlTagTypes.DESCRIPTION:
                            objTemp.Description = tag.InnerHtml;
                            break;
                        case XmlTagTypes.WEBSITE:
                            objTemp.Website = tag.InnerHtml;
                            break;
                    }
                }
            }
            return objTemp;
        }
        /// <summary>
        /// Information about empty template.
        /// </summary>
        /// <returns>Object of TemplateInfo class.</returns>
        public static TemplateInfo CreateEmptyTemplateObject()
        {
            TemplateInfo tempObj = new TemplateInfo();
            tempObj.TemplateName = "N/A";
            tempObj.Author = "N/A";
            tempObj.Description = "N/A";
            tempObj.Website = "N/A";
            return tempObj;
        }
        /// <summary>
        /// Obtain left tag,
        /// </summary>
        /// <returns>Object of XmlTag class.</returns>
        public static XmlTag GetLeftTag()
        {
            return (TagBuilder(XmlTagTypes.Placeholder.ToString().ToLower(), XmlTagTypes.Placeholder, XmlAttributeTypes.NAME, XmlAttributeTypes.NAME.ToString().ToLower(), "leftA", ""));
        }
        /// <summary>
        /// Build XML tag.
        /// </summary>
        /// <param name="tagName">Tag name.</param>
        /// <param name="type">XML tag types.<see cref="T:SageFrame.Templating.xmlparser.XmlTagTypes"/></param>
        /// <param name="attType">XML attribute type.<see cref="T:SageFrame.Templating.xmlparser.XmlAttributeTypes"/></param>
        /// <param name="attName">Attribute name.</param>
        /// <param name="attValue">Attribute value.</param>
        /// <param name="innerHTML">Inner HTML.</param>
        /// <returns>Object of XmlTag class.</returns>
        public static XmlTag TagBuilder(string tagName, XmlTagTypes type, XmlAttributeTypes attType, string attName, string attValue, string innerHTML)
        {
            XmlTag tag = new XmlTag();
            tag.TagType = type;
            tag.TagName = tagName;
            tag.LSTAttributes = AddAttributes(attName, attValue, attType);
            tag.InnerHtml = innerHTML;
            return tag;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="attName">Attribute name.</param>
        /// <param name="attValue">Attribute value.</param>
        /// <param name="attType">XML attribute type.<see cref="T:SageFrame.Templating.xmlparser.XmlAttributeTypes"/></param>
        /// <returns>List of LayoutAttribute class.</returns>
        public static List<LayoutAttribute> AddAttributes(string attName, string attValue, XmlAttributeTypes attType)
        {
            List<LayoutAttribute> lstAttributes = new List<LayoutAttribute>();
            lstAttributes.Add(new LayoutAttribute(attName, attValue, attType));
            return lstAttributes;
        }

       

       

       

        
       

       



       

    
      


       
       

       

      
       
       
       

      

        
       

      
        
      

       


    }
}
