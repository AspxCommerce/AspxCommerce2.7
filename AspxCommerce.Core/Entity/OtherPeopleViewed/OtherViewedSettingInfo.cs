using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

/// <summary>
/// Summary description for OtherViewedSettingInfo
/// </summary>
public class OtherViewedSettingInfo
{
    public OtherViewedSettingInfo()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    private string _headerTitle;
    private int _count;
    private bool _itemRating;
    [DataMember]
    public string HeaderTitle
    {
        get { return _headerTitle; }
        set { _headerTitle = value; }
    }
    [DataMember]
    public int NoOfItemShow
    {
        get { return _count; }
        set { _count = value; }
    }
    [DataMember]
    public bool ItemRatingShowOrNot
    {
        get { return _itemRating; }
        set { _itemRating = value; }
    }
}
