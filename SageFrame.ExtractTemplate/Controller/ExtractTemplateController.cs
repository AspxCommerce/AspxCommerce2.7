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
using System.Data;
#endregion

namespace SageFrame.ExtractTemplate
{
    /// <summary>
    /// Business logic for template extraction.
    /// </summary>
    public class ExtractTemplateController
    {

        /// <summary>
        /// Returns template permission by usermoduelID. 
        /// </summary>
        /// <param name="userModuleID">User module ID.</param>
        /// <returns>List of template permissions.</returns>
        public List<TemplatePermission> GetTemplatePermission(string userModuleID)
        {
            ExtractTemplateDataProvider objDataProvider = new ExtractTemplateDataProvider();
            return objDataProvider.GetTemplatePermission(userModuleID);
        }

        /// <summary>
        /// Returns list of ExtractInfo object containing the detail of Modules
        /// </summary>
        /// <param name="PaneName">Pane name.</param>
        /// <param name="portalID">Portal ID.</param>
        /// <returns>List of ExtractInfo object containing the detail of Modules</returns>
        public List<ExtractInfo> GetTemplateDetails(string paneName, int portalID)
        {
            ExtractTemplateDataProvider objDataProvider = new ExtractTemplateDataProvider();
            return objDataProvider.GetTemplateDetails(paneName, portalID);
        }

        /// <summary>
        /// Returns HTML module details
        /// </summary>
        /// <param name="HtmlUserModuleID">HtmlUserModuleID</param>
        /// <returns>Dataset containg the details of html modules.</returns>
        public DataSet MakeHtmlDataSet(string HtmlUserModuleID)
        {
            ExtractTemplateDataProvider objDataProvider = new ExtractTemplateDataProvider();
            return objDataProvider.MakeHtmlDataSet(HtmlUserModuleID);
        }


        /// <summary>
        /// Inserts template details.
        /// </summary>
        /// <param name="lstPageList">List of page extracted from XML.</param>
        /// <param name="objTemplateMenuall">List of menu extracted from XML.</param>
        /// <param name="portalID">Portal ID.</param>
        public void InsertTemplate(List<ExtractPageInfo> lstPageList, List<TemplateMenuAll> objTemplateMenuall, int portalID)
        {
            ExtractTemplateDataProvider objDataProvider = new ExtractTemplateDataProvider();
            objDataProvider.InsertTemplate(lstPageList, objTemplateMenuall, portalID);
        }

        /// <summary>
        /// Returns List of page permission.
        /// </summary>
        /// <param name="pageID">Page ID.</param>
        /// <returns>List of page permission.</returns>

        public List<PagePermission> GetPagePermission(string pageID)
        {
            ExtractTemplateDataProvider objDataProvider = new ExtractTemplateDataProvider();
            return objDataProvider.GetPagePermission(pageID);
        }


        /// <summary>
        /// Returns menu details
        /// </summary>
        /// <param name="portalID">Portal ID</param>
        /// <param name="menuUserModuleID">Menu's usermodule ID.</param>
        /// <returns>Dataset containing menu details.</returns>
        public DataSet GetMenuDetail(int portalID, string menuUserModuleID)
        {
            ExtractTemplateDataProvider objDataProvider = new ExtractTemplateDataProvider();
            return objDataProvider.GetMenuDetail(portalID, menuUserModuleID);
        }
    }
}
