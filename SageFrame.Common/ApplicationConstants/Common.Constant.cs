#region "Copyright"

/*
FOR FURTHER DETAILS ABOUT LICENSING, PLEASE VISIT "LICENSE.txt" INSIDE THE SAGEFRAME FOLDER
*/

#endregion

#region "References"

using System;
using System.Collections.Generic;
using System.Web;

#endregion


namespace SageFrame.Common.RegisterModule
{
    /// <summary>
    /// Define application veriables.
    /// </summary>
    public class Common
    {
        public static string TemporaryFolder = "Install\\Temp";
        public static string ModuleFolder = "Modules";
        public static string Password = "";
        public static bool RemoveZipFile = true;

        public static string LargeImagePath = "PageImages";
        public static string MediumImagePath = "PageImages\\mediumthumbs";
        public static string SmallImagePath = "PageImages\\smallthumbs";

        public static string DLLTargetPath = "bin";

        public static string TemporaryTemplateFolder = "TemplateDownloads";
    }
}
