#region "Copyright"
/*
FOR FURTHER DETAILS ABOUT LICENSING, PLEASE VISIT "LICENSE.txt" INSIDE THE SAGEFRAME FOLDER
*/
#endregion

#region "References"
using System;
using System.Collections.Generic;
using System.Web;
using System.Xml;
using System.Globalization;
using System.IO;
using SageFrame.Common;
using SageFrame.Common.CommonFunction;
#endregion

namespace SageFrame.Web
{
    /// <summary>
    /// Summary of application message.
    /// </summary>
    public static class SageMessage
    {
        /// <summary>
        /// Localize application menu.
        /// </summary>
        /// <param name="portalName">Portal Name</param>
        /// <param name="CurrentCultureName">Current culture name.</param>
        /// <param name="MenuKey">Menu key.</param>
        /// <param name="MenuAttribute">Menu attribute.</param>
        /// <returns></returns>
        public static string SageMenuLocalization(string portalName,string CurrentCultureName, string MenuKey, string MenuAttribute)
        {
            XmlDocument doc = new XmlDocument();
            string xmlPath = HttpContext.Current.Server.MapPath("~/XMLMessage/"+portalName+"Menu." + CurrentCultureName + ".xml");
            if (!File.Exists(xmlPath))
                xmlPath = HttpContext.Current.Server.MapPath("~/XMLMessage/" + portalName + "Menu.xml");

            doc.Load(xmlPath);
            XmlNode root = doc.DocumentElement;
            if (root.SelectSingleNode(HTMLHelper.StripTagsCharArray(MenuKey)) != null && root.SelectSingleNode(HTMLHelper.StripTagsCharArray(MenuKey)).SelectSingleNode(MenuAttribute) != null)
            {
                return root.SelectSingleNode(MenuKey).SelectSingleNode(MenuAttribute).ChildNodes[0].Value;
            }
            else
            {
                return string.Empty;
            }
        }
        /// <summary>
        /// Return application message.
        /// </summary>
        /// <param name="CurrentCultureName">Current culture name.</param>
        /// <param name="ModuleName">Module name.</param>
        /// <param name="MessageNode">Message node.</param>
        /// <returns>Message</returns>
        public static string ProcessSageMessage(string CurrentCultureName, string ModuleName, string MessageNode)
        {
            return ProvideSageMessage(CurrentCultureName, ModuleName, MessageNode);
        }
        /// <summary>
        /// Return application message.
        /// </summary>
        /// <param name="CurrentCultureName">Current culture name.</param>
        /// <param name="ModuleName">Module name.</param>
        /// <param name="MessageNode">Message node.</param>
        /// <returns>Message</returns>
        public static string ProvideSageMessage(string CurrentCultureName, string ModuleName, string MessageNode)
        {
            XmlDocument doc = new XmlDocument();
            string xmlPath = HttpContext.Current.Server.MapPath("~/XMLMessage/SageXML." + CurrentCultureName + ".xml");
            if (!File.Exists(xmlPath))
                xmlPath = HttpContext.Current.Server.MapPath("~/XMLMessage/SageXML.xml");

            doc.Load(xmlPath);
            XmlNode root = doc.DocumentElement;
            return root.SelectSingleNode(ModuleName).SelectSingleNode(MessageNode).ChildNodes[0].Value;
        }
        /// <summary>
        /// Return application message.
        /// </summary>
        /// <param name="CurrentCultureName">Current culture name.</param>
        /// <param name="ModuleName">Module name.</param>
        /// <param name="MessageNode">Message node.</param>
        /// <returns>Message</returns>
        public static string ListSageText(string CurrentCultureName, string ModuleName, string MessageNode)
        {
            XmlDocument doc = new XmlDocument();
            string xmlPath = HttpContext.Current.Server.MapPath("~/XMLMessage/SageXMLList." + CurrentCultureName + ".xml");
            if (!File.Exists(xmlPath))
                xmlPath = HttpContext.Current.Server.MapPath("~/XMLMessage/SageXMLList.xml");

             doc.Load(xmlPath);
            XmlNode root = doc.DocumentElement;
            return root.SelectSingleNode(ModuleName).SelectSingleNode(MessageNode).ChildNodes[0].Value;
        }
        /// <summary>
        /// Return list of module name.
        /// </summary>
        /// <param name="CurrentCultureName">Current culture name.</param>
        /// <param name="ModuleName">Module name.</param>
        /// <returns></returns>
        public static List<SageFrameStringKeyValue> ListSageModuleName(string CurrentCultureName, string ModuleName)
        {
            
            List<SageFrameStringKeyValue> modulePanes = new List<SageFrameStringKeyValue>();
            XmlDocument doc = new XmlDocument();
            string xmlPath = HttpContext.Current.Server.MapPath("~/XMLMessage/SageXMLList." + CurrentCultureName + ".xml");
            if (!File.Exists(xmlPath))
                xmlPath = HttpContext.Current.Server.MapPath("~/XMLMessage/SageXMLList.en-US.xml");

            doc.Load(xmlPath);
            XmlNode root = doc.DocumentElement;
            for (int i = 0; i < root.SelectSingleNode(ModuleName).ChildNodes.Count; i++)
            {
                modulePanes.Add(new SageFrameStringKeyValue(root.SelectSingleNode(ModuleName).ChildNodes[i].Name, root.SelectSingleNode(ModuleName).ChildNodes[i].InnerText));
            }
            return modulePanes;
        }
        /// <summary>
        /// Return module message.
        /// </summary>
        /// <param name="VertualPath">Path for message file.</param>
        /// <param name="MessageNode">Message node</param>
        /// <returns>Module message.</returns>
        public static string GetSageModuleLocalMessageByVertualPath(string VertualPath, string MessageNode)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                string CurrentCultureName = CultureInfo.CurrentCulture.Name;                           
                doc.Load(HttpContext.Current.Server.MapPath("~/" + VertualPath + "/SageModuleLocalText." + CurrentCultureName + ".xml"));
                if (doc != null)
                {
                    XmlNode root = doc.DocumentElement;
                    return root.SelectSingleNode(MessageNode).ChildNodes[0].Value;
                }
                else
                {
                    doc.Load(HttpContext.Current.Server.MapPath("~/" + VertualPath + "/SageModuleLocalText." + "en-US" + ".xml"));
                    if (doc != null)
                    {
                        XmlNode root = doc.DocumentElement;
                        return root.SelectSingleNode(MessageNode).ChildNodes[0].Value;
                    }
                    else
                    {
                        doc.Load(HttpContext.Current.Server.MapPath("~/" + VertualPath + "/SageModuleLocalText.xml"));
                        if (doc != null)
                        {
                            XmlNode root = doc.DocumentElement;
                            return root.SelectSingleNode(MessageNode).ChildNodes[0].Value;
                        }
                        else
                        {
                            return string.Empty;
                        }
                    }
                }
            }
            catch
            {
                return string.Empty;
            }

        }
        /// <summary>
        ///  Return module message.
        /// </summary>
        /// <param name="FullPathwithFileName">Full path for message file.</param>
        /// <param name="MessageNode">Message node</param>
        /// <returns>>Module message.</returns>
        public static string GetSageModuleLocalMessageByFullPathwithFileName(string FullPathwithFileName, string MessageNode)
        {
            try
            {
                XmlDocument doc = new XmlDocument();                
                doc.Load(FullPathwithFileName);
                if (doc != null)
                {
                    XmlNode root = doc.DocumentElement;
                    return root.SelectSingleNode(MessageNode).ChildNodes[0].Value;
                }
                else
                {
                    return string.Empty;
                }
            }
            catch
            {
                return string.Empty;
            }

        }
    }
}
