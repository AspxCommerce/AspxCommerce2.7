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
using SageFrame.Web;
using SageFrame.Web.Utilities;
using System.IO;
using Microsoft.SqlServer.Management.Smo;
using System.Data.SqlClient;
using Microsoft.SqlServer.Management.Common;
using System.Web.Hosting;
using System.Net;
using SageFrame.Common;
#endregion

namespace SageFrame.Modules.Admin.SQL
{
    public partial class ctl_SqlQueryAnalyser : BaseAdministrationUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        #region "Control Events"

        protected void imbUploadSqlScript_Click(object sender, EventArgs e)
        {
            LoadSqlScript();
        }

        protected void imbExecuteSql_Click(object sender, EventArgs e)
        {
            ExecuteSql();
        }

        #endregion

        #region "private methods"

        private void LoadSqlScript()
        {
            try
            {
                if (IsPostBack)
                {
                    if (fluSqlScript.HasFile && fluSqlScript.PostedFile.FileName != string.Empty)
                    {
                        string ext = Path.GetExtension(fluSqlScript.PostedFile.FileName);
                        if (Path.GetExtension(fluSqlScript.PostedFile.FileName) != ".sql" && Path.GetExtension(fluSqlScript.PostedFile.FileName) != ".txt")
                        {
                            ShowMessage(SageMessageTitle.Information.ToString(), "Invalid file format", "", SageMessageType.Alert);

                        }
                        else
                        {
                            var file = fluSqlScript.PostedFile.InputStream;
                            //GetBytesFromStream(System.IO.Stream)
                            file.Seek(0, SeekOrigin.Begin);
                            System.IO.StreamReader scriptFile = new System.IO.StreamReader(file);
                            txtSqlQuery.Text = scriptFile.ReadToEnd();
                        }
                    }
                    else
                    {
                        ShowMessage(SageMessageTitle.Information.ToString(), "Please upload SQL file", "", SageMessageType.Alert);
                    }
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        //public static byte[] ConvertImageToByteArray(Stream afuInputStream, int contentLength)
        //{
        //    byte[] sqlBinaryData = new byte[contentLength];
        //    afuInputStream.Read(sqlBinaryData, 0, contentLength);
        //    return sqlBinaryData;
        //}

        private void ExecuteSql()
        {
            try
            {
                SQLHandler objSqlh = new SQLHandler();
                if (chkRunAsScript.Checked == true)
                {
                    string strError = objSqlh.ExecuteScript(txtSqlQuery.Text);
                    if (string.IsNullOrEmpty(strError))
                    {


                        ShowMessage(SageMessageTitle.Information.ToString(), GetSageMessage("SQL", "TheQueryCompletedSuccessfully"), "", SageMessageType.Success);

                    }
                    else
                    {
                        ShowMessage(SageMessageTitle.Notification.ToString(), strError, "", SageMessageType.Alert);

                    }
                }
                else
                {
                    System.Data.DataTable dt = objSqlh.ExecuteSQL(txtSqlQuery.Text);
                    if (dt != null)
                    {
                        gdvResults.DataSource = dt;
                        gdvResults.DataBind();
                    }
                    else
                    {

                        ShowMessage(SageMessageTitle.Notification.ToString(), GetSageMessage("SQL", "ThereIsAnErrorInYourQuery"), "", SageMessageType.Alert);

                    }
                }
                string txt = txtSqlQuery.Text;
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        protected void btnBackup_Click(object sender, EventArgs e)
        {
            SessionLog sTracController = new SessionLog();
            sTracController.CreateBackup();
        }

        protected void btnDatabasebackup_Click(object sender, EventArgs e)
        {

            string dbbackupFolder = HostingEnvironment.ApplicationPhysicalPath + "Config//DBBackup//";
            if (!Directory.Exists(dbbackupFolder))
                Directory.CreateDirectory(dbbackupFolder);
            string dbBackupPath = Path.Combine(dbbackupFolder, SystemSetting.SageFrameDBName.ToString() + ".bak");

            if (File.Exists(dbBackupPath))
                File.Delete(dbBackupPath);
            try
            {

                SqlConnection connection = new SqlConnection(SystemSetting.SageFrameConnectionString);
                Backup sqlBackup = new Backup();
                sqlBackup.Action = BackupActionType.Database;
                sqlBackup.BackupSetDescription = "ArchiveDataBase:" +
                                                 DateTime.Now.ToShortDateString();
                sqlBackup.BackupSetName = "Archive";
                sqlBackup.Database = SystemSetting.SageFrameDBName;
                BackupDeviceItem deviceItem = new BackupDeviceItem(dbBackupPath, DeviceType.File);
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
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                string strURL = HostingEnvironment.ApplicationVirtualPath + "//Config//DBBackup//" + SystemSetting.SageFrameDBName.ToString() + ".bak";
                WebClient req = new WebClient();
                HttpResponse response = HttpContext.Current.Response;
                response.Clear();
                response.ClearContent();
                response.ClearHeaders();
                response.Buffer = true;
                response.AddHeader("Content-Disposition", "attachment;filename=\"" + SystemSetting.SageFrameDBName.ToString() + ".bak\"");
                byte[] data = req.DownloadData(Server.MapPath(strURL));
                response.BinaryWrite(data);
                response.End();
            }
        }


        protected void btnDatabaseCleanup_Click(object sender, EventArgs e)
        {
            try
            {
                SQLHelper objSqlHelper = new SQLHelper();
                objSqlHelper.ExecuteModuleDataCleanupScript();
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        #endregion




    }
}