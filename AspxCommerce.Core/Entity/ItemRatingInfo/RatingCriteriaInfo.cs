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
    public class RatingCriteriaInfo
    {
        #region "Private Members"
        int _rowTotal;
        int _itemRatingCriteriaID;
        string _itemRatingCriteria;
        int _storeID;
        int _portalID;      
        #endregion
        #region "Constructors"
        public RatingCriteriaInfo()
        {
        }
        public RatingCriteriaInfo(int rowTotal, int itemRatingCreteriaId, string itemRatingCriteria,int storeId,int portalId)
        {
            this.RowTotal = rowTotal;
            this.ItemRatingCriteriaID = itemRatingCreteriaId;
            this.ItemRatingCriteria = itemRatingCriteria;
            this.StoreID = storeId;
            this.PortalID = portalId;

        }
        #endregion
        #region "Public Members"
        [DataMember]
        public int RowTotal
        {
            get { return _rowTotal; }
            set { _rowTotal = value; }
        }
        [DataMember]
        public int ItemRatingCriteriaID
        {
            get { return _itemRatingCriteriaID; }
            set { _itemRatingCriteriaID = value; }
        }
        [DataMember]
        public string ItemRatingCriteria
        {
            get { return _itemRatingCriteria; }
            set { _itemRatingCriteria = value; }
        }
        [DataMember]
        public int StoreID
        {
            get { return _storeID; }
            set { _storeID = value; }
        }
        [DataMember]
        public int PortalID
        {
            get { return _portalID; }
            set { _portalID = value; }
        }
        #endregion
    }
}
