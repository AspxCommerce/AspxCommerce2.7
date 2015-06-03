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
    public class CategoryDetailsInfo
    {        
	#region Constructor
        public CategoryDetailsInfo()
        {
        }
        #endregion	
        #region Private
        private int _categoryID;	
		
        private string _categoryName;

        private int _categoryLevel;

        private int _itemCount;

        #endregion


        #region Public Fields
        [DataMember]
		public int CategoryID
		{
			get
			{
				return this._categoryID;
			}
			set
			{
				if ((this._categoryID != value))
				{
					this._categoryID = value;
				}
			}
		}
		
		
        [DataMember]
        public string CategoryName
        {
            get
            {
                return this._categoryName;
            }
            set
            {
                if ((this._categoryName != value))
                {
                    this._categoryName = value;
                }
            }
        }

        [DataMember]
        public int CategoryLevel
        {
            get
            {
                return this._categoryLevel;
            }
            set
            {
                if ((this._categoryLevel != value))
                {
                    this._categoryLevel = value;
                }
            }
        }
        [DataMember]
        public int ItemCount
        {
            get
            {
                return this._itemCount;
            }
            set
            {
                if ((this._itemCount != value))
                {
                    this._itemCount = value;
                }
            }
        }
        #endregion
    }
}
    

