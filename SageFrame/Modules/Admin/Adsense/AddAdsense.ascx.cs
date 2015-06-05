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
using SageFrame.GoogleAdUnit;
using System.Collections;
using SageFrame.Framework;
using SageFrame.GoogleAdsense;
#endregion


namespace SageFrame.Modules.Admin.Adsense
{
    public partial class AddAdsense : BaseAdministrationUserControl
    {
        int _userModuleCount = 0;
        public Int32 userModuleID = 0;
        SageFrameConfig pb = new SageFrameConfig();

        protected void Page_Init(object sender, EventArgs e)
        {
            userModuleID = Int32.Parse(SageUserModuleID);
            AdUnit1.AffiliateId = pb.GetSettingValueByIndividualKey(SageFrameSettingKeys.PortalGoogleAdSenseID);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                imbDelete.Attributes.Add("onclick", "javascript:return confirm('" + GetSageMessage("Adsense", "AreYouSureToDelete") + "')");
                AddImageUrls();
                BindDropDowns();
                try
                {
                    GoogleAdsenseController objController = new GoogleAdsenseController();
                    _userModuleCount = objController.CountAdsenseSettings(userModuleID, GetPortalID);
                }
                catch (Exception ex)
                {
                    ProcessException(ex);
                }
                if (_userModuleCount > 0)
                {
                    BindControls();
                }
            }
        }

        private void AddImageUrls()
        {
            imbBorder.ImageUrl = GetTemplateImageUrl("imgcolorpicker.png", true);
            imbBack.ImageUrl = GetTemplateImageUrl("imgcolorpicker.png", true);
            imblink.ImageUrl = GetTemplateImageUrl("imgcolorpicker.png", true);
            imbText.ImageUrl = GetTemplateImageUrl("imgcolorpicker.png", true);
            imbURL.ImageUrl = GetTemplateImageUrl("imgcolorpicker.png", true);
            imbSolidFill.ImageUrl = GetTemplateImageUrl("imgcolor.png", true);
            //ImbPreview.ImageUrl = GetTemplateImageUrl("imgpreview.png", true);
            //ImbSave.ImageUrl = GetTemplateImageUrl("imgsave.png", true);
            //imbDelete.ImageUrl = GetTemplateImageUrl("imgdelete.png", true);
            imbSolidFill.ImageUrl = GetTemplateImageUrl("imgcolorpicker.png", true);
        }

        private void BindControls()
        {
            try
            {
                bool isPublic = false;
                GoogleAdsenseController objController = new GoogleAdsenseController();
                List<GoogleAdsenseInfo> adsenseSetting = objController.GetAdSenseSettingsByUserModuleID(userModuleID, GetPortalID);
                foreach (GoogleAdsenseInfo adsContent in adsenseSetting)
                {
                    switch (adsContent.SettingName)
                    {
                        case "AdsenseShow":
                            if (adsContent.SettingValue.Trim() != "")
                            {
                                chkShow.Checked = bool.Parse(adsContent.SettingValue);
                            }
                            break;
                        case "AdsenseUnitFormat":
                            if (adsContent.SettingValue.Trim() != "")
                            {
                                ddlUnitFormat.SelectedValue = adsContent.SettingValue;
                                chkActive.Checked = (bool)adsContent.IsActive;
                            }
                            break;
                        case "AdsenseUnitType":
                            if (adsContent.SettingValue.Trim() != "")
                            {
                                ddlAddType.SelectedValue = adsContent.SettingValue;
                            }
                            break;
                        case "AdsenseChannelID":
                            if (adsContent.SettingValue.Trim() != "")
                            {
                                txtChannelID.Text = adsContent.SettingValue;
                            }
                            break;
                        case "AdsenseBorderColor":
                            if (adsContent.SettingValue.Trim() != "")
                            {
                                txtBorder.Text = adsContent.SettingValue;
                            }
                            break;
                        case "AdsenseBackColor":
                            if (adsContent.SettingValue.Trim() != "")
                            {
                                txtbackcolor.Text = adsContent.SettingValue;
                            }
                            break;
                        case "AdsenseLinkColor":
                            if (adsContent.SettingValue.Trim() != "")
                            {
                                txtLink.Text = adsContent.SettingValue;
                            }
                            break;
                        case "AdsenseTextColor":
                            if (adsContent.SettingValue.Trim() != "")
                            {
                                txtText.Text = adsContent.SettingValue;
                            }
                            break;
                        case "AdsenseURLColor":
                            if (adsContent.SettingValue.Trim() != "")
                            {
                                txtURL.Text = adsContent.SettingValue;
                            }
                            break;
                        case "AdsenseAnotherURL":
                            if (adsContent.SettingValue.Trim() != "")
                            {
                                if (isPublic == false)
                                {
                                    txtanotherURL.Text = adsContent.SettingValue;
                                    anotherURL.Visible = true;
                                    solidFill.Visible = false;
                                }
                            }
                            break;
                        case "AdsenseSolidFillColor":
                            if (adsContent.SettingValue.Trim() != "")
                            {
                                if (isPublic == false)
                                {
                                    txtSolidFill.Text = adsContent.SettingValue;
                                    solidFill.Visible = true;
                                    anotherURL.Visible = false;
                                }
                            }
                            break;
                        case "AdsenseAlternateAds":
                            ddlAlternateAds.SelectedValue = adsContent.SettingValue;
                            if (adsContent.SettingValue == "-1")
                            {
                                solidFill.Visible = false;
                                anotherURL.Visible = false;
                            }
                            if (adsContent.SettingValue == "0")
                            {
                                isPublic = true;
                                solidFill.Visible = false;
                                anotherURL.Visible = false;
                            }
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
            imbDelete.Visible = true;
            //lblDelete.Visible = true;

        }

        private void BindDropDowns()
        {
            ddlAlternateAds.DataSource = BindToEnum(typeof(AlternateAdTypes));
            ddlAlternateAds.DataBind();
            ddlAlternateAds.Items.Insert(0, new ListItem("Select", "-1"));

            ddlUnitFormat.DataSource = BindToEnum(typeof(AdUnitFormat));
            ddlUnitFormat.DataBind();

            ddlAddType.DataSource = BindToEnum(typeof(AdUnitType));
            ddlAddType.DataBind();
        }

        private Hashtable BindToEnum(Type enumType)
        {
            string[] names = Enum.GetNames(enumType);
            Array values = Enum.GetValues(enumType);
            Hashtable ht = new Hashtable();

            for (int i = 0; i < names.Length; i++)
                ht.Add(names[i], (int)values.GetValue(i));

            return ht;
        }

        private void PreviewAds()
        {
            AdUnit1.Visible = true;
            SageFrameConfig pb = new SageFrameConfig();
            AdUnit1.AffiliateId = pb.GetSettingValueByIndividualKey(SageFrameSettingKeys.PortalGoogleAdSenseID);
            string unitFormat = ddlUnitFormat.SelectedItem.Text;
            string unitType = ddlAddType.SelectedItem.Text;

            if (Enum.IsDefined(typeof(AdUnitFormat), unitFormat))
            {
                AdUnit1.AdUnitFormat = (AdUnitFormat)Enum.Parse(typeof(AdUnitFormat), unitFormat, true);
            }
            if (Enum.IsDefined(typeof(AdUnitType), unitType))
            {
                AdUnit1.AdUnitType = (AdUnitType)Enum.Parse(typeof(AdUnitType), unitType, true);
            }

            AdUnit1.ChannelId = txtChannelID.Text;
            if (txtbackcolor.Text.Trim() != "")
            {
                AdUnit1.BackColor = System.Drawing.ColorTranslator.FromHtml("#" + txtbackcolor.Text.Trim());
            }
            if (txtLink.Text.Trim() != "")
            {
                AdUnit1.LinkColor = System.Drawing.ColorTranslator.FromHtml("#" + txtLink.Text.Trim());
            }
            if (txtText.Text.Trim() != "")
            {
                AdUnit1.TextColor = System.Drawing.ColorTranslator.FromHtml("#" + txtText.Text.Trim());
            }
            if (txtBorder.Text.Trim() != "")
            {
                AdUnit1.BorderColor = System.Drawing.ColorTranslator.FromHtml("#" + txtBorder.Text.Trim());
            }
            if (txtURL.Text.Trim() != "")
            {
                AdUnit1.UrlColor = System.Drawing.ColorTranslator.FromHtml("#" + txtURL.Text.Trim());
            }

            if (ddlAlternateAds.SelectedValue != "-1")
            {
                AdUnit1.AlternateAdType = (AlternateAdTypes)Enum.Parse(typeof(AlternateAdTypes), ddlAlternateAds.SelectedItem.Text, true);
                if (ddlAlternateAds.SelectedValue == "1")
                {
                    AdUnit1.AnotherUrl = txtanotherURL.Text.Trim();
                }
                else if (ddlAlternateAds.SelectedValue == "2")
                {
                    if (txtSolidFill.Text.Trim() != "")
                    {
                        AdUnit1.SolidFillColor = System.Drawing.ColorTranslator.FromHtml("#" + txtSolidFill.Text.Trim());
                    }
                    else
                    {
                        AdUnit1.SolidFillColor = System.Drawing.ColorTranslator.FromHtml("#fffff");
                    }
                }
            }
        }

        protected void ddlAlternateAds_SelectedIndexChanged(object sender, EventArgs e)
        {
            AdUnit1.Visible = false;

            if (ddlAlternateAds.SelectedValue == "1")
            {
                anotherURL.Visible = true;
                solidFill.Visible = false;
            }
            else if (ddlAlternateAds.SelectedValue == "2")
            {
                solidFill.Visible = true;
                anotherURL.Visible = false;
            }
            else
            {
                solidFill.Visible = false;
                anotherURL.Visible = false;
            }

        }

        protected void SaveAdsense(bool IsActive, bool updateFlag)
        {
            try
            {
                string channelID = string.Empty;
                if (txtChannelID.Text != string.Empty)
                {
                    channelID = txtChannelID.Text;
                }
                else
                {
                    channelID = pb.GetSettingValueByIndividualKey(SageFrameSettingKeys.PortalGoogleAdsenseChannelID);
                }

                GoogleAdsenseController objController = new GoogleAdsenseController();

                objController.AddUpdateAdSense(userModuleID, "AdsenseShow", chkShow.Checked.ToString(), IsActive, GetPortalID, GetUsername, updateFlag);
                objController.AddUpdateAdSense(userModuleID, "AdsenseUnitFormat", ddlUnitFormat.SelectedValue, IsActive, GetPortalID, GetUsername, updateFlag);
                objController.AddUpdateAdSense(userModuleID, "AdsenseUnitType", ddlAddType.SelectedValue, IsActive, GetPortalID, GetUsername, updateFlag);
                objController.AddUpdateAdSense(userModuleID, "AdsenseChannelID", channelID, IsActive, GetPortalID, GetUsername, updateFlag);
                objController.AddUpdateAdSense(userModuleID, "AdsenseBorderColor", txtBorder.Text, IsActive, GetPortalID, GetUsername, updateFlag);
                objController.AddUpdateAdSense(userModuleID, "AdsenseBackColor", txtbackcolor.Text, IsActive, GetPortalID, GetUsername, updateFlag);
                objController.AddUpdateAdSense(userModuleID, "AdsenseLinkColor", txtLink.Text, IsActive, GetPortalID, GetUsername, updateFlag);
                objController.AddUpdateAdSense(userModuleID, "AdsenseTextColor", txtText.Text, IsActive, GetPortalID, GetUsername, updateFlag);
                objController.AddUpdateAdSense(userModuleID, "AdsenseURLColor", txtURL.Text, IsActive, GetPortalID, GetUsername, updateFlag);
                if (ddlAlternateAds.SelectedValue != "-1")
                {
                    objController.AddUpdateAdSense(userModuleID, "AdsenseAlternateAds", ddlAlternateAds.SelectedValue, IsActive, GetPortalID, GetUsername, updateFlag);
                    if (ddlAlternateAds.SelectedValue == "1")
                    {

                        objController.AddUpdateAdSense(userModuleID, "AdsenseAnotherURL", txtanotherURL.Text, IsActive, GetPortalID, GetUsername, updateFlag);
                        objController.AddUpdateAdSense(userModuleID, "AdsenseSolidFillColor", "", IsActive, GetPortalID, GetUsername, updateFlag);
                    }
                    else if (ddlAlternateAds.SelectedValue == "2")
                    {

                        objController.AddUpdateAdSense(userModuleID, "AdsenseSolidFillColor", txtSolidFill.Text, IsActive, GetPortalID, GetUsername, updateFlag);
                        objController.AddUpdateAdSense(userModuleID, "AdsenseAnotherURL", "", IsActive, GetPortalID, GetUsername, updateFlag);
                    }
                }
                else
                {

                    objController.AddUpdateAdSense(userModuleID, "AdsenseAlternateAds", ddlAlternateAds.SelectedValue, IsActive, GetPortalID, GetUsername, updateFlag);
                    objController.AddUpdateAdSense(userModuleID, "AdsenseAnotherURL", "", IsActive, GetPortalID, GetUsername, updateFlag);
                    objController.AddUpdateAdSense(userModuleID, "AdsenseSolidFillColor", "", IsActive, GetPortalID, GetUsername, updateFlag);
                }
                ShowMessage(SageMessageTitle.Information.ToString(), GetSageMessage("Adsense", "AdsenseSavedSuccessfully"), "", SageMessageType.Success);

            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        protected void imbDelete_Click(object sender, EventArgs e)
        {
            try
            {
                GoogleAdsenseController objController = new GoogleAdsenseController();
                objController.CountAdsenseSettings(userModuleID, GetPortalID);
                ClearForm();
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }

            if (_userModuleCount > 0)
            {
                DeleteAdsense();
            }
        }

        private void DeleteAdsense()
        {
            try
            {
                GoogleAdsenseController objController = new GoogleAdsenseController();
                objController.DeleteAdSense(userModuleID, GetPortalID);
                ShowMessage(SageMessageTitle.Information.ToString(), GetSageMessage("Adsense", "AdsenseDeletedSuccessfully"), "", SageMessageType.Success);

            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        private void ClearForm()
        {
            BindDropDowns();
            AdUnit1.Visible = false;
            imbDelete.Visible = false;
            //lblDelete.Visible = false;
            solidFill.Visible = false;
            txtChannelID.Text = "";
            txtBorder.Text = "";
            txtbackcolor.Text = "";
            txtLink.Text = "";
            txtText.Text = "";
            txtURL.Text = "";
            txtSolidFill.Text = "";
            txtanotherURL.Text = "";
            chkShow.Checked = true;
            chkActive.Checked = true;
        }

        protected void Preview_Click(object sender, EventArgs e)
        {
            PreviewAds();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            bool IsActive = false;
            bool UpdateFlag = false;
            if (chkActive.Checked == true)
            {
                IsActive = true;
            }
            try
            {
                GoogleAdsenseController objController = new GoogleAdsenseController();
                _userModuleCount = objController.CountAdsenseSettings(userModuleID, GetPortalID);
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }

            if (_userModuleCount > 0)
            {
                UpdateFlag = true;
            }
            SaveAdsense(IsActive, UpdateFlag);
            PreviewAds();
        }
    }
}