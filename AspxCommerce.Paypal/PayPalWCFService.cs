using System;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Collections.Generic;
using SageFrame.Web.Utilities;
using AspxCommerce.PayPal;

[ServiceContract(Namespace = "")]
[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
public class PayPalWCFService
{
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<CartInfoforPaypal> GetCartDetails(int storeID, int portalID, int customerID, string userName,
                                                  string cultureName, string sessionCode)
    {
        var ParaMeter = new List<KeyValuePair<string, object>>();
        ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
        ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
        ParaMeter.Add(new KeyValuePair<string, object>("@CustomerID", customerID));
        ParaMeter.Add(new KeyValuePair<string, object>("@UserName", userName));
        ParaMeter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
        ParaMeter.Add(new KeyValuePair<string, object>("@SessionCode", sessionCode));
        var sqLH = new SQLHandler();
        return sqLH.ExecuteAsList<CartInfoforPaypal>("usp_Aspx_GetCartDetails", ParaMeter);
    }

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<PayPalSettingInfo> GetAllPayPalSetting(int paymentGatewayID, int storeId, int portalId)
    {
        try
        {
            var parameterCollection = new List<KeyValuePair<string, object>>();
            parameterCollection.Add(new KeyValuePair<string, object>("@PaymentGatewayTypeID", paymentGatewayID));
            parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", storeId));
            parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", portalId));
            var sqLH = new SQLHandler();
            return sqLH.ExecuteAsList<PayPalSettingInfo>("usp_Aspx_PaypalSettingsGetAll", parameterCollection);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}
