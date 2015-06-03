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


namespace SageFrame.Core.SageFrame.Search
{
    public class SageFrameSearchInfo
    {
        /// <summary>
        /// Gets or sets page name.
        /// </summary>
        public string PageName { get; set; }

        /// <summary>
        /// Gets or sets user module title.
        /// </summary>
        public string UserModuleTitle { get; set; }

        /// <summary>
        /// Gets or sets HTML contents.
        /// </summary>
        public string HTMLContent { get; set; }

        /// <summary>
        /// Gets or sets URL.
        /// </summary>
        public string URL { get; set; }

        /// <summary>
        /// Gets or sets Updated content.
        /// </summary>
        public string UpdatedContentOn { get; set; }

        /// <summary>
        /// Gets or sets rowtotal.
        /// </summary>
        public int RowTotal { get; set; }

        /// <summary>
        /// Gets or sets searching word.
        /// </summary>
        public string SearchWord { get; set; }

        /// <summary>
        /// Initializes an instance of SageFrameSearchInfo class.
        /// </summary>
        public SageFrameSearchInfo() { }

    }
}
