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
    public class ItemsInfoSettings
    {
        public ItemsInfoSettings()
        {
        }

        private int _itemImageID;
        private int _itemID;
        private string _imagePath;
        private bool _isActive;
        private string _imageType;

        // [DataMember(Name = "AlternateText", Order = 1)]
        private string _alternateText;
        private System.Nullable<int> _displayOrder;


        public int ItemImageID
        {
            get { return _itemImageID; }
            set { _itemImageID = value; }
        }
        public int ItemID
        {
            get { return _itemID; }
            set { _itemID = value; }
        }
        public string ImagePath
        {
            get { return _imagePath; }
            set { _imagePath = value; }
        }


        public bool IsActive
        {
            get { return _isActive; }
            set { _isActive = value; }
        }


        public string ImageType
        {
            get { return _imageType; }
            set { _imageType = value; }
        }


        //get
        //    {
        //        return this._ShowInAdvanceSearch;
        //    }
        //    set
        //    {
        //        if ((this._ShowInAdvanceSearch != value))
        //        {
        //            this._ShowInAdvanceSearch = value;
        //        }
        //    }

        public string AlternateText
        {
            get { return _alternateText; }
            set { _alternateText = value; }
            /*   get
               {
                   return this._alternateText;
               }
               set
               {
                   if ((this._alternateText != value))
                   {
                       this._alternateText = value;
                   }
               }*/
        }


        public System.Nullable<int> DisplayOrder
        {
            get { return _displayOrder; }
            set { _displayOrder = value; }
        }
    }
}
