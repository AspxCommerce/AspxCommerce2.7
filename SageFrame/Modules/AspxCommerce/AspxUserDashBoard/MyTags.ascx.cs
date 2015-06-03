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
using System.Collections;
using AspxCommerce.Core;
using System.Collections.Generic;
using System.Text;

public partial class Modules_AspxMyTags_MyTags : BaseAdministrationUserControl
{
    public int storeID, portalID, customerID;

    public string sessionCode = string.Empty;

    public string cultureName, userName;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            AspxCommonInfo aspxCommonObj = new AspxCommonInfo();
            GetPortalCommonInfo(out storeID, out portalID, out customerID, out userName, out cultureName, out sessionCode);
            aspxCommonObj = new AspxCommonInfo(storeID, portalID, userName, cultureName, customerID, sessionCode);

            GetTagsByUserName(aspxCommonObj);
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }


    }
    Hashtable hst = null;
    public void GetTagsByUserName(AspxCommonInfo aspxCommonObj)
    {
        string modulePath = this.AppRelativeTemplateSourceDirectory;
        hst = AppLocalized.getLocale(modulePath);
        string pageExtension = SageFrameSettingKeys.PageExtension;
        List<TagDetailsInfo> lstTagDetail = AspxTagsController.GetTagsByUserName(aspxCommonObj);
        StringBuilder MyTags = new StringBuilder();
        MyTags.Append("<div id=\"divMyTags\">");
        MyTags.Append("<ul>");

        if (lstTagDetail != null && lstTagDetail.Count > 0)
        {
            foreach (TagDetailsInfo value in lstTagDetail)
            {
                MyTags.Append("<li class=\"tag_content\"><a href=\"");
                MyTags.Append(aspxRedirectPath);
                MyTags.Append("TaggedItem");
                MyTags.Append(pageExtension);
                MyTags.Append("?tagsId=");
                MyTags.Append(value.ItemTagIDs);
                MyTags.Append("\"><span>");
                MyTags.Append(value.Tag);
                MyTags.Append("</span></a>");
                MyTags.Append("<button type=\"button\" class=\"cssClassCross\" value=");
                MyTags.Append(value.ItemTagIDs);
                MyTags.Append(" onclick =\"Tags.DeleteMyTag(this)\"><span><i class=\"i-erase\"></i>");
                MyTags.Append(getLocale(""));
                MyTags.Append("</span></button></li>");
            }
        }
        else
        {
            MyTags.Append("<span class=\"cssClassNotFound\">");
            MyTags.Append(getLocale("Your tag list is empty!"));
            MyTags.Append("</span>");
        }
        MyTags.Append("</ul><div class=\"cssClassclear\"></div>");
        MyTags.Append("</div>");
        ltrMyTags.Text = MyTags.ToString();
    }

    private string getLocale(string messageKey)
    {
        string retStr = messageKey;
        if (hst != null && hst[messageKey] != null)
        {
            retStr = hst[messageKey].ToString();
        }
        return retStr;
    }
}
