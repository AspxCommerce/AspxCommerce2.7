#region "Copyright"
/*
FOR FURTHER DETAILS ABOUT LICENSING, PLEASE VISIT "LICENSE.txt" INSIDE THE SAGEFRAME FOLDER
*/
#endregion

#region "References"
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Web.Hosting;
#endregion

namespace SageFrame.Common
{
    /// <summary>
    /// Helper class for XML writer.
    /// </summary>
    public class XMLUtils
    {
        /// <summary>
        /// Return new XML writer settings.
        /// </summary>
        XmlWriterSettings settings = new XmlWriterSettings();
        /// <returns>Object of XmlWriterSettings.</returns>
        public static XmlWriterSettings GetXmlWriterSettings()
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.OmitXmlDeclaration = true;
            settings.Indent = true;
            return settings;
        }       
    }
}
