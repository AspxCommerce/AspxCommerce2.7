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
/// This class holds the properties for PackageInfo.
/// </summary>
[Serializable]
public class PackageInfo
{
    #region Properties
    /// <summary>
    /// Gets or sets PackageName.
    /// </summary>
    public string PackageName { get; set; }
    /// <summary>
    /// Gets or sets PackageType.
    /// </summary>
    public string PackageType { get; set; }
    /// <summary>
    /// Gets or sets FriendlyName.
    /// </summary>
    public string FriendlyName { get; set; }
    /// <summary>
    /// Gets or sets Description.
    /// </summary>
    public string Description { get; set; }
    /// <summary>
    /// Gets or sets ManifestFile.
    /// </summary>
    public string ManifestFile { get; set; }
    /// <summary>
    /// Gets or sets Version.
    /// </summary>
    public string Version { get; set; }
    /// <summary>
    /// Gets or sets OwnerName.
    /// </summary>
    public string OwnerName { get; set; }
    /// <summary>
    /// Gets or sets Organistaion.
    /// </summary>
    public string Organistaion { get; set; }
    /// <summary>
    /// Gets or sets URL.
    /// </summary>
    public string URL { get; set; }
    /// <summary>
    /// Gets or sets Email.
    /// </summary>
    public string Email { get; set; }
    private string _ReleaseNotes;
    /// <summary>
    /// Gets or sets ReleaseNotes.
    /// </summary>
    public string ReleaseNotes { get { return (_ReleaseNotes); } set { _ReleaseNotes = value; } }
    private string _License;
    /// <summary>
    /// Gets or sets License.
    /// </summary>
    public string License { get { return (_License); } set { _License = value; } }
    private string _TempFolderPath;
    /// <summary>
    /// Gets or sets TempFolderPath.
    /// </summary>
    public string TempFolderPath { get { return (_TempFolderPath); } set { _TempFolderPath = value; } }
    private string _InstalledFolderPath;
    /// <summary>
    /// Gets or sets InstalledFolderPath.
    /// </summary>
    public string InstalledFolderPath { get { return (_InstalledFolderPath); } set { _InstalledFolderPath = value; } }
    /// <summary>
    /// Gets or sets FolderName.
    /// </summary>
    public string FolderName { get; set; }
    #endregion
    /// <summary>
    /// Initializes a new instance of the PackageInfo class.
    /// </summary>
    public PackageInfo() { }

}

