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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using SageFrame.UserManagement;
using SageFrame.Web;
//using System.Web.Script.Serialization;


using SageFrame.Framework;
using System.Text;

namespace SageFrame
{
    /// <summary>
    /// Webservice which can connect to remove API from server side.
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class SageFrameWebService : WebService
    {
        /// <summary>
        /// Returns username list for given text and given number of user.
        /// </summary>
        /// <param name="prefixText">Text.</param>
        /// <param name="count">Count.</param>
        /// <returns>Array of string containing username.</returns>
        [WebMethod]
        public string[] GetUsernameList(string prefixText, int count)
        {
            List<string> items = new List<string>();
            try
            {
                BaseUserControl suc = new BaseUserControl();
                List<UserManagementInfo> objList = UserManagementController.GetUsernameByPortalIDAuto(prefixText, 12, count, suc.GetUsername);
                foreach (UserManagementInfo objInfo in objList)
                {
                    items.Add(objInfo.UserName);
                }
                return items.ToArray();
            }
            catch
            {
                return items.ToArray();
            }
        }

        //[WebMethod]
        //public string UpdateViewCount(int FAQID,int usermoduleID,int portalID)
        //{
        //    string question = string.Empty;
        //    int count = 0;
        //    try
        //  {
        //        FAQsDataContext dbFAQ = new FAQsDataContext(SystemSetting.SageFrameConnectionString);
        //        var FAQs = dbFAQ.sp_FAQsViewCountUpdate(FAQID, true, portalID);
        //        foreach (sp_FAQsViewCountUpdateResult faq in FAQs)
        //        {
        //            count = faq.ViewCount;
        //        }
        //        BaseUserControl suc = new BaseUserControl();
        //       var faqAll = dbFAQ.sp_GetFAQWithTemplate(usermoduleID, true, true, portalID, suc.GetUsername);
        //        foreach (sp_GetFAQWithTemplateResult faqs in faqAll)
        //        {
        //            if (FAQID == (int)faqs.FAQID)
        //            {
        //                question = faqs.QUESTION;
        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return question;
        //}
    }
}
