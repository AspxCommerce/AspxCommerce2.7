#region "Copyright"

/*
FOR FURTHER DETAILS ABOUT LICENSING, PLEASE VISIT "LICENSE.txt" INSIDE THE SAGEFRAME FOLDER
*/

#endregion

#region "References"

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#endregion

/// <summary>
/// Enity class for composite module.
/// </summary>
[Serializable]
public class CompositeModule
{

    private Int32 _compositemoduleid;
    private string _name;
    private string _packagetype;
    private string _friendlyName;
    private string _description;
    private string _version;
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

    #region "Public Properties"


    /// <summary>
    /// Gets or sets list of component class
    /// </summary>
    public List<Component> Components { get; set; }

    /// <summary>
    /// Gets or sets composite module's manifeast file
    /// </summary>
    public string ManifestFile
    {
        get { return _ManifestFile; }
        set { _ManifestFile = value; }
    }

    /// <summary>
    /// Gets or sets composite module's temp folder path name.
    /// </summary>
    public string TempFolderPath
    {
        get { return _tempFolderPath; }
        set { _tempFolderPath = value; }
    }

    /// <summary>
    /// Gets or sets composite module's installed folder path name.
    /// </summary>
    public string InstalledFolderPath
    {
        get { return _installedFolderPath; }
        set { _installedFolderPath = value; }
    }

    /// <summary>
    /// Gets or sets composite module's ID.
    /// </summary>
    public Int32 Compositemoduleid
    {
        get { return _compositemoduleid; }
        set { _compositemoduleid = value; }
    }

    /// <summary>
    /// Gets or sets composite module's name.
    /// </summary>
    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }

    /// <summary>
    /// Gets or sets composite module's friendly name.
    /// </summary>
    public string FriendlyName
    {
        get { return _friendlyName; }
        set { _friendlyName = value; }
    }

    /// <summary>
    /// Gets or sets composite module's description.
    /// </summary>
    public string Description
    {
        get { return _description; }
        set { _description = value; }
    }

    /// <summary>
    /// Gets or sets composite module's version.
    /// </summary>
    public string Version
    {
        get { return _version; }
        set { _version = value; }
    }
    /// <summary>
    /// Gets or sets composite module's folder name.
    /// </summary>
    public string FolderName
    {
        get { return _foldername; }
        set { _foldername = value; }
    }

    /// <summary>
    /// Gets or sets composite module's name.
    /// </summary>
    public string ModuleName
    {
        get { return _modulename; }
        set { _modulename = value; }
    }

    /// <summary>
    /// Gets or sets composite module's compatible version.
    /// </summary>
    public string CompatibleVersions
    {
        get { return _compatibleversions; }
        set { _compatibleversions = value; }
    }

    /// <summary>
    /// Gets or sets composite module's package type.
    /// </summary>
    public string PackageType
    {
        get { return _packagetype; }
        set { _packagetype = value; }
    }

    /// <summary>
    /// Gets or sets composite module's owner's name.
    /// </summary>
    public string Owner
    {
        get { return _owner; }
        set { _owner = value; }
    }

    /// <summary>
    /// Gets or sets composite module's organization.
    /// </summary>
    public string Organization
    {
        get { return _organization; }
        set { _organization = value; }
    }

    /// <summary>
    /// Gets or sets composite module's URL.
    /// </summary>
    public string URL
    {
        get { return _url; }
        set { _url = value; }
    }

    /// <summary>
    /// Gets or sets composite module's email.
    /// </summary>
    public string Email
    {
        get { return _email; }
        set { _email = value; }
    }

    /// <summary>
    /// Gets or sets composite module's release notes.
    /// </summary>
    public string ReleaseNotes
    {
        get { return _releaseNotes; }
        set { _releaseNotes = value; }
    }

    /// <summary>
    /// Gets or sets composite module's license.
    /// </summary>
    public string License
    {
        get { return _license; }
        set { _license = value; }
    }

    #endregion

    /// <summary>
    /// Initializes an instance of CompositeModule.
    /// </summary>
    public CompositeModule()
    {
        Components = new List<Component>();
    }
}

