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


namespace SageFrame.Core.TemplateManagement
{
    public class TemplateInfo
    {
        /// <summary>
        /// Gets or sets templateID
        /// </summary>
        public int TemplateID { get; set; }

        /// <summary>
        /// Gets or sets template's title
        /// </summary>
        public string TemplateTitle { get; set; }

        /// <summary>
        /// Gets or sets portal ID.
        /// </summary>
        public int PortalID { get; set; }

        /// <summary>
        /// Gets or sets template author.
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// Gets or sets template description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets template author's URL.
        /// </summary>
        public string AuthorURL { get; set; }

        /// <summary>
        /// Gets or sets template added by.
        /// </summary>
        public string AddedBy { get; set; }

        /// <summary>
        /// Gets or sets template added date.
        /// </summary>
        public DateTime AddedOn { get; set; }

        /// <summary>
        /// Gets or sets template thumbnail.
        /// </summary>
        public string ThumbNail { get; set; }        

        /// <summary>
        /// Initializes and instance of TemplateInfo class.
        /// </summary>
        public TemplateInfo() { }

        /// <summary>
        /// Initializes and instance of TemplateInfo class.
        /// </summary>
        /// <param name="_TemplateTitle">Template name.</param>
        /// <param name="_Author">Template's author.</param>
        /// <param name="_Description">Template's description.</param>
        /// <param name="_AuthorURL">Template's author URL.</param>
        /// <param name="_PortalID">Portal ID.</param>
        /// <param name="_AddedBy">Template added user's name.</param>
        public TemplateInfo(string _TemplateTitle, string _Author, string _Description,string _AuthorURL, int _PortalID, string _AddedBy)
        {
            this.TemplateTitle = _TemplateTitle;
            this.Author = _Author;
            this.Description = _Description;
            this.AuthorURL = _AuthorURL;
            this.PortalID = _PortalID;
            this.AddedBy = _AddedBy;
        }
    }
}
