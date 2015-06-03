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
using SageFrame.SageBannner.SettingInfo;
using SageFrame.Web.Utilities;
using SageFrame.SageBannner.Controller;
using SageFrame.SageBannner.Info;
using System.Text;
using SageFrame.Web.Common.SEO;
#endregion

public partial class Modules_Sage_Banner_ViewBanner : BaseAdministrationUserControl
{
    #region Variables
    public string Auto_Direction = "";
    public bool Auto_Slide;
    public bool Caption;
    public int DisplaySlideQty;
    public bool InfiniteLoop;
    public bool NumericPager;
    public bool EnableControl = false;
    public int Pause_Time;
    public bool RandomStart;
    public int Speed;
    public string BannerId = "";
    public string TransitionMode = "";
    public int UserModuleId;
    public int PortalId;
    public string SageURL = "";
    public string Extension;
    string modulePath = string.Empty;
    public int bannerCount = 0;
    public string Fullpath = string.Empty;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        Extension = SageFrameSettingKeys.PageExtension;
        SageURL = string.Format("{0}{1}", Request.ApplicationPath == "/" ? "" : Request.ApplicationPath, SageURL);
        UserModuleId = Int32.Parse(SageUserModuleID);
        PortalId = GetPortalID;
        modulePath = ResolveUrl(this.AppRelativeTemplateSourceDirectory);
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "globalVariables", " var SageBannerServicePath='" + ResolveUrl(modulePath) + "';", true);
        IncludeJS();
        IncludeCss();
        EnableControl = true;
        Fullpath = !IsParent ? SageURL + "/portal/" + GetPortalSEOName : SageURL;
        GetBannerSetting();
    }

    private void GetBannerSetting()
    {
        SageBannerSettingInfo obj = GetSageBannerSettingList(GetPortalID, Int32.Parse(SageUserModuleID));
        Auto_Slide = obj.Auto_Slide;
        Caption = obj.Caption;
        InfiniteLoop = obj.InfiniteLoop;
        NumericPager = obj.NumericPager;
        Pause_Time = obj.Pause_Time;
        RandomStart = obj.RandomStart;
        EnableControl = obj.EnableControl;
        Speed = obj.Speed;
        TransitionMode = obj.TransitionMode;
        BannerId = obj.BannerToUse;
        GetBannerImages(int.Parse(BannerId), UserModuleId, PortalId, GetCurrentCulture());
    }


    private void IncludeCss()
    {
        IncludeCss("SageResponsiveBanner", "/Modules/Sage_Banner/css/bx_styles.css", "/Modules/Sage_Banner/css/Module.css");
    }

    private void IncludeJS()
    {
        IncludeJs("Sage_Banner", "/Modules/Sage_Banner/js/jquery.bxslider.js");
        IncludeJs("Sage_Banner", "/Modules/Sage_Banner/js/picturefill.js", "/Modules/Sage_Banner/js/matchmedia.js"); 
        IncludeJs("Sage_Banner", "/Modules/Sage_Banner/js/SageBannerView.js");

    }

    public SageBannerSettingInfo GetSageBannerSettingList(int PortalID, int UserModuleID)
    {
        try
        {
            SageBannerController objc = new SageBannerController();
            return objc.GetSageBannerSettingList(PortalID, UserModuleID, GetCurrentCulture());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void GetBannerImages(int BannerID, int UserModuleID, int PortalID, string CultureCode)
    {
        try
        {
            List<SageBannerInfo> objSageBannerLst = new List<SageBannerInfo>();
            SageBannerController obj = new SageBannerController();
            objSageBannerLst = obj.GetBannerImages(BannerID, UserModuleID, PortalID, CultureCode);
            StringBuilder elem = new StringBuilder();
            elem.Append("<ul id=\"sfSlider\">");
            if (objSageBannerLst.Count > 0)
            {
                foreach (SageBannerInfo banner in objSageBannerLst)
                {
                    if (banner.ImagePath.Length == 0)
                    {
                        elem.Append("<li>");
                        elem.Append(banner.HTMLBodyText);
                        elem.Append("</li>");
                    }
                    else
                    {
                        string target = "#";
                        string readmoreLink = "#";
                        if (banner.LinkToImage != string.Empty)
                        {
                            readmoreLink = banner.LinkToImage;
                            target = "_blank";
                        }
                        else if (banner.ReadMorePage != string.Empty)
                        {
                            readmoreLink = Fullpath + banner.ReadMorePage + Extension;
                        }
                        else
                        {
                            readmoreLink = Fullpath + banner.ReadMorePage + Extension;
                        }
                        elem.Append("<li style=\"position:relative; display:none;\">");
                        elem.Append("<div class='bannerImageWrapper'>");
                        elem.Append("<div class='sfImageholder'>");

                        //Responsive Images

                        elem.Append("<div data-alt=\"SageFrame Banner Images\" data-picture=\"\">");

                        elem.Append("<div data-media=\"(min-width: 0px)\" data-src=");
                        elem.Append(ResolveUrl(modulePath));
                        elem.Append("images/ThumbNail/Small/");
                        elem.Append(banner.ImagePath);
                        elem.Append("></div>");

                        elem.Append("<div data-media=\"(min-width: 320px)\" data-src=");
                        elem.Append(ResolveUrl(modulePath));
                        elem.Append("images/ThumbNail/Medium/");
                        elem.Append(banner.ImagePath);
                        elem.Append("></div>");

                        elem.Append("<div data-media=\"(min-width: 768px)\" data-src=");
                        elem.Append(ResolveUrl(modulePath));
                        elem.Append("images/ThumbNail/Large/");
                        elem.Append(banner.ImagePath);
                        elem.Append("></div>");

                        elem.Append("<div data-media=\"(min-width: 960px)\" data-src=");
                        elem.Append(ResolveUrl(modulePath));
                        elem.Append("images/ThumbNail/Default/");
                        elem.Append(banner.ImagePath);
                        elem.Append("></div>");

                        //elem.Append("<noscript><img alt=\"Sageframe Bannner Images\" src=\"");
                        //elem.Append(ResolveUrl(modulePath));
                        //elem.Append("images/ThumbNail/Default/");
                        //elem.Append(banner.ImagePath);
                        //elem.Append("/></noscript>");
                        elem.Append("</div>");
                        elem.Append("</div>");
                        SEOHelper seoHelper = new SEOHelper();
                        string unwantedTag = seoHelper.RemoveUnwantedHTMLTAG(banner.Description);
                        if (banner.Description != null && banner.Description.Trim() != string.Empty && banner.Description.Trim() != "" && unwantedTag.Trim().Length > 0)
                        {
                            elem.Append("<div  class='sfBannerDesc'><p>");
                            elem.Append(banner.Description + "</p>");
                            elem.Append("<a target=\" " + target + " \" class='sfReadmore' href=\"");
                            elem.Append(readmoreLink);
                            elem.Append("\">");
                            elem.Append("<span>");
                            elem.Append(banner.ReadButtonText);
                            elem.Append("</span></a></div></div></li>");
                        }
                        else
                        {
                            elem.Append("</li>");
                        }
                    }
                }
                bannerCount++;
            }
            else
            {
                bannerCount = 0;
                elem.Append("No Banner To Display");
            }
            elem.Append("</ul>");
            sageSlider.Text = elem.ToString();
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }
}
