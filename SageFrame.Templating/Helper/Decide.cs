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
using SageFrame.Templating.xmlparser;
using System.Web;
using System.IO;
using System.Xml;
using System.Xml.Linq;
#endregion


namespace SageFrame.Templating
{
    /// <summary>
    /// Various utilities for templating.
    /// </summary>
    public class Decide
    {
        /// <summary>
        /// Check for block.
        /// </summary>
        /// <param name="pch">Place holders. <see cref="T:SageFrame.Templating.Placeholders"/></param>
        /// <param name="middleBlock">XmlTag class containing xml details.</param>
        /// <returns>True if block exist.</returns>
        public static bool HasBlock(Placeholders pch, XmlTag middleBlock)
        {
            bool status = false;
            status = middleBlock.LSTChildNodes.Exists(
                delegate(XmlTag tag)
                {
                    return (Utils.CompareStrings(Utils.GetAttributeValueByName(tag, XmlAttributeTypes.NAME), pch));
                }
                );
            return status;
        }
        /// <summary>
        /// Check for spot light.
        /// </summary>
        /// <param name="placeholder">XmlTag class containing xml details.</param>
        /// <returns>True if spot light..</returns>
        public static bool IsSpotLight(XmlTag placeholder)
        {
            string pchName = Utils.GetAttributeValueByName(placeholder, XmlAttributeTypes.NAME);
            bool status = false;
            if (Utils.CompareStrings(pchName, "spotlight"))
            {
                status = true;
            }
            return status;
        }
        /// <summary>
        /// Check for custom block.
        /// </summary>
        /// <param name="placeholder">XmlTag class containing xml details.</param>
        /// <returns>True for define custom block.</returns>
        public static bool IsCustomBlockDefined(XmlTag placeholder)
        {

            string activeTemplate = HttpContext.Current.Session["SageFrame.ActiveTemplate"] != null ? HttpContext.Current.Session["SageFrame.ActiveTemplate"].ToString() : "Default";
            string pchName = Utils.GetAttributeValueByName(placeholder, XmlAttributeTypes.NAME);
            string FilePath = HttpContext.Current.Server.MapPath("~/") + "//" + activeTemplate + "//sections";
            bool status = false;
            if (Directory.Exists(FilePath))
            {
                DirectoryInfo dir = new DirectoryInfo(FilePath);
                foreach (FileInfo file in dir.GetFiles("*.htm"))
                {
                    if (Utils.CompareStrings(Path.GetFileNameWithoutExtension(file.Name), pchName))
                    {
                        status = true;
                        break;
                    }
                }
            }
            return status;
        }
        /// <summary>
        /// Check foor wrappers in position array.
        /// </summary>
        /// <param name="positionArr">Array of string of wrapper position.</param>
        /// <param name="wrapArray">Array of string of wrapper.</param>
        /// <returns>True for contain wrapper.</returns>
        public static bool Contains(string[] positionArr, string[] wrapArray)
        {
            bool Contains = false;
            foreach (string wrap in wrapArray)
            {
                if (positionArr.Contains(wrap))
                {
                    Contains = true;
                    break;
                }
            }
            return Contains;
        }
        /// <summary>
        /// Check for default template.
        /// </summary>
        /// <param name="TemplateName">Template name.</param>
        /// <returns>True if default templates.</returns>
        public static bool IsTemplateDefault(string TemplateName)
        {
            return TemplateName.ToLower().Equals("default");
        }
        /// <summary>
        /// Check for valid xml.
        /// </summary>
        /// <param name="xml">xml in string format.</param>
        /// <returns>True if valid xml.</returns>
        public static bool IsXmlInputValid(string xml)
        {
            try
            {
                xml = xml.Replace(Environment.NewLine, "");
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xml);
                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }
        /// <summary>
        /// Mode for templating left block.
        /// </summary>
        /// <param name="middleBlock">XmlTag class containing xml details.</param>
        /// <returns>Mode</returns>
        public static int LeftBlockMode(XmlTag middleBlock)
        {
            int status = 0;
            if (Decide.HasBlock(Placeholders.LEFTTOP, middleBlock) && Decide.HasBlock(Placeholders.LEFTBOTTOM, middleBlock) && Decide.HasBlock(Placeholders.LEFTA, middleBlock) && Decide.HasBlock(Placeholders.LEFTB, middleBlock))
            {
                status = 1;
            }
            else if (Decide.HasBlock(Placeholders.LEFTTOP, middleBlock) && Decide.HasBlock(Placeholders.LEFTBOTTOM, middleBlock) && Decide.HasBlock(Placeholders.LEFTA, middleBlock) && !Decide.HasBlock(Placeholders.LEFTB, middleBlock))
            {
                status = 2;
            }
            else if (Decide.HasBlock(Placeholders.LEFTTOP, middleBlock) && Decide.HasBlock(Placeholders.LEFTBOTTOM, middleBlock) && !Decide.HasBlock(Placeholders.LEFTA, middleBlock) && Decide.HasBlock(Placeholders.LEFTB, middleBlock))
            {
                status = 3;
            }
            else if (Decide.HasBlock(Placeholders.LEFTTOP, middleBlock) && !Decide.HasBlock(Placeholders.LEFTBOTTOM, middleBlock) && Decide.HasBlock(Placeholders.LEFTA, middleBlock) && !Decide.HasBlock(Placeholders.LEFTB, middleBlock))
            {
                status = 4;
            }
            else if (Decide.HasBlock(Placeholders.LEFTTOP, middleBlock) && !Decide.HasBlock(Placeholders.LEFTBOTTOM, middleBlock) && !Decide.HasBlock(Placeholders.LEFTA, middleBlock) && Decide.HasBlock(Placeholders.LEFTB, middleBlock))
            {
                status = 5;
            }
            else if (!Decide.HasBlock(Placeholders.LEFTTOP, middleBlock) && Decide.HasBlock(Placeholders.LEFTBOTTOM, middleBlock) && !Decide.HasBlock(Placeholders.LEFTA, middleBlock) && Decide.HasBlock(Placeholders.LEFTB, middleBlock))
            {
                status = 6;
            }
            else if (!Decide.HasBlock(Placeholders.LEFTTOP, middleBlock) && Decide.HasBlock(Placeholders.LEFTBOTTOM, middleBlock) && Decide.HasBlock(Placeholders.LEFTA, middleBlock) && !Decide.HasBlock(Placeholders.LEFTB, middleBlock))
            {
                status = 7;
            }
            else if (!Decide.HasBlock(Placeholders.LEFTTOP, middleBlock) && !Decide.HasBlock(Placeholders.LEFTBOTTOM, middleBlock) && Decide.HasBlock(Placeholders.LEFTA, middleBlock) && Decide.HasBlock(Placeholders.LEFTB, middleBlock))
            {
                status = 8;
            }
            else if (!Decide.HasBlock(Placeholders.LEFTTOP, middleBlock) && !Decide.HasBlock(Placeholders.LEFTBOTTOM, middleBlock) && Decide.HasBlock(Placeholders.LEFTA, middleBlock) && !Decide.HasBlock(Placeholders.LEFTB, middleBlock))
            {
                status = 10;
            }
            else if (!Decide.HasBlock(Placeholders.LEFTTOP, middleBlock) && !Decide.HasBlock(Placeholders.LEFTBOTTOM, middleBlock) && !Decide.HasBlock(Placeholders.LEFTA, middleBlock) && Decide.HasBlock(Placeholders.LEFTB, middleBlock))
            {
                status = 9;
            }
            else if (Decide.HasBlock(Placeholders.LEFTTOP, middleBlock) && !Decide.HasBlock(Placeholders.LEFTBOTTOM, middleBlock) && Decide.HasBlock(Placeholders.LEFTA, middleBlock) && Decide.HasBlock(Placeholders.LEFTB, middleBlock))
            {
                status = 11;
            }
            else if (!Decide.HasBlock(Placeholders.LEFTTOP, middleBlock) && Decide.HasBlock(Placeholders.LEFTBOTTOM, middleBlock) && Decide.HasBlock(Placeholders.LEFTA, middleBlock) && Decide.HasBlock(Placeholders.LEFTB, middleBlock))
            {
                status = 12;
            }
            return status;

        }
        /// <summary>
        /// Mode for templating right block.
        /// </summary>
        /// <param name="middleBlock">XmlTag class containing xml details.</param>
        /// <returns>Mode</returns>
        public static int RightBlockMode(XmlTag middleBlock)
        {
            int status = 0;
            if (Decide.HasBlock(Placeholders.RIGHTTOP, middleBlock) && Decide.HasBlock(Placeholders.RIGHTBOTTOM, middleBlock) && Decide.HasBlock(Placeholders.RIGHTA, middleBlock) && Decide.HasBlock(Placeholders.RIGHTB, middleBlock))
            {
                status = 1;
            }
            else if (Decide.HasBlock(Placeholders.RIGHTTOP, middleBlock) && Decide.HasBlock(Placeholders.RIGHTBOTTOM, middleBlock) && Decide.HasBlock(Placeholders.RIGHTA, middleBlock) && !Decide.HasBlock(Placeholders.RIGHTB, middleBlock))
            {
                status = 2;
            }
            else if (Decide.HasBlock(Placeholders.RIGHTTOP, middleBlock) && Decide.HasBlock(Placeholders.RIGHTBOTTOM, middleBlock) && !Decide.HasBlock(Placeholders.RIGHTA, middleBlock) && Decide.HasBlock(Placeholders.RIGHTB, middleBlock))
            {
                status = 3;
            }
            else if (Decide.HasBlock(Placeholders.RIGHTTOP, middleBlock) && !Decide.HasBlock(Placeholders.RIGHTBOTTOM, middleBlock) && Decide.HasBlock(Placeholders.RIGHTA, middleBlock) && !Decide.HasBlock(Placeholders.RIGHTB, middleBlock))
            {
                status = 4;
            }
            else if (Decide.HasBlock(Placeholders.RIGHTTOP, middleBlock) && !Decide.HasBlock(Placeholders.RIGHTBOTTOM, middleBlock) && !Decide.HasBlock(Placeholders.RIGHTA, middleBlock) && Decide.HasBlock(Placeholders.RIGHTB, middleBlock))
            {
                status = 5;
            }
            else if (!Decide.HasBlock(Placeholders.RIGHTTOP, middleBlock) && Decide.HasBlock(Placeholders.RIGHTBOTTOM, middleBlock) && !Decide.HasBlock(Placeholders.RIGHTA, middleBlock) && Decide.HasBlock(Placeholders.RIGHTB, middleBlock))
            {
                status = 6;
            }
            else if (!Decide.HasBlock(Placeholders.RIGHTTOP, middleBlock) && Decide.HasBlock(Placeholders.RIGHTBOTTOM, middleBlock) && Decide.HasBlock(Placeholders.RIGHTA, middleBlock) && !Decide.HasBlock(Placeholders.RIGHTB, middleBlock))
            {
                status = 7;
            }
            else if (!Decide.HasBlock(Placeholders.RIGHTTOP, middleBlock) && !Decide.HasBlock(Placeholders.RIGHTBOTTOM, middleBlock) && Decide.HasBlock(Placeholders.RIGHTA, middleBlock) && Decide.HasBlock(Placeholders.RIGHTB, middleBlock))
            {
                status = 8;
            }
            else if (!Decide.HasBlock(Placeholders.RIGHTTOP, middleBlock) && !Decide.HasBlock(Placeholders.RIGHTBOTTOM, middleBlock) && Decide.HasBlock(Placeholders.RIGHTA, middleBlock) && !Decide.HasBlock(Placeholders.RIGHTB, middleBlock))
            {
                status = 10;
            }
            else if (!Decide.HasBlock(Placeholders.RIGHTTOP, middleBlock) && !Decide.HasBlock(Placeholders.RIGHTBOTTOM, middleBlock) && !Decide.HasBlock(Placeholders.RIGHTA, middleBlock) && Decide.HasBlock(Placeholders.RIGHTB, middleBlock))
            {
                status = 9;
            }
            else if (Decide.HasBlock(Placeholders.RIGHTTOP, middleBlock) && !Decide.HasBlock(Placeholders.RIGHTBOTTOM, middleBlock) && Decide.HasBlock(Placeholders.RIGHTA, middleBlock) && Decide.HasBlock(Placeholders.RIGHTB, middleBlock))
            {
                status = 11;
            }
            else if (!Decide.HasBlock(Placeholders.RIGHTTOP, middleBlock) && Decide.HasBlock(Placeholders.RIGHTBOTTOM, middleBlock) && Decide.HasBlock(Placeholders.RIGHTA, middleBlock) && Decide.HasBlock(Placeholders.RIGHTB, middleBlock))
            {
                status = 12;
            }
            return status;

        }


    }
}
