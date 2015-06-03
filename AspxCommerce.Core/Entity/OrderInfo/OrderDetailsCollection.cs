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



using System.Collections.Generic;


namespace AspxCommerce.Core
{
   public class OrderDetailsCollection
   {
       #region Private Members

       private OrderDetailsInfo _ObjOrderDetails;
       private PaymentInfo _ObjPaymentInfo;
       private UserAddressInfo _ObjBillingAddressInfo;
       private UserAddressInfo _ObjShippingAddressInfo;
       private List<OrderItemInfo> _LstOrderItemsInfo;
       private CommonInfo _ObjCommonInfo;
       private List<OrderTaxInfo> _ObjOrderTaxInfo;
       private List<GiftCardUsage>  _giftCardDetail;      

       #endregion

       #region Public Members

       public OrderDetailsInfo ObjOrderDetails
       {
           get
           {
               return this._ObjOrderDetails;
           }
           set
           {
               if ((this._ObjOrderDetails != value))
               {
                   this._ObjOrderDetails = value;
               }
           }
       }

       public PaymentInfo ObjPaymentInfo
       {
           get
           {
               return this._ObjPaymentInfo;
           }
           set
           {
               if ((this._ObjPaymentInfo != value))
               {
                   this._ObjPaymentInfo = value;
               }
           }
       }

       public UserAddressInfo ObjBillingAddressInfo
       {
           get
           {
               return this._ObjBillingAddressInfo;
           }
           set
           {
               if ((this._ObjBillingAddressInfo != value))
               {
                   this._ObjBillingAddressInfo = value;
               }
           }
       }

       public UserAddressInfo ObjShippingAddressInfo
       {
           get
           {
               return this._ObjShippingAddressInfo;
           }
           set
           {
               if ((this._ObjShippingAddressInfo != value))
               {
                   this._ObjShippingAddressInfo = value;
               }
           }
       }

       public List<OrderItemInfo> LstOrderItemsInfo
       {
           get
           {
               return this._LstOrderItemsInfo;
           }
           set
           {
               if ((this._LstOrderItemsInfo != value))
               {
                   this._LstOrderItemsInfo = value;
               }
           }
       }

       public List<GiftCardUsage> GiftCardDetail {
           get
           {
               return this._giftCardDetail;
           }
           set
           {
               if ((this._giftCardDetail != value))
               {
                   this._giftCardDetail = value;
               }
           }
       }

       public CommonInfo ObjCommonInfo
       {
           get
           {
               return this._ObjCommonInfo;
           }
           set
           {
               if ((this._ObjCommonInfo != value))
               {
                   this._ObjCommonInfo = value;
               }
           }
       }
       public List<OrderTaxInfo> ObjOrderTaxInfo
       {
           get
           {
               return this._ObjOrderTaxInfo;
           }
           set
           {
               if ((this._ObjOrderTaxInfo != value))
               {
                   this._ObjOrderTaxInfo = value;
               }
           }
       }

       public List<CouponSession> Coupons { get; set; }


       #endregion
   }
}
