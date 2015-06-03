using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AspxCommerce.Core;
using AspxCommerce.USPS;
using SageFrame.Web;
using SageFrame.Web.Utilities;
using System.Collections;

public partial class Modules_AspxCommerce_AspxShipmentsManagement_ShipmentLabelProcessor : BaseAdministrationUserControl
{
    private int _storeId, _portalId, _orderId;
    private string _cultureName;
    private string _dimensionUnit;
    private decimal _totalWeight;
    private List<ItemDetail> _itemslist;
    private string _weightUnit;
    
    Hashtable hst = null;
    
    

    protected void Page_Load(object sender, EventArgs e)
    {

        _storeId = GetStoreID;        _portalId = GetPortalID;
        _cultureName = GetCurrentCultureName;
        
        GetStoreSettings(_storeId, _portalId, GetCurrentCultureName);
        if (Request.QueryString["oid"] != null)
        {
            _orderId = Convert.ToInt32(Request.QueryString["oid"]);
            lblErrorMessage.Text = "";
            if (CheckLabelExist(_orderId))
            {
                DisplayLabel();
                ClearSession();
            }
            else
            {
                GetOrderDetails(_orderId);
            }
        }
        else
        {
            ShowError("Please submit valid data!");
                   }
        IncludeLanguageJS();
        
    }
    

    #region Real Time Shipping Label Creation

    private void DisplayLabel()
    {
        string url = ResolveUrl("~/Modules/AspxCommerce/AspxShipmentsManagement/LabelPreview.aspx");
        url = url + "?isfile=false&oid=" + _orderId + "&sid=" + _storeId + "&pid=" + _portalId;

                      string ifrm =
            string.Format("<iframe id={3} src={0}  frameborder='no' scrolling='auto' height={1} width={2} ></iframe>",
                          url, "500px",
                          "100%", "ifLabelPreview");
        string div =
            "<div><input type=\"button\" value=\"Reload\" onclick=\"document.getElementById('ifLabelPreview').contentWindow.location.reload(true);\" /></div>";
        ltLabelPreview.Text = ifrm + div;
    }

    private bool CheckLabelExist(int orderId)
    {
        try
        {
            AspxShipProviderMgntController ctl = new AspxShipProviderMgntController();
            AspxCommonInfo commonObj = new AspxCommonInfo();
            commonObj.StoreID = _storeId;
            commonObj.PortalID = _portalId;
            bool isExist = ctl.IsShippingLabelCreated(orderId, commonObj);
            return isExist;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

       private void GetOrderDetails(int orderId)
    {
        try
        {
            List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>
                                                               {new KeyValuePair<string, object>("@OrderId", orderId)};
            parameter.Add(new KeyValuePair<string, object>("@CultureName", _cultureName));
            SQLHandler sqlH = new SQLHandler();
            OrderLabel orderDetail = sqlH.ExecuteAsObject<OrderLabel>("usp_Aspx_GetOrderDetailByOrderIdForLabel",
                                                                      parameter);
            if (orderDetail != null)
            {
                Session["labelOrderInfo"] = orderDetail;
                string path = GetProviderSourceLocation(orderDetail.ShippingMethodId);
                               if (!string.IsNullOrEmpty(path))
                {
                    string controlPath = ResolveUrl(@"~/" + path);
                    BaseAdministrationUserControl uc = (BaseAdministrationUserControl)Page.LoadControl(controlPath);
                    phShippingLabelHolder.Controls.Add(uc);
                }
                else
                {
                                       dvCustomLabelCreater.Visible = true;
                    ClearSession(orderId);
                    GetOrderDetails(_storeId, _portalId, orderId, GetCurrentCultureName);
                }
            }
            else {
                ShowError("shipping label is not required for this order!");
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private string GetProviderSourceLocation(int shippingMethodId)
    {
        try
        {
            List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
            parameter.Add(new KeyValuePair<string, object>("@ShippingMethodId", shippingMethodId));
            SQLHandler sqlH = new SQLHandler();
            return sqlH.ExecuteAsScalar<string>("usp_Aspx_GetShippingProviderLocation", parameter);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    #endregion


    #region Custom Label Creation

    private void ClearSession()
    {
        if (Session["labelOrderInfo"] != null)
            Session.Remove("labelOrderInfo");
        if (Session["sl_toAddress"] != null)
            Session.Remove("sl_toAddress");
        if (Session["sl_items"] != null)
            Session.Remove("sl_items");
        if (Session["sl_pid"] != null)
            Session.Remove("sl_pid");
        if (Session["sl_frAddress"] != null)
            Session.Remove("sl_frAddress");
        if (Session["sl_setting"] != null)
            Session.Remove("sl_setting");
    }

    private void ClearSession(int orderId)
    {
                      if (Session["prev_oid"] == null)
        {
            Session["prev_oid"] = orderId;
        }
        else
        {
            if (int.Parse(Session["prev_oid"].ToString()) != orderId)
            {
                Session.Remove("labelOrderInfo");
                Session.Remove("sl_toAddress");
                Session.Remove("sl_items");
                Session.Remove("sl_pid");
                Session.Remove("sl_frAddress");
                Session.Remove("sl_setting");
                Session["prev_oid"] = orderId;
            }
        }
    }

    private void ClearForm()
    {
        txtPackageHeight.Text = "";
        txtPackageLength.Text = "";
        txtPackageWidth.Text = "";
    }

    private void CreateLabel()
    {
        try
        {
            AddressInfo addInfo = (AddressInfo) Session["sl_toAddress"];
            AddressInfo billAddInfo = (AddressInfo) Session["sl_frAddress"];
            AspxCommerce.Core.WareHouseAddress whaInfo = GetWareHouseAddress(GetStoreID, GetPortalID);
            _itemslist = new List<ItemDetail>();
            _itemslist = (List<ItemDetail>) Session["sl_items"];
            GeneratePDF.BasicPackageInfo basciPagIngo = new GeneratePDF.BasicPackageInfo();
            basciPagIngo.TrackingNo = "";
            basciPagIngo.TotalWeight = Convert.ToDecimal(lblPackageTotalWeight.Text);
            basciPagIngo.WeightUnit = lblStoreWeightUnit.Text;
            basciPagIngo.WaterMark = "";
            basciPagIngo.BarcodeNo = "";
            basciPagIngo.SenderName = billAddInfo.FirstName + " " + billAddInfo.LastName;
            basciPagIngo.CautionMessage = "Handle With Care.";
            basciPagIngo.ServiceType = lblUserShippingMethod.Text;
            basciPagIngo.Length = Convert.ToInt32(txtPackageLength.Text);
            basciPagIngo.Width = Convert.ToInt32(txtPackageWidth.Text);
            basciPagIngo.Height = Convert.ToInt32(txtPackageHeight.Text);
            basciPagIngo.DimensionUnit = lblStoreDimensionUnit.Text;

            AspxCommonInfo aspxCommonObj = new AspxCommonInfo();
            aspxCommonObj.StoreID = GetStoreID;
            aspxCommonObj.PortalID = GetPortalID;
            aspxCommonObj.UserName = GetUsername;
            aspxCommonObj.CultureName = GetCurrentCultureName;
            GeneratePDF gPdf = new GeneratePDF();
            gPdf.CreateShipmentLabel(addInfo, whaInfo, aspxCommonObj, basciPagIngo);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void SaveLabelInfo(ShippingLabelInfo info)
    {
        try
        {
            AspxCommonInfo commonObj = new AspxCommonInfo();
            commonObj.StoreID = _storeId;
            commonObj.PortalID = _portalId;
            commonObj.UserName = GetUsername;
            AspxShipProviderMgntController ctl = new AspxShipProviderMgntController();
            ctl.SaveShippingLabelInfo(info, commonObj);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void DisplayFile(string path)
    {
        try
        {
            dvShipmentForm.Visible = false;
            dvLabelPreview.Visible = true;

            string url = ResolveUrl("~/Modules/AspxCommerce/AspxShipmentsManagement/LabelPreview.aspx");
            url = url + "?isfile=true&path=" + path;

                                  string ifrm =
                string.Format(
                    "<iframe id={3} src={0}  frameborder='no' scrolling='auto' height={1} width={2} ></iframe>", url,
                    "500px",
                    "100%", "ifLabelPreview");
            string div = "<div><input type='button' value='Reload' onclick='document.getElementById(" +
                         "'ifLabelPreview'" + ").contentWindow.location.reload(true);' /></div>";
            ltPreviewLabel.Text = ifrm + div;
        }
        catch (Exception ex)
        {
            ShowError(ex);
        }
    }

    private void BindWareHouseAddress(AspxCommerce.Core.WareHouseAddress wareHouseAddress)
    {
        if (wareHouseAddress != null)
        {
            StringBuilder billingAdr = new StringBuilder();
            billingAdr.Append("<table><tr>");
            billingAdr.Append("<td>"+getLocale("WareHouse Name:")+"</td>");
            billingAdr.Append("<td>" + wareHouseAddress.Name + "</td></tr><tr>");
            billingAdr.Append("<td>"+getLocale("Address:")+"</td>");
            billingAdr.Append("<td>" + wareHouseAddress.Address + "</td></tr><tr>");
            if (!string.IsNullOrEmpty(wareHouseAddress.StreetAddress1))
            {
                billingAdr.Append("<td>"+getLocale("Street Address1:")+"</td>");
                billingAdr.Append("<td>" + wareHouseAddress.StreetAddress1 + "</td></tr><tr>");
            }
            if (!string.IsNullOrEmpty(wareHouseAddress.StreetAddress2))
            {
                billingAdr.Append("<td>"+getLocale("Street Address2:")+"</td>"); 
                billingAdr.Append("<td>" + wareHouseAddress.StreetAddress2 + "</td></tr><tr>");
            }
            billingAdr.Append("<td>"+getLocale("Country:")+"</td>");
            billingAdr.Append("<td>" + wareHouseAddress.Country + "</td></tr><tr>");
            billingAdr.Append("<td>"+getLocale("City:")+"</td>");
            billingAdr.Append("<td>" + wareHouseAddress.City + "</td></tr><tr>");
            billingAdr.Append("<td>"+getLocale("State:")+"</td>");
            billingAdr.Append("<td>" + wareHouseAddress.State + "</td></tr><tr>");
            billingAdr.Append("<td>"+getLocale("ZipCode:")+"</td>");
            billingAdr.Append("<td>" + wareHouseAddress.PostalCode + "</td></tr><tr>");
            billingAdr.Append("<td>"+getLocale("Email Address:")+"</td>");
            billingAdr.Append("<td>" + wareHouseAddress.Email + "</td></tr><tr>");
            billingAdr.Append("<td>"+getLocale("Phone No:")+"</td>");
            billingAdr.Append("<td>" + wareHouseAddress.Phone + "</td></tr>");
            billingAdr.Append("</table>");

            ltWareHouse.Text = billingAdr.ToString();
            btnCreateLabel.Enabled = true;
        }
        else
        {
            Exception ex = new Exception("Please add warehouse address before shipping!");
            ShowError(ex);
            ltWareHouse.Text = "<span>No WareHouse Address!</span>";
            btnCreateLabel.Enabled = false;
        }

    }

    private void BindBillingAddress(AddressInfo billingAddress)
    {
        string modulePath = this.AppRelativeTemplateSourceDirectory;
        hst = AppLocalized.getLocale(modulePath);
        StringBuilder billingAdr = new StringBuilder();
        billingAdr.Append("<table><tr>");
        billingAdr.Append("<td>"+getLocale("Customer Name:")+"</td>");
        billingAdr.Append("<td>" + billingAddress.FirstName + " " + billingAddress.LastName + "</td></tr><tr>");
        billingAdr.Append("<td>"+getLocale("Address:")+"</td>");
        billingAdr.Append("<td>" + billingAddress.Address1 + "</td></tr><tr>");
        if (!string.IsNullOrEmpty(billingAddress.Address2))
        {
            billingAdr.Append("<td>"+getLocale("Address2:")+"</td>");
            billingAdr.Append("<td>" + billingAddress.Address2 + "</td></tr><tr>");
        }
        billingAdr.Append("<td>"+getLocale("Country:")+"</td>");
        billingAdr.Append("<td>" + billingAddress.Country + "</td></tr><tr>");
        billingAdr.Append("<td>"+getLocale("City:")+"</td>");
        billingAdr.Append("<td>" + billingAddress.City + "</td></tr><tr>");
        billingAdr.Append("<td>"+getLocale("State:")+"</td>");
        billingAdr.Append("<td>" + billingAddress.State + "</td></tr><tr>");
        billingAdr.Append("<td>"+getLocale("ZipCode:")+"</td>");
        billingAdr.Append("<td>" + billingAddress.Zip + "</td></tr><tr>");
        billingAdr.Append("<td>"+getLocale("Email Address:")+"</td>");
        billingAdr.Append("<td>" + billingAddress.Email + "</td></tr><tr>");
        billingAdr.Append("<td>"+getLocale("Phone No:")+"</td>");
        billingAdr.Append("<td>" + billingAddress.Phone + "</td></tr>");
        billingAdr.Append("</table>");

        ltBillingAddress.Text = billingAdr.ToString();
        Session["sl_frAddress"] = billingAddress;
    }

    private void BindShippingAddress(AddressInfo shippingAddress)
    {
        StringBuilder shippingAdr = new StringBuilder();
        shippingAdr.Append("<table><tr>");
        shippingAdr.Append("<td width=\"28%\">"+getLocale("Customer Name:")+"</td>");
        shippingAdr.Append("<td>" + shippingAddress.FirstName + " " + shippingAddress.LastName + "</td></tr><tr>");
        shippingAdr.Append("<td>"+getLocale("Address:")+"</td>");
        shippingAdr.Append("<td>" + shippingAddress.Address1 + "</td></tr><tr>");
        if (!string.IsNullOrEmpty(shippingAddress.Address2))
        {
            shippingAdr.Append("<td>"+getLocale("Address2:")+"</td>");
            shippingAdr.Append("<td>" + shippingAddress.Address2 + "</td></tr><tr>");
        }
        shippingAdr.Append("<td>"+getLocale("Country:")+"</td>");
        shippingAdr.Append("<td>" + shippingAddress.Country + "</td></tr><tr>");
        shippingAdr.Append("<td>"+getLocale("City:")+"</td>");
        shippingAdr.Append("<td>" + shippingAddress.City + "</td></tr><tr>");
        shippingAdr.Append("<td>"+getLocale("State:")+"</td>");
        shippingAdr.Append("<td>" + shippingAddress.State + "</td></tr><tr>");
        shippingAdr.Append("<td>"+getLocale("ZipCode:")+"</td>");
        shippingAdr.Append("<td>" + shippingAddress.Zip + "</td></tr><tr>");
        shippingAdr.Append("<td>"+getLocale("Email Address:")+"</td>");
        shippingAdr.Append("<td>" + shippingAddress.Email + "</td></tr><tr>");
        shippingAdr.Append("<td>"+getLocale("Phone No:")+"</td>");
        shippingAdr.Append("<td>" + shippingAddress.Phone + "</td></tr>");
        shippingAdr.Append("</table>");
        ltShippingAddress.Text = shippingAdr.ToString();
        Session["sl_toAddress"] = shippingAddress;
    }


    private void GetOrderDetails(int storeId, int portalId, int orderId, string cultureName)
    {
        try
        {

            OrderLabel orderDetail = GetOrderDetailByOrderId(orderId, storeId, portalId, cultureName);
            if (orderDetail != null)
            {
                AddressInfo billingAddress = GetUserBillingAddress(orderDetail.UserBillingAddressId);
                BindBillingAddress(billingAddress);
                int providerId = GetShippingProviderIdByShippingMethod(orderDetail.ShippingMethodId);
                Session["labelOrderInfo"] = orderDetail;
                lblUserShippingMethod.Text = string.Format("{0}:{1}", ""+getLocale("User Selected Method:")+"",
                                                           orderDetail.ShippingMethodName);
                AddressInfo shippingAddress = GetUserShippingAddress(orderDetail.UserShippingAddressId, storeId,
                                                                     portalId);
                BindShippingAddress(shippingAddress);
                AspxCommerce.Core.WareHouseAddress wareHouseAddress = GetWareHouseAddress(storeId, portalId);

                BindWareHouseAddress(wareHouseAddress);

                BindPackageDetails(orderDetail);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    private int GetShippingProviderIdByShippingMethod(int shippingMethodId)
    {
        try
        {
            if (Session["sl_pid"] != null)
            {
                return int.Parse(Session["sl_pid"].ToString());
            }
            else
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@ShippingMethodId", shippingMethodId));
                SQLHandler sqlH = new SQLHandler();
                int id = sqlH.ExecuteAsScalar<int>("usp_Aspx_GetShippingProviderByShippingMethod", parameter);
                Session["sl_pid"] = id;
                return id;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private OrderLabel GetOrderDetailByOrderId(int orderId, int storeId, int portalId, string cultureName)
    {
        try
        {
            if (Session["labelOrderInfo"] != null)
            {
                return (OrderLabel) Session["labelOrderInfo"];
            }
            else
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@OrderId", orderId));
                parameter.Add(new KeyValuePair<string, object>("@CultureName", _cultureName));
                SQLHandler sqlH = new SQLHandler();
                OrderLabel orderDetail = sqlH.ExecuteAsObject<OrderLabel>("usp_Aspx_GetOrderDetailByOrderIdForLabel",
                                                                          parameter);
                               orderDetail.StoreId = storeId;
                orderDetail.PortalId = portalId;
                orderDetail.CultureName = cultureName;
                return orderDetail;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private AddressInfo GetUserBillingAddress(int billingAddressId)
    {
        try
        {
            if (Session["sl_frAddress"] != null)
            {
                return (AddressInfo) Session["sl_frAddress"];
            }
            else
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@BillingAddressId", billingAddressId));
                parameter.Add(new KeyValuePair<string, object>("@StoreId", _storeId));
                parameter.Add(new KeyValuePair<string, object>("@PortalId", _portalId));
                SQLHandler sqlH = new SQLHandler();
                AddressInfo adrInfo = sqlH.ExecuteAsObject<AddressInfo>("usp_Aspx_GetBillingAddressForLabel",
                                                                        parameter);
                return adrInfo;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private AspxCommerce.Core.WareHouseAddress GetWareHouseAddress(int storeId, int portalId)
    {
        try
        {
            SQLHandler sqlHandler = new SQLHandler();
            List<KeyValuePair<string, object>> paramList = new List<KeyValuePair<string, object>>();
            paramList.Add(new KeyValuePair<string, object>("@StoreID", storeId));
            paramList.Add(new KeyValuePair<string, object>("@PortalID", portalId));
            AspxCommerce.Core.WareHouseAddress cl =
                sqlHandler.ExecuteAsObject<AspxCommerce.Core.WareHouseAddress>("[usp_Aspx_GetActiveWareHouse]",
                                                                               paramList);
            return cl;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private AddressInfo GetUserShippingAddress(int shippingAddressId, int storeId, int portalId)
    {
        try
        {
            if (Session["sl_toAddress"] != null)
            {
                return (AddressInfo) Session["sl_toAddress"];
            }
            else
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@PortalId", portalId));
                parameter.Add(new KeyValuePair<string, object>("@StoreId", storeId));
                parameter.Add(new KeyValuePair<string, object>("@ShippingAddressId", shippingAddressId));
                SQLHandler sqlH = new SQLHandler();
                return sqlH.ExecuteAsObject<AddressInfo>("usp_Aspx_GetShippingAddressForLabel",
                                                         parameter);
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void GetStoreSettings(int storeId, int portalId, string cultureName)
    {
        var storeConfig = new StoreSettingConfig();
        _dimensionUnit = storeConfig.GetStoreSettingsByKey(StoreSetting.DimensionUnit, storeId, portalId, cultureName);
        _weightUnit = storeConfig.GetStoreSettingsByKey(StoreSetting.WeightUnit, storeId, portalId, cultureName);
        lblStoreWeightUnit.Text = _weightUnit;
        lblStoreDimensionUnit.Text = _dimensionUnit;
    }

    private void BindPackageDetails(OrderLabel orderItemsInfo)
    {
        string[] itemnames = orderItemsInfo.ItemNames.Split(',');
        string[] wgts = orderItemsInfo.Weights.Split(',');
        string[] qtys = orderItemsInfo.ItemQuantities.Split(',');
        string[] prices = orderItemsInfo.Prices.Split(',');
        var packageBuilder = new StringBuilder();
        int len = itemnames.Length;
        _itemslist = new List<ItemDetail>();
        _itemslist.Clear();
        packageBuilder.Append("<table>");
        for (int i = 0; i < len; i++)
        {
            _itemslist.Add(new ItemDetail()
                               {
                                   Description = itemnames[i],
                                   Quantity = int.Parse(qtys[i]),
                                   Weight = decimal.Parse(wgts[i]),
                                   Price = decimal.Parse(prices[i]),
                               });

            if (i == 0)
            {
                packageBuilder.Append("<tr><th>S.N</th>");
                packageBuilder.Append("<th>");
                packageBuilder.Append(getLocale("Product Name"));
                packageBuilder.Append("</th>");
                packageBuilder.Append("<th>"+getLocale("Weight")+"</th>");
                packageBuilder.Append("<th>"+getLocale("Quantity")+"</th>");
                packageBuilder.Append("</tr>");

            }
            _totalWeight += decimal.Parse(wgts[i])*int.Parse(qtys[i]);
            packageBuilder.Append("<tr>");
            packageBuilder.Append("<td>" + i + 1 + "</td>" +
                                  "<td>" + itemnames[i] + "</td>" +
                                  "<td>" + wgts[i] + "</td>" +
                                  "<td>" + qtys[i] + "</td>"
                );
            packageBuilder.Append("</tr>");
        }
        Session["sl_items"] = _itemslist;
        packageBuilder.Append("</table>");
        lblPackageTotalWeight.Text = _totalWeight.ToString();
        ltPackageDetail.Text = packageBuilder.ToString();
           }

    private void ShowError(Exception ex)
    {
        lblErrorMessage.Text = ex.ToString();
    }

    private void ShowError(string error)
    {
        lblErrorMessage.Text = error;
    }

    protected void btnCreateLabel_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                CreateLabel();
            }
        }
        catch (Exception ex)
        {
            ShowError(ex);
        }
    }
    
    private string getLocale(string messageKey)
    {
        string retStr = messageKey;
        if (hst != null && hst[messageKey] != null)
        {
            retStr = hst[messageKey].ToString();
        }
        return retStr;
    }

    #endregion
}
