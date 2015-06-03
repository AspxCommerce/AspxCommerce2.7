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

namespace SageFrame.Templating.xmlparser
{
    /// <summary>
    /// Enum for XML tag types.
    /// </summary>
    public enum XmlTagTypes
    {
        Layout=0,
        Section=1,
        Placeholder=2,

        PAGEPRESETS=3,
        PAGEPRESET=4,
        PRESET=5,
        PAGES=6,
        TEMPLATE = 7,
        NAME = 8,
        AUTHOR = 9,
        DESCRIPTION = 10,
        WEBSITE = 11,
        WRAPPERS=12,
        WRAP=13,
        SFLEFT=14,
        SFMIDDLE=15,
        SFRIGHT=16
    }

    
}
