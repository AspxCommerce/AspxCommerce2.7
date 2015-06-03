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
/// Entity clas for component.
/// </summary>
[Serializable]
public class Component
{
    /// <summary>
    /// Gets or sets component's name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets component's friendly name.
    /// </summary>
    public string FriendlyName { get; set; }

    /// <summary>
    /// Gets or sets component's description.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Gets or sets component's version.
    /// </summary>
    public string Version { get; set; }

    /// <summary>
    /// Gets or sets component's business controller class.
    /// </summary>
    public string BusinesscontrollerClass { get; set; }

    /// <summary>
    /// Gets or sets component's zip file name.
    /// </summary>
    public string ZipFile { get; set; }

    /// <summary>
    /// Returns or retains true if the component  is checked.
    /// </summary>
    public bool IsChecked { get; set; }


}
