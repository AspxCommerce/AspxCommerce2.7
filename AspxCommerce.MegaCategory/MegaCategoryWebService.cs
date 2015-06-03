using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using AspxCommerce.Core;
using AspxCommerce.MegaCategory;

/// <summary>
/// Summary description for MegaCategoryWebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
 [System.Web.Script.Services.ScriptService]
public class MegaCategoryWebService : System.Web.Services.WebService
{

    public MegaCategoryWebService()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public MegaCategorySettingInfo GetMegaCategorySetting(AspxCommonInfo aspxCommonObj)
    {
        try
        {
            MegaCategoryController mega = new MegaCategoryController();
            MegaCategorySettingInfo lstGetMegaCategorySetting = mega.GetMegaCategorySetting(aspxCommonObj);
            return lstGetMegaCategorySetting;

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    [WebMethod]
    public List<MegaCategorySettingInfo> MegaCategorySettingUpdate(string SettingValues, string SettingKeys, AspxCommonInfo aspxCommonObj)
    {
        try
        {
            MegaCategoryController mega = new MegaCategoryController();
            List<MegaCategorySettingInfo> lstGetMegaCategorySetting = mega.MegaCategorySettingUpdate(SettingValues, SettingKeys, aspxCommonObj);
            return lstGetMegaCategorySetting;

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

}

