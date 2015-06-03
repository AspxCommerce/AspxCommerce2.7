#region
/*
SageFrame® - http://www.sageframe.com
Copyright (c) 2009-2010 by SageFrame
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
WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SageFrame.Web;
using System.IO;
using SageFrame.Common.Shared;
using System.Text;
using SageFrame.BreadCrum;

namespace SageFrame.Controls
{
    public partial class ctl_AdminBreadCrum : BaseUserControl
    {
        public int PortalID = 0;
        public int MenuID = 0;
        public string PageName = string.Empty, AppPath = string.Empty, pagePath = string.Empty;
        string Extension;
        protected void Page_Init(object sender, EventArgs e)
        {
            Initialize();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Extension = SageFrameSettingKeys.PageExtension;
            //pagePath = ResolveUrl(GetParentURL) + GetReduceApplicationName;
            pagePath = GetHostURL() + "/";
            pagePath = IsParent ? pagePath : pagePath + "portal/" + GetPortalSEOName + "/";
            BuildBreadCrumb();
        }

        public void Initialize()
        {
            PortalID = GetPortalID;
        }

        public void BuildBreadCrumb()
        {
            string breadcrumb = string.Empty;
            PageName = Path.GetFileNameWithoutExtension(PagePath);
            List<BreadCrumInfo> BreadCurmList = new List<BreadCrumInfo>();
            BreadCrumDataProvider dp = new BreadCrumDataProvider();
            BreadCurmList = dp.GetBreadCrumb(PageName, PortalID, MenuID, GetCurrentCulture());
            if (BreadCurmList != null)
            {
                //breadcrumb = obj.TabPath != "" ? obj.TabPath : "";
                // string[] arrPages = breadcrumb.Split('/');
                StringBuilder html = new StringBuilder();
                html.Append("<ul>");
                int length = breadcrumb.Length;
                string childPages = "";
                int index = 0;
                foreach (BreadCrumInfo item in BreadCurmList)
                {
                    if (index != 0)
                    {
                        if (item.TabPath != string.Empty)
                        {
                            childPages += item.TabPath + "/";
                            childPages = childPages.Substring(0, childPages.Length - 1);
                            var pageLink = pagePath + childPages + SageFrameSettingKeys.PageExtension;
                            if (item.TabPath == "Admin")
                            {
                                pageLink = pagePath + "Admin/Admin" + Extension;
                            }
                            if (item.TabPath.IndexOf("Super-User") > -1)
                            {
                                pageLink = pagePath + "Admin/Admin" + Extension;
                            }
                            childPages += "/";
                            if (index == length - 1)
                            {
                                if (item.TabPath == "Admin" || item.TabPath == "Super-User")
                                {
                                    html.Append("");
                                }
                                else
                                {
                                    if (BreadCurmList.Count() == index + 1)
                                        html.Append("<li><a href=" + pageLink + ">" + item.TabPath.Replace("-", " ") + "</a></li>");
                                    else
                                    html.Append("<li><span>" + item.TabPath.Replace("-", " ") + "</span></li>");
                                }
                            }
                            else
                            {
                                if (item.TabPath == "Admin" || item.TabPath == "Super-User")
                                {
                                    var homeimage = (Request.ApplicationPath != "/" ? Request.ApplicationPath : "") + "/Administrator/Templates/default/images/home-icon.png";
                                    html.Append("<li class='sfFirst'><a href=" + pageLink + "><i class='icon-home' ></i></a></li>");
                                    //html.Append("<li class='sfFirst'><span><i class='icon-home' ></i></span></li>");
                                }
                                else
                                {
                                    if (item.LocalPage != "")
                                    {
                                        if (BreadCurmList.Count() == index + 1)
                                            html.Append("<li><a href=" + pageLink + ">" + item.LocalPage.Replace("-", " ") + "</a></li>");
                                        else
                                            html.Append("<li><span>" + item.LocalPage.Replace("-", " ") + "</span></li>");
                                    }
                                    else
                                    {
                                        if (BreadCurmList.Count() == index + 1)
                                            html.Append("<li><a href=" + pageLink + ">" + item.TabPath.Replace("-", " ") + "</a></li>");
                                        else
                                        html.Append("<li><span>" + item.TabPath.Replace("-", " ") + "</span></li>");
                                    }
                                }
                            }
                        }
                    }
                    index++;
                }
                html.Append("</ul>");
                ltrBreadCrumb.Text = html.ToString();
            }
        }
    }
}