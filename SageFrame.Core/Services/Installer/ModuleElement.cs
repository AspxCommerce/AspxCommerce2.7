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
/// Class that contains method for module elements.
/// </summary>
[Serializable]
public class ModuleElement
{
    /// <summary>
    /// Gets or sets module's friendly name.
    /// </summary>
    public string FriendlyName { get; set; }

    /// <summary>
    /// Gets or sets module's cache time.
    /// </summary>
    public string CacheTime { get; set; }

    /// <summary>
    /// Gets or sets list of object of ControlInfo class.
    /// </summary>
    public List<ControlInfo> Controls { get; set; }
}

