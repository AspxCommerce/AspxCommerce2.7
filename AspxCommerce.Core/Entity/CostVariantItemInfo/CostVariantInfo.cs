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
     public class CostVariantInfo
    {
        private System.Nullable<int> _rowTotal;
        private System.Nullable<int> _costVariantID;
		private string _costVariantName;
        private bool _isSystem;
		
        public CostVariantInfo()
		{
		}

        [DataMember(Name = "_rowTotal", Order = 0)]
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
        [DataMember(Name = "_costVariantID", Order = 1)]
        public System.Nullable<int> CostVariantID
		{
			get
			{
				return this._costVariantID;
			}
			set
			{
				if ((this._costVariantID != value))
				{
					this._costVariantID = value;
				}
			}
		}

        [DataMember(Name = "_costVariantName", Order = 2)]
		public string CostVariantName
		{
			get
			{
				return this._costVariantName;
			}
			set
			{
				if ((this._costVariantName != value))
				{
					this._costVariantName = value;
				}
			}
		}
        [DataMember(Name = "_isSystem", Order = 3)]
        public bool IsSystem
        {
            get
            {
                return this._isSystem;
            }
            set
            {
                if ((this._isSystem != value))
                {
                    this._isSystem = value;
                }
            }
        }		
    }
}
