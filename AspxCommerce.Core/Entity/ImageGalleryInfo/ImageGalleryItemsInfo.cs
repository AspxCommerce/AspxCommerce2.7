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
    public class ImageGalleryItemsInfo
    {
        #region Private Fields
        private int _sku;
        private string _name;
        private string _filePath;
        private string _description;
        private string _shortDescription;
        private decimal _price;
        private decimal _listPrice;
        private decimal _weight;
        private string _imagePath;
        private string _alternateText;

        //i.ItemID AS ID, i.ItemID, it.ItemTypeID , it.ItemTypeName, ias.AttributeSetID, ias.AttributeSetName, 
		//ian.AttributeValue, i.AddedOn, i.IsActive AS Status,  [SKU], [Name],[FilePath], [Price], 
        //[ListPrice],[Weight] ,[Quantity] , [Visibility],[Description],[ShortDescription], [ImagePath]

        //i.ItemID, i.HidePrice, i.HideInRSSFeed, i.HideToAnonymous, --it.ItemTypeID , it.ItemTypeName, ias.AttributeSetID, ias.AttributeSetName, 
        //ian.AttributeValue, i.AddedOn, , ii.[AlternateText], [SKU], [Name],[FilePath], [Price], 
        //[ListPrice],[Weight] ,[Quantity] , [Visibility],[Description],[ShortDescription], ii.[ImagePath]

        //[ItemImageID],[ItemID],[ImagePath],[IsActive],IT.[ImageType],[AlternateText],[DisplayOrder] 

        #endregion

        #region properties 
        [DataMember]
        public int SKU
        {
            get { return _sku; }
            set { _sku = value; }
        }
        [DataMember]
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        [DataMember]
        public string FilePath
        {
            get { return _filePath; }
            set { _filePath = value; }
        }
        [DataMember]
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }
        [DataMember]
        public string ShortDescription
        {
            get { return _shortDescription; }
            set { _shortDescription = value; }
        }        
        [DataMember]
        public decimal Price
        {
            get { return _price; }
            set { _price = value; }
        }
        [DataMember]
        public decimal ListPrice
        {
            get { return _listPrice; }
            set { _listPrice = value; }
        }
        [DataMember]
        public decimal Weight
        {
            get { return _weight; }
            set { _weight = value; }
        } 
        [DataMember]
        public string ImagePath
        {
            get { return _imagePath; }
            set { _imagePath = value; }
        } 
        [DataMember]
        public string AlternateText
        {
            get { return _alternateText; }
            set { _alternateText = value; }
        }          
        #endregion

        #region constructors
        public ImageGalleryItemsInfo()
        {
        }

        public ImageGalleryItemsInfo(int SKU, string name, string filepath, string description, string shortDescription, decimal price, decimal listPrice, decimal weight, string imagePath, string alternateText)
        {
            this.SKU = SKU;
            this.Name = name;
            this.FilePath = filepath;
            this.Description = description;
            this.ShortDescription = shortDescription;
            this.Price = price;
            this.ListPrice = listPrice;
            this.Weight = weight;
            this.ImagePath = imagePath;
            this.AlternateText = alternateText;
        }
        #endregion

    }
}
