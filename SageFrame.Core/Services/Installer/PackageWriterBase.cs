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
using System.Xml;

#endregion


namespace SageFrame.Core.Services.Installer
{
    /// <summary>
    /// Class that contains properties and methods that are used to  create composite package.
    /// </summary>
    public class PackageWriterBase
    {
        private CompositeModule _Package;

        #region "Constructors"

        /// <summary>
        /// Initializes an instance of PackageWriterBase class.
        /// </summary>
        protected PackageWriterBase()
        {
        }

        /// <summary>
        /// Initializes an instance of PackageWriterBase class.
        /// </summary>
        /// <param name="package">CompositeModule object.</param>
        public PackageWriterBase(CompositeModule package)
        {
            _Package = package;
        }
        #endregion

        #region Public Properties

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
        /// Writes package end element.
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
            writer.WriteStartElement("package");

            writer.WriteAttributeString("name", Package.Name);
            writer.WriteAttributeString("type", Package.PackageType);
            writer.WriteAttributeString("version", Package.Version.ToString());

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
            writer.WriteElementString("releaseNotes", Package.ReleaseNotes);

            //Write Dependencies
            //if (Dependencies.Count > 0)
            //{
            //    writer.WriteStartElement("dependencies");
            //    foreach (KeyValuePair<string, string> kvp in Dependencies)
            //    {
            //        writer.WriteStartElement("dependency");
            //        writer.WriteAttributeString("type", kvp.Key);
            //        writer.WriteString(kvp.Value);
            //        writer.WriteEndElement();
            //    }
            //    writer.WriteEndElement();
            //}

            //Write components Element
            writer.WriteStartElement("components");
        }

        /// <summary>
        /// Writes composite sfe file for the given XmlWriter object.
        /// </summary>
        /// <param name="writer">XmlWriter object where the start of the element is to be insert.</param>
        private void WriteCompositesfeFile(XmlWriter writer)
        {
            //Start package Element
            writer.WriteStartElement("package");

            writer.WriteAttributeString("name", Package.Name);
            writer.WriteAttributeString("type", Package.PackageType);
            writer.WriteAttributeString("version", Package.Version.ToString());

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
            writer.WriteElementString("releaseNotes", Package.ReleaseNotes);

            //Write Dependencies
            //if (Dependencies.Count > 0)
            //{
            //    writer.WriteStartElement("dependencies");
            //    foreach (KeyValuePair<string, string> kvp in Dependencies)
            //    {
            //        writer.WriteStartElement("dependency");
            //        writer.WriteAttributeString("type", kvp.Key);
            //        writer.WriteString(kvp.Value);
            //        writer.WriteEndElement();
            //    }
            //    writer.WriteEndElement();
            //}

            //Write components Element
            writer.WriteStartElement("components");
        }

        //protected virtual void WriteFilesToManifest(XmlWriter writer)
        //{
        //    var fileWriter = new FileComponentWriter(BasePath, Files, Package);
        //    fileWriter.WriteManifest(writer);
        //}

        //protected virtual void WriteManifestComponent(XmlWriter writer)
        //{
        //}
        //public void CreatePackage(string archiveName, string manifestName, string manifest, bool createManifest)
        //{
        //    if (createManifest)
        //    {
        //        WriteManifest(manifestName, manifest);
        //    }
        //    AddFile(manifestName);
        //    CreateZipFile(archiveName);
        //}
        //public void WriteManifest(XmlWriter writer, string manifest)
        //{
        //    WriteManifestStartElement(writer);
        //    writer.WriteRaw(manifest);

        //    //Close Dotnetnuke Element
        //    WriteManifestEndElement(writer);

        //    //Close Writer
        //    writer.Close();
        //}

        ///// <summary>
        ///// WriteManifest writes the manifest assoicated with this PackageWriter to a string
        ///// </summary>
        ///// <param name="packageFragment">A flag that indicates whether to return the package element
        ///// as a fragment (True) or whether to add the outer dotnetnuke and packages elements (False)</param>
        ///// <returns>The manifest as a string</returns>
        ///// <remarks></remarks>
        //public string WriteManifest(bool packageFragment)
        //{
        //    //Create a writer to create the processed manifest
        //    var sb = new StringBuilder();
        //    XmlWriter writer = XmlWriter.Create(sb, XmlUtils.GetXmlWriterSettings(ConformanceLevel.Fragment));

        //    WriteManifest(writer, packageFragment);

        //    //Close XmlWriter
        //    writer.Close();

        //    //Return new manifest
        //    return sb.ToString();
        //}

        //public void WriteManifest(XmlWriter writer, bool packageFragment)
        //{
        //    Log.StartJob(Util.WRITER_CreatingManifest);

        //    if (!packageFragment)
        //    {
        //        //Start dotnetnuke element
        //        WriteManifestStartElement(writer);
        //    }

        //    //Start package Element
        //    WritePackageStartElement(writer);

        //    //write Script Component
        //    if (Scripts.Count > 0)
        //    {
        //        var scriptWriter = new ScriptComponentWriter(BasePath, Scripts, Package);
        //        scriptWriter.WriteManifest(writer);
        //    }

        //    //write Clean Up Files Component
        //    if (CleanUpFiles.Count > 0)
        //    {
        //        var cleanupFileWriter = new CleanupComponentWriter(BasePath, CleanUpFiles);
        //        cleanupFileWriter.WriteManifest(writer);
        //    }

        //    //Write the Custom Component
        //    WriteManifestComponent(writer);

        //    //Write Assemblies Component
        //    if (Assemblies.Count > 0)
        //    {
        //        var assemblyWriter = new AssemblyComponentWriter(AssemblyPath, Assemblies, Package);
        //        assemblyWriter.WriteManifest(writer);
        //    }

        //    //Write AppCode Files Component
        //    if (AppCodeFiles.Count > 0)
        //    {
        //        var fileWriter = new FileComponentWriter(AppCodePath, AppCodeFiles, Package);
        //        fileWriter.WriteManifest(writer);
        //    }

        //    //write Files Component
        //    if (Files.Count > 0)
        //    {
        //        WriteFilesToManifest(writer);
        //    }

        //    //write ResourceFiles Component
        //    if (Resources.Count > 0)
        //    {
        //        var fileWriter = new ResourceFileComponentWriter(BasePath, Resources, Package);
        //        fileWriter.WriteManifest(writer);
        //    }

        //    //Close Package
        //    WritePackageEndElement(writer);

        //    if (!packageFragment)
        //    {
        //        //Close Dotnetnuke Element
        //        WriteManifestEndElement(writer);
        //    }
        //    Log.EndJob(Util.WRITER_CreatedManifest);
        //}

        //public static void WriteManifestEndElement(XmlWriter writer)
        //{
        //    //Close packages Element
        //    writer.WriteEndElement();

        //    //Close root Element
        //    writer.WriteEndElement();
        //}

        //public static void WriteManifestStartElement(XmlWriter writer)
        //{
        //    //Start the new Root Element
        //    writer.WriteStartElement("dotnetnuke");
        //    writer.WriteAttributeString("type", "Package");
        //    writer.WriteAttributeString("version", "5.0");

        //    //Start packages Element
        //    writer.WriteStartElement("packages");
        //}
    }
}
