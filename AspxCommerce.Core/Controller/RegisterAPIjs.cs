using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web;
using System.Xml;
using System.Web.Hosting;
using System.Collections;

namespace AspxCommerce.Core
{
    public class RegisterAPIjs
    {
        public void AddAPIjsOnInsatllation(XmlDocument doc, string tempFolderPath)
        {          
            CreateAndCopyAPIFolderjs(doc, tempFolderPath);
        }

        private void CreateAndCopyAPIFolderjs(XmlDocument doc,string tempFolderPath)
        {
            string desx = Path.Combine(HostingEnvironment.ApplicationPhysicalPath, "Modules");
            string desxx = Path.Combine(desx, "AspxCommerce");
            string destinationDirectory = Path.Combine(desxx, "AspxAPIJs");
        
            string sourceFolder = Path.Combine(tempFolderPath, "APIjs");

            string[] files = Directory.GetFiles(sourceFolder);

            if (!Directory.Exists(destinationDirectory))
            {
                Directory.CreateDirectory(destinationDirectory);
            }
            foreach (string s in files)
            {
                string fileName = Path.GetFileName(s);
                string destFile = Path.Combine(destinationDirectory, ParseFileNameWithoutPath(fileName));           
                if (!File.Exists(destFile))
                {
                    File.Copy(s, destFile, true);
                } 
            }                     

        }

        private string ParseFileNameWithoutPath(string path)
        {
            if (path != null && path != string.Empty)
            {
                char seperator = '\\';
                string[] file = path.Split(seperator);
                return file[file.Length - 1];
            }
            return string.Empty;
        }

        public void RemoveAPIjsOnUnstallation(XmlDocument doc)
        {
            ArrayList apijsArray = new ArrayList();
            XmlNodeList apijsList = doc.SelectNodes("sageframe/folders/folder/apijs/js");
            foreach (XmlNode item in apijsList)
            {
                apijsArray.Add(item.InnerXml);
            }

            string destPath = Path.Combine(HostingEnvironment.ApplicationPhysicalPath, "Modules");
            destPath = Path.Combine(destPath, "AspxCommerce");
            string destinationFolder = Path.Combine(destPath, "AspxAPIJs");

            foreach (string item in apijsArray)
            {
                string fileName = Path.Combine(destinationFolder, item);
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }
            }

            string[] isFolderEmpty = Directory.GetFiles(destinationFolder);

            if (!(isFolderEmpty.Count() > 0))
            {
                Directory.Delete(destinationFolder);
            }
        }
    }
}
