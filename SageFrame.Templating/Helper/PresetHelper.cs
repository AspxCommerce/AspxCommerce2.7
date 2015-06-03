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
using System.Xml;
using SageFrame.Templating.xmlparser;
using System.IO;
#endregion

namespace SageFrame.Templating
{
    /// <summary>
    /// Helper class for preset.
    /// </summary>
    public class PresetHelper
    {
        /// <summary>
        /// Preset parser.
        /// </summary>
        /// <param name="xmlFile">String format of XML.</param>
        /// <param name="startParseNode">Node to start parse.</param>
        /// <returns>List of PresetInfo class.</returns>
        public static List<PresetInfo> ParsePreset(string xmlFile, string startParseNode)
        {
            List<PresetInfo> lstPreset = new List<PresetInfo>();
            XmlDocument doc = XmlHelper.LoadXMLDocument(xmlFile);
            XmlNodeList sectionList = doc.SelectNodes(startParseNode);
            foreach (XmlNode preset in sectionList)
            {
                PresetInfo tag = new PresetInfo();
                tag.PresetName = preset.Attributes["preset"].Value;
                tag.LSTPages = PageList(preset.InnerText);
                lstPreset.Add(tag);

            }
            return lstPreset;
        }
        /// <summary>
        /// Preset parser.
        /// </summary>
        /// <param name="xmlFile">String format of XML.</param>
        /// <param name="startParseNode">Node to start parse.</param>
        /// <param name="lstAllPages">List of pages.</param>
        /// <returns></returns>
        public static List<PresetInfo> ParsePreset(string xmlFile, string startParseNode,out List<string> lstAllPages)
        {
            List<PresetInfo> lstPreset = new List<PresetInfo>();
            XmlDocument doc = XmlHelper.LoadXMLDocument(xmlFile);
            XmlNodeList sectionList = doc.SelectNodes(startParseNode);
            List<string> lstAllPagesIn = new List<string>();
            foreach (XmlNode preset in sectionList)
            {
                PresetInfo tag = new PresetInfo();
                tag.PresetName = preset.Attributes["layout"].Value;
                tag.LSTPages = PageList(preset.InnerText);                
                lstPreset.Add(tag);
                if (preset.InnerText.Equals("All") || preset.InnerText.Equals("all"))
                {
                    tag.IsDefault = true;
                }
                else
                {
                    tag.IsDefault = false;
                }
                foreach (string page in tag.LSTPages)
                {
                    lstAllPagesIn.Add(page);
                }

            }
            lstAllPages = lstAllPagesIn;
            return lstPreset;
        }
        /// <summary>
        /// List of page.
        /// </summary>
        /// <param name="pages">Page name with comma separator.</param>
        /// <returns>List of page.</returns>
        static List<string> PageList(string pages)
        {
            List<string> lstPages = new List<string>();
            string[] arrPages = pages.Split(',');

            foreach (string page in arrPages)
            {
                string expr = page;
                lstPages.Add((expr.IndexOf('*') == -1 || expr.IndexOf("All") == -1) ? expr: expr);

            }
            return lstPages;
        }
        /// <summary>
        /// preset details.
        /// </summary>
        /// <param name="xmlPath">String format of XML.</param>
        /// <returns>object of PresetInfo class.</returns>
        public static PresetInfo LoadPresetDetails(string xmlPath)
        {
            if (!File.Exists(xmlPath))
            {
                xmlPath = string.Format("{0}/pagepreset.xml",Utils.GetPresetPath_DefaultTemplate("default"));
            }
            XmlDocument doc = XmlHelper.LoadXMLDocument(xmlPath);
            XmlNode xnpreset = doc.SelectSingleNode("pagepreset");
            XmlNodeList xnlist = xnpreset.ChildNodes;
            PresetInfo preset = new PresetInfo();
            List<KeyValue> lstLayouts = new List<KeyValue>();
            
            foreach (XmlNode node in xnlist)
            {
                switch (node.Name)
                {
                    case "layout":
                        XmlNodeList layouts = node.ChildNodes;
                      
                        foreach (XmlNode layout in layouts)
                        {
                            lstLayouts.Add(new KeyValue(layout.Attributes["layout"].Value, layout.InnerText));
                        }                        
                        break;
                    case "theme":
                        preset.ActiveTheme = node.InnerText;
                        break;
                    case "width":
                        preset.ActiveWidth = node.InnerText;
                        break;                

                }
                preset.lstLayouts = lstLayouts;

            }
            return preset;

        }
        /// <summary>
        /// Write preset.
        /// </summary>
        /// <param name="xmlpath">Path for xml file.</param>
        /// <param name="objPreset">Object of PresetInfo class.</param>
        public static void WritePreset(string xmlpath, PresetInfo objPreset)
        {
            if (File.Exists(xmlpath))
            {
                File.Delete(xmlpath);
                WriteNewPreset(xmlpath, objPreset);

            }
            else
            {
                WriteNewPreset(xmlpath, objPreset);
            }
        }
        /// <summary>
        /// Write new preset.
        /// </summary>
        /// <param name="xmlpath">Path for xml file.</param>
        /// <param name="objPreset">Object of PresetInfo class.</param>
        static void WriteNewPreset(string xmlpath, PresetInfo objPreset)
        {
            using (XmlTextWriter writer = new XmlTextWriter(xmlpath, Encoding.UTF8))
            {
                writer.Formatting = Formatting.Indented;
                writer.WriteStartDocument();
                writer.WriteStartElement("pagepreset");
               

                writer.WriteStartElement("layout");
                foreach (KeyValue kvp in objPreset.lstLayouts)
                {
                    writer.WriteStartElement("preset");
                    writer.WriteAttributeString("layout", kvp.Key);
                    writer.WriteString(Utils.GetSEOName(kvp.Value,"-"));
                    writer.WriteEndElement();
                }
                
                writer.WriteEndElement();
                writer.WriteStartElement("theme");
                writer.WriteString(objPreset.ActiveTheme);
                writer.WriteEndElement();

                writer.WriteStartElement("width");
                writer.WriteString(objPreset.ActiveWidth);
                writer.WriteEndElement();

                writer.WriteEndElement();
                writer.WriteEndDocument();
                writer.Close();
            }
        }
        /// <summary>
        /// Update preset page.
        /// </summary>
        /// <param name="objPreset">Object of PresetInfo class.</param>
        /// <param name="xmlpath">Path for xml file.</param>
        /// <returns>0:Everything is normal,  1:A few pages are already applied to presets, 2:All Page is already applied</returns>
        public static int UpdatePresetPages(PresetInfo objPreset, string xmlpath)
        {
            XmlDocument doc = XmlHelper.LoadXMLDocument(xmlpath);
            XmlNode xnpreset = doc.SelectSingleNode("pagepresets");
            XmlNodeList xnlist = xnpreset.ChildNodes;
                        
            bool isApplied = false;
            bool isAllPageApplied = false;
            bool isPageApplied = false;
            int nodecount = 0;
            foreach (XmlNode node in xnlist)
            {

                if (node.Attributes["preset"].Value == objPreset.PresetName || node.Attributes["preset"].Value == Path.GetFileNameWithoutExtension(objPreset.PresetName.ToLower()))
                {
                    isApplied = true;
                    node.InnerText = objPreset.Pages;                   
                }
                if (node.InnerText.ToLower() == "" || node.InnerText.ToLower() == "none")
                {
                    xnlist[nodecount].ParentNode.RemoveChild(xnlist[nodecount]);
                }
                else if (node.InnerText.ToLower() == "all" && objPreset.Pages.ToLower() == "all" && node.Attributes["preset"].Value.ToLower() != Path.GetFileNameWithoutExtension(objPreset.PresetName.ToLower()))
                {
                    isAllPageApplied = true;
                }
                if (node.InnerText.ToLower() != "all" && Path.GetFileNameWithoutExtension(objPreset.Pages.ToLower()) != "all")
                {
                    string[] arrPages = node.InnerText.ToLower().Split(',');
                    string[] arrPagesNew=objPreset.Pages.ToLower().Split(',');
                    List<string> arrFinalPages=new List<string>();
                    
                    foreach (string page in arrPagesNew)
                    {
                        if (!arrPages.Contains(page) && page!="all")
                        {
                            arrFinalPages.Add(page);
                        }
                        else
                        {
                            isPageApplied = true;
                        }
                    }
                    objPreset.Pages = string.Join(",", arrFinalPages.ToArray());
                }
                nodecount++;
                
            }
            
            if (!isApplied && !isAllPageApplied && objPreset.Pages!="" && objPreset.Pages!="none")
            {
                XmlElement elem = doc.CreateElement("page");
                elem.SetAttribute("preset",Path.GetFileNameWithoutExtension(objPreset.PresetName));
                elem.InnerText = objPreset.Pages;
                xnpreset.AppendChild(elem);
            }
            doc.Save(xmlpath);

            return(isAllPageApplied?1:isPageApplied?2:0);
            ///Return Login
            ///0:Everything is normal
            ///1:A few pages are already applied to presets
            ///2:All Page is already applied


        }
        /// <summary>
        /// Delete preset from pages.
        /// </summary>
        /// <param name="presetName">Preset name.</param>
        /// <param name="xmlpath">Path for xml file.</param>
        public static void DeletePresetFromPresetPages(string presetName, string xmlpath)
        {
            XmlDocument doc = XmlHelper.LoadXMLDocument(xmlpath);
            XmlNode xnpreset = doc.SelectSingleNode("pagepresets");
            XmlNodeList xnlist = xnpreset.ChildNodes;
            int nodecount = 0;
            foreach (XmlNode node in xnlist)
            {

                if (node.InnerText.ToLower() == "" || node.InnerText.ToLower() == "none" || node.Attributes["preset"].Value == Path.GetFileNameWithoutExtension(presetName.ToLower()))
                {
                    xnlist[nodecount].ParentNode.RemoveChild(xnlist[nodecount]);
                }
                nodecount++;            
            }

            doc.Save(xmlpath);

        }      
        /// <summary>
        /// Load assigne preset for associate page.
        /// </summary>
        /// <param name="TemplateName">Template name.</param>
        /// <param name="PageName">Page name.</param>
        /// <returns>Preset path.</returns>
        public static string LoadActivePresetForPage(string TemplateName,string PageName)
        {
            string presetPath = TemplateName.ToLower().Equals("default")?Utils.GetPresetPath_DefaultTemplate(TemplateName):Utils.GetPresetPath(TemplateName);
            List<PresetInfo> lstActivePresets = new List<PresetInfo>();
            string pagepreset = presetPath + "/" + TemplateConstants.PagePresetFile;
            List<string> lstAllPages = new List<string>();
            if (File.Exists(pagepreset))
            {
                if (!CacheHelper.Get("PresetList", out lstActivePresets))
                {
                    lstActivePresets = PresetHelper.ParsePreset(pagepreset, "pagepreset/layout/preset", out lstAllPages);
                    CacheHelper.Add(lstActivePresets, "PresetList");
                }
            }
            else
            {
                lstActivePresets.Add(PresetInfo.GetPresetPages("default", "*"));
            }
            string pagepresetactive = string.Empty;
            string defaultpreset = "layout.ascx";
            foreach (PresetInfo preset in lstActivePresets)
            {
                if (preset.IsDefault)
                {
                    defaultpreset = string.Format("{0}.ascx", preset.PresetName); ;                    
                }
                else
                {
                    string presetPathFull = string.Empty;
                    foreach (string page in preset.LSTPages)
                    {
                        if (page.ToLower().Equals(PageName.ToLower()))
                        {
                            pagepresetactive = string.Format("{0}.ascx", preset.PresetName); ;           
                            break;
                        }
                    }
                }
            }
            if (pagepresetactive == string.Empty)
            {
                pagepresetactive = defaultpreset;
            }
            pagepresetactive =Decide.IsTemplateDefault(TemplateName)?string.Format("~/Core/Template/{1}",TemplateName,pagepresetactive):string.Format("~/Templates/{0}/{1}",TemplateName,pagepresetactive);           
            return pagepresetactive;
        }
        /// <summary>
        /// Load handheld controls.
        /// </summary>
        /// <param name="TemplateName">Templates name.</param>
        /// <returns>Preset path.</returns>
        public static string LoadHandheldControl(string TemplateName)
        {
            return (Decide.IsTemplateDefault(TemplateName) ? "~/Core/Template/handheld.ascx" : string.Format("~/Templates/{0}/handheld.ascx", TemplateName));
        }
        /// <summary>
        /// Load device type 3.
        /// </summary>
        /// <param name="TemplateName">Template name.</param>
        /// <returns>Preset path.</returns>
        public static string LoadDeviceType3(string TemplateName)
        {
            return (Decide.IsTemplateDefault(TemplateName) ? "~/Core/Template/handheld.ascx" : string.Format("~/Templates/{0}/devicetype3.ascx", TemplateName));
        }
        /// <summary>
        /// Load active page preset.
        /// </summary>
        /// <param name="TemplateName">Template name.</param>
        /// <param name="PageName">Page name.</param>
        /// <returns>Object of PresetInfo class.</returns>
        public static PresetInfo LoadActivePagePreset(string TemplateName, string PageName)
        {
            string presetPath = TemplateName.ToLower().Equals("default") ? Utils.GetPresetPath_DefaultTemplate(TemplateName) : Utils.GetPresetPath(TemplateName);           
            string pagepreset = presetPath + "/" + TemplateConstants.PagePresetFile;           
            PresetInfo pagepresetactive = new PresetInfo();
            pagepresetactive = LoadPresetDetails(pagepreset);
            string defaultPreset = string.Empty;
            foreach (KeyValue kvp in pagepresetactive.lstLayouts)
            {                
                    string presetPathFull = string.Empty;
                    string[] arrPages = kvp.Value.ToString().Split(',');
                    foreach (string page in arrPages)
                    {
                        if (page.ToLower().Equals(PageName.ToLower()))
                        {
                            pagepresetactive.ActiveLayout = kvp.Key;
                            break;
                        }
                    }
                    if (kvp.Value.Equals("all") || kvp.Value.Equals("All") || kvp.Value.Equals("*") || kvp.Value.Equals(""))
                    {
                        defaultPreset = kvp.Key;
                    }
            }
            if (pagepresetactive.ActiveLayout == "" || pagepresetactive.ActiveLayout == null)
            {
                pagepresetactive.ActiveLayout = defaultPreset;
            }
            return pagepresetactive;
        }
        /// <summary>
        /// Update existing preset.
        /// </summary>
        /// <param name="preset">Object of PresetInfo class.</param>
        /// <param name="TemplateName">Template name.</param>
        public static void UpdatePreset(PresetInfo preset, string TemplateName)
        {
            string presetPath = TemplateName.ToLower().Equals("default") ? Utils.GetPresetPath_DefaultTemplate(TemplateName) : Utils.GetPresetPath(TemplateName);
           
            try
            {
                WritePreset(string.Format("{0}/pagepreset.xml",presetPath), preset);
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        
    }
}
