#region "Copyright"
/*
FOR FURTHER DETAILS ABOUT LICENSING, PLEASE VISIT "LICENSE.txt" INSIDE THE SAGEFRAME FOLDER
*/
#endregion

#region  "References"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
#endregion

namespace SageFrame.Templating.xmlparser
{
    /// <summary>
    /// Class that contains properties for XML helper.
    /// </summary>
    public class XmlHelper
    {
        /// <summary>
        /// loads xml documents from the path provided.
        /// </summary>
        /// <param name="filePath">File path for the XML.</param>
        /// <returns>XmlDocument object containing the XML file's string in memory.</returns>
        public static XmlDocument LoadXMLDocument(string filePath)
        {
            XmlTextReader reader = new XmlTextReader(filePath);
            XmlDocument xmlDoc = new XmlDocument();
            try
            {
                reader.WhitespaceHandling = WhitespaceHandling.None;
                //Load the file into the XmlDocument
                xmlDoc.Load(reader);
                //Close off the connection to the file.
                reader.Close();
                //Add and item representing the document to the listbox

            }
            catch (Exception)
            {

                reader.Close();
            }
            return xmlDoc;
        }

        /// <summary>
        /// Returns XML nodes from XML documents from the selected node.
        /// </summary>
        /// <param name="doc">XmlDocument object containing the xml document.</param>
        /// <param name="selectednode">Node to be selected.</param>
        /// <returns>XML nodes list inside the provided node.</returns>
        public static XmlNodeList GetXMLNodes(XmlDocument doc, string selectednode)
        {
            XmlNodeList xnLst = doc.SelectNodes(selectednode);
            return xnLst;
        }

        /// <summary>
        /// Returns XML in string format.
        /// </summary>
        /// <param name="filePath">XML file path.</param>
        /// <returns>String containing xml string.</returns>
        public static string GetXMLString(string filePath)
        {

            StreamReader sr = null;
            string xml = null;
            try
            {
                sr = new StreamReader(filePath);
                xml = sr.ReadToEnd();
            }
            finally
            {
                if (sr != null)
                {
                    sr.Close();
                    sr = null;
                }
            }
            return xml;
        }
    }
}
