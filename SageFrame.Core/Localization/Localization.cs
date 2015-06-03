#region "Copyright"

/*
FOR FURTHER DETAILS ABOUT LICENSING, PLEASE VISIT "LICENSE.txt" INSIDE THE SAGEFRAME FOLDER
*/
#endregion

#region "References"

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Specialized;
using System.IO;
using System.Xml; 

#endregion

namespace SageFrame.Localization
{
    /// <summary>
    /// Contains localization default methods and properties.
    /// </summary>
    public class Localization
    {
        /// <summary>
        /// Initializes a new instance of the Localization class.
        /// </summary>
        public Localization()
        {
        }

        /// <summary>
        /// Returns  the path for application resource directory.
        /// </summary>
        public static string ApplicationResourceDirectory
        {
            get
            {
                return "~/XMLMessage";
            }
        }

        /// <summary>
        /// Returns default application culture.
        /// </summary>
        public static string SystemLocale
        {
            get
            {
                return "en-US";
            }
        }
        /// <summary>
        /// Returns default timezone  file path.
        /// </summary>
        public static string TimezonesFile
        {
            get
            {
                return (ApplicationResourceDirectory + "/TimeZones.xml");
            }
        }

        /// <summary>
        /// Returns timezone collection.
        /// </summary>
        /// <param name="language">language</param>
        /// <returns>Collection of timezones</returns>
        public static NameValueCollection GetTimeZones(string language)
        {
            string cacheKey = ("sageframe-" + (language + "-timezones"));
            NameValueCollection timeZones= new NameValueCollection();
            string filePath = HttpContext.Current.Server.MapPath("~/XMLMessage/TimeZones." +language + ".xml");
            if ((File.Exists(filePath) == false))
            {
                filePath = HttpContext.Current.Server.MapPath("~/XMLMessage/TimeZones.en-US.xml");
            }
            try
            {
                XmlDocument d = new XmlDocument();
                d.Load(filePath);
                foreach (XmlNode n in d.SelectSingleNode("root").ChildNodes)
                {
                    if ((n.NodeType != XmlNodeType.Comment))
                    {
                        timeZones.Add(n.Attributes["name"].Value, n.Attributes["key"].Value);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return timeZones;
        }
    }
}
