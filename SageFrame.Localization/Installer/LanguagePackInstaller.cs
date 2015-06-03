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
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Collections;
using System.Xml.XPath;
using System.Xml;
using SageFrame.Modules;
using SageFrame.Web;
using SageFrame.Web.Utilities;
using RegisterModule;
#endregion



namespace SageFrame.Localization
{
    /// <summary>
    /// Localization language pack installer class.
    /// </summary>
    public class LanguagePackInstaller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileModule">Object of FileUpload class. </param>
        /// <returns></returns>
        public ArrayList Step0CheckLogic(FileUpload fileModule)
        {
            PackageInfo package = new PackageInfo();
            int ReturnValue = 0;
            try
            {
                //check for Empty entry
                if (fileModule.HasFile)
                {
                    //Check if valid Zip file submitted
                    if (IsVAlidZipContentType(fileModule.FileName))
                    {
                        string path = HttpContext.Current.Server.MapPath("~/");
                        string temPath = SageFrame.Common.RegisterModule.Common.TemporaryFolder;
                        string destPath = Path.Combine(path, temPath);
                        if (!Directory.Exists(destPath))
                            Directory.CreateDirectory(destPath);

                        string filePath = destPath + "\\" + fileModule.FileName;
                        fileModule.SaveAs(filePath);
                        string ExtractedPath = string.Empty;
                        ZipUtil.UnZipFiles(filePath, destPath, ref ExtractedPath, SageFrame.Common.RegisterModule.Common.Password, SageFrame.Common.RegisterModule.Common.RemoveZipFile);
                        package.TempFolderPath = ExtractedPath;
                        fileModule.FileContent.Dispose();
                        if (!string.IsNullOrEmpty(package.TempFolderPath) && Directory.Exists(package.TempFolderPath))
                        {
                            switch (Step1CheckLogic(package.TempFolderPath, package))
                            {
                                case 0://No manifest file
                                    DeleteTempDirectory(package.TempFolderPath);
                                    ReturnValue = 3;
                                    break;
                                case -1://Invalid Manifest file
                                    DeleteTempDirectory(package.TempFolderPath);
                                    ReturnValue = 4;
                                    break;
                                case 1://Already exist
                                    ReturnValue = 2;
                                    break;
                                case 2://Fresh Installation
                                    ReturnValue = 1;
                                    break;
                            }
                        }
                        else
                        {
                            ReturnValue = 0;
                        }
                    }
                    else
                    {
                        ReturnValue = -1;//"Invalid Zip file submited to upload!";
                    }
                }
                else
                {
                    ReturnValue = 0;// "No package file is submited to upload!";
                }
            }
            catch
            {
                ReturnValue = -1;
            }
            ArrayList arrColl = new ArrayList();
            arrColl.Add(ReturnValue);
            arrColl.Add(package);
            return arrColl;
        }
        /// <summary>
        /// Checks valid manifest file.
        /// </summary>
        /// <param name="root">Object of XmlElement.</param>
        /// <param name="package">Object of PackageInfo. </param>
        /// <returns>Returns true for sageframe</returns>
        public bool checkValidManifestFile(XmlElement root, PackageInfo package)
        {
            if (root.Name == "sageframe")
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// Checks for language exists.
        /// </summary>
        /// <param name="moduleName">moduleName</param>
        /// <param name="package">Object of PackageInfo class. </param>
        /// <returns>False</returns>
        public bool IsLanguageExists(string moduleName, PackageInfo package)
        {

            return false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="TempUnzippedPath">TempUnzippedPath</param>
        /// <param name="package">Object of PackageInfo</param>
        /// <returns></returns>
        public int Step1CheckLogic(string TempUnzippedPath, PackageInfo package)
        {
            if (checkFormanifestFile(TempUnzippedPath, package) != "")
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(TempUnzippedPath + '\\' + package.ManifestFile);
                XmlElement root = doc.DocumentElement;
                if (checkValidManifestFile(root, package))
                {
                    XmlNodeList xnList = doc.SelectNodes("sageframe/packages");
                    foreach (XmlNode xn in xnList)
                    {
                        package.PackageName = xn["package"].Attributes["name"].InnerXml.ToString();

                        if (!String.IsNullOrEmpty(package.PackageName) && IsLanguageExists(package.PackageName.ToLower(), package))
                        {
                            string path = HttpContext.Current.Server.MapPath("~/");
                            string targetPath = path + SageFrame.Common.RegisterModule.Common.ModuleFolder + '\\' + package.FolderName;
                            package.InstalledFolderPath = targetPath;
                            return 1;//Already exist
                        }
                        else
                        {
                            return 2;//Not Exists
                        }
                    }
                }
                else
                {
                    return -1;//Invalid Manifest file
                }
            }
            return 0;//No manifest file
        }
        /// <summary>
        /// Checks for .sfe.
        /// </summary>
        /// <param name="TempUnzippedPath">TempUnzippedPath</param>
        /// <param name="package">Object of PackageInfo. </param>
        /// <returns>Manifest file.</returns>
        public string checkFormanifestFile(string TempUnzippedPath, PackageInfo package)
        {
            DirectoryInfo dir = new DirectoryInfo(TempUnzippedPath);
            foreach (FileInfo f in dir.GetFiles("*.*"))
            {
                if (f.Extension.ToLower() == ".sfe")
                {
                    package.ManifestFile = f.Name;
                    return package.ManifestFile;
                }
                else
                {
                    package.ManifestFile = "";
                }
            }
            return package.ManifestFile;
        }
        /// <summary>
        /// Checks valid zip.
        /// </summary>
        /// <param name="p"></param>
        /// <returns>Returns true if file extension is valid for uploaded file.</returns>
        private bool IsVAlidZipContentType(string p)
        {
            // extract and store the file extension into another variable
            String fileExtension = p.Substring(p.Length - 3, 3);
            // array of allowed file type extensions
            string[] validFileExtensions = { "zip" };
            var flag = false;
            // loop over the valid file extensions to compare them with uploaded file
            for (var index = 0; index < validFileExtensions.Length; index++)
            {
                if (fileExtension.ToLower() == validFileExtensions[index].ToString().ToLower())
                {
                    flag = true;
                }
            }
            return flag;
        }
        /// <summary>
        /// RollBack module installation if error occur during  installation.
        /// </summary>
        /// <param name="ModuleID">ModuleID</param>
        /// <param name="PortalID">PortalID</param>
        public void ModulesRollBack(int ModuleID, int PortalID)
        {
            try
            {
                SQLHandler objSQL = new SQLHandler();
                objSQL.ModulesRollBack(ModuleID, PortalID);
            }
            catch
            {
                //ProcessException(e);
            }
        }
        /// <summary>
        /// Fills package information.
        /// </summary>
        /// <param name="package">Object of PackageInfo class.</param>
        /// <returns>Object of PackageInfo.</returns>
        public PackageInfo fillPackageInfo(PackageInfo package)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(package.TempFolderPath + '\\' + package.ManifestFile);
            XmlElement root = doc.DocumentElement;
            if (!String.IsNullOrEmpty(root.ToString()))
            {
                XmlNodeList xnList = doc.SelectNodes("sageframe/packages/package");
                foreach (XmlNode xn in xnList)
                {
                    package.PackageType = xn.Attributes["type"].InnerText.ToString();
                    package.FriendlyName = xn["friendlyname"].InnerXml.ToString();
                    package.Description = xn["description"].InnerXml.ToString();
                    package.Version = xn.Attributes["version"].InnerText.ToString();
                    package.OwnerName = xn["owner"].ChildNodes[0].InnerXml.ToString();
                    package.Organistaion = xn["owner"].ChildNodes[1].InnerXml.ToString();
                    package.URL = xn["owner"].ChildNodes[2].InnerXml.ToString();
                    package.Email = xn["owner"].ChildNodes[3].InnerXml.ToString();
                    package.ReleaseNotes = xn["releasenotes"].InnerXml.ToString();
                    package.License = xn["license"].InnerXml.ToString();
                }
            }
            return package;
        }
        /// <summary>
        /// Package installation.
        /// </summary>
        /// <param name="package">Object of PackageInfo.</param>
        /// <param name="destinationpath">Destinationpath</param>
        /// <param name="isOverWrite">IsOverWrite</param>
        /// <param name="selectedModules">List of object of FileDetails.</param>
        public void InstallPackage(PackageInfo package, string destinationpath, bool isOverWrite, List<FileDetails> selectedModules)
        {
            XmlDocument doc = new XmlDocument();
            ArrayList dllFiles = new ArrayList();

            doc.Load(package.TempFolderPath + '\\' + package.ManifestFile);
            XmlElement root = doc.DocumentElement;
            if (!String.IsNullOrEmpty(root.ToString()))
            {
                XmlNodeList xnList = doc.SelectNodes("sageframe/packages/package/components/component/languagefiles/languagefile");
                foreach (XmlNode xn in xnList)
                {
                    string dir = xn["path"].InnerText.ToString();
                    bool isExist = selectedModules.Exists(
                        delegate(FileDetails fd)
                        {
                            return (dir.Contains(fd.FilePath));
                        }

                        );
                    if (isExist)
                    {
                        string sourcefile = package.TempFolderPath + "\\" + dir + "\\" + xn["name"].InnerText.ToString();

                        if (Directory.Exists(destinationpath + "/" + dir))
                        {
                            if (!File.Exists(destinationpath + "/" + dir + "\\" + xn["name"].InnerText.ToString()))
                            {
                                File.Copy(sourcefile, destinationpath + "/" + dir + "\\" + xn["name"].InnerText.ToString());
                            }
                            else if (isOverWrite)
                            {
                                File.Delete(destinationpath + "/" + dir + "\\" + xn["name"].InnerText.ToString());
                                File.Copy(sourcefile, destinationpath + "/" + dir + "\\" + xn["name"].InnerText.ToString());
                            }
                        }
                        else if (!Directory.Exists(destinationpath + "/" + dir))
                        {
                            Directory.CreateDirectory(destinationpath + "/" + dir);
                            File.Copy(sourcefile, destinationpath + "/" + dir + "\\" + xn["name"].InnerText.ToString());
                        }

                    }
                }
            }

            DeleteTempDirectory(package.TempFolderPath);
        }
        /// <summary>
        /// Compares existing files.
        /// </summary>
        /// <param name="package">Object of PackageInfo. </param>
        /// <param name="destinationpath">Destinationpath</param>
        /// <returns>List of FileDetails. </returns>
        public static List<FileDetails> CompareExistingFiles(PackageInfo package, string destinationpath)
        {
            XmlDocument doc = new XmlDocument();
            ArrayList dllFiles = new ArrayList();
            List<FileDetails> lstFiles = new List<FileDetails>();
            doc.Load(package.TempFolderPath + '\\' + package.ManifestFile);
            XmlElement root = doc.DocumentElement;
            if (!String.IsNullOrEmpty(root.ToString()))
            {
                XmlNodeList xnList = doc.SelectNodes("sageframe/packages/package/components/component/languagefiles/languagefile");
                foreach (XmlNode xn in xnList)
                {
                    string dir = xn["path"].InnerText.ToString();
                    string sourcefile = package.TempFolderPath + "\\" + dir + "\\" + xn["name"].InnerText.ToString();

                    FileDetails obj = new FileDetails();
                    obj.FilePath = dir + "\\" + xn["name"].InnerText.ToString();

                    if (File.Exists(destinationpath + "/" + dir + "\\" + xn["name"].InnerText.ToString()))
                    {
                        obj.IsExists = true;

                    }
                    else
                    {
                        obj.IsExists = false;

                    }
                    lstFiles.Add(obj);


                }
            }
            return lstFiles;
        }
        /// <summary>
        /// Deletes temporary directory.
        /// </summary>
        /// <param name="TempDirectory">TempDirectory</param>
        public void DeleteTempDirectory(string TempDirectory)
        {
            try
            {
                if (!string.IsNullOrEmpty(TempDirectory))
                {
                    if (Directory.Exists(TempDirectory))
                        Directory.Delete(TempDirectory, true);
                }
            }
            catch (IOException ex)
            {
                throw ex;//cant delete folder
            }
        }
    }
}

