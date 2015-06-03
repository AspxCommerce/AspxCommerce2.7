/*
FOR FURTHER DETAILS ABOUT LICENSING, PLEASE VISIT "LICENSE.txt" INSIDE THE SAGEFRAME FOLDER
*/
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Web.Configuration;
using System.Web.Hosting;
using RegisterModule;
using System.IO;
using System.Xml;
using SageFrame.Web;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;
using System.Collections.Specialized;
using Microsoft.SqlServer.Management.Sdk.Sfc;
using System.ComponentModel;
using SageFrame.Utilities;
public partial class Modules_Upgrade_SageframeUpgrade : BaseAdministrationUserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblErrorMsg.Text = "";
    }

    public void WriteToLog(string txt)
    {
        string path = HostingEnvironment.ApplicationPhysicalPath + "log.txt";
        object obj = new object();
        lock (obj)
        {
            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine(txt);
                sw.Dispose();
            }
        }
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        try
        {
            if (this.fuUpgrade.HasFile && this.fuUpgrade.FileName.ToLower().EndsWith(".zip"))
            {
                string fileName = this.fuUpgrade.FileName;
                long filesize = fuUpgrade.PostedFile.ContentLength;

                if (filesize < 1048000)
                {
                    string tempFolder = HostingEnvironment.ApplicationPhysicalPath + "Upgrade";
                    string savePath = tempFolder + "\\" + fileName;
                    if (!Directory.Exists(tempFolder))
                        Directory.CreateDirectory(tempFolder);
                    this.fuUpgrade.SaveAs(savePath);
                    string errorMessage = string.Empty;
                    if (!IsZipValid(savePath, ref errorMessage))
                    {
                        this.lblErrorMsg.Text = errorMessage;
                    }
                    else
                    {
                        Server.Transfer(ResolveUrl("~/Modules/Upgrade/upgrade.aspx?zip=" + fileName));
                    }
                }
                else
                {
                    this.lblErrorMsg.Text = "The zip file size exceed 10 MB";
                }
            }
            else
            {
                this.lblErrorMsg.Text = "Please select Zip File";
            }
        }
        catch (Exception ex)
        {
            ShowMessage("", ex.ToString(), "", SageMessageType.Error);
        }
    }

    /// <summary>
    /// Compares the version of the uploading zip and existing application version.
    /// </summary>
    /// <param name="configFilePath">Config file path.</param>
    /// <returns>True if the uploading version is higher then the current application version.</returns>
    public static bool IsNewVersion(string configFilePath)
    {
        bool flag = false;
        XmlDocument doc = new XmlDocument();
        doc.Load(configFilePath);
        XmlNode versionNode = doc.SelectSingleNode("/CONFIG/SAGEFRAME");
        string installerVersion = versionNode.Attributes["VERSION"].Value;
        string prevVersion = Config.GetSetting("SageFrameVersion");
        if (Convert.ToDecimal(installerVersion) >= Convert.ToDecimal(prevVersion))
        {
            flag = true;
        }
        return flag;
    }

    /// <summary>
    /// Compares the version of the zip being uploaded to the existing application. Also checks if the zip is valid upgrader zip.
    /// </summary>
    /// <param name="filePath"></param>
    /// <param name="errormessage"></param>
    /// <returns></returns>
    public static bool IsZipValid(string filePath, ref string errormessage)
    {
        bool IsValid = false;
        string outputFolder = Path.Combine(HostingEnvironment.ApplicationPhysicalPath, @"Upgrade\UploadedFiles");
        if (!Directory.Exists(outputFolder))
            Directory.CreateDirectory(outputFolder);
        string extractPath = string.Empty;
        string configFileName = "config.xml";
        bool isConfigFileFound = ZipUtil.UnZipConfigFile(filePath, outputFolder, ref extractPath, "", true, configFileName);
        if (isConfigFileFound)
        {
            IsValid = IsNewVersion(extractPath + @"\SystemFiles\" + configFileName);
            if (!IsValid)
            {
                errormessage = "Your site is already Upgraded with this Version!! Try Another version";
            }
        }
        else
        {
            errormessage = "This is not a valid upgrader zip";
        }
        return IsValid;
    }
}
