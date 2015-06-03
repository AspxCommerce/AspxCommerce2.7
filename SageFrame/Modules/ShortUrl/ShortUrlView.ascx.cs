using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SageFrame.Core;
using SageFrame.Web;
public partial class Modules_ShortUrl_ShortUrlView : BaseAdministrationUserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnGenerateShortUrl_Click(object sender, EventArgs e)
    {
        ShortUrlController controller = new ShortUrlController();
        string encodedUrl = controller.EncodeUrl(txtUrl.Text.Trim());
        hypCode.Text = encodedUrl;
        hypCode.NavigateUrl = encodedUrl;
    }
}