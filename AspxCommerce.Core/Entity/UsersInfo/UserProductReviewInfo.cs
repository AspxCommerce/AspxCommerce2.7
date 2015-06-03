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
    [Serializable]
  public  class UserProductReviewInfo
    {
      [DataMember]
      private int _itemRatingID;

      [DataMember]
		private int _itemID;

      [DataMember]
		private string _itemName;

      [DataMember]
		private string _imagePath;

      [DataMember]
		private System.Nullable<int> _itemReviewID;

      [DataMember]
		private string _username;

      [DataMember]
		private System.Nullable<System.DateTime> _addedOn;

      [DataMember]
		private string _addedBy;

      [DataMember]
		private string _reviewSummary;

      [DataMember]
		private string _review;

      [DataMember]
		private string _itemRatingCriteria;

      [DataMember]
		private System.Nullable<int> _itemRatingCriteriaID;

      [DataMember]
		private System.Nullable<decimal> _ratingValue;

      [DataMember]
		private System.Nullable<decimal> _ratingAverage;

        public UserProductReviewInfo()
		{
		}
		
		public int ItemRatingID
		{
			get
			{
				return this._itemRatingID;
			}
			set
			{
				if ((this._itemRatingID != value))
				{
					this._itemRatingID = value;
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
		
		
		public string ImagePath
		{
			get
			{
				return this._imagePath;
			}
			set
			{
				if ((this._imagePath != value))
				{
					this._imagePath = value;
				}
			}
		}
		
		
		public System.Nullable<int> ItemReviewID
		{
			get
			{
				return this._itemReviewID;
			}
			set
			{
				if ((this._itemReviewID != value))
				{
					this._itemReviewID = value;
				}
			}
		}
		
		
		public string Username
		{
			get
			{
				return this._username;
			}
			set
			{
				if ((this._username != value))
				{
					this._username = value;
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
		
		
		public string ReviewSummary
		{
			get
			{
				return this._reviewSummary;
			}
			set
			{
				if ((this._reviewSummary != value))
				{
					this._reviewSummary = value;
				}
			}
		}
		
		public string Review
		{
			get
			{
				return this._review;
			}
			set
			{
				if ((this._review != value))
				{
					this._review = value;
				}
			}
		}		
		
		public string ItemRatingCriteria
		{
			get
			{
				return this._itemRatingCriteria;
			}
			set
			{
				if ((this._itemRatingCriteria != value))
				{
					this._itemRatingCriteria = value;
				}
			}
		}
		
		public System.Nullable<int> ItemRatingCriteriaID
		{
			get
			{
				return this._itemRatingCriteriaID;
			}
			set
			{
				if ((this._itemRatingCriteriaID != value))
				{
					this._itemRatingCriteriaID = value;
				}
			}
		}
		
		public System.Nullable<decimal> RatingValue
		{
			get
			{
				return this._ratingValue;
			}
			set
			{
				if ((this._ratingValue != value))
				{
					this._ratingValue = value;
				}
			}
		}
		
		public System.Nullable<decimal> RatingAverage
		{
			get
			{
				return this._ratingAverage;
			}
			set
			{
				if ((this._ratingAverage != value))
				{
					this._ratingAverage = value;
				}
			}
		}
	}
}

