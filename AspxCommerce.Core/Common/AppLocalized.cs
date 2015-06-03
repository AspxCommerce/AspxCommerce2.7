using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using SageFrame.Web;
using System.Web;

namespace AspxCommerce.Core
{
    public class AppLocalized : SageUserControl
    {
        public AppLocalized()
        {
        }

        public static Hashtable getLocale(string moduleFolderPath)
        {            
            AppLocalized obj = new AppLocalized();
            string localFilePath = obj.FilePath(moduleFolderPath);            
            string fillString = string.Empty;
            localFilePath = HttpContext.Current.Server.MapPath(localFilePath);
            Hashtable hst = new Hashtable();
            if (File.Exists(localFilePath))
            {                
                using (StreamReader streamReader = File.OpenText(localFilePath))
                {
                    string inputString = streamReader.ReadLine();
                    while (inputString != null)
                    {
                        string regexPattern = "(\"(?<key>[^\"]+)\"\\s*:\\s*\"(?<value>[^\"]+)\")|(\'(?<key>[^\']+)\'\\s*:\\s*\'(?<value>[^\']+)\')";
                        Regex regex = new Regex(regexPattern, RegexOptions.IgnorePatternWhitespace);
                        if (regex.IsMatch(inputString))
                        {
                            Match match = regex.Match(inputString);
                            string key = match.Groups[3].Value.Trim();
                            string value = match.Groups[4].Value.Trim();
                            if (hst[key] != null)
                            { }
                            else
                            {
                                hst.Add(key, value);
                            }
                        }
                        inputString = streamReader.ReadLine();
                    }
                }
            }
            return hst;            
        }

        public string FilePath(string moduleFolderPath)
        {

            string FileUrl = string.Empty;
            string strScript = string.Empty;
            string langFolder = moduleFolderPath + "Language/";
            if (Directory.Exists(HttpContext.Current.Server.MapPath(langFolder)))
            {
                bool isTrue = false;
                string[] fileList = Directory.GetFiles(HttpContext.Current.Server.MapPath(langFolder));

                string regexPattern = ".*\\\\(?<file>[^\\.]+)(\\.[a-z]{2}-[A-Z]{2})?\\.js";

                Regex regex = new Regex(regexPattern, RegexOptions.IgnorePatternWhitespace);

                Match match = regex.Match(fileList[0]);
                string languageFile = match.Groups[2].Value;

                isTrue = GetCurrentCulture() == "en-US" ? true : false;
                if (isTrue)
                {
                    FileUrl = langFolder + languageFile + ".js";

                }
                else
                {
                    FileUrl = langFolder + languageFile + "." + GetCurrentCulture() + ".js";
                }
                string inputString = string.Empty;
                if (!File.Exists(HttpContext.Current.Server.MapPath(FileUrl)))
                {
                    FileUrl = langFolder + languageFile + ".js";
                }

            }
            return FileUrl;

        }
        
    }
}
