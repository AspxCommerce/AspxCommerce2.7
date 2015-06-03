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
using System.IO;
using SageFrame.Templating.xmlparser;
using System.Web;
using System.Text.RegularExpressions;
using SageFrame.Web;
#endregion

namespace SageFrame.Templating
{
    /// <summary>
    /// Helper class for various utilities.
    /// </summary>
    public class Utils
    {
        /// <summary>
        /// Validate Image thumbnail extension.
        /// </summary>
        /// <param name="file">Object of FileInfo class.</param>
        /// <returns>True for valid extension.</returns>
        public static bool ValidateThumbImage(FileInfo file)
        {
            bool isValid = false;
            if (file.Extension == ".png" || file.Extension == ".jpg" || file.Extension == ".gif")
            {
                isValid = true;
            }
            return isValid;
        }
        /// <summary>
        /// Check for valid XML tag.
        /// </summary>
        /// <param name="tag">Object of XmlTag class.</param>
        /// <returns>True for valid tag.</returns>
        public static bool IsValidTag(XmlTag tag)
        {
            return (Enum.IsDefined(typeof(XmlTagTypes), tag.TagName.ToUpper()));
        }
        /// <summary>
        /// Obtain template path.
        /// </summary>
        /// <param name="TemplateName">Template name.</param>
        /// <returns>Template path.</returns>
        public static string GetTemplateInfoFilePath(string TemplateName)
        {
            return (GetAbsolutePath(TemplateConstants.TemplateDirectory + TemplateName + TemplateConstants.TemplateInfo));
        }
        /// <summary>
        /// Obtain default path.
        /// </summary>
        /// <param name="TemplateName">Template name.</param>
        /// <returns>Template path.</returns>
        public static string GetTemplateInfoFilePath_Default(string TemplateName)
        {
            return (GetAbsolutePath(TemplateConstants.DefaultTemplateDir + "Template" + TemplateConstants.TemplateInfo));
        }
        /// <summary>
        /// Obtain template path.
        /// </summary>
        /// <param name="TemplateName">Template name.</param>
        /// <returns>Template path.</returns>
        public static string GetTemplatePath(string TemplateName)
        {
            return (GetAbsolutePath(TemplateConstants.TemplateDirectory + TemplateName));
        }
        /// <summary>
        /// Obtain template path.
        /// </summary>
        /// <param name="TemplateName">Template name.</param>
        /// <returns>Default template path.</returns>
        public static string GetTemplatePath_Default(string TemplateName)
        {
            return (GetAbsolutePath(TemplateConstants.DefaultTemplateDir + "Template"));
        }
        /// <summary>
        /// Obtain theme path.
        /// </summary>
        /// <param name="TemplateName">Template name.</param>
        /// <returns>Theme path</returns>
        public static string GetThemePath(string TemplateName)
        {
            return (GetAbsolutePath(TemplateConstants.TemplateDirectory + TemplateName + TemplateConstants.ThemeDirectory));
        }
        /// <summary>
        ///  Obtain default theme path.
        /// </summary>
        /// <param name="TemplateName">Template name.</param>
        /// <returns>Default theme path.</returns>
        public static string GetThemePath_Default(string TemplateName)
        {
            return (GetAbsolutePath(TemplateConstants.DefaultTemplateDir + "Template" + TemplateConstants.ThemeDirectory));
        }
        /// <summary>
        /// Obtain admin template path.
        /// </summary>
        /// <returns>Admin template path.</returns>
        public static string GetAdminTemplatePath()
        {
            return (GetAbsolutePath("Administrator/Templates/"));
        }
        /// <summary>
        /// Obtain preset path.
        /// </summary>
        /// <param name="TemplateName">Template name.</param>
        /// <returns>preset path.</returns>
        public static string GetPresetPath(string TemplateName)
        {
            return (GetAbsolutePath(TemplateConstants.TemplateDirectory + TemplateName));
        }
        /// <summary>
        /// Obtain preset path for default template.
        /// </summary>
        /// <param name="TemplateName">Template name.</param>
        /// <returns>Preset path for default template.</returns>
        public static string GetPresetPath_DefaultTemplate(string TemplateName)
        {
            return (GetAbsolutePath(string.Format("{0}Template", TemplateConstants.DefaultTemplateDir)));
        }
        /// <summary>
        /// Replace back slash from file path.
        /// </summary>
        /// <param name="filepath">File path</param>
        /// <returns>Replaced path.</returns>
        public static string ReplaceBackSlash(string filepath)
        {
            if (filepath != null)
            {
                filepath = filepath.Replace("\\", "/");
            }
            return filepath;
        }
        /// <summary>
        /// Obtain absolute path.
        /// </summary>
        /// <param name="filepath">File path.</param>
        /// <returns>Absolute path.</returns>
        public static string GetAbsolutePath(string filepath)
        {
            return (Utils.ReplaceBackSlash(Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath.ToString(), filepath)));
        }
        /// <summary>
        /// Compare string.
        /// </summary>
        /// <param name="string1">First string.</param>
        /// <param name="string2">Second string.</param>
        /// <returns>True if equals.</returns>
        public static bool CompareStrings(object string1, object string2)
        {
            return (string1.ToString().ToLower().Equals(string2.ToString().ToLower()));
        }
        /// <summary>
        /// Compare string.
        /// </summary>
        /// <param name="string1">First string.</param>
        /// <param name="string2">Second string.</param>
        /// <returns>True if equals.</returns>
        public static bool CompareStrings(string string1, string string2)
        {
            return (string1.ToString().ToLower().Equals(string2.ToString().ToLower()));
        }
        /// <summary>
        /// Compare string.
        /// </summary>
        /// <param name="string1">First string.</param>
        /// <param name="string2">Second string.</param>
        /// <returns>True if equals.</returns>
        public static bool CompareStrings(string string1, object string2)
        {
            return (string1.ToString().ToLower().Equals(string2.ToString().ToLower()));
        }
        /// <summary>
        /// Compare string.
        /// </summary>
        /// <param name="string1">First string.</param>
        /// <param name="string2">Second string.</param>
        /// <returns>True if equals.</returns>
        public static bool CompareStrings(object string1, string string2)
        {

            return (string1.ToString().ToLower().Equals(string2.ToString().ToLower()));
        }
        /// <summary>
        /// Obtain number from string.
        /// </summary>
        /// <param name="expr">String with number.</param>
        /// <returns>Numbers from string.</returns>
        public static string ExtractNumbers(string expr)
        {
            return string.Join(null, System.Text.RegularExpressions.Regex.Split(expr, "[^\\d]"));
        }
        /// <summary>
        /// Delete directory.
        /// </summary>
        /// <param name="target_dir">Path of directory.</param>
        public static void DeleteDirectory(string target_dir)
        {
            if (Directory.Exists(target_dir))
            {
                string[] files = Directory.GetFiles(target_dir);
                string[] dirs = Directory.GetDirectories(target_dir);

                foreach (string file in files)
                {
                    File.SetAttributes(file, FileAttributes.Normal);
                    File.Delete(file);
                }

                foreach (string dir in dirs)
                {
                    DeleteDirectory(dir);
                }

                Directory.Delete(target_dir, true);
            }
        }
        /// <summary>
        /// Delete file.
        /// </summary>
        /// <param name="target_file">File path.</param>
        public static void DeleteFile(string target_file)
        {
            if (File.Exists(target_file))
            {
                File.Delete(target_file);
            }
        }
        /// <summary>
        /// Obtain file name with extension from file path..
        /// </summary>
        /// <param name="filename">File path.</param>
        /// <param name="ext">Extension.</param>
        /// <returns>File name with extension.</returns>
        public static string GetFileNameWithExtension(string filename, string ext)
        {

            if (Path.HasExtension(filename))
            {
                filename = Path.GetFileNameWithoutExtension(filename);
                filename = string.Format("{0}{1}{2}", filename, ".", ext);
            }
            else
            {
                filename = string.Format("{0}{1}{2}", filename, ".", ext);
            }
            return filename;
        }
        /// <summary>
        /// Check for XML header.
        /// </summary>
        /// <param name="xml">String format of XML.</param>
        /// <returns>True if contain XML header.</returns>
        public static bool ContainsXmlHeader(string xml)
        {

            return (xml.Contains("<?xml"));

        }
        /// <summary>
        /// Obtain attribue value based on name.
        /// </summary>
        /// <param name="tag">Object of XmlTag class.</param>
        /// <param name="_type">XML attribute types.<see cref="T:SageFrame.Templating.xmlparser.XmlAttributeTypes"/></param>
        /// <returns></returns>
        public static string GetAttributeValueByName(XmlTag tag, XmlAttributeTypes _type)
        {
            string value = string.Empty;
            string name = _type.ToString();
            LayoutAttribute attr = new LayoutAttribute();
            attr = tag.LSTAttributes.Find(
                delegate(LayoutAttribute attObj)
                {
                    return (Utils.CompareStrings(attObj.Name, name));
                }
                );
            return attr == null ? "" : attr.Value;
        }
        /// <summary>
        /// Obtain attribue value based on name.
        /// </summary>
        /// <param name="tag">Object of XmlTag class.</param>
        /// <param name="_type">XML attribute types.<see cref="T:SageFrame.Templating.xmlparser.XmlAttributeTypes"/></param>
        /// <param name="defaultValue">Default value.</param>
        /// <returns>Attribue value</returns>
        public static string GetAttributeValueByName(XmlTag tag, XmlAttributeTypes _type, string defaultValue)
        {
            string value = string.Empty;
            string name = _type.ToString();
            LayoutAttribute attr = new LayoutAttribute();
            attr = tag.LSTAttributes.Find(
                delegate(LayoutAttribute attObj)
                {
                    return (Utils.CompareStrings(attObj.Name, name));
                }
                );
            return attr == null ? defaultValue : attr.Value;
        }
        /// <summary>
        /// Obtain inner HTML tag.
        /// </summary>
        /// <param name="pch">Placeholders.<see cref="T:SageFrame.Templating.Placeholders"/></param>
        /// <param name="middleBlock"></param>
        /// <returns></returns>
        public static string GetTagInnerHtml(Placeholders pch, XmlTag middleBlock)
        {
            XmlTag currentTag = new XmlTag();
            currentTag = middleBlock.LSTChildNodes.Find(
                delegate(XmlTag tag)
                {
                    return (Utils.CompareStrings(Utils.GetAttributeValueByName(tag, XmlAttributeTypes.NAME), pch));
                }
                );
            return currentTag.InnerHtml;
        }
        /// <summary>
        ///Capitalized first character of given string.
        /// </summary>
        /// <param name="s">String</param>
        /// <returns>String with the first character capitalized.</returns>
        public static string UppercaseFirst(string s)
        {
            // Check for empty string.
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            // Return char and concat substring.
            return char.ToUpper(s[0]) + s.Substring(1);
        }
        /// <summary>
        /// Build URL.
        /// </summary>
        /// <param name="item">Page name.</param>
        /// <param name="appPath">Application path.</param>
        /// <param name="PortalSEOName">Portal name.</param>
        /// <param name="PortalID">PortalID.</param>
        /// <returns>URL</returns>
        public static string BuildURL(string item, string appPath, string PortalSEOName, int PortalID)
        {
            SageFrameConfig objsfConfig = new SageFrameConfig();
            string portalchange = !objsfConfig.IsParent ? string.Format("/portal/{0}", PortalSEOName) : "";
            string url = string.Empty;
            if (item == "/Admin" + SageFrameSettingKeys.PageExtension)
            {
                url = string.Format("{0}{1}/Admin{2}", appPath, portalchange, item);
            }
            else
            {
                url = string.Format("{0}{1}{2}", appPath, portalchange, item);
            }

            return url;
        }
        /// <summary>
        /// Obtain SEO name replacing space with "-".
        /// </summary>
        /// <param name="str">SEO name.</param>
        /// <param name="replacer">Optional parameter.</param>
        /// <returns>SEO name replacing space with "-".</returns>
        public static string GetSEOName(string str, string replacer)
        {
            return str.Replace(" ", "-");
        }
        /// <summary>
        /// Formatting HTML output.
        /// </summary>
        /// <param name="content">String format of HTML</param>
        /// <returns>Formatted HTML.</returns>
        public static string FormatHtmlOutput(string content)
        {
            string pattern1 = "</div>";
            MatchCollection collection1 = Regex.Matches(content, pattern1, RegexOptions.IgnoreCase);
            List<string> matchListClosingDiv = new List<string>();
            foreach (Match tmpmatch in collection1)
            {
                if (!matchListClosingDiv.Contains(tmpmatch.Value))
                    matchListClosingDiv.Add(tmpmatch.Value);
            }

            int i = 0;
            foreach (string match in matchListClosingDiv)
            {
                string elementPattern = string.Format("{0}\n{1}", GetIndentTabs(i), match);
                content = Regex.Replace(content, match, elementPattern);
                i++;
            }

            string pattern = "\\<div\\s*[^>]*\\>";
            MatchCollection collection = Regex.Matches(content, pattern, RegexOptions.IgnoreCase);

            List<string> matchList = new List<string>();
            foreach (Match tmpmatch in collection)
            {
                if (!matchList.Contains(tmpmatch.Value))
                    matchList.Add(tmpmatch.Value);
            }

            foreach (string match in matchList)
            {
                string elementPattern = match + "\n";
                content = Regex.Replace(content, match, elementPattern);
            }
            return content;

        }
        /// <summary>
        /// Obtain indent tabs.
        /// </summary>
        /// <param name="tabCount">Number of count.</param>
        /// <returns>Indented tabs.</returns>
        public static string GetIndentTabs(int tabCount)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < tabCount; i++)
            {
                sb.Append(" ");
            }
            return sb.ToString();
        }
        /// <summary>
        ///Remove unwanted character from file path.
        /// </summary>
        /// <param name="illegal">File path.</param>
        /// <returns>Well formed file path.</returns>
        public static string CleanFilePath(string illegal)
        {
            string regexSearch = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());
            Regex r = new Regex(string.Format("[{0}]", Regex.Escape(regexSearch)));
            illegal = r.Replace(illegal, "");
            return illegal;
        }
        /// <summary>
        /// Remove unwanted character from file path.
        /// </summary>
        /// <param name="path">File path.</param>
        /// <returns>Well formed file path.</returns>
        public static bool FilePathHasInvalidChars(string path)
        {

            return (path.IndexOfAny(System.IO.Path.GetInvalidPathChars()) >= 0);
        }
        /// <summary>
        /// Remove space from string.
        /// </summary>
        /// <param name="value">String</param>
        /// <returns>String without space.</returns>
        public static string CleanString(string value)
        {
            value = Regex.Replace(value, "\\s+", "");
            return value.ToLower();
        }
        /// <summary>
        /// Obtain SEO name 
        /// </summary>
        /// <param name="value">String</param>
        /// <returns>SEO name replacing space with "-".</returns>
        public static string GetSeoName(string value)
        {
            return (value.Replace(" ", "-"));
        }
        /// <summary>
        /// String format of default layout if default layout remove from application.
        /// </summary>
        /// <returns>Default layout if default layout remove from application.</returns>
        public static string FallbackLayout()
        {
            string xml = "";
            StringBuilder sb = new StringBuilder();
            sb.Append(@"<?xml version=""1.0"" encoding=""utf-8""?>");
            sb.Append(@"<layout name=""layout_all"">");
            sb.Append(@"<section name=""sfHeader"">");
            sb.Append(@"<placeholder name=""headertop"">headertop</placeholder>");
            sb.Append(@"<placeholder name=""banner"">banner</placeholder>");
            sb.Append(@"<placeholder name=""navigation"">navigation</placeholder>");
            sb.Append(@"<placeholder name=""spotlight"" mode=""fixed"">pos1,pos2,pos3</placeholder>");
            sb.Append(@"</section>");
            sb.Append(@"<section name=""sfContent"" colwidth=""20"">");
            sb.Append(@"<placeholder name=""fulltopspan"">FullTopSpan</placeholder>");
            sb.Append(@"<placeholder name=""lefttop"" class=""sftest"">LeftTop</placeholder>");
            sb.Append(@"<placeholder name=""leftA"" class=""leftaclass"">LeftA</placeholder>");
            sb.Append(@"<placeholder name=""leftB"">LeftB</placeholder>");
            sb.Append(@"<placeholder name=""leftbottom"">LeftBottom</placeholder>");
            sb.Append(@"<placeholder name=""righttop"">RightTop</placeholder>");
            sb.Append(@"<placeholder name=""rightA"">RightA</placeholder>");
            sb.Append(@"<placeholder name=""rightB"">RightB</placeholder>");
            sb.Append(@"<placeholder name=""rightbottom"">RightBottom</placeholder>");
            sb.Append(@"<placeholder name=""middletop"">MiddleTop</placeholder>");
            sb.Append(@"<placeholder name=""middlebottom"">MiddleBottom</placeholder>");
            sb.Append(@"<placeholder name=""middlemaintop"">MiddleMainTop</placeholder>");
            sb.Append(@"<placeholder name=""MiddleMainBottom"">MiddleMainBottom</placeholder>");
            sb.Append(@"<placeholder name=""middlemaincurrent"">middlemaincurrent</placeholder>");
            sb.Append(@"<placeholder name=""fullbottomspan"">FullBottomSpan</placeholder>");
            sb.Append(@"</section>");
            sb.Append(@"<section name=""sfFooter"">");
            sb.Append(@"<placeholder name=""footer"">footer</placeholder>");
            sb.Append(@"</section>");
            sb.Append(@"<wrappers>");
            sb.Append(@"<wrap type=""placeholder"" class=""testblock"" depth=""3"">middle,right</wrap>");
            sb.Append(@"</wrappers>");
            sb.Append(@"</layout>");
            xml = sb.ToString();
            return xml;
        }
    }
}
