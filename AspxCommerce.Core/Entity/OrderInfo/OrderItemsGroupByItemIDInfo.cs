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
using System.Runtime.Serialization;

namespace AspxCommerce.Core
{
    [DataContract]
    [Serializable]
   public class OrderItemsGroupByItemIDInfo
   {
       #region constructor
       public OrderItemsGroupByItemIDInfo()
        {
        }
       #endregion
       #region Private Members
       [DataMember(Name = "_rowTotal", Order = 0)]
       private System.Nullable<int> _rowTotal;

       [DataMember(Name = "_itemID", Order = 1)]
       private int _itemID;

       [DataMember(Name = "_itemName", Order = 2)]
       private string _itemName;

       [DataMember(Name = "_quantity", Order = 5)]
       private string _quantity;
       #endregion
       #region Public Members

       public System.Nullable<int> RowTotal
       {
           get
           {
               return this._rowTotal;
           }
           set
           {
               if ((this._rowTotal != value))
               {
                   this._rowTotal = value;
               }
           }
       }

       public int ItemID
       {
           get
           {
               return this._itemID;
           }
           set
           {
               if ((this._itemID != value))
               {
                   this._itemID = value;
               }
           }
       }

       public string ItemName
       {
           get
           {
               return this._itemName;
           }
           set
           {
               if ((this._itemName != value))
               {
                   this._itemName = value;
               }
           }
       }

       public string Quantity
       {
           get
           {
               return this._quantity;
           }
           set
           {
               if ((this._quantity != value))
               {
                   this._quantity = value;
               }
           }
       }


       #endregion
   }
}
