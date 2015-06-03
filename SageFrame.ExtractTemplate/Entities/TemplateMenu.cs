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

namespace SageFrame.ExtractTemplate
{
    /// <summary>
    /// Class that contains template related properties.
    /// </summary>
    public class TemplateMenu
    {
        /// <summary>
        /// Gets or sets user module ID.
        /// </summary>
        public int UserModuleID { get; set; }

        /// <summary>
        /// Gets or sets menu item ID.
        /// </summary>
        public int MenuItemID { get; set; }

        /// <summary>
        /// Gets or sets menu ID.
        /// </summary>
        public int MenuID { get; set; }

        /// <summary>
        /// Gets or sets link type os the template.
        /// </summary>
        public int LinkType { get; set; }

        /// <summary>
        /// Gets or sets page ID.
        /// </summary>
        public int PageID { get; set; }

        /// <summary>
        /// Gets or sets menu level.
        /// </summary>
        public int MenuLevel { get; set; }

        /// <summary>
        /// Gets or sets menu order.
        /// </summary>
        public int MenuOrder { get; set; }

        /// <summary>
        /// Returns or retains true if the module is visible.
        /// </summary>
        public bool Isvisible { get; set; }

        /// <summary>
        /// Returns or retains true if the module is active.
        /// </summary>
        public bool IsActive{ get; set; }

        /// <summary>
        /// Gets or sets temoplate link URL.
        /// </summary>
        public string LinkURL { get; set; }

        /// <summary>
        /// Gets or sets template image icon.
        /// </summary>
        public string ImageIcon { get; set; }

        /// <summary>
        /// Gets or sets template caption.
        /// </summary>
        public string Caption { get; set; }

        /// <summary>
        /// Gets or sets HTML content.
        /// </summary>
        public string HtmlContent { get; set; }

        /// <summary>
        /// Gets or sets title title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets menu name.
        /// </summary>
        public string MenuName { get; set; }

        /// <summary>
        /// Gets or sets parent ID.
        /// </summary>
        public int ParentID { get; set; }

        /// <summary>
        /// Initializes an instance of TemplateMenu class.
        /// </summary>
        public TemplateMenu(){}
    }
}
