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




namespace AspxCommerce.Core
{
    public class StoreInfo
    {
        #region Private Properties
        private int _storeID;

        private string _storeName;

        private string _storeDescription;

        private int _portalID;

        private System.Nullable<bool> _isActive;

        private System.Nullable<bool> _isDeleted;

        private System.Nullable<bool> _isModified;

        private System.Nullable<System.DateTime> _addedOn;

        private System.Nullable<System.DateTime> _updatedOn;

        private System.Nullable<System.DateTime> _deletedOn;

        private string _addedBy;

        private string _updatedBy;

        private string _deletedBy;

        #endregion

        #region Public Properties

        public int StoreID
        {
            get
            {
                return this._storeID;
            }
            set
            {
                if ((this._storeID != value))
                {
                    this._storeID = value;
                }
            }
        }

        public string StoreName
        {
            get
            {
                return this._storeName;
            }
            set
            {
                if ((this._storeName != value))
                {
                    this._storeName = value;
                }
            }
        }

        public string StoreDescription
        {
            get
            {
                return this._storeDescription;
            }
            set
            {
                if ((this._storeDescription != value))
                {
                    this._storeDescription = value;
                }
            }
        }

        public int PortalID
        {
            get
            {
                return this._portalID;
            }
            set
            {
                if ((this._portalID != value))
                {
                    this._portalID = value;
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

        public System.Nullable<bool> IsDeleted
        {
            get
            {
                return this._isDeleted;
            }
            set
            {
                if ((this._isDeleted != value))
                {
                    this._isDeleted = value;
                }
            }
        }

        public System.Nullable<bool> IsModified
        {
            get
            {
                return this._isModified;
            }
            set
            {
                if ((this._isModified != value))
                {
                    this._isModified = value;
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

        public System.Nullable<System.DateTime> UpdatedOn
        {
            get
            {
                return this._updatedOn;
            }
            set
            {
                if ((this._updatedOn != value))
                {
                    this._updatedOn = value;
                }
            }
        }

        public System.Nullable<System.DateTime> DeletedOn
        {
            get
            {
                return this._deletedOn;
            }
            set
            {
                if ((this._deletedOn != value))
                {
                    this._deletedOn = value;
                }
            }
        }

        public string AddedBy
        {
            get
            {
                return this._addedBy;
            }
            set
            {
                if ((this._addedBy != value))
                {
                    this._addedBy = value;
                }
            }
        }

        public string UpdatedBy
        {
            get
            {
                return this._updatedBy;
            }
            set
            {
                if ((this._updatedBy != value))
                {
                    this._updatedBy = value;
                }
            }
        }

        public string DeletedBy
        {
            get
            {
                return this._deletedBy;
            }
            set
            {
                if ((this._deletedBy != value))
                {
                    this._deletedBy = value;
                }
            }
        } 
        #endregion
    }
}
