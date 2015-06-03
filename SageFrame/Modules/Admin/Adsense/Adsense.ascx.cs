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
using SageFrame.Framework;
using SageFrame.GoogleAdsense;
using SFE.GoogleAdUnit;
#endregion

namespace SageFrame.Modules.Admin.Adsense
{
    public partial class Adsense : BaseAdministrationUserControl
    {
        int _userModuleCount = 0;
        SageFrameConfig pb = new SageFrameConfig();
        protected void Page_Init(object sender, EventArgs e)
        {
            hdnUserModuleID.Value = SageUserModuleID;
            AdsenseDisplay.AffiliateId = pb.GetSettingValueByIndividualKey(SageFrameSettingKeys.PortalGoogleAdSenseID);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    GoogleAdsenseController objController = new GoogleAdsenseController();
                    _userModuleCount = objController.CountAdSense(Int32.Parse(hdnUserModuleID.Value), GetPortalID);
                }
                catch (Exception ex)
                {
                    ProcessException(ex);
                }
                if (_userModuleCount > 0)
                {
                    BindAdsControl();
                }
                else if (_userModuleCount == 0)
                {
                    //BindDefaultAdsControl();
                }
                else
                {
                    AdsenseDisplay.Visible = false;
                }
            }
        }

        private void BindDefaultAdsControl()
        {
            try
            {
                AdsenseDisplay.Visible = true;
                AdsenseDisplay.ChannelId = pb.GetSettingValueByIndividualKey(SageFrameSettingKeys.PortalGoogleAdsenseChannelID);
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        private void BindAdsControl()
        {
            try
            {
                AdsenseDisplay.Visible = true;
                GoogleAdsenseController objController = new GoogleAdsenseController();
                List<GoogleAdsenseInfo> adsenseSetting = objController.GetAdSenseSettingsByUserModuleID(Int32.Parse(hdnUserModuleID.Value), GetPortalID);
                foreach (GoogleAdsenseInfo adsContent in adsenseSetting)
                {
                    switch (adsContent.SettingName)
                    {
                        case "AdsenseUnitFormat":
                            AdsenseDisplay.AdUnitFormat = (AdUnitFormat)Enum.Parse(typeof(AdUnitFormat), adsContent.SettingValue, true);
                            break;
                        case "AdsenseUnitType":
                            AdsenseDisplay.AdUnitType = (AdUnitType)Enum.Parse(typeof(AdUnitType), adsContent.SettingValue, true);
                            break;
                        case "AdsenseChannelID":
                            AdsenseDisplay.ChannelId = adsContent.SettingValue;
                            break;
                        case "AdsenseBorderColor":
                            if (adsContent.SettingValue.Trim() != "")
                            {
                                AdsenseDisplay.BorderColor = System.Drawing.ColorTranslator.FromHtml("#" + adsContent.SettingValue.Trim());
                            }
                            break;
                        case "AdsenseBackColor":

                            if (adsContent.SettingValue.Trim() != "")
                            {
                                AdsenseDisplay.BackColor = System.Drawing.ColorTranslator.FromHtml("#" + adsContent.SettingValue.Trim());
                            }
                            break;
                        case "AdsenseLinkColor":
                            if (adsContent.SettingValue.Trim() != "")
                            {
                                AdsenseDisplay.LinkColor = System.Drawing.ColorTranslator.FromHtml("#" + adsContent.SettingValue.Trim());
                            }
                            break;
                        case "AdsenseTextColor":
                            if (adsContent.SettingValue.Trim() != "")
                            {
                                AdsenseDisplay.TextColor = System.Drawing.ColorTranslator.FromHtml("#" + adsContent.SettingValue.Trim());
                            }
                            break;
                        case "AdsenseURLColor":
                            if (adsContent.SettingValue.Trim() != "")
                            {
                                AdsenseDisplay.UrlColor = System.Drawing.ColorTranslator.FromHtml("#" + adsContent.SettingValue.Trim());
                            }
                            break;
                        case "AdsenseAnotherURL":
                            if (adsContent.SettingValue.Trim() != "")
                            {
                                AdsenseDisplay.AnotherUrl = adsContent.SettingValue.Trim();
                            }
                            break;
                        case "AdsenseSolidFillColor":
                            if (adsContent.SettingValue.Trim() != "")
                            {
                                AdsenseDisplay.SolidFillColor = System.Drawing.ColorTranslator.FromHtml("#" + adsContent.SettingValue.Trim());
                            }
                            break;
                        case "AdsenseAlternateAds":
                            if (adsContent.SettingValue.Trim() != "" && adsContent.SettingValue.Trim() != "-1")
                            {
                                AdsenseDisplay.AlternateAdType = (AlternateAdTypes)Enum.Parse(typeof(AlternateAdTypes), adsContent.SettingValue, true);
                            }
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }
    }
}