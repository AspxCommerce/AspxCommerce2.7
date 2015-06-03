/*
AspxCommerce® - http://www.aspxcommerce.com
Copyright (c) 2011-2015 by AspxCommerce

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
WITH THE SOFTWARE OR THE USE OF OTHER DEALINGS IN THE SOFTWARE. 
*/

using System;
using SageFrame.Web;
using AspxCommerce.Core;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using System.IO;
using System.Collections;

public partial class Modules_AspxCommerce_AspxCategoryLister_CategoryBreadCrumb : BaseAdministrationUserControl
{
    private int StoreID, PortalID, CustomerID;
    private string UserName, CultureName, SessionCode;
    private string sageRedirectPath, pageExtension, resolveUrl;
    private AspxCommonInfo aspxCommonObj;
    private Hashtable hst = null;

    public string Breadcrumb;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                IncludeCss("CategoryBreadCrumb", "/Templates/" + TemplateName + "/css/BreadCrumb/BreadCrumb.css");
                GetPortalCommonInfo(out StoreID, out PortalID, out CustomerID, out UserName, out CultureName, out SessionCode);
                aspxCommonObj = new AspxCommonInfo(StoreID, PortalID, UserName, CultureName, CustomerID, SessionCode);

                string pageName = Request.Url.ToString();
                pageName = Path.GetFileNameWithoutExtension(pageName);
                Breadcrumb = new JavaScriptSerializer().Serialize(AspxBreadCrumbController.GetBreadCrumb(pageName, aspxCommonObj));
                AppLocalized.getLocale(this.AppRelativeTemplateSourceDirectory);
            }
            IncludeLanguageJS();
            BreadCrumb();
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }

    #region Localization Method
    private string getLocale(string messageKey)
    {
        string retStr = messageKey;
        if (hst != null && hst[messageKey] != null)
        {
            retStr = hst[messageKey].ToString();
        }
        return retStr;
    }
    #endregion

    #region BreadCrumb
    private string[] urlSegments;
    private void BreadCrumb()
    {
        resolveUrl = ResolveUrl("~/");
        if (!IsParent)
        {
            sageRedirectPath = ResolveUrl(GetParentURL + "/portal/" + GetPortalSEOName + "/");
        }
        else
        {
            sageRedirectPath = resolveUrl;
        }
        pageExtension = SageFrameSettingKeys.PageExtension;

        urlSegments = System.Web.HttpContext.Current.Request.Url.Segments;
        string cat = urlSegments[1];

        StringBuilder breadCrumblist = new StringBuilder();

        if (urlSegments.Length > 0)
        {
            breadCrumblist.Append("<ul>");
            if (resolveUrl == "/")
            {
                if (urlSegments.Length <= 2)
                    breadCrumblist.Append(switchCase("", urlSegments[1], ""));
                else
                    breadCrumblist.Append(switchCase(cat, urlSegments[2], cat));
            }
            else
            {
                if (urlSegments.Length <= 3)
                    breadCrumblist.Append(switchCase("", urlSegments[2], ""));
                else
                    breadCrumblist.Append(switchCase(urlSegments[2], urlSegments[3], urlSegments[2]));
                    
            }
            breadCrumblist.Append("</ul>");
            breadcrumb.InnerHtml = breadCrumblist.ToString();
        }
        else
        {
            breadcrumb.Visible = false;
            breadcrumb.InnerHtml = "";
        }
    }
    #endregion

    #region GetCategoryOnly
    private string GetCategoryOnly(List<BreadCrumInfo> bcfList, bool reverse)
    {
        StringBuilder breadCrumbsLi = new StringBuilder();
        if (bcfList.Count > 0)
        {
            breadCrumbsLi.Append("<li class=\"first\"><a href=");
            breadCrumbsLi.Append(sageRedirectPath);
            breadCrumbsLi.Append("home");
            breadCrumbsLi.Append(pageExtension);
            breadCrumbsLi.Append(" class=\"i-home\">home</a></li>");
            if (reverse)
                bcfList.Reverse();
            for (int i = 0; i < bcfList.Count - 1; i++)
            {
                breadCrumbsLi.Append("<li><a href=\"");
                breadCrumbsLi.Append(sageRedirectPath);
                breadCrumbsLi.Append("category/");
                breadCrumbsLi.Append(AspxUtility.fixedEncodeURIComponent(bcfList[i].TabPath));
                breadCrumbsLi.Append(pageExtension);
                breadCrumbsLi.Append("\">");
                breadCrumbsLi.Append(bcfList[i].TabPath);
                breadCrumbsLi.Append("</a></li>");
            }
            breadCrumbsLi.Append("<li class=\"last\">");
            breadCrumbsLi.Append(System.Web.HttpUtility.HtmlDecode(bcfList[bcfList.Count - 1].TabPath));
            breadCrumbsLi.Append("</li>");
        }
        return breadCrumbsLi.ToString();
    }
    #endregion

    #region listBuilder
    private StringBuilder listBuilder(string breadCrumbTitle)
    {
        StringBuilder breadCrumb = new StringBuilder();
        breadCrumb.Append("<li class=\"first\"><a href=");
        breadCrumb.Append(sageRedirectPath);
        breadCrumb.Append("home");
        breadCrumb.Append(pageExtension);
        breadCrumb.Append(" class=\"i-home\">");
        breadCrumb.Append(getLocale("home"));
        breadCrumb.Append("</a></li>");
        breadCrumb.Append("<li class=\"last\">");
        breadCrumb.Append(getLocale(breadCrumbTitle));
        breadCrumb.Append("</li>");
        return breadCrumb;
    }
    #endregion

    #region SageBreadCrumb
    private string GetSageBreadCrumb(string breadCrumbTitle)
    {
        List<BreadCrumInfo> breadCrumbList = AspxBreadCrumbController.GetBreadCrumb(breadCrumbTitle, aspxCommonObj);
        StringBuilder breadCrumbListHtml = new StringBuilder();
        if (breadCrumbList.Count > 0)
        {
            breadCrumbListHtml.Append("<li class=\"first\"><a href=");
            breadCrumbListHtml.Append(sageRedirectPath);
            breadCrumbListHtml.Append("home");
            breadCrumbListHtml.Append(pageExtension);
            breadCrumbListHtml.Append(" class=\"i-home\">");
            breadCrumbListHtml.Append("home</a></li>");
            for (int i = 1; i < breadCrumbList.Count - 1; i++)
            {
                breadCrumbListHtml.Append("<li><a href=\"");
                breadCrumbListHtml.Append(sageRedirectPath);
                breadCrumbListHtml.Append(AspxUtility.fixedEncodeURIComponent(breadCrumbList[i].TabPath));
                breadCrumbListHtml.Append(pageExtension);
                breadCrumbListHtml.Append("\" >");
                breadCrumbListHtml.Append(breadCrumbList[i].TabPath);
                breadCrumbListHtml.Append("</a></li>");
            }
            breadCrumbListHtml.Append("<li class=\"last\">");
            breadCrumbListHtml.Append(AspxUtility.fixedDecodeURIComponent(breadCrumbList[breadCrumbList.Count - 1].TabPath));
            breadCrumbListHtml.Append("</li>");
        }
        return breadCrumbListHtml.ToString();
    }
    #endregion

    private string switchCase(string breadCrumbApplicationPath, string breadCrumbFileName, string compareSegment)
    {
        breadCrumbApplicationPath = AspxUtility.fixedDecodeURIComponent(System.Web.HttpUtility.UrlDecode(breadCrumbApplicationPath).Replace("/", "").Trim());
        breadCrumbFileName = AspxUtility.fixedDecodeURIComponent(System.Web.HttpUtility.UrlDecode(breadCrumbFileName).Replace("/", "").Trim()).Split('.')[0];

        StringBuilder breadCrumblist = new StringBuilder();
        List<BreadCrumInfo> list = new List<BreadCrumInfo>();
        string current = string.Empty;
        if (breadCrumbApplicationPath == "category")
        {
            list = AspxBreadCrumbProvider.GetCategoryName(breadCrumbFileName, aspxCommonObj);
            breadCrumblist.Append(GetCategoryOnly(list, true));
        }
        else if (breadCrumbApplicationPath == "item")
        {
            list = AspxBreadCrumbProvider.GetItemCategories(breadCrumbFileName, aspxCommonObj);
            breadCrumblist.Append(GetCategoryOnly(list, false));
        }
        else if (breadCrumbApplicationPath == "tagsitems")
        {
            breadCrumblist.Append(listBuilder("Tags"));
        }
        else if (breadCrumbApplicationPath == "Search")
        {
            breadCrumblist.Append(listBuilder("Search"));
        }
        else if (breadCrumbApplicationPath == "option")
        {
            breadCrumblist.Append(listBuilder("Shopping Options"));
        }
        else if (breadCrumbApplicationPath == "brand" || breadCrumbApplicationPath == "service")
        {
            breadCrumblist.Append(listBuilder(breadCrumbFileName));
        }
        else if(string.IsNullOrEmpty(breadCrumbApplicationPath))
        {
            if (resolveUrl == "/")
            {
                current = urlSegments[1].Split('.')[0].Replace("/", "").Trim();
                if (current != "portal")
                {
                    current = AspxUtility.fixedDecodeURIComponent(current);
                    if (current.ToLower() != "default" && current.ToLower() != "home")
                    {
                        breadCrumblist.Append(GetSageBreadCrumb(current));
                    }
                }
                else
                {
                    current = urlSegments[urlSegments.Length - 1].Split('.')[0].Replace("/", "").Trim();
                    current = AspxUtility.fixedDecodeURIComponent(current);
                    if (current.ToLower() != "default" && current.ToLower() != "home")
                    {
                        breadCrumblist.Append(GetSageBreadCrumb(current));
                    }
                }
            }
            else
            {
                current = urlSegments[urlSegments.Length - 1].Split('.')[0].Replace("/", "").Trim();
                if (current != "portal")
                {
                    current = AspxUtility.fixedDecodeURIComponent(current);
                    if (current.ToLower() != "default" && current.ToLower() != "home")
                    {
                        breadCrumblist.Append(GetSageBreadCrumb(current));
                    }
                }
                else
                {
                    current = urlSegments[3].Split('.')[0].Replace("/", "").Trim();
                    current = AspxUtility.fixedDecodeURIComponent(current);
                    if (current.ToLower() != "default" && current.ToLower() != "home")
                    {
                        breadCrumblist.Append(GetSageBreadCrumb(current));
                    }
                }
            }
        }
        else if (resolveUrl == "/")
            {
                if (compareSegment.Replace("/", "").Trim() == "portal")
                {
                    if (urlSegments.Length <= 4)
                    {
                        breadCrumblist.Append(switchCase("", urlSegments[3], ""));
                    }
                    else
                    {
                        breadCrumblist.Append(switchCase(urlSegments[3], urlSegments[4], urlSegments[3]));
                    }
                }
                else
                {
                    string urlValue = urlSegments[3].Split('.')[0].Replace("/", "").Trim();
                    if (!string.IsNullOrEmpty(urlValue) && urlValue != "default" && urlValue.ToLower() != "home")
                    {
                        breadCrumblist.Append(GetSageBreadCrumb(urlValue));
                    }
                }
            }
            else
            {
                if (compareSegment.Replace("/", "").Trim() == "portal")
                {
                    if (urlSegments.Length <= 5)
                    {
                        breadCrumblist.Append(switchCase("", urlSegments[4], ""));
                    }
                    else
                    {
                        breadCrumblist.Append(switchCase(urlSegments[4], urlSegments[5], urlSegments[4]));
                    }
                }
                else
                {
                    string urlValue = urlSegments[4].Split('.')[0].Replace("/", "").Trim();
                    if (!string.IsNullOrEmpty(urlValue) && urlValue != "default" && urlValue.ToLower() != "home")
                    {
                        breadCrumblist.Append(GetSageBreadCrumb(urlValue));
                    }
                }
            }
        return breadCrumblist.ToString();
    }
}