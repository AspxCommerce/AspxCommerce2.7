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
using System.Text.RegularExpressions;
#endregion

namespace SageFrame.Localization
{
    /// <summary>
    /// Localization helper class.
    /// </summary>
    public class LocalizationHelper
    {
        /// <summary>
        /// Replace the backslash in string with front slash.
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public static string ReplaceBackSlash(string filepath)
        {
            if (filepath != null)
            {
                filepath = filepath.Replace("\\", "/");
            }
            return filepath;
        }
        /// <summary>
        /// Obtains default file name.
        /// </summary>
        /// <param name="filepath">filepath</param>
        /// <returns>Default file name for matched regex.</returns>
        public static string GetDefaultFileName(string filepath)
        {
            string fileName = Path.GetFileName(filepath);
            string ext = Path.GetExtension(filepath);
            string defaultFileName = Regex.IsMatch(fileName, @".[a-z]{2}-[A-Z]{2}" + ext + "|" + ext + "|." + @"[a-z]{2}-[A-Z]{1}[a-z]{3}-[A-Z]{2}" + ext) ? Regex.Replace(fileName, @".[a-z]{2}-[A-Z]{2}" + ext + "|" + ext + "|." + @"[a-z]{2}-[A-Z]{1}[a-z]{3}-[A-Z]{2}" + ext, "") + ext : fileName;
            return defaultFileName;

        }
        /// <summary>
        /// Obtains default file name and file path.
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns>File path and default file name for matched regex.</returns>
        public static string GetDefaultFilePath(string filepath)
        {
            string fileName = Path.GetFileName(filepath);
            filepath = filepath.Replace(fileName, "");
            string ext = Path.GetExtension(filepath);
            string defaultFileName = Regex.IsMatch(fileName, @".[a-z]{2}-[A-Z]{2}" + ext + "|" + ext + "|." + @"[a-z]{2}-[A-Z]{1}[a-z]{3}-[A-Z]{2}" + ext) ? Regex.Replace(fileName, @".[a-z]{2}-[A-Z]{2}" + ext + "|" + ext + "|." + @"[a-z]{2}-[A-Z]{1}[a-z]{3}-[A-Z]{2}" + ext, "") + ext : fileName;
            return Path.Combine(filepath, defaultFileName);

        }
        /// <summary>
        /// Checks for default file path for given filepath.
        /// </summary>
        /// <param name="filepath">filepath</param>
        /// <returns>True for matched file path with new file path and default file name.</returns>
        public static bool IsDefaultFile(string filepath)
        {
            bool status = false;
            string fileName = Path.GetFileName(filepath);
            string newfilepath = filepath.Replace(fileName, "");
            string ext = Path.GetExtension(filepath);
            string defaultFileName = Regex.IsMatch(fileName, @".[a-z]{2}-[A-Z]{2}" + ext + "|" + ext + "|." + @"[a-z]{2}-[A-Z]{1}[a-z]{3}-[A-Z]{2}" + ext) ? Regex.Replace(fileName, @".[a-z]{2}-[A-Z]{2}" + ext + "|" + ext + "|." + @"[a-z]{2}-[A-Z]{1}[a-z]{3}-[A-Z]{2}" + ext, "") + ext : fileName;
            if(filepath==Path.Combine(newfilepath, defaultFileName))
            {
                status = true;
            }
            return status;
        }
    }
}
