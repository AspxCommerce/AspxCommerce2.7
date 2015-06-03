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
using ICSharpCode.SharpZipLib;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Checksums;
using System.Xml;
using SageFrame.Common;
using SageFrame.Localization;
#endregion

/// <summary>
/// Package writer base.
/// </summary>
public class PackageWriterBase
    {
    /// <summary>
    /// Writes zip file.
    /// </summary>
        /// <param name="filesToZip"> List of objects of FileDetails class. </param>
        /// <param name="path">path</param>
        /// <param name="manifestPath">manifestPath</param>
        /// <param name="manifest">manifest</param>
        public static void WriteZipFile(List<FileDetails> filesToZip, string path,string manifestPath,string manifest )
        {
            int compression = 9;
            FileDetails fd = new FileDetails(manifest, manifestPath,manifestPath);
            filesToZip.Insert(0,fd);

            foreach (FileDetails obj in filesToZip)
                if (!File.Exists(obj.FilePath))
                    throw new ArgumentException(string.Format("The File {0} does not exist!", obj.FileName));

            Object _locker=new Object();
            lock(_locker)
            {
                Crc32 crc32 = new Crc32();
                ZipOutputStream stream = new ZipOutputStream(File.Create(path));
                stream.SetLevel(compression);
                for (int i = 0; i < filesToZip.Count; i++)
                {
                    ZipEntry entry = new ZipEntry(filesToZip[i].FolderInfo + "/" + filesToZip[i].FileName);
                    entry.DateTime = DateTime.Now;
                    if (i == 0)
                    {
                        entry = new ZipEntry(manifest);
                    }
                    using (FileStream fs = File.OpenRead(filesToZip[i].FilePath))
                    {
                        byte[] buffer = new byte[fs.Length];
                        fs.Read(buffer, 0, buffer.Length);
                        entry.Size = fs.Length;
                        fs.Close();
                        crc32.Reset();
                        crc32.Update(buffer);
                        entry.Crc = crc32.Value;
                        stream.PutNextEntry(entry);
                        stream.Write(buffer, 0, buffer.Length);
                    }
                }
                stream.Finish();
                stream.Close();
                DeleteManifest(manifestPath);
            }
           
        }
         /// <summary>
         /// Writes manifest.
         /// </summary>
        /// <param name="path">path</param>
        /// <param name="filename">filename</param>
        /// <param name="selectedResxFiles">List of object of FileDetails class. </param>
        /// <param name="package">Objects of PackageInfo class.</param>
        public static void WriteManifest(string path,string filename,List<FileDetails> selectedResxFiles,PackageInfo package)
        {
            XmlWriter writer = XmlWriter.Create(path + @"/" + filename + ".sfe",XMLUtils.GetXmlWriterSettings());
            // Write first element
            writer.WriteStartElement("sageframe");
            writer.WriteAttributeString("type", "Package");
            writer.WriteAttributeString("version", "1.0.0.1");
            writer.WriteStartElement("packages");
            WritePackageDetails(writer,selectedResxFiles,package);
            // close writer
            writer.Close();
           
        }
          /// <summary>
          /// Writes package details.
         /// </summary>
        /// <param name="writer">Object of XmlWriter class.</param>
        /// <param name="selectedResxFiles">selectedResxFiles</param>
        /// <param name="package">Object of PackageInfo class. </param>
        private static void WritePackageDetails(XmlWriter writer, List<FileDetails> selectedResxFiles,PackageInfo package)
        {
            writer.WriteStartElement("package");
            writer.WriteAttributeString("name", package.PackageName);
            writer.WriteAttributeString("type", package.PackageType);
            writer.WriteAttributeString("version", package.Version);
            writer.WriteElementString("friendlyname", package.FriendlyName);
            writer.WriteElementString("description", package.Description);
            writer.WriteStartElement("owner");
            writer.WriteElementString("name", package.OwnerName);
            writer.WriteElementString("organization",package.Organistaion);
            writer.WriteElementString("url","");
            writer.WriteElementString("email", package.Email);
            writer.WriteEndElement();
            writer.WriteElementString("license", package.License);
            writer.WriteElementString("releasenotes", package.ReleaseNotes);
            writer.WriteStartElement("components");
            writer.WriteStartElement("component");
            writer.WriteAttributeString("type", "CoreLanguage");
            writer.WriteStartElement("languagefiles");
            writer.WriteElementString("code","");
            writer.WriteElementString("displayname", "");
            writer.WriteElementString("fallback", "");           
           
            WritePackageResources(writer,selectedResxFiles);
            WritePackageEndElements(writer);
           

        }
    /// <summary>
    /// Writes package resources.
    /// </summary>
        /// <param name="writer">Object of XmlWriter class.</param>
        /// <param name="selectedResxFiles">List of objects of FileDetails class.</param>
        public static void WritePackageResources(XmlWriter writer, List<FileDetails> selectedResxFiles)
        {
            foreach (FileDetails fd in selectedResxFiles)
            {
                writer.WriteStartElement("languagefile");
                writer.WriteElementString("path", fd.FolderInfo);
                writer.WriteElementString("name", fd.FileName);
                writer.WriteEndElement();
            }
        }
    /// <summary>
    /// Writes package end element.
    /// </summary>
        /// <param name="writer">Object of XmlWriter. </param>
        public static void WritePackageEndElements(XmlWriter writer)
        {
           
            writer.WriteEndElement();
            writer.WriteEndElement();
            writer.WriteEndElement();
            writer.WriteEndElement();
        }
    /// <summary>
    /// Deletes manifest.
    /// </summary>
        /// <param name="manifest">manifest</param>
        public static void DeleteManifest(string manifest)
        {
            if(File.Exists(manifest))
            File.Delete(manifest);
        }
    }

