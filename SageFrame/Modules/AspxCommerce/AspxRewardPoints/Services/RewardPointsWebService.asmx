<%@ WebService Language="C#" Class="RewardPointsWebService" %>

/*
AspxCommerce® - http://www.aspxcommerce.com
Copyright (c) 20011-2012 by AspxCommerce
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
using System.Web;
using System.Web.Services;
using System.Collections.Generic;
using AspxCommerce.Core;
using SageFrame.Web.Utilities;
using AspxCommerce.RewardPoints;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class RewardPointsWebService : System.Web.Services.WebService
{

    [WebMethod]
    public string HelloWorld()
    {
        return "Hello World";
    }

    [WebMethod]
    public void RewardPointsSaveGeneralSettings(GeneralSettingsCommonInfo generalSettingobj, AspxCommonInfo aspxCommonObj)
    {
        try
        {
            RewardPointsController.RewardPointsSaveGeneralSettings(generalSettingobj, aspxCommonObj);
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }

    [WebMethod]
    public List<GeneralSettingInfo> GetGeneralSetting(AspxCommonInfo aspxCommonObj)
    {
        try
        {
            List<GeneralSettingInfo> lstGeneralSettingsInfo = RewardPointsController.GetGeneralSetting(aspxCommonObj);
            return lstGeneralSettingsInfo;

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [WebMethod]
    public void RewardPointsSaveUpdateNewRule(RewardPointsCommonInfo rewardPointsCommonObj, AspxCommonInfo aspxCommonObj)
    {
        try
        {
            RewardPointsController.RewardPointsSaveUpdateNewRule(rewardPointsCommonObj, aspxCommonObj);

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [WebMethod]
    public List<RewardPointsSettingInfo> RewardPointsSettingGetAll(int offset, int limit, AspxCommonInfo aspxCommonObj, RewardPointsCommonInfo rewardPointsCommonObj)
    {
        try
        {
            List<RewardPointsSettingInfo> lstRewardPointsSettingInfo = RewardPointsController.RewardPointsSettingGetAll(offset, limit, aspxCommonObj, rewardPointsCommonObj);
            return lstRewardPointsSettingInfo;


        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [WebMethod]
    public void RewardPointsRuleDelete(RewardPointsCommonInfo rewardPointCommonObj, AspxCommonInfo aspxCommonObj)
    {
        try
        {
            RewardPointsController.RewardPointsRuleDelete(rewardPointCommonObj, aspxCommonObj);

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [WebMethod]
    public List<RewardRuleListInfo> RewardPointsRuleListBind(AspxCommonInfo aspxCommonObj)
    {
        try
        {
            List<RewardRuleListInfo> rulelist = RewardPointsController.RewardPointsRuleListBind(aspxCommonObj);
            return rulelist;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [WebMethod]
    public List<MyRewardPointsInfo> GetMyRewardPointsHistory(int offset, int limit, AspxCommonInfo aspxCommonObj, RewardPointsHistoryCommonInfo RewardPointsHistoryCommonObj)
    {
        try
        {

            List<MyRewardPointsInfo> history = RewardPointsController.GetMyRewardPointsHistory(offset, limit,
                                                                                                 aspxCommonObj,
                                                                                                 RewardPointsHistoryCommonObj);
            return history;

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [WebMethod]
    public List<RewardPointsHistryInfo> RewardPointsHistoryGetAll(int offset, int limit, AspxCommonInfo aspxCommonObj, RewardPointsHistoryCommonInfo RewardPointsHistoryCommonObj)
    {
        try
        {
            List<RewardPointsHistryInfo> histryInfos = RewardPointsController.RewardPointsHistoryGetAll(offset, limit,
                                                                                                          aspxCommonObj,
                                                                                                          RewardPointsHistoryCommonObj);
            return histryInfos;

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [WebMethod]
    public void RewardPointsSaveNewsLetter(RewardPointsNLCommonInfo rewardPointsInfo, AspxCommonInfo aspxCommonObj)
    {
        try
        {
            RewardPointsController.RewardPointsSaveNewsLetter(rewardPointsInfo, aspxCommonObj);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    [WebMethod]
    public void RewardPointsSavePolling(RewardPointsPollCommonInfo rewardPointsInfo, AspxCommonInfo aspxCommonObj)
    {
        try
        {
            RewardPointsController.RewardPointsSavePolling(rewardPointsInfo, aspxCommonObj);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [WebMethod]
    public void RewardPointsDeleteNewsLetter(RewardPointsNLCommonInfo rewardPointsInfo, AspxCommonInfo aspxCommonObj)
    {
        try
        {
            RewardPointsController.RewardPointsDeleteNewsLetter(rewardPointsInfo, aspxCommonObj);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [WebMethod(EnableSession = true)]
    public void SetSessionVariable(string key, object value)
    {
        HttpContext.Current.Session[key] = value;
    }

    //[WebMethod(EnableSession = true)]
    //public bool RewardPointsIschecked(string key)
    //{
    //    if (System.Web.HttpContext.Current.Session[key] != null)
    //    {
    //        string i = System.Web.HttpContext.Current.Session[key].ToString();
    //        return Convert.ToBoolean(i.ToString());
    //    }
    //    else
    //    {
    //        return false;
    //    }

    //}

    [WebMethod(EnableSession = true)]
    public decimal RewardPointsSelectedValue(string key)
    {
        if (System.Web.HttpContext.Current.Session[key] != null)
        {
            string i = System.Web.HttpContext.Current.Session[key].ToString();
            return Convert.ToDecimal(i.ToString());
        }
        else
        {
            return 0;
        }

    }

    [WebMethod]
    public bool IsPurchaseActive(AspxCommonInfo aspxCommonObj)
    {
        try
        {
            bool isPurchaseActive = RewardPointsController.IsPurchaseActive(aspxCommonObj);
            return isPurchaseActive;
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }

    [WebMethod]
    public bool RewardPointsGeneralSettingsIsActive(AspxCommonInfo aspxCommonObj)
    {
        try
        {
            bool IsActive = RewardPointsController.RewardPointsGeneralSettingsIsActive(aspxCommonObj);
            return IsActive;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    
    
}

