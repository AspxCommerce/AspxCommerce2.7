#region "Copyright"

/*
FOR FURTHER DETAILS ABOUT LICENSING, PLEASE VISIT "LICENSE.txt" INSIDE THE SAGEFRAME FOLDER
*/

#endregion

#region "References"

using System;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using SageFrame.Web;
using RegisterModule;
using System.Web;
using System.Web.Hosting;
using SageFrame.SageFrameClass.Services;

#endregion


namespace SageFrame.Core.Services.Installer
{
    /// <summary>
    /// Class that contains the methods and properties for SFE writer.
    /// </summary>
    public class SfeWriter : BaseAdministrationUserControl
    {
        private CompositeModule _Package;

        #region "Constructors"

        /// <summary>
        /// Initializes an instance of SfeWriter class.
        /// </summary>
        protected SfeWriter()
        {
        }

        /// <summary>
        /// Initializes an instance of SfeWriter class.
        /// </summary>
        /// <param name="package">CompositeModule object.</param>
        public SfeWriter(CompositeModule package)
        {
            _Package = package;

        }
        #endregion

        #region "Public Properties"

        /// <summary>
        /// Gets or sets temporary folder path.
        /// </summary>
        public string TempFolderPath { get; set; }

        /// <summary>
        /// Gets or sets zip path.
        /// </summary>
        public string ZipPath { get; set; }

        /// <summary>
        /// Gets or sets CompositeModule object.
        /// </summary>
        public CompositeModule Package
        {
            get
            {
                return _Package;
            }
            set
            {
                _Package = value;
            }
        }
        #endregion

        /// <summary>
        /// Creates package for the given manifest.
        /// </summary>
        /// <param name="archiveName">Archive name.</param>
        /// <param name="manifestName">Manifest name.</param>
        /// <param name="response">HttpResponse object.</param>
        /// <param name="tempFolderPath">Temporary folder path.</param>
        public void CreatePackage(string archiveName, string manifestName, HttpResponse response, string tempFolderPath)//, HttpResponse response
        {
            try
            {
                TempFolderPath = tempFolderPath;
                AddFile(manifestName);
                WriteManifest(manifestName);

            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
            CreateZipFile(archiveName, response);

        }

        /// <summary>
        /// Adds file form the manifest.
        /// </summary>
        /// <param name="manifestName">Manifest name.</param>
        private void AddFile(string manifestName)
        {
            string sourcepath = Path.Combine(HostingEnvironment.ApplicationPhysicalPath, "Modules");
            DirectoryInfo dir = new DirectoryInfo(sourcepath);
            foreach (Component component in Package.Components)
            {
                DirectoryInfo[] sourcedir = dir.GetDirectories(component.Name, SearchOption.AllDirectories);
                foreach (DirectoryInfo dirs in sourcedir)
                {
                    Installers installhelp = new Installers();
                    string ZipPath = Path.Combine(TempFolderPath, dirs.Name + ".zip");
                    ZipUtil.ZipFiles(dirs.FullName, ZipPath, string.Empty);
                    component.IsChecked = true;

                }
            }

        }

        /// <summary>
        /// Creates zip file from temporary folder.
        /// </summary>
        /// <param name="archiveName">Archive name.</param>
        /// <param name="Response">HttpResponse object.</param>
        public void CreateZipFile(string archiveName, HttpResponse Response)//, HttpResponse response
        {
            try
            {
                string path = TempFolderPath;
                ZipPath = Path.Combine(path, archiveName + ".zip");
                List<String> list = new List<string>();
                foreach (string fileName in Directory.GetFiles(TempFolderPath))
                {
                    list.Add(fileName);
                }

                ZipUtil.CreateZipResponse(list, Response, archiveName, TempFolderPath);
                //   ZipUtil.ZipFiles(TempFolderPath, ZipPath, string.Empty);

                //System.IO.FileInfo fileInfo = new System.IO.FileInfo(ZipPath);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            { //HttpContext.Current.Response.End();
            }


        }

        /// <summary>
        /// Writes the manifest file.
        /// </summary>
        /// <param name="manifestName">Manifest name.</param>
        public void WriteManifest(string manifestName)
        {
            string manifestfile = "sfe_" + manifestName;
            string manifestPath = Path.Combine(TempFolderPath, manifestfile);
            if (!manifestPath.EndsWith(".sfe"))
            {
                manifestPath = manifestPath + ".sfe";
            }
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.OmitXmlDeclaration = true;
            settings.NewLineOnAttributes = true;
            XmlWriter writer = XmlWriter.Create(manifestPath, settings);
            WriteManifestStartElement(writer);
            WritePackageStartElement(writer);
            WriteComponentStartElement(writer);
            //Close Dotnetnuke Element
            WriteManifestEndElement(writer);
            WriteManifestEndElement(writer);
            //Close Writer
            writer.Close();
        }



        /// <summary>
        /// Writes  package end element for the given XmlWriter object.
        /// </summary>
        /// <param name="writer">XmlWriter object where the start of the element is to be insert.</param>
        private void WritePackageEndElement(XmlWriter writer)
        {
            //Close components Element
            writer.WriteEndElement();

            //Close package Element
            writer.WriteEndElement();
        }

        /// <summary>
        /// Writes package start element for the given XmlWriter object.
        /// </summary>
        /// <param name="writer">XmlWriter object where the start of the element is to be insert.</param>
        private void WritePackageStartElement(XmlWriter writer)
        {
            //Start package Element
            writer.WriteStartElement("folder");
            writer.WriteElementString("name", Package.Name);
            writer.WriteElementString("foldername", Package.Name);

            writer.WriteElementString("type", Package.PackageType);
            writer.WriteElementString("version", Package.Version.ToString());

            //Write FriendlyName
            writer.WriteElementString("friendlyName", Package.FriendlyName);

            //Write Description
            writer.WriteElementString("description", Package.Description);



            writer.WriteElementString("owner", Package.Owner);
            writer.WriteElementString("organization", Package.Organization);
            writer.WriteElementString("url", Package.URL);
            writer.WriteElementString("email", Package.Email);


            //Write License
            writer.WriteElementString("license", Package.License);

            //Write Release Notes
            writer.WriteElementString("releasenotes", Package.ReleaseNotes);


            //Write components Element
            writer.WriteStartElement("modules");
        }

        /// <summary>
        /// Writes components start element for the given XmlWriter object.
        /// </summary>
        /// <param name="writer">XmlWriter object where the start of the element is to be insert.</param>
        private void WriteComponentStartElement(XmlWriter writer)
        {
            foreach (Component component in Package.Components)
            {
                if (component.IsChecked)
                {

                    writer.WriteStartElement("module");
                    writer.WriteElementString("name", component.Name);
                    writer.WriteElementString("friendlyname", component.FriendlyName);
                    writer.WriteElementString("description", component.Description);
                    writer.WriteElementString("version", component.Version.ToString());
                    writer.WriteElementString("businesscontrollerclass", component.BusinesscontrollerClass);
                    writer.WriteElementString("ZipFile", component.ZipFile);
                    writer.WriteEndElement();
                }
            }
        }

        /// <summary>
        ///  Writes manifest end element for the given XmlWriter object.
        /// </summary>
        /// <param name="writer">XmlWriter object where the start of the element is to be insert.</param>
        public static void WriteManifestEndElement(XmlWriter writer)
        {
            //Close packages Element
            writer.WriteEndElement();

            //Close root Element
            writer.WriteEndElement();

        }


        /// <summary>
        /// Writes manifest start element for the given XmlWriter object.
        /// </summary>
        /// <param name="writer">XmlWriter object where the start of the element is to be insert.</param>
        public static void WriteManifestStartElement(XmlWriter writer)
        {
            //Start the new Root Element
            writer.WriteStartElement("sageframe");
            writer.WriteAttributeString("version", "1.0.0.0");
            writer.WriteAttributeString("type", "module");

            //Start packages Element
            writer.WriteStartElement("folders");

        }
        /// <summary>
        /// Creates temporary folder.
        /// </summary>
        /// <param name="manifestName">Manifest name.</param>
        public void GetTempPath(string manifestName)
        {
            string path = Path.Combine(HostingEnvironment.ApplicationPhysicalPath, "Resources");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string folderPath = Path.Combine(path, "temp");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            TempFolderPath = Path.Combine(folderPath, manifestName);
            if (Directory.Exists(TempFolderPath))
            {
                Directory.Delete(TempFolderPath, true);
            }
            Directory.CreateDirectory(TempFolderPath);
        }
    }
}
