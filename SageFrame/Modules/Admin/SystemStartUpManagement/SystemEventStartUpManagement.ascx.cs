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
using SageFrame.Core;
using System.IO;
using SageFrame.Common;
#endregion

public partial class Modules_Admin_SystemStartUpManagement_SystemEventStartUpManagement : BaseAdministrationUserControl
{
    public void Page_Load(object sender, EventArgs e)
    {       
        try
        {
            if (!IsPostBack)
            {               
                ClearForm();
                BindData();
                pnlSystemEventStartUpList.Style.Add("display", "block");
            }
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }
    private void BindData()
    {
        try
        {
            SystemStartupController ctl = new SystemStartupController();
            grdList.DataSource = ctl.GetSystemEventStartUpList(GetPortalID);
            grdList.DataBind();
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }
    private void ClearForm()
    {
        try
        {
            chkIsActive.Checked = false;
            chkIsAdmin.Checked = false;
            chkIsControlUrl.Checked = false;
            HideAll();
            pnlSystemEventStartUpList.Style.Add("display", "block");
            pnlEventStartUp.Style.Add("display", "none");
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }
   
    private void HideAll()
    {
        try
        {
            pnlEventStartUp.Style.Add("display", "none");
            pnlSystemEventStartUpList.Style.Add("display", "none");
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }
    protected void imbAddNew_Click(object sender, EventArgs e)
    {
        try
        {
            ClearForm();
            hdnPortalStartUpID.Value = "0";
            pnlEventStartUp.Style.Add("display", "block");
            pnlSystemEventStartUpList.Style.Add("display", "none");
            LoadSources(Server.MapPath("~/Modules"));            
            LoadEventLocation();
            chkIsAdmin.Checked = false;
            chkIsControlUrl.Checked = false;
            chkIsActive.Checked = true;
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }

    }

    private void LoadSources(string strRoot)
    {
        try
        {
            string ApplicationPath = HttpContext.Current.Server.MapPath("~/");
            string strPath = strRoot;
            if (Directory.Exists(strPath))
            {
                DirectoryInfo dti = new DirectoryInfo(strPath);
                DirectoryInfo[] colldti = dti.GetDirectories();
                foreach (DirectoryInfo dir in colldti)
                {
                    if (dir.Name != ".svn")
                    {
                        LoadSources(dir.FullName);
                        FileInfo[] collFile = dir.GetFiles("*.ascx");
                        foreach (FileInfo mfile in collFile)
                        {
                            string FileName = mfile.FullName.Remove(0, ApplicationPath.Length);
                            FileName = FileName.Replace("\\", "/");
                            ddlControlUrl.Items.Add(new ListItem(FileName, FileName.ToLower()));
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }  

    private void LoadEventLocation()
    {
        try
        {
            SystemStartupController ctl = new SystemStartupController();
            List<SystemEventLocationInfo> obj = ctl.GetEventLocationList();            
            ddlEventLocation.DataSource = obj;
            ddlEventLocation.DataTextField = "EventLocationName";
            ddlEventLocation.DataValueField = "EventLocationName";
            ddlEventLocation.DataBind(); 
           
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }

    
    protected void imbSave_Click(object sender, EventArgs e)
    {
        Save();
    }


    private void Save()
    {
        try
        {           
            SystemStartupController ctl = new SystemStartupController();
            Int32 PortalStartUpID = Int32.Parse(hdnPortalStartUpID.Value);
            ctl.UpdateSystemEventStartUp(PortalStartUpID, GetPortalID, ddlControlUrl.SelectedValue, ddlEventLocation.SelectedValue,chkIsAdmin.Checked,chkIsControlUrl.Checked,chkIsActive.Checked, GetUsername);
            ClearForm();
            BindData();
            HttpRuntime.Cache.Remove(CacheKeys.StartupSageSetting);
        }

        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }

    public void EditSystemEventStartUp(Int32 PortalStartUpID)
    {
        try
        {
            SystemStartupController ctl = new SystemStartupController();           
            SystemEventStartUpInfo objInfo = ctl.GetSystemEventStartUpDetails(PortalStartUpID);           
            if (objInfo != null)
            {              
                 ClearForm();
                 pnlSystemEventStartUpList.Style.Add("display", "none");
                 pnlEventStartUp.Style.Add("display", "block");
                 LoadSources(Server.MapPath("~/Modules"));                
                 LoadEventLocation();
                 hdnPortalStartUpID.Value =PortalStartUpID.ToString();
                 ddlControlUrl.SelectedIndex = ddlControlUrl.Items.IndexOf(ddlControlUrl.Items.FindByValue(objInfo.ControlUrl));                
                 ddlEventLocation.SelectedIndex = ddlEventLocation.Items.IndexOf(ddlEventLocation.Items.FindByValue(objInfo.EventLocationName));
                 chkIsAdmin.Checked = objInfo.IsAdmin;
                 chkIsControlUrl.Checked = objInfo.IsControlUrl;
                 chkIsActive.Checked = objInfo.IsActive;

            }
            
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }

    protected void imbCancel_Click(object sender, EventArgs e)
    {
        try
        {
            ClearForm();
            HttpRuntime.Cache.Remove(CacheKeys.StartupSageSetting);
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }
    protected void grdList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grdList.PageIndex = e.NewPageIndex;
            BindData();
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }

    protected void gdvList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
               
                ImageButton imgDelete = (ImageButton)e.Row.FindControl("imbDelete");
                ImageButton imgEdit = (ImageButton)e.Row.FindControl("imbEdit");                
                int idxIsSystem = GetColumnIndexByName(e.Row, "IsSystem");
                string isSystem = e.Row.Cells[idxIsSystem].Text;
                if (bool.Parse(isSystem))
                {
                    imgDelete.Visible = false;
                    imgEdit.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }

    protected void grdList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            SystemStartupController ctl = new SystemStartupController();
            Int32 PortalStartUpID = Int32.Parse(e.CommandArgument.ToString());         
            switch (e.CommandName.ToString())
            {
                case "EditEvent":
                    EditSystemEventStartUp(PortalStartUpID);
                    break;
                case "DeleteEvent":
                    ctl.DeleteSystemEventStartUp(PortalStartUpID,GetUsername);
                    ClearForm();
                    BindData();
                    break;
            }
            HttpRuntime.Cache.Remove(CacheKeys.StartupSageSetting);
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }
    private int GetColumnIndexByName(GridViewRow row, string columnName)
    {
        int columnIndex = 0;
        foreach (DataControlFieldCell cell in row.Cells)
        {
            if (cell.ContainingField is BoundField)
                if (((BoundField)cell.ContainingField).DataField.Equals(columnName))
                    break;
            columnIndex++; // keep adding 1 while we don't have the correct name
        }
        return columnIndex;
    }

}