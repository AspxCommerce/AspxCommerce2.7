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
    public class OrderStatusListInfo
    {
        public OrderStatusListInfo()
        {
        }

        [DataMember(Name = "_rowTotal", Order = 0)]
        private System.Nullable<int> _rowTotal;

        [DataMember(Name = "OrderStatusID", Order = 1)]
		private int _orderStatusID;

        [DataMember(Name = "OrderStatusAliasName", Order = 2)]
        private string _orderStatusAliasName;
               
        [DataMember(Name = "AliasToolTip", Order = 3)]
		private string _aliasToolTip;

        [DataMember(Name = "AliasHelp", Order = 4)]
		private string _aliasHelp;

        [DataMember(Name = "AddedOn", Order = 5)]
        private System.Nullable<System.DateTime> _addedOn;

        [DataMember(Name = "_isSystemUsed", Order = 6)]
        private System.Nullable<bool> _isSystemUsed;

        [DataMember(Name = "IsActive", Order = 7)]
        private System.Nullable<bool> _isActive;
        [DataMember(Name = "IsReduceInQuantity", Order = 8)]
        private System.Nullable<bool> _isReduceInQuantity;
        
		
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

		public int OrderStatusID
		{
			get
			{
				return this._orderStatusID;
			}
			set
			{
				if ((this._orderStatusID != value))
				{
					this._orderStatusID = value;
				}
			}
		}

        public string OrderStatusAliasName
        {
            get
            {
                return this._orderStatusAliasName;
            }
            set
            {
                if ((this._orderStatusAliasName != value))
                {
                    this._orderStatusAliasName = value;
                }
            }
        }

        public string AliasToolTip
        {
            get
            {
                return this._aliasToolTip;
            }
            set
            {
                if ((this._aliasToolTip != value))
                {
                    this._aliasToolTip = value;
                }
            }
        }

        public string AliasHelp
        {
            get
            {
                return this._aliasHelp;
            }
            set
            {
                if ((this._aliasHelp != value))
                {
                    this._aliasHelp = value;
                }
            }
        }
		
		public System.Nullable<System.DateTime> AddedOn
		{
			get
			{
				return this._addedOn;
			}
			set
			{
				if ((this._addedOn != value))
				{
					this._addedOn = value;
				}
			}
		}

        public System.Nullable<bool> IsSystemUsed
        {
            get
            {
                return this._isSystemUsed;
            }
            set
            {
                if ((this._isSystemUsed != value))
                {
                    this._isSystemUsed = value;
                }
            }
        }
		
		public System.Nullable<bool> IsActive
		{
			get
			{
				return this._isActive;
			}
			set
			{
				if ((this._isActive != value))
				{
					this._isActive = value;
				}
			}
		}
        public System.Nullable<bool> IsReduceInQuantity
        {
            get
            {
                return this._isReduceInQuantity;
            }
            set
            {
                if ((this._isReduceInQuantity != value))
                {
                    this._isReduceInQuantity = value;
                }
            }
        }		
    }
    public class SaveOrderStatusInfo 
    {
        public int OrderStatusID { get; set; }
        public string OrderStatusAliasName { get; set; }
        public string AliasToolTip { get; set; }
        public string AliasHelp { get; set; }
        public bool IsSystemUsed { get; set; }
        public bool IsActive { get; set; }
        public bool IsReduceInQuantity { get; set; }
        
    }
  
}
