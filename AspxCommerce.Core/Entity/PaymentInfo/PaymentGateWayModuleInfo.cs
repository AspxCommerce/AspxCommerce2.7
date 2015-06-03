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

namespace AspxCommerce.Core
{
  
[Serializable]
public class PaymentGateWayModuleInfo
{   
    private Int32 _paymentGatewayTypeID;
    private string _paymentGatewayTypeName;
    private string _name;
    private string _friendlyName;
    private string _description;
    private string _version;   
    private string _foldername;  
    private string _manifestFile = string.Empty;
    private string _tempFolderPath = string.Empty;
    private string _installedFolderPath = string.Empty;  
    private string _cultureName = string.Empty;
    private string _settingKey = string.Empty;
    private string _settingValue= string.Empty;
    private int _storeID =1;
    private int _portalID =1;

    #region "Public Properties"
    public Int32 PaymentGatewayTypeID
    {
        get { return _paymentGatewayTypeID; }
        set { _paymentGatewayTypeID = value; }
    }
    public string PaymentGatewayTypeName
    {
        get { return _paymentGatewayTypeName; }
        set { _paymentGatewayTypeName = value; }
    }
   
    public string CultureName
    {
        get { return _cultureName; }
        set { _cultureName = value; }
    }
    public string SettingKey
    {
        get { return _settingKey; }
        set { _settingKey = value; }
    }
    public string SettingValue
    {
        get { return _settingValue; }
        set { _settingValue = value; }
    }

    public string ManifestFile
    {
        get { return _manifestFile; }
        set { _manifestFile = value; }
    }

    public string TempFolderPath
    {
        get { return _tempFolderPath; }
        set { _tempFolderPath = value; }
    }

    public string InstalledFolderPath
    {
        get { return _installedFolderPath; }
        set { _installedFolderPath = value; }
    }

   
    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }
    public string FriendlyName
    {
        get { return _friendlyName; }
        set { _friendlyName = value; }
    }
    public string Description
    {
        get { return _description; }
        set { _description = value; }
    }
    public string Version
    {
        get { return _version; }
        set { _version = value; }
    }
   
    public string FolderName
    {
        get { return _foldername; }
        set { _foldername = value; }
    }

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

    #endregion

    public PaymentGateWayModuleInfo()
    {
        //
        // TODO: Add constructor logic here
        //
    }
}

}