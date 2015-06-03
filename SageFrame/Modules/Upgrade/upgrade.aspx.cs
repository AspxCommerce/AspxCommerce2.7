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
using System.Web.Services;
using SageFrame.Utilities;
using System.Threading;




[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 

[System.Web.Script.Services.ScriptService]
public partial class upgrade : System.Web.UI.Page
{
    public string appPath = "";
    public string imagePath = "";
    public string upgradingGif = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        string zipFile = Request.QueryString["zip"] as string;
        appPath = ResolveUrl("~/Modules/Upgrade/upgrade.aspx");
        if (!String.IsNullOrEmpty(zipFile))
        {
            ViewState["fileName"] = zipFile;
        }
        imagePath = Page.Request.Url.Scheme + "://" + Request.Url.Authority + (HttpContext.Current.Request.ApplicationPath != "/" ? HttpContext.Current.Request.ApplicationPath : "") + "/Modules/Upgrade/Image/upgrading-logo.jpg";
        upgradingGif = Page.Request.Url.Scheme + "://" + Request.Url.Authority + (HttpContext.Current.Request.ApplicationPath != "/" ? HttpContext.Current.Request.ApplicationPath : "") + "/Modules/Upgrade/Image/384.GIF";
    }

    [System.Web.Services.WebMethod(EnableSession = true)]
    [System.Web.Script.Services.ScriptMethod()]
    public static string Upgrade(string installerZipFile)
    {
        if (!String.IsNullOrEmpty(installerZipFile))
        {
            bool dbBackupFlag = false;
            string tempFolder = HostingEnvironment.ApplicationPhysicalPath + "Upgrade";
            string zipFilePath = tempFolder + "\\" + installerZipFile;
            string dbBackupPath = string.Empty;
            string extractPath = string.Empty;
            try
            {
                extractPath = UnZipFiles(zipFilePath, tempFolder);
            }
            catch (Exception e)
            {
                WriteToLog(e.Message);
            }
            XmlDocument doc = LoadXml(extractPath);
            CreateLogFile();
            WriteToLog("Putting site down......");
            UpdateSiteDownFile(true);
            SqlConnection connection = new SqlConnection(SystemSetting.SageFrameConnectionString);
            string backupFolder = HostingEnvironment.ApplicationPhysicalPath + "Resources\\BackUp";
            string dbbackupFolder = HostingEnvironment.ApplicationPhysicalPath + "Resources\\UpgradeBackUp";
            if (!Directory.Exists(backupFolder))
                Directory.CreateDirectory(backupFolder);
            if (!Directory.Exists(dbbackupFolder))
                Directory.CreateDirectory(dbbackupFolder);
            XmlNodeList nodeList = doc.SelectNodes("/CONFIG/FILES/FILE");
            if (nodeList != null && nodeList.Count > 0)
            {
                try
                {
                    WriteToLog("Backing up Files......");
                    //Bacups all the changing files.
                    ProcessFiles(nodeList, extractPath);
                    //Zips the backfiles.
                    ZipBackUpFiles();
                    dbBackupPath = Path.Combine(dbbackupFolder, (DateTime.Now.ToShortDateString().Replace("/", "_") + DateTime.Now.Millisecond) + ".bak");
                    WriteToLog("Backing up database......");
                    dbBackupFlag = BackupDatabase(SystemSetting.SageFrameDBName, dbBackupPath, connection);
                    WriteToLog("Executing SQL scripts......");
                    ExecuteSqlScript(extractPath);
                    //extractPath + @"\SystemFiles\" + configFileName
                    try
                    {
                        System.GC.Collect();
                        System.GC.WaitForPendingFinalizers();
                        //if (File.Exists(dbBackupPath))
                        //{
                        //    File.SetAttributes(dbBackupPath, FileAttributes.Archive);
                        //    File.Delete(dbBackupPath);
                        //}
                        if (Directory.Exists(backupFolder))
                            Directory.Delete(backupFolder, true);
                    }
                    catch (Exception e)
                    {
                        WriteToLog("Cannot delete :" + e.Message);
                    }

                }
                catch (Exception e)
                {
                    WriteToLog("Error Occured during upgrading process.");
                    WriteToLog("Please check Log.txt file on root directory.");
                    WriteToLog("Error Detail:");
                    WriteToLog(e.Message);
                    RollBackAllFiles(nodeList);
                    if (dbBackupFlag)
                    {
                        RollBackDatabase(SystemSetting.SageFrameDBName, dbBackupPath);//, connection
                    }
                }
                finally
                {
                    try
                    {
                        string configFilePath = Path.Combine(extractPath, @"SystemFiles\config.xml");
                        FileInfo file = new FileInfo(configFilePath);
                        bool _FileUse = IsFileLocked(file);
                        while (_FileUse)
                        {
                            try
                            {
                                Thread.Sleep(1000);
                                _FileUse = IsFileLocked(file);
                            }
                            catch (Exception)
                            {
                                Thread.Sleep(1000);
                                _FileUse = false;
                            }
                        }

                        //deleting zip and its extraction folder
                        if (!string.IsNullOrEmpty(tempFolder) && Directory.Exists(tempFolder))
                            Directory.Delete(tempFolder, true);
                    }
                    catch (Exception e)
                    {
                        WriteToLog("cannot delete" + e.Message);
                    }
                    //}
                }

                WriteToLog("Updating SageFrame version......");
                UpdateSageFrameVersion(doc);

                WriteToLog("Putting site up....");
                UpdateSiteDownFile(false);
                HttpContext.Current.Response.AddHeader("Location", HostingEnvironment.ApplicationVirtualPath + "/Default.aspx");
            }
        }
        return "done";
    }

    /// <summary>
    /// Zips  the backup files.
    /// </summary>
    public static void ZipBackUpFiles()
    {
        string backupLocation = @"Resources\UpgradeBackUp";
        string tempBackupLocation = @"Resources\BackUp\";
        string backupPath = HostingEnvironment.ApplicationPhysicalPath + tempBackupLocation;
        string backupZip = HostingEnvironment.ApplicationPhysicalPath + backupLocation + "Upgraded" + DateTime.Now.ToShortDateString().Replace("/", "_") + DateTime.Now.Millisecond + ".zip";
        // Zip all the files in backup
        ZipUtil.ZipFiles(backupPath, backupZip, "");

        // Have to enable to delete the code later and the type of error may occur:"the file is being used by another process".
        //Directory.Delete(backupPath, true);
    }

    private void UpdateVersion()
    {
        //bool flag = false;
        //XmlDocument doc = new XmlDocument();
        //XmlNode versionNode = doc.SelectSingleNode("/CONFIG/SAGEFRAME");
        //string installerVersion = versionNode.Attributes["VERSION"].Value;
        string prevVersion = Config.GetSetting("SageFrameVersion");
        //Config.S
    }


    public static bool IsFileLocked(FileInfo file)
    {
        FileStream stream = null;

        try
        {
            stream = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None);
        }
        catch (IOException)
        {
            //the file is unavailable because it is:
            //still being written to
            //or being processed by another thread
            //or does not exist (has already been processed)
            return true;
        }
        finally
        {
            if (stream != null)
                stream.Close();
        }

        //file is not locked
        return false;
    }

    public static XmlDocument LoadXml(string extractPath)
    {
        XmlDocument doc = new XmlDocument();
        try
        {
            string configFilePath = Path.Combine(extractPath, @"SystemFiles\config.xml");
            WriteToLog("loading xml");
            //if (File.Exists(configFilePath))
            //    doc.Load(configFilePath);
            TextReader reader = new StreamReader(configFilePath);
            doc.Load(reader);
            WriteToLog("loaded");
            reader.Close();
            WriteToLog("reader close");
            reader.Dispose();
        }
        catch (Exception e)
        {
            throw e;
        }
        return doc;
    }

    public static void CreateLogFile()
    {
        string path = HostingEnvironment.ApplicationPhysicalPath + "log.txt";
        //if (File.Exists(path))
        File.WriteAllText(path, "");

        // File.Create(path).Close();

    }

    public static void ProcessFiles(XmlNodeList nodeList, string zipFilePath)
    {
        bool isFileBackUpSuccess = false;
        try
        {
            WriteToLog("Updating SageFrame files.....");
            foreach (XmlNode node in nodeList)
            {
                string backupLocation = "";
                string unzippedFileLocation = "";
                string rootPath = "";
                var filePath = node.Attributes["path"].Value;
                if (filePath.StartsWith(@"..\"))
                {
                    string path = HostingEnvironment.ApplicationPhysicalPath;
                    rootPath = path.Substring(0, path.LastIndexOf(@"\"));
                    rootPath = rootPath.Substring(0, rootPath.LastIndexOf(@"\") + 1);
                    filePath = filePath.Substring(3);
                    unzippedFileLocation = zipFilePath + "\\UpgradeFiles\\";
                    backupLocation = @"Resources\BackUp\";
                }
                else
                {
                    rootPath = HostingEnvironment.ApplicationPhysicalPath;
                    unzippedFileLocation = zipFilePath;
                    backupLocation = @"Resources\BackUp\";
                }
                if (File.Exists(rootPath + filePath.Replace("SageFrame\\", "")))
                    isFileBackUpSuccess = FileBackUp(filePath, rootPath + backupLocation, rootPath);
                else
                    isFileBackUpSuccess = true;
                if (isFileBackUpSuccess)
                    FileReplacer(filePath, unzippedFileLocation, rootPath);
            }
        }
        catch (Exception e) { throw e; }
    }

    public static void RollBackAllFiles(XmlNodeList nodeList)
    {
        foreach (XmlNode node in nodeList)
        {
            FileRollBack(HostingEnvironment.ApplicationPhysicalPath + @"Modules\Upgrade\BackUp\SageFrame\" + node.Attributes["path"].Value, HostingEnvironment.ApplicationPhysicalPath + node.Attributes["path"].Value);
            WriteToLog("Backing up file: " + node.Attributes["path"].Value);
        }
    }

    public static string UnZipFiles(string filePath, string outputFolder)
    {
        string extractPath = string.Empty;
        ZipUtil.UnZipFiles(filePath, outputFolder, ref extractPath, "", true);
        return extractPath;
    }

    public static void WriteToLog(string txt)
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

    public static string GetParseValue(string pattern, string input)
    {
        string returnVal = "";
        Match match = Regex.Match(input, pattern, RegexOptions.IgnoreCase);
        if (match.Success)
        {
            returnVal = match.Groups[1].Value;
        }
        return returnVal;
    }

    /**********************************************************************************/

    public static void ExecuteSqlScript(string extractPath)
    {
        if (Directory.Exists(extractPath + @"\SystemFiles\SQLScript\"))
        {
            string[] strFileName = Directory.GetFiles(extractPath + @"\SystemFiles\SQLScript\");
            if (strFileName[0] != null || strFileName[0] != "")
            {               
                if (HttpContext.Current.Session["_SageScriptFile"] == null)
                    RunSqlScript(SystemSetting.SageFrameConnectionString, extractPath + @"\SystemFiles\SQLScript\", 0);
                int length = ((ArrayList)HttpContext.Current.Session["_SageScriptFile"]).Count;
                for (int i = 0; i <= length; i++)
                {
                    if (((ArrayList)HttpContext.Current.Session["_SageScriptFile"]).Count > 0)
                    {
                        RunSqlScript(SystemSetting.SageFrameConnectionString, extractPath + @"\SystemFiles\SQLScript\", ((ArrayList)HttpContext.Current.Session["_SageScriptFile"]).Count);
                    }
                }
            }
            HttpContext.Current.Session["_SageScriptFile"] = null;
        }
    }

    public static void UpdateSiteDownFile(bool flag)
    {
        XmlTextWriter tw;
        string isShow = flag ? "true" : "false";
        XmlDocument ConfigDoc = new XmlDocument();
        ConfigDoc.Load(HttpContext.Current.Server.MapPath("~/Modules/Upgrade/SiteMaintainceConfig.xml"));
        XmlNode xmlnode = ConfigDoc.SelectSingleNode("/Config/MySection1/isDown");
        string pathRoot = HttpContext.Current.Server.MapPath("~/Modules/Upgrade/SiteMaintainceConfig.xml");
        xmlnode.InnerText = isShow;
        tw = new XmlTextWriter(pathRoot, System.Text.Encoding.UTF8);
        tw.Formatting = Formatting.Indented;
        tw.Indentation = 4;
        ConfigDoc.Save(tw);
        tw.Close();
    }

    public static void RollBackDatabase(string databaseName, string bakfilePath)
    {
        SqlConnection.ClearAllPools();

        SqlConnection cnn = SetSingleUser(databaseName);
        ServerConnection serverConn = new ServerConnection(cnn);
        Server server = new Server(serverConn);

        Database db = server.Databases[cnn.Database];
        Restore restore = new Restore();
        restore.Action = RestoreActionType.Database;
        restore.Devices.AddDevice(bakfilePath, DeviceType.File);
        restore.Database = databaseName;
        restore.ReplaceDatabase = true;
        restore.Complete += new ServerMessageEventHandler(Restore_Completed);
        restore.PercentComplete += new PercentCompleteEventHandler(CompletionStatusInPercent);
        restore.PercentCompleteNotification = 10;

        SqlCommand cmd = cnn.CreateCommand();
        cmd.CommandText = string.Format("use master");
        cmd.ExecuteNonQuery();


        restore.SqlRestore(server);
        db = server.Databases[databaseName];
        db.SetOnline();
        server.Refresh();

        if (server.ConnectionContext.IsOpen)
            server.ConnectionContext.Disconnect();

    }


    private static SqlConnection SetSingleUser(string databaseName)
    {
        string v;
        v = "alter database " + databaseName + " set SINGLE_USER WITH ROLLBACK IMMEDIATE";
        SqlConnection connection = new SqlConnection(SystemSetting.SageFrameConnectionString);
        SqlCommand cmd = new SqlCommand(v, connection);
        cmd.Connection.Open();
        cmd.ExecuteNonQuery();
        return connection;
    }

    private static void Restore_Completed(object sender, ServerMessageEventArgs args)
    {
        WriteToLog("Restoration of existing database completed");
    }

    private static void CompletionStatusInPercent(object sender, PercentCompleteEventArgs args)
    {

        WriteToLog("Percent completed:" + args.Percent + "%.");
    }

    public static bool BackupDatabase(String databaseName, String destinationPath, SqlConnection connection)
    {
        bool flag = false;
        try
        {
            Backup sqlBackup = new Backup();
            sqlBackup.Action = BackupActionType.Database;
            sqlBackup.BackupSetDescription = "ArchiveDataBase:" +
                                             DateTime.Now.ToShortDateString();
            sqlBackup.BackupSetName = "Archive";
            sqlBackup.Database = databaseName;
            BackupDeviceItem deviceItem = new BackupDeviceItem(destinationPath, DeviceType.File);
            ServerConnection serverConn = new ServerConnection(connection);
            Server sqlServer = new Server(serverConn);
            //Database db = sqlServer.Databases[databaseName];

            sqlBackup.Initialize = true;
            sqlBackup.Checksum = true;
            sqlBackup.ContinueAfterError = true;
            sqlBackup.Devices.Add(deviceItem);
            sqlBackup.Incremental = false;

            sqlBackup.ExpirationDate = DateTime.Now.AddDays(3);
            sqlBackup.LogTruncation = BackupTruncateLogType.Truncate;
            sqlBackup.FormatMedia = false;
            sqlBackup.SqlBackup(sqlServer);
            sqlBackup.UnloadTapeAfter = true;
            flag = true;
        }
        catch (Exception e)
        {
            throw e;
        }

        return flag;
    }

    public static void RunSqlScript(string connectionString1, string actualpath, int Counter)
    {
        object obj = new object();
        lock (obj)
        {
            try
            {
                switch (Counter)
                {
                    case 0:
                        string[] ScriptFiles = System.IO.Directory.GetFiles(actualpath).OrderBy(p => p).ToArray();
                        ArrayList arrColl = new ArrayList();
                        foreach (string scriptFile in ScriptFiles)
                        {
                            arrColl.Add(scriptFile);
                        }
                        HttpContext.Current.Session["_SageScriptFile"] = arrColl;
                        break;
                    default:
                        ArrayList arrC = (ArrayList)HttpContext.Current.Session["_SageScriptFile"];
                        string strFile = arrC[0].ToString();
                        RunGivenSQLScript(strFile, connectionString1);

                        if (arrC.Count != 0)
                        {
                            arrC.RemoveAt(0);
                        }
                        HttpContext.Current.Session["_SageScriptFile"] = arrC;
                        break;
                }
            }
            catch (Exception ex)
            {
                HttpContext.Current.Session["_SageScriptFile"] = null;
                string sss = ex.Message;
            }
        }
    }

    public static void RunGivenSQLScript(string scriptFile, string conn)
    {
        SqlConnection sqlcon = new SqlConnection(conn);
        sqlcon.Open();
        SqlCommand sqlcmd = new SqlCommand();
        sqlcmd.Connection = sqlcon;
        sqlcmd.CommandType = CommandType.Text;
        StreamReader reader = null;
        reader = new StreamReader(scriptFile);
        string script = reader.ReadToEnd();
        try
        {
            ExecuteLongSql(sqlcon, script);

        }
        catch (Exception)
        {

            throw;
        }
        finally
        {
            reader.Close();
            sqlcon.Close();
        }

    }

    public static void ExecuteLongSql(SqlConnection connection, string Script)
    {
        string sql = "";
        sql = Script;
        Regex regex = new Regex("^GO", RegexOptions.IgnoreCase | RegexOptions.Multiline);
        string[] lines = regex.Split(sql);
        SqlTransaction transaction = connection.BeginTransaction();
        using (SqlCommand cmd = connection.CreateCommand())
        {
            cmd.Connection = connection;
            cmd.Transaction = transaction;
            foreach (string line in lines)
            {

                if (line.Length > 0)
                {
                    cmd.CommandText = line;
                    cmd.CommandType = CommandType.Text;
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
        transaction.Commit();
    }

    public static void UpdateSageFrameVersion(XmlDocument doc)
    {
        XmlNode versionNode = doc.SelectSingleNode("/CONFIG/SAGEFRAME");
        string version = versionNode.Attributes["VERSION"].Value;
        Config.UpdateSageFrameVersion(version);
    }

    public void SiteDown(string flag)
    {
        XmlDocument ConfigDoc = new XmlDocument();
        ConfigDoc.Load(Server.MapPath("~/Modules/Upgrade/SiteMaintainceConfig.xml"));

        XmlNode xmlnode = ConfigDoc.SelectSingleNode("/Config/MySection1/isDown");
        string pathRoot = Server.MapPath("~/Modules/Upgrade/SiteMaintainceConfig.xml");
        XmlTextWriter tw;
        if (xmlnode.InnerText != flag)
        {
            xmlnode.InnerText = flag;
            tw = new XmlTextWriter(pathRoot, System.Text.Encoding.UTF8);
            tw.Formatting = Formatting.Indented;
            tw.Indentation = 4;
            ConfigDoc.Save(tw);
            tw.Close();
        }
    }

    public static bool FileBackUp(string sourcePath, string destPath, string rootPath)
    {
        try
        {
            string destFolder = destPath + sourcePath.Substring(0, sourcePath.LastIndexOf("\\"));
            string path = rootPath + sourcePath.Replace("SageFrame\\", "");
            if (!Directory.Exists(destFolder))
                Directory.CreateDirectory(destFolder);
            string fName = Path.GetFileName(path);
            string destFile = Path.Combine(destPath, sourcePath);
            File.Copy(path, destFile, true);
            return true;
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    public static bool FileReplacer(string sourcePath, string UnzipFolderPath, string rootPath)
    {
        try
        {
            string parentFolder = "";
            string SrcPath = sourcePath.Replace("SageFrame\\", "");
            string destPath = rootPath + SrcPath;
            string path = Path.Combine(UnzipFolderPath, sourcePath);
            parentFolder = destPath.Substring(0, destPath.LastIndexOf(@"\") + 1);
            if (!Directory.Exists(parentFolder))
            {
                Directory.CreateDirectory(parentFolder);
            }
            File.Copy(path, destPath, true);
            return true;
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    public static bool FileRollBack(string sourcePath, string destPath)
    {
        try
        {
            if (File.Exists(destPath))
                File.Delete(destPath);

            File.Move(sourcePath, destPath);
            return true;
        }
        catch (Exception e)
        {
            throw e;
        }
    }

}