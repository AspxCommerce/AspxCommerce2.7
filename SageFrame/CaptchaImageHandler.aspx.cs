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
using System.Drawing.Imaging;
using SageFrame.Web;
using SageFrame.Framework;
using SageFrame.Common;
#endregion

namespace SageFrame
{
    public partial class CaptchaImageHandler : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                CaptchaImage ci = new CaptchaImage(this.Session[SessionKeys.CaptchaImageText].ToString(), 200, 50, "SageFrame BrainDigit");
                this.Response.Clear();
                this.Response.ContentType = "image/jpeg";
                ci.Image.Save(this.Response.OutputStream, ImageFormat.Jpeg);
                ci.Dispose();
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }
    }
}
