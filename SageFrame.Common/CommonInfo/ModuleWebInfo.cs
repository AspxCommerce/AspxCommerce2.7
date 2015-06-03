#region "Copyright"

/*
FOR FURTHER DETAILS ABOUT LICENSING, PLEASE VISIT "LICENSE.txt" INSIDE THE SAGEFRAME FOLDER
*/

#endregion

#region "References"

using System;
using System.Collections.Generic;
using System.Text;

#endregion

/// <summary>
/// This class holds the properties for ModuleWebInfo
/// </summary>
[Serializable]
public class ModuleWebInfo
{
    /// <summary>
    /// Get or set module ID.
    /// </summary>
    public int ModuleID { get; set; }
    /// <summary>
    /// Get or set module name.
    /// </summary>
    public string ModuleName { get; set; }
    /// <summary>
    /// Get or set module release date.
    /// </summary>
    public DateTime? ReleaseDate { get; set; }
    /// <summary>
    /// Get or set module description.
    /// </summary>
    public string Description { get; set; }
    /// <summary>
    /// Get or set module version.
    /// </summary>
    public string Version { get; set; }
    /// <summary>
    /// Get or set module download URL.
    /// </summary>
    public string DownloadUrl { get; set; }

}