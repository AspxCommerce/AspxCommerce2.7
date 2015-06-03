#region "Copyright"

/*
FOR FURTHER DETAILS ABOUT LICENSING, PLEASE VISIT "LICENSE.txt" INSIDE THE SAGEFRAME FOLDER
*/

#endregion

#region "References"

using System;
using System.Collections.Generic;
using System.Web;

#endregion


/// <summary>
///  This class holds the properties for Module
/// </summary>
[Serializable]
public class ModuleInfo
{
   
    private Int32 _moduleid;
    private string _name;
    private string _packagetype;
    private string _friendlyName;
    private string _description;
    private string _version;
    private string _businesscontrollerclass;
    private string _foldername;
    private string _modulename;
    private string _compatibleversions;
    private string _ManifestFile = string.Empty;
    private string _tempFolderPath = string.Empty;
    private string _installedFolderPath = string.Empty;
    private string _owner = string.Empty;
    private string _organization = string.Empty;
    private string _url = string.Empty;
    private string _email = string.Empty;
    private string _releaseNotes = string.Empty;
    private string _license = string.Empty;

    /// <summary>
    /// Get or set module definition ID.
    /// </summary>
    public int ModuleDefID { get; set; }
    /// <summary>
    /// Get or set IsPortable.
    /// </summary>
    public bool IsPortable { get; set; }
    /// <summary>
    ///  Get or set IsSearchable.
    /// </summary>
    public bool IsSearchable { get; set; }
    /// <summary>
    /// Get or set IsUpgradable.
    /// </summary>
    public bool IsUpgradable { get; set; }
    /// <summary>
    /// Get or set package name.
    /// </summary>
    public string PackageName { get; set; }
    /// <summary>
    /// Get or set package description.
    /// </summary>
    public string PackageDescription { get; set; }
    /// <summary>
    /// Get or set PortalID
    /// </summary>
    public int PortalID { get; set; }
    /// <summary>
    /// Get  or set user name.
    /// </summary>
    public string Username { get; set; }
    /// <summary>
    /// Get or set module permission ID
    /// </summary>
    public int ModuleDefPermissionID { get; set; }
    /// <summary>
    /// Get or set portal module permission ID.
    /// </summary>
    public int PortalModulePermissionID { get; set; }
    /// <summary>
    /// Get or set list of ModuleInfo object.
    /// </summary>
    public List<ModuleInfo> ChildModules { get; set; }

    private bool _isPremium;
    private int _supportedFeatures;
    private string _dependencies = string.Empty;
    private string _permissions = string.Empty;


    /// <summary>
    /// Get or set application's dependencies.
    /// </summary>
    public string dependencies
    {
        get { return _dependencies; }
        set { _dependencies = value; }
    }
    /// <summary>
    /// Get or set permission. 
    /// </summary>
    public string permissions
    {
        get { return _permissions; }
        set { _permissions = value; }
    }
    /// <summary>
    /// Get or set supported features.
    /// </summary>
    public int supportedFeatures
    {
        get { return _supportedFeatures; }
        set { _supportedFeatures = value; }
    }
    /// <summary>
    /// Get or set premium.
    /// </summary>
    public bool isPremium
    {
        get { return _isPremium; }
        set { _isPremium = value; }
    }

    /// <summary>
    /// Get or set ManifestFile.
    /// </summary>
    public string ManifestFile
    {
        get { return _ManifestFile; }
        set { _ManifestFile = value; }
    }
    /// <summary>
    /// Get or set temporary folder path.
    /// </summary>
    public string TempFolderPath
    {
        get { return _tempFolderPath; }
        set { _tempFolderPath = value; }
    }
    /// <summary>
    /// Get or set application installed folder path.
    /// </summary>
    public string InstalledFolderPath
    {
        get { return _installedFolderPath; }
        set { _installedFolderPath = value; }
    }
    /// <summary>
    /// Get or set module ID.
    /// </summary>
    public Int32 ModuleID
    {
        get { return _moduleid; }
        set { _moduleid = value; }
    }
    /// <summary>
    /// Get or set name.
    /// </summary>
    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }
    /// <summary>
    /// Get or set friendly name.
    /// </summary>
    public string FriendlyName
    {
        get { return _friendlyName; }
        set { _friendlyName = value; }
    }
    /// <summary>
    /// Get or set description.
    /// </summary>
    public string Description
    {
        get { return _description; }
        set { _description = value; }
    }
    /// <summary>
    /// Get or set version.
    /// </summary>
    public string Version
    {
        get { return _version; }
        set { _version = value; }
    }
    /// <summary>
    /// Get or set BusinessControllerClass.
    /// </summary>
    public string BusinessControllerClass
    {
        get { return _businesscontrollerclass; }
        set { _businesscontrollerclass = value; }
    }
    /// <summary>
    /// Get or set folder name.
    /// </summary>
    public string FolderName
    {
        get { return _foldername; }
        set { _foldername = value; }
    }
    /// <summary>
    /// Get or set module name.
    /// </summary>
    public string ModuleName
    {
        get { return _modulename; }
        set { _modulename = value; }
    }
    /// <summary>
    /// Get or set compatible version.
    /// </summary>
    public string CompatibleVersions
    {
        get { return _compatibleversions; }
        set { _compatibleversions = value; }
    }
    /// <summary>
    /// Get or set package type.
    /// </summary>
    public string PackageType
    {
        get { return _packagetype; }
        set { _packagetype = value; }
    }
    /// <summary>
    /// Get or set owner.
    /// </summary>
    public string Owner
    {
        get { return _owner; }
        set { _owner = value; }
    }
    /// <summary>
    /// Get or set organization.
    /// </summary>
    public string Organization
    {
        get { return _organization; }
        set { _organization = value; }
    }
    /// <summary>
    /// Get or set URL.
    /// </summary>
    public string URL
    {
        get { return _url; }
        set { _url = value; }
    }
    /// <summary>
    /// Get or set Email.
    /// </summary>
    public string Email
    {
        get { return _email; }
        set { _email = value; }
    }
    /// <summary>
    /// Get or set ReleaseNotes.
    /// </summary>
    public string ReleaseNotes
    {
        get { return _releaseNotes; }
        set { _releaseNotes = value; }
    }
    /// <summary>
    /// Get or set license.
    /// </summary>
    public string License
    {
        get { return _license; }
        set { _license = value; }
    }


    /// <summary>
    /// Initializes a new instance of the ModuleInfo class.
    /// </summary>
    public ModuleInfo()
    {
        ChildModules = new List<ModuleInfo>();
        //
        // TODO: Add constructor logic here
        //
    }
}
