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
using System.IO;
#endregion

namespace SageFrame.Templating
{
    /// <summary>
    /// Interface for HTMLBuilder.
    /// </summary>
    public interface IHTMLBuilder
    {
        string GenerateOuterWrappers();
        string GenerateDefaultSectionWrappers();
        string GenerateBlockWrappers(string placeholder);
        string GetHTMLContent(string htmlfile,string placeholder);
        void PutPlaceHolders(string name, ref string html,string placeholder);
    }
}
