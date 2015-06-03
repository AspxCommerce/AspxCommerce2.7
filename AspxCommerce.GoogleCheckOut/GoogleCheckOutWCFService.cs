using System;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Collections.Generic;
using SageFrame.Web.Utilities;
using AspxCommerce.GoogleCheckOut;

[ServiceContract(Namespace = "")]
[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
public class GoogleCheckOutWCFService
{
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<CartInfoforGoogleCheckOut> GetCartDetailsForPG(int storeID, int portalID, int customerID, string userName, string cultureName, string sessionCode, string country, string state, string zip, int addressID)
    {
        List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
        ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
        ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
        ParaMeter.Add(new KeyValuePair<string, object>("@CustomerID", customerID));
        ParaMeter.Add(new KeyValuePair<string, object>("@UserName", userName));
        ParaMeter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
        ParaMeter.Add(new KeyValuePair<string, object>("@SessionCode", sessionCode));
        ParaMeter.Add(new KeyValuePair<string, object>("@Country", country));
        ParaMeter.Add(new KeyValuePair<string, object>("@State", state));
        ParaMeter.Add(new KeyValuePair<string, object>("@Zip", zip));
        ParaMeter.Add(new KeyValuePair<string, object>("@AddressID", addressID));
        SQLHandler sqLH = new SQLHandler();
        return sqLH.ExecuteAsList<CartInfoforGoogleCheckOut>("usp_Aspx_GetCartDetailsForPG", ParaMeter);
    }

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<GoogleCheckOutSettingInfo> GetAllGoogleCheckOutSetting(int paymentGatewayID, int storeId, int portalId)
    {
        try
        {
            List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
            parameterCollection.Add(new KeyValuePair<string, object>("@PaymentGatewayTypeID", paymentGatewayID));
            parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", storeId));
            parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", portalId));
            SQLHandler sqLH = new SQLHandler();
            return sqLH.ExecuteAsList<GoogleCheckOutSettingInfo>("usp_Aspx_GoogleCheckOutSettingsGetAll", parameterCollection);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<GoogleCheckOutSettingKeyInfo>GoogleCheckOutSettingKey()
    {
        try
        {

            SQLHandler sqLH = new SQLHandler();
            return sqLH.ExecuteAsList<GoogleCheckOutSettingKeyInfo>("[usp_Aspx_GoogleCheckOutSettingsKey]");
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}
