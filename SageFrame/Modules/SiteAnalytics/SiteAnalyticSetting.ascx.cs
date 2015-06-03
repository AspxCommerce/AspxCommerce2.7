#region "Copyright"
/*
FOR FURTHER DETAILS ABOUT LICENSING, PLEASE VISIT "LICENSE.txt" INSIDE THE SAGEFRAME FOLDER
*/
#endregion

#region "References"
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
using DashBoardControl.Info;
using DashBoardControl;
using System.Collections.Generic;
using SageFrame.Web;
#endregion

public partial class Modules_DashBoardControl_DashBoardControlSetting : BaseAdministrationUserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadSettings();
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            SaveSetting();
            ShowMessage("", "", "Save sucess", SageMessageType.Success);
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }

    private void LoadSettings()
    {
        List<DashBoardSettingInfo> lstSetting = DashBoardController.GetDashBoardSetting(int.Parse(SageUserModuleID), GetPortalID);
        foreach (DashBoardSettingInfo obj in lstSetting)
        {
            switch (obj.SettingKey)
            {
                case "START_DATE":
                    txtStartDate.Text = obj.SettingValue;
                    break;
                case "END_DATE":
                    txtEndDate.Text = obj.SettingValue;
                    break;              
            }
        }
    }

    private void SaveSetting()
    {
        List<DashBoardSettingInfo> lstSetting = new List<DashBoardSettingInfo>();
        lstSetting.Add(new DashBoardSettingInfo(DashBoardSetting.START_DATE.ToString(), txtStartDate.Text));
        lstSetting.Add(new DashBoardSettingInfo(DashBoardSetting.END_DATE.ToString(), txtEndDate.Text));
        lstSetting.ForEach(
            delegate(DashBoardSettingInfo obj)
            {
                obj.PortalID = GetPortalID;
                obj.UserModuleID = int.Parse(SageUserModuleID);
                obj.AddedBy = GetUsername;
                obj.IsActive = 1;
            }
            );
        try
        {
            DashBoardController.AddDashBoardSetting(lstSetting);
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }
}