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
    /// Manupulates data for HTMLController
    /// </summary>
    public class HTMLDataProvider
    {
        /// <summary>
        /// Initializes a new instance of the HTMLDataProvider class.
        /// </summary>
        public HTMLDataProvider()
        {
        }

        /// <summary>
        /// Connects to database and deletes HTML comments by commentID
        /// </summary>
        /// <param name="HTMLCommentID">HTMLCommentID</param>
        /// <param name="PortalID">portalID</param>
        /// <param name="DeletedBy">HTML deleted user's name</param>
        public void HTMLCommentDeleteByCommentID(int HTMLCommentID, int PortalID, string DeletedBy)
        {
            try
            {
                List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@HTMLCommentID", HTMLCommentID));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@DeletedBy", DeletedBy));
                SQLHandler sq = new SQLHandler();
                sq.ExecuteNonQuery("dbo.sp_HtmlCommentDeleteByCommentID", ParaMeterCollection);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Connects to database and returns HTML comments by HTMLCommentID
        /// </summary>
        /// <param name="PortalID">portalID</param>
        /// <param name="HTMLCommentID">HTMLCommentID</param>
        /// <returns>Returns HTML Comments</returns>
        public HTMLContentInfo HtmlCommentGetByHTMLCommentID(int PortalID, int HTMLCommentID)
        {
            try
            {
                List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@HTMLCommentID", HTMLCommentID));
                SQLHandler sq = new SQLHandler();
                return sq.ExecuteAsObject<HTMLContentInfo>("dbo.sp_HtmlCommentGetByHTMLCommentID", ParaMeterCollection);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Connects to database and adds html comment
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
                List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@HTMLTextID", HTMLTextID));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@Comment", Comment));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@IsApproved", IsApproved));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@IsActive", IsActive));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@AddedOn", AddedOn));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@AddedBy", AddedBy));
                SQLHandler sq = new SQLHandler();
                sq.ExecuteNonQuery("dbo.sp_HtmlCommentAdd", ParaMeterCollection, "@HTMLCommentID");
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Connects to database and updates html comment.
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
                List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@HTMLCommentID", HTMLCommentID));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@Comment", Comment));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@IsApproved", IsApproved));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@IsActive", IsActive));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@IsModified", IsModified));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@UpdatedOn", UpdatedOn));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@UpdatedBy", UpdatedBy));
                SQLHandler sq = new SQLHandler();
                sq.ExecuteNonQuery("dbo.sp_HtmlCommentUpdate", ParaMeterCollection);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Connects to database and checks if the user is authentcate to edit HTML
        /// </summary>
        /// <param name="UserModuleID">userModuleID</param>
        /// <param name="Username">userName</param>
        /// <param name="PortalID">portalID</param>
        /// <returns>returns true if the user has permission to edit</returns>
        public bool IsAuthenticatedToEdit(int UserModuleID, string Username, int PortalID)
        {
            try
            {
                List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@PermissionKey", "EDIT"));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@UserModuleID", UserModuleID));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@UserName", Username));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
                SQLHandler sagesql = new SQLHandler();
                return sagesql.ExecuteAsScalar<bool>("sp_CheckUserModulePermissionByPermissionKeyADO", ParaMeterCollection);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Connects to database and returns the HTML content  for given portal , username and culture
        /// </summary>
        /// <param name="PortalID">portalID</param>
        /// <param name="UsermoduleID">usermoduleID</param>
        /// <param name="CultureName">Culturename</param>
        /// <returns>returns HTMLContentInfo object</returns>
        public HTMLContentInfo GetHTMLContent(int PortalID, int UsermoduleID, string CultureName)
        {
            try
            {
                List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@UsermoduleID", UsermoduleID));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@CultureName", CultureName));
                SQLHandler sagesql = new SQLHandler();
                HTMLContentInfo objHtmlInfo = sagesql.ExecuteAsObject<HTMLContentInfo>("dbo.sp_HtmlTextGetByPortalAndUserModule", ParaMeterCollection);
                return objHtmlInfo;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Connects to database and returns the comment lists for superuser role.
        /// </summary>
        /// <param name="PortalID">portalID</param>
        /// <param name="HTMLTextID">HTMLTextID</param>
        /// <returns>Lists of HTMLContentInfo object which contains the comments. </returns>
        public List<HTMLContentInfo> BindCommentForSuperUser(int PortalID, string HTMLTextID)
        {
            try
            {
                List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@HTMLTextID", HTMLTextID));
                SQLHandler Sq = new SQLHandler();
                List<HTMLContentInfo> ml = Sq.ExecuteAsList<HTMLContentInfo>("dbo.sp_HtmlCommentGetAllByHTMLTextID", ParaMeterCollection);
                return ml;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Connects to database and returns the comment lists.
        /// </summary>
        /// <param name="PortalID">portalID</param>
        /// <param name="HTMLTextID">HTMLTextID</param>
        /// <returns>Lists of HTMLContentInfo object which contains the comments. </returns>
        public List<HTMLContentInfo> BindCommentForOthers(int PortalID, string HTMLTextID)
        {
            try
            {
                List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@HTMLTextID", HTMLTextID));
                SQLHandler Sq = new SQLHandler();
                List<HTMLContentInfo> ml = Sq.ExecuteAsList<HTMLContentInfo>("dbo.sp_HtmlActiveCommentGetByHTMLTextID", ParaMeterCollection);
                return ml;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Connects to database and returns HTMLContentInfo containing HTML text by poralID, usermoduleID and culture name.
        /// </summary>
        /// <param name="PortalID">portalID</param>
        /// <param name="UsermoduleID">userModuleID</param>
        /// <param name="CultureName">Culture name</param>
        /// <returns>Returns HTMLContentInfo containing HTML </returns>
        public HTMLContentInfo HtmlTextGetByPortalAndUserModule(int PortalID, int UsermoduleID, string CultureName)
        {
            try
            {
                List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@UsermoduleID", UsermoduleID));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@CultureName", CultureName));
                SQLHandler Sq = new SQLHandler();
                return Sq.ExecuteAsObject<HTMLContentInfo>("dbo.sp_HtmlTextGetByPortalAndUserModule", ParaMeterCollection);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Connects to database and updates HTML Text.
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
                List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@UserModuleID", UserModuleID));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@Content", Content));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@CultureName", CultureName));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@IsAllowedToComment", IsAllowedToComment));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@IsActive", IsActive));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@IsModified", IsModified));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@UpdatedOn", UpdatedOn));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@UpdatedBy", UpdatedBy));
                SQLHandler Sq = new SQLHandler();
                Sq.ExecuteNonQuery("dbo.sp_HtmlTextUpdate", ParaMeterCollection);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Connects to database and adds HTML Text.
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
                List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@UserModuleID", UserModuleID));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@Content", Content));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@CultureName", CultureName));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@IsAllowedToComment", IsAllowedToComment));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@IsModified", IsModified));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@IsActive", IsActive));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@AddedOn", AddedOn));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@AddedBy", AddedBy));
                SQLHandler Sq = new SQLHandler();
                return Sq.ExecuteNonQueryAsGivenType<int>("dbo.sp_HtmlTextAdd", ParaMeterCollection, "@HTMLTextID");
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Connects to database and  check if the user is authehticate to edit HTML
        /// </summary>
        /// <param name="usermoduleid">userModuleID</param>
        /// <param name="username">username</param>
        /// <param name="portalid">portalID</param>
        /// <returns>Returns true if the user is authenticate to edit HTML content</returns>
        public static bool IsAuthenticatedToEdit(string usermoduleid, string username, int portalid)
        {
            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@PermissionKey", "EDIT"));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@UserModuleID", Int32.Parse(usermoduleid)));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@UserName", username));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@PortalID", portalid));
            SQLHandler sagesql = new SQLHandler();
            return sagesql.ExecuteAsScalar<bool>("sp_CheckUserModulePermissionByPermissionKeyADO", ParaMeterCollection);
        }
    }
}