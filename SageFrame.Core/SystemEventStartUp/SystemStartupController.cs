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
using SageFrame.Web;
using SageFrame.Web.Utilities;
using System.Collections;
using System.Web;

#endregion


namespace SageFrame.Core
{
    /// <summary>
    /// Controller class for system event start up.
    /// </summary>
    public class SystemStartupController
    {
        /// <summary>
        /// Initialiezes an instance of SystemStartupController.
        /// </summary>
        public SystemStartupController()
        {
        }

        /// <summary>
        /// Connects to database and returns system event start up list.
        /// </summary>
        /// <param name="PortalID">portal ID</param>
        /// <returns>List of SystemEventStartUpInfo containing name of controls to be loaded initially. </returns>
        public List<SystemEventStartUpInfo> GetSystemEventStartUpList(int PortalID)
        {
            try
            {
                string sp = "[dbo].[usp_GetSystemEventStartUpList]";
                SQLHandler SQLH = new SQLHandler();
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
                return SQLH.ExecuteAsList<SystemEventStartUpInfo>(sp, ParamCollInput);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// Connects to database and returns the startup control's details  by portalStartUpID.
        /// </summary>
        /// <param name="PortalStartUpID">PortalStartUpID</param>
        /// <returns>Details of startup event.</returns>
        public SystemEventStartUpInfo GetSystemEventStartUpDetails(int PortalStartUpID)
        {
            try
            {
                string sp = "[dbo].[usp_GetSystemEventStartUpDetails]";
                SQLHandler SQLH = new SQLHandler();
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@PortalStartUpID", PortalStartUpID));
                return SQLH.ExecuteAsObject<SystemEventStartUpInfo>(sp, ParamCollInput);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// Connects to database and returns the list of portal startup events.
        /// </summary>
        /// <param name="IsAdmin">Set true if the startup event are admins.</param>
        /// <returns>List of portal startup event's details.</returns>
        public List<GetPortalStartUpList> FnGetPortalStartUpList(bool IsAdmin)
        {
            try
            {
                string sp = "[dbo].[usp_GetPortalStartUpList]";
                SQLHandler SQLH = new SQLHandler();
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@IsAdmin", IsAdmin));
                return SQLH.ExecuteAsList<GetPortalStartUpList>(sp, ParamCollInput);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// Connects to database and returns array list of startup event's url.
        /// </summary>
        /// <param name="Position">Startup event's position.</param>
        /// <param name="PortalID">Portal ID.</param>
        /// <param name="IsAdmin">Set true if the startup event is of admin.</param>
        /// <returns>ArratyList of user startup event.</returns>
        public ArrayList GetStartUpControls(string Position, int PortalID, bool IsAdmin)
        {
            try
            {
                ArrayList arrColl = new ArrayList();
                Hashtable hst = new Hashtable();
                if (IsAdmin == false)
                {
                    if (HttpRuntime.Cache["StartupSageSetting"] != null)
                    {
                        hst = (Hashtable)HttpRuntime.Cache["StartupSageSetting"];
                    }
                    else
                    {
                        List<GetPortalStartUpList> objList = FnGetPortalStartUpList(IsAdmin);
                        hst.Add("StartupSettingKey", objList);
                    }
                    //need to be cleared when any key is chnaged
                    HttpRuntime.Cache.Insert("StartupSageSetting", hst);
                    List<GetPortalStartUpList> objNList = (List<GetPortalStartUpList>)hst["StartupSettingKey"];
                    arrColl = GetControlList(objNList, Position, PortalID);
                    return arrColl;
                }
                else
                {
                    if (HttpRuntime.Cache["StartupAdminSageSetting"] != null)
                    {
                        hst = (Hashtable)HttpRuntime.Cache["StartupAdminSageSetting"];
                    }
                    else
                    {
                        List<GetPortalStartUpList> objList = FnGetPortalStartUpList(IsAdmin);
                        hst.Add("StartupAdminSettingKey", objList);
                    }
                    //need to be cleared when any key is chnaged
                    HttpRuntime.Cache.Insert("StartupAdminSageSetting", hst);
                    List<GetPortalStartUpList> objNList = (List<GetPortalStartUpList>)hst["StartupAdminSettingKey"];
                    arrColl = GetControlList(objNList, Position, PortalID);
                    return arrColl;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Returns Arraylist extracted from list of startup event details.
        /// </summary>
        /// <param name="objNList">List of portal start up.</param>
        /// <param name="Position">Position of startup event.</param>
        /// <param name="PortalID">Portal ID.</param>
        /// <returns>ArrayList containing the startup events.</returns>
        private ArrayList GetControlList(List<GetPortalStartUpList> objNList, string Position, int PortalID)
        {
            ArrayList arrColl = new ArrayList();
            foreach (GetPortalStartUpList mitem in objNList)
            {
                if (mitem.EventLocationName.ToLower() == Position.ToLower() && mitem.PortalID == PortalID)
                {
                    arrColl.Add(mitem.ControlUrl);
                }
            }
            return arrColl;

        }

        /// <summary>
        /// Connects to database and updates a startup event and returns list of startup user startup event details.
        /// </summary>
        /// <param name="PortalStartUpID">Portal startup ID</param>
        /// <param name="PortalID">Portal ID.</param>
        /// <param name="ControlUrl">User control's URL.</param>
        /// <param name="EventLocation">User startup event's location.</param>
        /// <param name="IsAdmin">Set true if user startup event is of admin.</param>
        /// <param name="IsControlUrl">Set true if the URL is of user control.</param>
        /// <param name="IsActive">Set true if the startup event is active.</param>
        /// <param name="Username">User's name.</param>
        /// <returns>List of startup events.</returns>
        public List<SystemEventStartUpInfo> UpdateSystemEventStartUp(int PortalStartUpID, int PortalID, string ControlUrl, string EventLocation, bool IsAdmin, bool IsControlUrl, bool IsActive, string Username)
        {
            try
            {
                string sp = "[dbo].[usp_UpdateSystemEventStartUp]";
                SQLHandler SQLH = new SQLHandler();
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@PortalStartUpID", PortalStartUpID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@ControlUrl", ControlUrl));
                ParamCollInput.Add(new KeyValuePair<string, object>("@EventLocation", EventLocation));
                ParamCollInput.Add(new KeyValuePair<string, object>("@IsAdmin", IsAdmin));
                ParamCollInput.Add(new KeyValuePair<string, object>("@IsControlUrl", IsControlUrl));
                ParamCollInput.Add(new KeyValuePair<string, object>("@IsActive", IsActive));
                ParamCollInput.Add(new KeyValuePair<string, object>("@UserName", Username));
                return SQLH.ExecuteAsList<SystemEventStartUpInfo>(sp, ParamCollInput);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// Connects to database and deletes system startup events.
        /// </summary>
        /// <param name="PortalStartUpID">Startup event's ID.</param>
        /// <param name="UserName">User's name.</param>
        /// <returns>List of startup events.</returns>
        public List<SystemEventStartUpInfo> DeleteSystemEventStartUp(int PortalStartUpID, string UserName)
        {
            try
            {
                string sp = "[dbo].[usp_DeleteSystemEventStartUp]";
                SQLHandler SQLH = new SQLHandler();
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@PortalStartUpID", PortalStartUpID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@UserName", UserName));
                return SQLH.ExecuteAsList<SystemEventStartUpInfo>(sp, ParamCollInput);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Connects to database and returns startup event's list.
        /// </summary>
        /// <returns>List of startup events.</returns>
        public List<SystemEventLocationInfo> GetEventLocationList()
        {
            try
            {
                string sp = "[dbo].[usp_GetEventLocationList]";
                SQLHandler SQLH = new SQLHandler();
                return SQLH.ExecuteAsList<SystemEventLocationInfo>(sp);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
