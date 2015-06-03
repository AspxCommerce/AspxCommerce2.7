using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AspxCommerce.Core;
using AspxCommerce.USPS.Entity;
using SageFrame.Web;
using AspxCommerce.USPS.Controller;
using AspxCommerce.USPS;
using SageFrame.Web.Utilities;
using System.Text;

public partial class Modules_AspxCommerce_AspxUSPS_shipment_usps : BaseAdministrationUserControl
{

    private string cultureName, _dimensionUnit, _weightUnit;
    private decimal  _totalWeight = 0;
    private UspsSetting _uspsSetting;
    private OriginAddress _fromAddress;
    private DestinationAddress _toAddress;
    private List<ItemDetail> _itemslist;
    public int storeId, portalId;

    protected void Page_Init(object sender, EventArgs e)
    {
        storeId = GetStoreID;// Convert.ToInt32(Request.QueryString["sid"]);
        portalId = GetPortalID;// Convert.ToInt32(Request.QueryString["pid"]);
        cultureName = GetCurrentCultureName;// Request.QueryString["cn"];
        GetStoreSettings(storeId, portalId, cultureName);
        if (!IsPostBack)
        {
            BindMailType();
            BindServieType();
            BindImageType();
            if (Request.QueryString["oid"] != null)
            {
                dvLabelPreview.Visible = false;
                dvShipmentForm.Visible = true;
                int orderId = Convert.ToInt32(Request.QueryString["oid"]);

                ClearSession(orderId);
                GetOrderDetails(storeId, portalId, orderId, cultureName);


            }
            else
            {
                lblErrorMessage.Text = "ERROR: Please submit valid data!";
            }

        }
        IncludeLanguageJS();

    }

    protected void Page_Load(object sender, EventArgs e)
    {

       
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
        txtPackageGirth.Text = "";
        txtPackageHeight.Text = "";
        txtPackageLength.Text = "";
        txtPackageWidth.Text = "";
        ddlImageType.SelectedIndex = -1;
    }

    private void CreateLabel()
    {
        try
        {


            string api = rblLabelTypeList.SelectedValue;
            var items = (List<ItemDetail>)Session["sl_items"];
            var orderInfo = (OrderLabel)Session["labelOrderInfo"];
            if (items == null)
            {
                BindPackageDetails(orderInfo);
                items = (List<ItemDetail>)Session["sl_items"];
            }
            var totalWt = items.Sum(itemDetail => itemDetail.Weight);
            if (totalWt == 0)
            {
                throw new Exception("Minimum weight of package must be 0.1");
            }
            decimal totalWtInOunce = 0;
            if (_weightUnit.ToLower().Trim() == "lbs" || _weightUnit.ToLower().Trim() == "lb")
            {
                               totalWtInOunce = (decimal)((totalWt) * 16);
            }
            else
            {
                               totalWt = totalWt * (decimal)2.2;
            }

            var package = new USPSShipment();
            package.Width = decimal.Parse(txtPackageWidth.Text.Trim());
            package.Height = decimal.Parse(txtPackageHeight.Text.Trim());
            package.FromAddress = (OriginAddress)Session["sl_frAddress"];
            package.GrossOunce = totalWtInOunce;
            package.GrossPound = totalWt;
            var total = CalculateGirth();
            package.Container = Container.RECTANGULAR;
            package.ContentType = ContentType.OTHER;
            package.ImageLayout = ImageLayout.ALLINONEFILE;
            package.ImageType = (ImageType)Enum.Parse(typeof(ImageType), ddlImageType.SelectedValue);
            package.Items = items;
            package.NonDeliveryOption = NonDeliveryOption.RETURN;
            package.Option = "1";
            package.POZipCode = "";
            package.ServiceType = (ServiceType)Enum.Parse(typeof(ServiceType), ddlServiceType.SelectedValue);
            package.ToAddress = (DestinationAddress)Session["sl_toAddress"];
            package.Length = decimal.Parse(txtPackageLength.Text.Trim());
            package.Grith = decimal.Parse(txtPackageGirth.Text.Trim());
            package.Api = api;
            var providerId = int.Parse(Session["sl_pid"].ToString());

            var labelCreater = new USPS();

            var response = labelCreater.SendShipmentConfirmation(package, providerId, GetStoreID, GetPortalID);

            if (!response.IsFailed)
            {


                if (response.IsDomestic)
                {
                    string trackingNo = response.DeliveryConfirmationNumber;
                                                          ShippingLabelInfo info = new ShippingLabelInfo();
                    info.TrackingNo = trackingNo;
                    info.ShippingLabelImage = response.LabelString;
                    info.OrderID = orderInfo.OrderId;
                    info.Extension = package.ImageType.ToString().ToLower();
                    info.IsRealTime = true;
                    SaveLabelInfo(info);
                    DisplayFile(response.TempLabelPath);
                    lblErrorMessage.Text = "";
                }
                else
                {
                    string barcodeNo = response.IntlResponse.BarcodeNumber;
                                      
                    ShippingLabelInfo info = new ShippingLabelInfo();
                    info.ShippingLabelImage = response.LabelString;
                    info.OrderID = orderInfo.OrderId;
                    info.Extension = package.ImageType.ToString().ToLower();
                    info.IsRealTime = true;
                    info.TrackingNo = "";
                    info.BarcodeNo = barcodeNo;
                    SaveLabelInfo(info);
                }
                ClearForm();

            }
            else
            {
                lblErrorMessage.Text = response.Error;
            }
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
            commonObj.StoreID = storeId;
            commonObj.PortalID = portalId;
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

                                  string ifrm = string.Format("<iframe id={3} src={0}  frameborder='no' scrolling='auto' height={1} width={2} ></iframe>", url, "500px",
                           "100%", "ifLabelPreview");
            string div = "<div><input type=\"button\" value=\"Reload\" onclick=\"document.getElementById('ifLabelPreview').contentWindow.location.reload(true);\" /></div>";
            ltPreviewLabel.Text = ifrm + div;
        }
        catch (Exception ex)
        {

            ShowError(ex);
        }


    }

    private void BindWareHouseAddress(AspxCommerce.Core.WareHouseAddress wareHouseAddress)
    {
        StringBuilder billingAdr = new StringBuilder();
        billingAdr.Append("<table cellspacing=\"0\" cellpadding=\"0\" border=\"0\" width=\"100%\"><tr>");
        billingAdr.Append("<td>WareHouse Name:</td>");
        billingAdr.Append("<td>" + wareHouseAddress.Name + "</td></tr><tr>");
        billingAdr.Append("<td>Address:</td>");
        billingAdr.Append("<td>" + wareHouseAddress.Address + "</td></tr><tr>");
        if (!string.IsNullOrEmpty(wareHouseAddress.StreetAddress1))
        {
            billingAdr.Append("<td>Street Address1:</td>");
            billingAdr.Append("<td>" + wareHouseAddress.StreetAddress1 + "</td></tr><tr>");
        }
        if (!string.IsNullOrEmpty(wareHouseAddress.StreetAddress2))
        {
            billingAdr.Append("<td>Street Address2:</td>");
            billingAdr.Append("<td>" + wareHouseAddress.StreetAddress2 + "</td></tr><tr>");
        }
        billingAdr.Append("<td>Country:</td>");
        billingAdr.Append("<td>" + wareHouseAddress.Country + "</td></tr><tr>");
        billingAdr.Append("<td>City:</td>");
        billingAdr.Append("<td>" + wareHouseAddress.City + "</td></tr><tr>");
        billingAdr.Append("<td>State:</td>");
        billingAdr.Append("<td>" + wareHouseAddress.State + "</td></tr><tr>");
        billingAdr.Append("<td>ZipCode:</td>");
        billingAdr.Append("<td>" + wareHouseAddress.PostalCode + "</td></tr><tr>");
        billingAdr.Append("<td>Email Address:</td>");
        billingAdr.Append("<td>" + wareHouseAddress.Email + "</td></tr><tr>");
        billingAdr.Append("<td>Phone No:</td>");
        billingAdr.Append("<td>" + wareHouseAddress.Phone + "</td></tr>");
        billingAdr.Append("</table>");
        ltWareHouse.Text = billingAdr.ToString();


    }

    private void BindBillingAddress(OriginAddress billingAddress)
    {
        StringBuilder billingAdr = new StringBuilder();
        billingAdr.Append("<table cellspacing=\"0\" cellpadding=\"0\" border=\"0\" width=\"100%\"><tr>");
        billingAdr.Append("<td>Customer Name:</td>");
        billingAdr.Append("<td>" + billingAddress.FirstName + " " + billingAddress.LastName + "</td></tr><tr>");
        billingAdr.Append("<td>Address:</td>");
        billingAdr.Append("<td>" + billingAddress.Address1 + "</td></tr><tr>");
        if (!string.IsNullOrEmpty(billingAddress.Address2))
        {
            billingAdr.Append("<td>Address2:</td>");
            billingAdr.Append("<td>" + billingAddress.Address2 + "</td></tr><tr>");
        }
        billingAdr.Append("<td>Country:</td>");
        billingAdr.Append("<td>" + billingAddress.Country + "</td></tr><tr>");
        billingAdr.Append("<td>City:</td>");
        billingAdr.Append("<td>" + billingAddress.City + "</td></tr><tr>");
        billingAdr.Append("<td>State:</td>");
        billingAdr.Append("<td>" + billingAddress.State + "</td></tr><tr>");
        billingAdr.Append("<td>ZipCode:</td>");
        billingAdr.Append("<td>" + billingAddress.Zip + "</td></tr><tr>");
        billingAdr.Append("<td>Email Address:</td>");
        billingAdr.Append("<td>" + billingAddress.Email + "</td></tr><tr>");
        billingAdr.Append("<td>Phone No:</td>");
        billingAdr.Append("<td>" + billingAddress.Phone + "</td></tr>");
        billingAdr.Append("</table>");

        ltBillingAddress.Text = billingAdr.ToString();
        _fromAddress = billingAddress;
        _fromAddress.CountryName = "";
        Session["sl_frAddress"] = _fromAddress;
    }

    private void BindShippingAddress(DestinationAddress shippingAddress)
    {
        StringBuilder shippingAdr = new StringBuilder();
        shippingAdr.Append("<table cellspacing=\"0\" cellpadding=\"0\" border=\"0\" width=\"100%\"><tr>");
        shippingAdr.Append("<td>Customer Name:</td>");
        shippingAdr.Append("<td>" + shippingAddress.FirstName + " " + shippingAddress.LastName + "</td></tr><tr>");
        shippingAdr.Append("<td>Address:</td>");
        shippingAdr.Append("<td>" + shippingAddress.ToAddress + "</td></tr><tr>");
        if (!string.IsNullOrEmpty(shippingAddress.ToAddress2))
        {
            shippingAdr.Append("<td>Address2:</td>");
            shippingAdr.Append("<td>" + shippingAddress.ToAddress2 + "</td></tr><tr>");
        }
        shippingAdr.Append("<td>Country:</td>");
        shippingAdr.Append("<td>" + shippingAddress.ToCountry + "</td></tr><tr>");
        shippingAdr.Append("<td>City:</td>");
        shippingAdr.Append("<td>" + shippingAddress.ToCity + "</td></tr><tr>");
        shippingAdr.Append("<td>State:</td>");
        shippingAdr.Append("<td>" + shippingAddress.ToState + "</td></tr><tr>");
        shippingAdr.Append("<td>ZipCode:</td>");
        shippingAdr.Append("<td>" + shippingAddress.Zip + "</td></tr><tr>");
        shippingAdr.Append("<td>Email Address:</td>");
        shippingAdr.Append("<td>" + shippingAddress.Email + "</td></tr><tr>");
        shippingAdr.Append("<td>Phone No:</td>");
        shippingAdr.Append("<td>" + shippingAddress.Phone + "</td></tr>");
        shippingAdr.Append("</table>");
        ltShippingAddress.Text = shippingAdr.ToString();

        _toAddress = shippingAddress;
        _toAddress.ToCountryName = "";
        Session["sl_toAddress"] = _toAddress;

    }

    private void BindServieType()
    {
                                    ddlServiceType.DataSource = Enum.GetNames(typeof(ServiceType));
        ddlServiceType.DataBind();
                                          
    }

    private void GetOrderDetails(int storeId, int portalId, int orderId, string cultureName)
    {
        try
        {

            OrderLabel orderDetail = GetOrderDetailByOrderId(orderId, storeId, portalId, cultureName);
            if (orderDetail != null)
            {
                OriginAddress billingAddress = GetUserBillingAddress(orderDetail.UserBillingAddressId);
                BindBillingAddress(billingAddress);
                int providerId = GetShippingProviderIdByShippingMethod(orderDetail.ShippingMethodId);
                ProviderSetting(providerId, storeId, portalId);
                Session["labelOrderInfo"] = orderDetail;
                lblUserShippingMethod.Text = string.Format("{0}:{1}", "User Selected Method:",
                                                           orderDetail.ShippingMethodName);
                DestinationAddress shippingAddress = GetUserShippingAddress(orderDetail.UserShippingAddressId, storeId, portalId);
                BindShippingAddress(shippingAddress);
                AspxCommerce.Core.WareHouseAddress wareHouseAddress = GetWareHouseAddress(storeId, portalId);
                BindWareHouseAddress(wareHouseAddress);
                if (shippingAddress.ToCountry != null && shippingAddress.ToCountry.ToLower().Trim() != "united states" && shippingAddress.ToCountry.ToLower().Trim() != wareHouseAddress.Country.ToLower().Trim())
                {
                    int count = rblLabelTypeList.Items.Count;
                    for (int i = 0; i < count; i++)
                    {
                        rblLabelTypeList.Items[i].Enabled = false;
                    }
                    rblLabelTypeList.Items.Add(new ListItem()
                                                   {
                                                       Text = "International Label",
                                                       Selected = true,
                                                       Value = "internationallabel"
                                                   });
                }
                BindPackageDetails(orderDetail);

            }


        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }

    private decimal CalculateGirth()
    {
        var l = txtPackageLength.Text.Trim();
        var w = txtPackageWidth.Text.Trim();
        var h = txtPackageLength.Text.Trim();

        if (l != "" && w != "" && h != "")
        {
            var regex = new Regex(@"^\d+$");
            if (regex.IsMatch(h) && regex.IsMatch(w) && regex.IsMatch(l))
            {
                var length = decimal.Parse(l);
                var width = decimal.Parse(w);
                var height = decimal.Parse(h);

                var girth = height + height + width + width;
                txtPackageGirth.Text = girth.ToString();
                var total = length + girth;
                return total;
            }
        }
        return 0;
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

                var parameter = new List<KeyValuePair<string, object>> { new KeyValuePair<string, object>("@ShippingMethodId", shippingMethodId) };
                var sqlH = new SQLHandler();
                var id = sqlH.ExecuteAsScalar<int>("usp_Aspx_GetShippingProviderByShippingMethod", parameter);
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
                return (OrderLabel)Session["labelOrderInfo"];
            }
            else
            {
                var parameter = new List<KeyValuePair<string, object>> { new KeyValuePair<string, object>("@OrderId", orderId) };
                parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
                var sqlH = new SQLHandler();
                var orderDetail = sqlH.ExecuteAsObject<OrderLabel>("usp_Aspx_GetOrderDetailByOrderIdForLabel", parameter);
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

    private OriginAddress GetUserBillingAddress(int billingAddressId)
    {
        try
        {
            if (Session["sl_frAddress"] != null)
            {
                return (OriginAddress)Session["sl_frAddress"];
            }
            else
            {

                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@BillingAddressId", billingAddressId));
                parameter.Add(new KeyValuePair<string, object>("@StoreId", storeId));
                parameter.Add(new KeyValuePair<string, object>("@PortalId", portalId));
                var sqlH = new SQLHandler();
                return sqlH.ExecuteAsObject<OriginAddress>("usp_Aspx_GetBillingAddressForLabel",
                                                                parameter);

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
            AspxCommerce.Core.WareHouseAddress cl = sqlHandler.ExecuteAsObject<AspxCommerce.Core.WareHouseAddress>("[usp_Aspx_GetActiveWareHouse]",
                                                                                    paramList);
            return cl;

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private DestinationAddress GetUserShippingAddress(int shippingAddressId, int storeId, int portalId)
    {
        try
        {
            if (Session["sl_toAddress"] != null)
            {
                return (DestinationAddress)Session["sl_toAddress"];
            }
            else
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@ShippingAddressId", shippingAddressId));
                parameter.Add(new KeyValuePair<string, object>("@StoreId", storeId));
                parameter.Add(new KeyValuePair<string, object>("@PortalId", portalId));
                SQLHandler sqlH = new SQLHandler();
                return sqlH.ExecuteAsObject<DestinationAddress>("usp_Aspx_GetShippingAddressForLabel",
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

    private UspsSetting ProviderSetting(int providerId, int storeId, int portalId)
    {
        UspsController uspsController = new UspsController();
        AspxCommonInfo commonObj = new AspxCommonInfo();
        commonObj.StoreID = storeId;
        commonObj.PortalID = portalId;
        _uspsSetting = uspsController.GetSetting(providerId, commonObj);

        Session["sl_setting"] = _uspsSetting;
        return _uspsSetting;
    }

    private void BindImageType()
    {
                                                 
                                    ddlImageType.DataSource = Enum.GetNames(typeof(AspxCommerce.USPS.ImageType));
        ddlImageType.DataBind();
    }

    private void BindMailType()
    {
        ddlMailType.DataSource = Enum.GetNames(typeof(AspxCommerce.USPS.MailType));
        ddlMailType.DataBind();
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
                packageBuilder.Append("<th>Product Name</th>");
                packageBuilder.Append("<th>Weight</th>");
                packageBuilder.Append("<th>Quantity</th>");
                packageBuilder.Append("</tr>");

            }
            _totalWeight += decimal.Parse(wgts[i]) * int.Parse(qtys[i]);
            packageBuilder.Append("<tr>");
            packageBuilder.Append("<td>" + i + 1 + "</td>" +
                                  "<td>" + itemnames[i] + "</td>" +
                                  "<td>" + wgts[i] + "</td>" +
                                  "<td>" + qtys[i] + "</td>"
                );
            packageBuilder.Append("</tr>");
        }
        Session["sl_tWeight"] = _totalWeight;
        Session["sl_items"] = _itemslist;
        packageBuilder.Append("</table>");
        lblPackageTotalWeight.Text = _totalWeight.ToString();
        ltPackageDetail.Text = packageBuilder.ToString();
           }

    private void ShowError(Exception ex)
    {
        lblErrorMessage.Text = ex.ToString();
    }

    protected void btnCreateLabel_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["sl_tWeight"] != null)
                _totalWeight = (decimal) Session["sl_tWeight"];
                       _uspsSetting = (UspsSetting)Session["sl_setting"];
            if (_uspsSetting == null)
                _uspsSetting = ProviderSetting(int.Parse(Session["sl_pid"].ToString()), storeId, portalId);

            if (_uspsSetting.UspsMaxWeight >= _totalWeight && _uspsSetting.UspsMinWeight <= _totalWeight)
            {
                if (Page.IsValid)
                    CreateLabel();
            }
            else
            {
                throw new Exception("Weight limit is not matched as provider mentioned");
            }

        }
        catch (Exception ex)
        {
            ShowError(ex);
            ProcessException(ex);
        }

    }

    protected void btnBackForm_Click(object sender, EventArgs e)
    {
        dvLabelPreview.Visible = false;
        dvShipmentForm.Visible = true;
    }

    protected void btnCalculateGirth_Click(object sender, EventArgs e)
    {

        CalculateGirth();
        Page.Validate();

    }

    protected void RequiredFieldValidator4_ServerValidate(object source, ServerValidateEventArgs args)
    {
        if (txtPackageGirth.Text.Trim() != "0")
            args.IsValid = true;
        else
            args.IsValid = false;
    }
}
