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
using SageFrame.SageBannner.Provider;
using SageFrame.SageBannner.SettingInfo;
using SageFrame.Common;
using SageFrame.Core;
using SageFrame.Web.Utilities;
using SageFrame.SageBannner.Controller;
#endregion

public partial class Modules_Sage_Banner_SettingBanner : BaseAdministrationUserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadDropDownBanner(Int32.Parse(SageUserModuleID), GetPortalID);
            GetSetting();
            
        }
    }

    private void LoadDropDownBanner(int UserModuleID, int PortalID)
    {
        try
        {
            SageBannerController obj=new SageBannerController();
            ddlBannerListToUse.DataSource = obj.LoadBannerOnDropDown(UserModuleID, PortalID,GetCurrentCulture());
            ddlBannerListToUse.DataValueField = "BannerID";
            ddlBannerListToUse.DataTextField = "BannerName";
            ddlBannerListToUse.DataBind();
            ddlBannerListToUse.Items.Insert(0, new ListItem("ChooseBanner", "-1"));
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }
    protected void btnRefreshList_Click(object sender,EventArgs e)
    {
        LoadDropDownBanner(Int32.Parse(SageUserModuleID), GetPortalID);
    }


    public void GetSetting()
    {
        SageBannerSettingInfo obj = GetSageBannerSetting(GetPortalID, Int32.Parse(SageUserModuleID),GetCurrentCulture());
        chkAutoSlide.Checked = obj.Auto_Slide;
        chkInfiniteLoop.Checked = obj.InfiniteLoop;
        chkNumeric.Checked = obj.NumericPager;
        txtPauseTime.Text = Convert.ToString(obj.Pause_Time);
        txtSpeed.Text = Convert.ToString(obj.Speed);
        ddlTransitionMode.SelectedValue = obj.TransitionMode;
        chkEnableControl.Checked = obj.EnableControl;
        if (obj.TransitionMode == "horizontal")
        {
            ddlTransitionMode.SelectedValue = "0";
        }
        else
        {
            ddlTransitionMode.SelectedValue = "2";

        }
        ddlBannerListToUse.SelectedValue = obj.BannerToUse;
    }

    protected void imbSaveBannerSetting_Click(object sender, EventArgs e)
    {
        int userModuleID = Int32.Parse(SageUserModuleID);
        if (txtSpeed.Text == null)
        {
            txtSpeed.Text = Convert.ToString(0);
        }
        if (txtPauseTime.Text == null)
        {
            txtPauseTime.Text = Convert.ToString(0);
        }
        try
        {
            string userName = GetUsername;
            int portalID = GetPortalID;
            SaveBannerSetting("BannerToUse", ddlBannerListToUse.SelectedValue, userModuleID, userName, userName, portalID);
            SaveBannerSetting("TransitionMode", ddlTransitionMode.SelectedItem.Text, userModuleID, userName, userName, portalID);
            SaveBannerSetting("InfiniteLoop", Convert.ToString(chkInfiniteLoop.Checked).ToLower(), userModuleID, userName, userName, portalID);
            SaveBannerSetting("Speed", txtSpeed.Text, userModuleID, userName, userName, portalID);
            SaveBannerSetting("NumericPager", Convert.ToString(chkNumeric.Checked).ToLower(), userModuleID, userName, userName, portalID);
            SaveBannerSetting("Auto_Slide", Convert.ToString(chkAutoSlide.Checked).ToLower(), userModuleID, userName, userName, portalID);
            SaveBannerSetting("Pause_Time", txtPauseTime.Text, userModuleID, userName, userName, portalID);
            SaveBannerSetting("EnableControl", Convert.ToString(chkEnableControl.Checked).ToLower(), userModuleID, userName, userName, portalID);
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }

        ShowMessage(SageMessageTitle.Information.ToString(), SageMessage.GetSageModuleLocalMessageByVertualPath("Modules/Sage_Banner/ModuleLocalText", "SettingSavedSucessfully"), "", SageMessageType.Success);
        HttpRuntime.Cache.Remove("BannerImages_" + GetCurrentCulture() + "_" + userModuleID.ToString());
        HttpRuntime.Cache.Remove("BannerSetting_" + GetCurrentCulture() + "_" + userModuleID.ToString());
    }

    private void SaveBannerSetting(string Key, string value, int usermoduleid, string Addedby, string Updatedby, int PortalID)
    {
        try
        {
            SageBannerProvider objHelp = new SageBannerProvider();
            objHelp.SaveBannerSetting(Key, value, usermoduleid, Addedby, Updatedby, PortalID,GetCurrentCulture());
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }

    public SageBannerSettingInfo GetSageBannerSetting(int PortalID, int UserModuleID, string CultureCode)
    {
        try
        {
            SageBannerController objC = new SageBannerController();
            return objC.GetSageBannerSettingList(PortalID, UserModuleID,CultureCode);
        }
        catch (Exception ex)
        {
            throw (ex);
        }
    }
}
