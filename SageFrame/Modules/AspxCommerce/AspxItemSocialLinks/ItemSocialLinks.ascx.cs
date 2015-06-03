using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SageFrame.Web;

public partial class Modules_AspxCommerce_AspxItemSocialLinks_ItemSocialLinks :BaseUserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        IncludeCss("ItemSocialLinks", "/Modules/AspxCommerce/AspxItemSocialLinks/css/smpl-share.css");
        IncludeJs("ItemSocialLinks","/Modules/AspxCommerce/AspxItemSocialLinks/Js/smpl-share.js");
    }
}