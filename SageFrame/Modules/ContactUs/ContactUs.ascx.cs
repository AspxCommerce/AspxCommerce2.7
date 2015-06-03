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
using System.Web.UI;
using System.Web.UI.WebControls;
using SageFrame.Web;
using SageFrame.Framework;
using SageFrame.ContactUs;
using SageFrame.Message;

namespace SageFrame.Modules.ContactUs
{
    public partial class ContactUs : BaseAdministrationUserControl
    {
        public string ModulePath = "";
        public int UserModuleID = 0;
        public int PortalID = 0;
        public string UserName = "";
        public string messageSubject = string.Empty;
        public string emailSucessMsg = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    IncludeCss("ContactUs", "/Modules/ContactUs/css/module.css", "/css/jquery.alerts.css");
                    IncludeJs("ContactUs", "/Modules/ContactUs/js/ContactUs.js", "/js/jquery.validate.js", "/js/jquery.alerts.js");
                    IncludeLanguageJS();
                    UserName = GetUsername;
                    UserModuleID = Int32.Parse(SageUserModuleID);
                    PortalID = GetPortalID;
                    ModulePath = ResolveUrl(this.AppRelativeTemplateSourceDirectory);
                    messageSubject = GetSageMessage("ContactUs", "ContactUsEmailSubject");
                    emailSucessMsg = GetSageMessage("ContactUs", "ContactUsIsAddedSuccessfully");
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }
    }
}