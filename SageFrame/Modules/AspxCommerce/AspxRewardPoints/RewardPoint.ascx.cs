using AspxCommerce.Core;
using AspxCommerce.RewardPoints;
using SageFrame.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modules_AspxCommerce_AspxRewardPoints_RewardPoint : BaseAdministrationUserControl
{
    public string servicePath = "";
    public double rewardpoint = 0;
    public string GeneralSettings = string.Empty;
    private int StoreID, PortalID, CustomerID;
    private string UserName, CultureName;
    public string SessionCode = string.Empty;
    private AspxCommonInfo aspxCommonObj = new AspxCommonInfo();
    protected void Page_Load(object sender, EventArgs e)
    {

        servicePath = ResolveUrl(this.TemplateSourceDirectory);

        rewardpoint = CheckOutSessions.Get<double>("RewardPoints", 0);

        GetPortalCommonInfo(out StoreID, out PortalID, out CustomerID, out UserName, out CultureName, out SessionCode);
        aspxCommonObj = new AspxCommonInfo(StoreID, PortalID, UserName, CultureName, CustomerID, SessionCode);

        List<GeneralSettingInfo> lstGeneralSet = RewardPointsController.GetGeneralSetting(aspxCommonObj);

        StringBuilder scriptrewardPoint = new StringBuilder();

        if (lstGeneralSet.Count > 0)
        {
            GeneralSettings = new JavaScriptSerializer().Serialize(lstGeneralSet.FirstOrDefault()).ToString();
        }

    }
}