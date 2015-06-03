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
using System.Threading;
using System.Text.RegularExpressions;
using System.Collections;
using SageFrame.Web.Utilities;

#endregion


namespace SageFrame.HTMLText
{
    /// <summary>
    /// Business logic class for  HTML Text
    /// </summary>
    public class HTMLController
    {
        /// <summary>
        /// Initializes a new instance of the HTMLController class.
        /// </summary>
        public HTMLController()
        {
        }

        /// <summary>
        /// Deletes HTML comments by commentID.
        /// </summary>
        /// <param name="HTMLCommentID">HTMLCommentID</param>
        /// <param name="PortalID">portalID</param>
        /// <param name="DeletedBy">HTML deleted user's name</param>
        public void HTMLCommentDeleteByCommentID(int HTMLCommentID, int PortalID, string DeletedBy)
        {
            try
            {
                HTMLDataProvider objProvider = new HTMLDataProvider();
                objProvider.HTMLCommentDeleteByCommentID(HTMLCommentID, PortalID, DeletedBy);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Returns HTML comments by HTMLCommentID.
        /// </summary>
        /// <param name="PortalID">portalID</param>
        /// <param name="HTMLCommentID">HTMLCommentID</param>
        /// <returns>Returns HTML Comments</returns>
        public HTMLContentInfo HtmlCommentGetByHTMLCommentID(int PortalID, int HTMLCommentID)
        {
            try
            {
                HTMLDataProvider objProvider = new HTMLDataProvider();
                HTMLContentInfo htmlInfo = objProvider.HtmlCommentGetByHTMLCommentID(PortalID, HTMLCommentID);
                return htmlInfo;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Adds html comment.
        /// </summary>
        /// <param name="HTMLTextID">HTMLTextID</param>
        /// <param name="Comment">Comment</param>
        /// <param name="IsApproved">Set true if the html comment is approve</param>
        /// <param name="IsActive">Set true if the html comment is active</param>
        /// <param name="AddedOn">Added Date</param>
        /// <param name="PortalID">portalID</param>
        /// <param name="AddedBy">Added by</param>
        public void HtmlCommentAdd(string HTMLTextID, string Comment, bool IsApproved, bool IsActive, DateTime AddedOn, int PortalID, string AddedBy)
        {
            try
            {
                HTMLDataProvider objProvider = new HTMLDataProvider();
                objProvider.HtmlCommentAdd(HTMLTextID, Comment, IsApproved, IsActive, AddedOn, PortalID, AddedBy);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Updates html comment.
        /// </summary>
        /// <param name="HTMLCommentID">HTMLCommentID</param>
        /// <param name="Comment">Comment</param>
        /// <param name="IsApproved">Set true if the comment is approved</param>
        /// <param name="IsActive">Set true if the comment is active</param>
        /// <param name="IsModified">Set true if the  comment is modified</param>
        /// <param name="UpdatedOn">Date on which the comment is updated</param>
        /// <param name="PortalID">portalID</param>
        /// <param name="UpdatedBy">Updated user's name</param>
        public void HtmlCommentUpdate(object HTMLCommentID, string Comment, bool IsApproved, bool IsActive, bool IsModified, DateTime UpdatedOn, int PortalID, string UpdatedBy)
        {
            try
            {
                HTMLDataProvider objProvider = new HTMLDataProvider();
                objProvider.HtmlCommentUpdate(HTMLCommentID, Comment, IsApproved, IsActive, IsModified, UpdatedOn, PortalID, UpdatedBy);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// Checks if the user is authentcate to edit HTML.
        /// </summary>
        /// <param name="UserModuleID">userModuleID</param>
        /// <param name="Username">userName</param>
        /// <param name="PortalID">portalID</param>
        /// <returns>returns true if the user has permission to edit</returns>
        public bool IsAuthenticatedToEdit(int UserModuleID, string Username, int PortalID)
        {
            try
            {
                HTMLDataProvider objProvider = new HTMLDataProvider();
                bool IsAuthenticated = objProvider.IsAuthenticatedToEdit(UserModuleID, Username, PortalID);
                return IsAuthenticated;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// Returns the HTML content  for given portal , username and culture.
        /// </summary>
        /// <param name="PortalID">portalID</param>
        /// <param name="UsermoduleID">usermoduleID</param>
        /// <param name="CultureName">Culturename</param>
        /// <returns>returns HTMLContentInfo object</returns>
        public HTMLContentInfo GetHTMLContent(int PortalID, int UsermoduleID, string CultureName)
        {
            try
            {
                HTMLDataProvider objProvider = new HTMLDataProvider();
                HTMLContentInfo htmlContentInfo = objProvider.GetHTMLContent(PortalID, UsermoduleID, CultureName);
                return htmlContentInfo;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// Returns the comment lists for superuser role.
        /// </summary>
        /// <param name="PortalID">portalID</param>
        /// <param name="HTMLTextID">HTMLTextID</param>
        /// <returns>Lists of HTMLContentInfo object which contains the comments. </returns>
        public List<HTMLContentInfo> BindCommentForSuperUser(int PortalID, string HTMLTextID)
        {
            try
            {
                HTMLDataProvider objProvider = new HTMLDataProvider();
                List<HTMLContentInfo> ml = objProvider.BindCommentForSuperUser(PortalID, HTMLTextID);
                return ml;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Returns the comment lists.
        /// </summary>
        /// <param name="PortalID">portalID</param>
        /// <param name="HTMLTextID">HTMLTextID</param>
        /// <returns>Lists of HTMLContentInfo object which contains the comments. </returns>
        public List<HTMLContentInfo> BindCommentForOthers(int PortalID, string HTMLTextID)
        {
            try
            {
                HTMLDataProvider objProvider = new HTMLDataProvider();
                List<HTMLContentInfo> ml = objProvider.BindCommentForOthers(PortalID, HTMLTextID);
                return ml;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Returns HTMLContentInfo containing HTML text by poralID, usermoduleID and culture name.
        /// </summary>
        /// <param name="PortalID">portalID</param>
        /// <param name="UsermoduleID">userModuleID</param>
        /// <param name="CultureName">Culture name</param>
        /// <returns>Returns HTMLContentInfo containing HTML </returns>
        public HTMLContentInfo HtmlTextGetByPortalAndUserModule(int PortalID, int UsermoduleID, string CultureName)
        {
            try
            {
                HTMLDataProvider objProvider = new HTMLDataProvider();
                HTMLContentInfo objHtmlContent = objProvider.HtmlTextGetByPortalAndUserModule(PortalID, UsermoduleID, CultureName);
                return objHtmlContent;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Updates HTML Text.
        /// </summary>
        /// <param name="UserModuleID">userModuleID</param>
        /// <param name="Content">Content to be save</param>
        /// <param name="CultureName">Culture name</param>
        /// <param name="IsAllowedToComment">Set true if comment is allowed.</param>
        /// <param name="IsActive">Set true if the HTML text is active.</param>
        /// <param name="IsModified">Set true if the HTML text is modified.</param>
        /// <param name="UpdatedOn">Updated date</param>
        /// <param name="PortalID">portalID</param>
        /// <param name="UpdatedBy">Updated by</param>
        public void HtmlTextUpdate(string UserModuleID, string Content, string CultureName, bool IsAllowedToComment, bool IsActive, bool IsModified, DateTime UpdatedOn, int PortalID, string UpdatedBy)
        {
            try
            {
                HTMLDataProvider objProvider = new HTMLDataProvider();
                objProvider.HtmlTextUpdate(UserModuleID, Content, CultureName, IsAllowedToComment, IsActive, IsModified, UpdatedOn, PortalID, UpdatedBy);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// Adds HTML Text.
        /// </summary>
        /// <param name="UserModuleID">userModuleID</param>
        /// <param name="Content">Content</param>
        /// <param name="CultureName">Culture name</param>
        /// <param name="IsAllowedToComment">Set true if comment is allowed.</param>
        /// <param name="IsModified">Set true if the HTML text is modified.</param>
        /// <param name="IsActive">Set true if the HTML text is active.</param>
        /// <param name="AddedOn">Added date</param>
        /// <param name="PortalID">portalID</param>
        /// <param name="AddedBy">HTML text added user's name</param>
        /// <returns>Returns 1 if inserted succesfully</returns>
        public int HtmlTextAdd(string UserModuleID, string Content, string CultureName, bool IsAllowedToComment, bool IsModified, bool IsActive, DateTime AddedOn, int PortalID, string AddedBy)
        {
            try
            {
                HTMLDataProvider objProvider = new HTMLDataProvider();
                int htmlTextAdd = objProvider.HtmlTextAdd(UserModuleID, Content, CultureName, IsAllowedToComment, IsModified, IsActive, AddedOn, PortalID, AddedBy);
                return htmlTextAdd;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Removes unWanted HTML tags for a given string
        /// </summary>
        /// <param name="str">string from which unwanted value is to be remove</param>
        /// <returns>Unwanted HTML value Tag cleared string</returns>
        public string RemoveUnwantedHTMLTAG(string str)
        {
            str = System.Text.RegularExpressions.Regex.Replace(str, "<br/>$", "");
            str = System.Text.RegularExpressions.Regex.Replace(str, "^&nbsp;", "");
            //str = System.Text.RegularExpressions.Regex.Replace(str, "/(<form[^\>]*\>)([\s\S]*)(\<\/form\>)/i", "");
            str = System.Text.RegularExpressions.Regex.Replace(str, "<form[^>]*>", "");
            str = System.Text.RegularExpressions.Regex.Replace(str, "</form>", "");
            return str; //Regex.Replace(str, @"<(.|\n)*?>", string.Empty);
        }

        /// <summary>
        /// Check if the user is authehticate to edit HTML.
        /// </summary>
        /// <param name="usermoduleid">userModuleID</param>
        /// <param name="username">username</param>
        /// <param name="portalid">portalID</param>
        /// <returns>Returns true if the user is authenticate to edit HTML content</returns>
        public static bool IsAuthenticatedToEdit(string usermoduleid, string username, int portalid)
        {
            try
            {
                HTMLDataProvider objProvider = new HTMLDataProvider();
                bool IsAunticateToEdit = HTMLDataProvider.IsAuthenticatedToEdit(usermoduleid, username, portalid);
                return IsAunticateToEdit;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
