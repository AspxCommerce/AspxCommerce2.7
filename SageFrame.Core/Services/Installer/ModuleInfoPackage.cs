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
/// Class that inherits ModuleInfo class and has properties gfor module packages.
/// </summary>
[Serializable]
public class ModuleInfoPackage : ModuleInfo
{
    List<ModuleElement> _moduleElements = new List<ModuleElement>();
    List<string> _fileNames = new List<string>();
    /// <summary>
    /// Gets or sets modules's elements.
    /// </summary>
    public List<ModuleElement> ModuleElements { get { return _moduleElements; } }

    /// <summary>
    /// Gets or sets module file names.
    /// </summary>
    public List<string> FileNames { get { return _fileNames; } }
}

