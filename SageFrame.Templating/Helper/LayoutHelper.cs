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
using System.IO;
#endregion

namespace SageFrame.Templating
{
    /// <summary>
    /// Helper class for layout..
    /// </summary>
    public class LayoutHelper
    {
        /// <summary>
        /// Create layouts.
        /// </summary>
        /// <param name="TemplateName">Template name.</param>
        /// <param name="PresetObj">Object of PresetInfo class.</param>
        public static void CreateLayoutControls(string TemplateName, PresetInfo PresetObj)
        {
            string templatePath = TemplateName.ToLower().Equals("default") ? Utils.GetTemplatePath_Default(TemplateName) : Utils.GetTemplatePath(TemplateName);
            string presetPath = TemplateName.ToLower().Equals("default") ? Utils.GetPresetPath_DefaultTemplate(TemplateName) : Utils.GetPresetPath(TemplateName);
            ModulePaneGenerator mg = new ModulePaneGenerator();
            string filePath = templatePath + "/layouts/" + PresetObj.ActiveLayout.Replace(".xml", "") + ".xml";
            XmlParser parser = new XmlParser();
            try
            {
                List<XmlTag> lstXmlTag = parser.GetXmlTags(filePath, "layout/section");
                List<XmlTag> lstWrappers = parser.GetXmlTags(filePath, "layout/wrappers");

                string html = mg.GenerateHTML(lstXmlTag, lstWrappers, 2);
                string controlclass = Path.GetFileNameWithoutExtension(filePath);
                string controlname = string.Format("{0}.ascx", controlclass);
                if (!File.Exists(templatePath + "/" + controlname))
                {
                    FileStream fs = null;
                    using (fs = File.Create(templatePath + "/" + controlname))
                    {

                    }

                }
                else
                {
                    File.Delete(templatePath + "/" + controlname);
                    FileStream fs = null;
                    using (fs = File.Create(templatePath + "/" + controlname))
                    {

                    }
                }

                using (StreamWriter sw = new StreamWriter(templatePath + "/" + controlname))
                {
                    sw.Write("<%@ Control Language=\"C#\" ClassName=" + controlclass + " %>");
                    sw.Write(html);
                }


            }
            catch (Exception)
            {

                throw;
            }


        }
        /// <summary>
        /// Create handheld layout control.
        /// </summary>
        /// <param name="TemplateName">Template name.</param>
        public static void CreateHandheldLayoutControls(string TemplateName)
        {
            string templatePath = TemplateName.ToLower().Equals("default") ? Utils.GetTemplatePath_Default(TemplateName) : Utils.GetTemplatePath(TemplateName);
            string presetPath = TemplateName.ToLower().Equals("default") ? Utils.GetPresetPath_DefaultTemplate(TemplateName) : Utils.GetPresetPath(TemplateName);
            ModulePaneGenerator mg = new ModulePaneGenerator();
            string filePath = templatePath + "/layouts/handheld.xml";
            XmlParser parser = new XmlParser();
            try
            {
                List<XmlTag> lstXmlTag = parser.GetXmlTags(filePath, "layout/section");
                List<XmlTag> lstWrappers = parser.GetXmlTags(filePath, "layout/wrappers");

                string html = mg.GenerateHTML(lstXmlTag, lstWrappers, 2);
                string controlclass = Path.GetFileNameWithoutExtension(filePath);
                string controlname = string.Format("{0}.ascx", controlclass);
                if (!File.Exists(templatePath + "/" + controlname))
                {
                    FileStream fs = null;
                    using (fs = File.Create(templatePath + "/" + controlname))
                    {

                    }

                }
                else
                {
                    File.Delete(templatePath + "/" + controlname);
                    FileStream fs = null;
                    using (fs = File.Create(templatePath + "/" + controlname))
                    {

                    }
                }

                using (StreamWriter sw = new StreamWriter(templatePath + "/" + controlname))
                {
                    sw.Write("<%@ Control Language=\"C#\" ClassName=" + controlclass + " %>");
                    sw.Write(html);
                }


            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
