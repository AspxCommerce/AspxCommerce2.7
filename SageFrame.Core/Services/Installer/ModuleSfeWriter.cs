/*
SageFrame® - http://www.sageframe.com
Copyright (c) 2009-2012 by SageFrame
Permission is hereby granted, free of charge, to any person obtaining
a copy of this software and associated documentation files (the
"Software"), to deal in the Software without restriction, including
without limitation the rights to use, copy, modify, merge, publish,
distribute, sublicense, and/or sell copies of the Software, and to
permit persons to whom the Software is furnished to do so, subject to
the following conditions:

The above copyright notice and this permission notice shall be
included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using SageFrame.Web;
using RegisterModule;
using System.Web.Hosting;
using SageFrame.SageFrameClass.Services;
using System.Web;

/// <summary>
/// Helper class for module's SFE file.
/// </summary>

[Serializable]
public class ModuleSfeWriter : BaseAdministrationUserControl
{
    private ModuleInfoPackage _module;

    #region "Constructors"

    /// <summary>
    /// Initializes an instance of ModuleSfeWriter class.
    /// </summary>
    protected ModuleSfeWriter()
    {
    }


    /// <summary>
    /// Initializes an instance of ModuleSfeWriter class.
    /// </summary>
    /// <param name="module">ModuleInfoPackage object.</param>
    public ModuleSfeWriter(ModuleInfoPackage module)
    {
        _module = module;

    }
    #endregion

    #region "Public Properties"


    /// <summary>
    /// Gets or sets temporary folder name.
    /// </summary>
    public string TempFolderPath { get; set; }
    public ModuleInfoPackage Module
    {
        get { return _module; }
        set { _module = value; }
    }
    #endregion


    /// <summary>
    /// Creates package from the  given file list and manifest.
    /// </summary>
    /// <param name="archiveName">Archive name.</param>
    /// <param name="manifestName">Manifest name.</param>
    /// <param name="FileList">File list.</param>
    /// <param name="Response">HttpResponse object response.</param>
    /// <param name="SFEPath">SFE file path.</param>
    /// <param name="package">ModuleInfoPackage objcet conatining package details.</param>
    /// <param name="strFiles">Files to move.</param>
    public void CreatePackage(string archiveName, string manifestName, List<string> FileList, HttpResponse Response, string SFEPath, ModuleInfoPackage package)
    {

        WriteManifest(manifestName, SFEPath, package);
        CreateZipResponse(FileList, Response, Module.FolderName, SFEPath);
    }


    /// <summary>
    /// Adds file to the module zip.
    /// </summary>
    /// <param name="manifestName">Manifest name.</param>
    private void AddFile(string manifestName)
    {
        string sourcepath = Path.Combine(HostingEnvironment.ApplicationPhysicalPath, "Modules");
        DirectoryInfo dir = new DirectoryInfo(sourcepath);

        foreach (string fileName in _module.FileNames)
        {
            DirectoryInfo[] sourcedir = dir.GetDirectories(fileName, SearchOption.AllDirectories);
            foreach (DirectoryInfo dirs in sourcedir)
            {
                Installers installhelp = new Installers();
                string ZipPath = Path.Combine(TempFolderPath, dirs.Name + ".zip");

                ZipUtil.ZipFiles(dirs.FullName, ZipPath, string.Empty);
            }
        }

    }

    /// <summary>
    /// Creates zip from list of zip file  into one.
    /// </summary>
    /// <param name="ZipFileList">List if string of zip name.</param>
    /// <param name="Response">HttpResponse object response</param>
    /// <param name="FolderName">Output zip name.</param>
    /// <param name="TempFolder">Folder name containing the zips.</param>
    public void CreateZipResponse(List<string> ZipFileList, HttpResponse Response, string FolderName, string TempFolder)
    {
        try
        {
            ZipUtil.CreateZipResponse(ZipFileList, Response, FolderName, TempFolder);
        }
        catch { }
    }


    /// <summary>
    /// Creates zip file into the temporary folder.
    /// </summary>
    /// <param name="archiveName">Archive file name.</param>
    public void CreateZipFile(string archiveName)
    {
        string path = Path.Combine(HostingEnvironment.ApplicationPhysicalPath, "Resources\\temp\\TempResources");
        path = Path.Combine(path, "NewPackage");
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        string ZipPath = Path.Combine(path, archiveName + ".zip");

        ZipUtil.ZipFiles(TempFolderPath, ZipPath, string.Empty);
        System.IO.FileInfo fileInfo = new System.IO.FileInfo(ZipPath);


    }


    /// <summary>
    /// Writes manifest nodes and value from package details.
    /// </summary>
    /// <param name="manifestName">Manifest name.</param>
    /// <param name="SFEPath">SFE file path.</param>
    /// <param name="package">ModuleInfoPackage object conataining module package information.</param>

    public void WriteManifest(string manifestName, string SFEPath, ModuleInfoPackage package)
    {
        string manifestfile = manifestName + ".SFE";// "sfe_" +
        string manifestPath = Path.Combine(SFEPath, manifestfile);

        XmlWriterSettings settings = new XmlWriterSettings();
        settings.Indent = true;
        settings.OmitXmlDeclaration = true;
        settings.NewLineOnAttributes = true;

        XmlWriter writer = XmlWriter.Create(manifestPath, settings);

        WriteManifestStartElement(writer);

        WritePackageStartElement(writer);
        WriteModuleElements(writer);
        WriteFilesInfo(writer);

        WriteManifestEndElement(writer);
        // WriteManifestEndElement(writer);

        //Close Writer
        writer.Close();
        package.FileNames.Add(manifestPath);

    }

    /// <summary>
    /// Writes  modules elements from module details.
    /// </summary>
    /// <param name="writer">XmlWriter object containing module name.</param>
    private void WriteModuleElements(XmlWriter writer)
    {
        writer.WriteStartElement("modules");
        WriteModuleElement(writer);
        writer.WriteEndElement();
    }

    /// <summary>
    /// Writes package end elements for any xmlwriter.
    /// </summary>
    /// <param name="writer">XMLWriter object inwhich the </param>
    private void WritePackageEndElement(XmlWriter writer)
    {

        writer.WriteEndElement();


        writer.WriteEndElement();
    }


    /// <summary>
    /// Writes  package start elements.
    /// </summary>
    /// <param name="writer">XmlWriter object where the start of the element is to be insert.</param>
    private void WritePackageStartElement(XmlWriter writer)
    {
        //Start package Element
        writer.WriteStartElement("folder");
        writer.WriteElementString("name", Module.FriendlyName);
        writer.WriteElementString("friendlyname", Module.FriendlyName);
        writer.WriteElementString("foldername", Module.FolderName);
        writer.WriteElementString("modulename", Module.ModuleName);
        writer.WriteElementString("description", Module.Description);
        writer.WriteElementString("version", Module.Version);
        writer.WriteElementString("businesscontrollerclass", Module.BusinessControllerClass);
        writer.WriteElementString("compatibleversions", Module.CompatibleVersions);
        writer.WriteElementString("owner", Module.Owner);
        writer.WriteElementString("organization", Module.Organization);
        writer.WriteElementString("url", Module.URL);
        writer.WriteElementString("email", Module.Email);
        writer.WriteElementString("releasenotes", Module.ReleaseNotes);
        writer.WriteElementString("license", Module.License);
    }

    /// <summary>
    /// Writes module elements for the given XmlWriter object.
    /// </summary>
    /// <param name="writer">XmlWriter object where the start of the element is to be insert.</param>
    private void WriteModuleElement(XmlWriter writer)
    {
        writer.WriteStartElement("module");

        foreach (ModuleElement moduleElement in Module.ModuleElements)
        {
            writer.WriteElementString("friendlyname", moduleElement.FriendlyName);
            writer.WriteElementString("cachetime", moduleElement.CacheTime);

            writer.WriteStartElement("controls");
            foreach (ControlInfo controlInfo in moduleElement.Controls)
            {
                WriteControlElement(writer, controlInfo);
            }
            writer.WriteEndElement();
        }

        writer.WriteEndElement();

    }


    /// <summary>
    /// Writes control elements for the given XmlWriter object.
    /// </summary>
    /// <param name="writer">XmlWriter object where the start of the element is to be insert.</param>
    /// <param name="controlInfo">ControlInfo object containing module control details.</param>
    private void WriteControlElement(XmlWriter writer, ControlInfo controlInfo)
    {
        writer.WriteStartElement("control");
        writer.WriteElementString("key", controlInfo.Key);
        writer.WriteElementString("title", controlInfo.Title);
        writer.WriteElementString("src", controlInfo.Src);
        writer.WriteElementString("type", controlInfo.Type);
        writer.WriteElementString("helpurl", controlInfo.HelpUrl);
        writer.WriteElementString("supportspartialrendering", controlInfo.SupportSpatial);
        writer.WriteEndElement();
    }


    /// <summary>
    /// Writes Files info for the input  XmlWriter object.
    /// </summary>
    /// <param name="writer">XmlWriter object where the start of the element is to be insert.</param>
    private void WriteFilesInfo(XmlWriter writer)
    {
        writer.WriteStartElement("files");
        foreach (string fileName in Module.FileNames)
        {
            if (!string.IsNullOrEmpty(fileName) && (fileName.EndsWith(".dll") || fileName.EndsWith(".SqlDataProvider")))
            {
                writer.WriteStartElement("file");
                writer.WriteElementString("name", fileName.Substring(fileName.LastIndexOf("\\") + 1));
                writer.WriteEndElement();
            }
        }
        writer.WriteEndElement();
    }


    /// <summary>
    /// writes manifest end element for the input  XmlWriter object.
    /// </summary>
    /// <param name="writer">XmlWriter object where the start of the element is to be insert.</param>
    public static void WriteManifestEndElement(XmlWriter writer)
    {


        writer.WriteEndElement();

        writer.WriteEndElement();

        //Close root Element
        writer.WriteEndElement();

    }


    /// <summary>
    /// Writes manifest start element for the input  XmlWriter object.
    /// </summary>
    /// <param name="writer">XmlWriter object where the start of the element is to be insert.</param>
    public static void WriteManifestStartElement(XmlWriter writer)
    {
        //Start the new Root Element
        writer.WriteStartElement("sageframe");
        writer.WriteAttributeString("version", "1.0.0.0");
        writer.WriteAttributeString("type", "Module");

        //Start packages Element
        writer.WriteStartElement("folders");

    }
}

