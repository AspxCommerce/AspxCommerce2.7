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
/// Entity class  of control information.
/// </summary>
[Serializable]
public class ControlInfo
{
    /// <summary>
    /// Gets or sets control key.
    /// </summary>
    public string Key { get; set; }

    /// <summary>
    /// Gets or sets control's title.
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Gets or sets control type.
    /// </summary>
    public string Type { get; set; }

    /// <summary>
    /// Gets or sets control's help URL.
    /// </summary>
    public string HelpUrl { get; set; }

    /// <summary>
    /// Gets or sets controls partial supports
    /// </summary>
    public string SupportSpatial { get; set; }
    
    /// <summary>
    /// Gets or sets control's src.
    /// </summary>
    public string Src { get; set; }
}

