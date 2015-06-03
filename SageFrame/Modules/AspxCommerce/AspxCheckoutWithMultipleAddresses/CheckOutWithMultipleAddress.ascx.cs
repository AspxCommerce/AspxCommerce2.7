/*
AspxCommerce® - http://www.aspxcommerce.com
Copyright (c) 2011-2015 by AspxCommerce

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
WITH THE SOFTWARE OR THE USE OF OTHER DEALINGS IN THE SOFTWARE. 
*/



using System;
using System.Web;
using SageFrame.Web;

public partial class Modules_AspxCheckoutInformationContent_CheckOutWithMultipleAddress : BaseAdministrationUserControl
{
    public int StoreID, PortalID, CustomerID;
    public string UserName;
    public string CultureName;
    public string SessionCode = string.Empty;
    public bool IsFShipping = false;
    public decimal Discount = 0;
    public string CouponCode = string.Empty;
    public string UserIP = string.Empty;


    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["IsFreeShipping"] != null)
            {
                IsFShipping = bool.Parse(Session["IsFreeShipping"].ToString());

            }
            if (Session["DiscountAmount"] != null)
            {
                Discount = decimal.Parse(Session["DiscountAmount"].ToString());

            }
            if (Session["CouponCode"] != null)
            {
                CouponCode = Session["CouponCode"].ToString();

            }
            if (!IsPostBack)
            {
                IncludeCss("CheckOutWithMultipleAddress", "/Templates/" + TemplateName + "/css/JQueryUIFront/jquery.ui.all.css", "/Templates/" + TemplateName + "/css/PopUp/style.css");
                IncludeJs("CheckOutWithMultipleAddress", "/js/encoder.js", "/js/FormValidation/jquery.validate.js", "/js/PopUp/custom.js", "/js/jquery.cookie.js");
                StoreID = GetStoreID;
                PortalID = GetPortalID;
                UserName = GetUsername;
                CustomerID = GetCustomerID;
                CultureName = GetCurrentCultureName;
                UserIP = HttpContext.Current.Request.UserHostAddress;
                if (HttpContext.Current.Session.SessionID != null)
                {
                    SessionCode = HttpContext.Current.Session.SessionID.ToString();
                }
            }
            IncludeLanguageJS();
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }    
}
