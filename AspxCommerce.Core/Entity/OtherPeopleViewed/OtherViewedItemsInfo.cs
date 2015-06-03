using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

/// <summary>
/// Summary description for OtherViewedItemsInfo
/// </summary>
public class OtherViewedItemsInfo
{
    public OtherViewedItemsInfo()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    private int _rowTotal;
    private int _itemID;
       private string _itemName;
      private string _sku;
    private string _imagePath;
    private int _price;
    private bool _isRating;
       
    [DataMember]
    public int RowTotal
    {
        get { return _rowTotal; }
        set { _rowTotal = value; }
    }
       
        [DataMember]
        public int ItemID
        {
            get { return _itemID; }
            set { _itemID = value; }
        }
        [DataMember]
        public string ItemName
        {
            get { return _itemName; }
            set { _itemName = value; }
        }
        [DataMember]
        public string SKU
        {
            get { return _sku; }
            set { _sku = value; }
        }
        [DataMember]
        public string ImagePath
        {
            get { return _imagePath; }
            set { _imagePath = value; }
        }
        [DataMember]
        public int Price
        {
            get { return _price; }
            set { _price = value; }
        }
        [DataMember]
        public bool IsRating
        {
            get { return _isRating; }
            set { _isRating = value; }
        }
      
}
