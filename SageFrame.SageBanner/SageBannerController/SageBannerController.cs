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
using SageFrame.SageBannner.Info;
using SageFrame.SageBannner.Provider;
using System.Data;
using System.Data.SqlClient;
using SageFrame.Web;
using SageFrame.Web.Utilities;
using SageFrame.SageBannner.SettingInfo;
using System.Web;
#endregion

namespace SageFrame.SageBannner.Controller
{ 
    /// <summary>
    /// Business logic for SageBanner.
    /// </summary>
    public class SageBannerController
    {
        /// <summary>
        /// Saves banner content.
        /// </summary>
        /// <param name="obj">SageBannerInfo object.</param>
        public void SaveBannerContent(SageBannerInfo obj)
        {
            SageBannerProvider objpro = new SageBannerProvider();
            objpro.SaveBannerContent(obj);
        }
        /// <summary>
        /// Saves banner information.
        /// </summary>
        /// <param name="objB">SageBannerInfo object</param>
        public void SaveBannerInformation(SageBannerInfo objB)
        {
            SageBannerProvider objBP = new SageBannerProvider();
            objBP.SaveBannerInformation(objB);
        }
        /// <summary>
        /// Loads banner name on dropdown for given PortalID.
        /// </summary>
        /// <param name="UserModuleID">UserModuleID</param>
        /// <param name="PortalID">PortalID</param>
        /// <param name="CultureCode">CultureCode</param>
        /// <returns>SageBannerInfo list</returns>
        public List<SageBannerInfo> LoadBannerOnDropDown(int UserModuleID, int PortalID, string CultureCode)
        {
            try
            {
                SageBannerProvider obj = new SageBannerProvider();
                return obj.LoadBannerOnDropDown(UserModuleID, PortalID, CultureCode);
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }
         /// <summary>
         /// Loads banner images on grid.
         /// </summary>
        /// <param name="BannerID">BannerID</param>
        /// <param name="UserModuleID">UserModuleID</param>
        /// <param name="PortalID">PortalID</param>
        /// <param name="CultureCode">CultureCode</param>
        /// <returns>SageBannerInfo list</returns>
        public List<SageBannerInfo> LoadBannerImagesOnGrid(int BannerID, int UserModuleID, int PortalID, string CultureCode)
        {
            try
            {
                SageBannerProvider obj = new SageBannerProvider();
                return obj.LoadBannerImagesOnGrid(BannerID, UserModuleID, PortalID, CultureCode);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        /// <summary>
        /// Loads html content on grid.
        /// </summary>
        /// <param name="BannerID">BannerID</param>
        /// <param name="UserModuleID">UserModuleID</param>
        /// <param name="PortalID">PortalID</param>
        /// <param name="CultureCode">CultureCode</param>
        /// <returns>SageBannerInfo list</returns>
        public List<SageBannerInfo> LoadHTMLContentOnGrid(int BannerID, int UserModuleID, int PortalID, string CultureCode)
        {
            try
            {
                SageBannerProvider obj = new SageBannerProvider();
                return obj.LoadHTMLContentOnGrid(BannerID, UserModuleID, PortalID, CultureCode);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        /// <summary>
        /// Loads banner list on grid.
        /// </summary>
        /// <param name="PortalID">PortalID</param>
        /// <param name="UserModuleID">UserModuleID</param>
        /// <param name="CultureCode">CultureCode</param>
        /// <returns>SageBannerInfo list.</returns>
        public List<SageBannerInfo> LoadBannerListOnGrid(int PortalID, int UserModuleID, string CultureCode)
        {
            SageBannerProvider obj = new SageBannerProvider();
            return obj.LoadBannerListOnGrid(PortalID, UserModuleID, CultureCode);
        }

        /// <summary>
        /// Obtain image information for given ImageID.
        /// </summary>
        /// <param name="ImageID"></param>
        /// <returns>SageBannerInfo object</returns>
        public SageBannerInfo GetImageInformationByID(int ImageID)
        {
            try
            {
                SageBannerProvider objp = new SageBannerProvider();
                return objp.GetImageInformationByID(ImageID);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// <summary>
        /// Obtain html content for edit for given ImageID.
        /// </summary>
        /// <param name="ImageID">ImageID</param>
        /// <returns>SageBannerInfo object</returns>

        public SageBannerInfo GetHTMLContentForEditByID(int ImageID)
        {
            try
            {
                SageBannerProvider objp = new SageBannerProvider();
                return objp.GetHTMLContentForEditByID(ImageID);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// Deletes banner content for given ImageId.
        /// </summary>
        /// <param name="ImageId">ImageId</param>
        public void DeleteBannerContentByID(int ImageId)
        {
            try
            {
                SageBannerProvider objp = new SageBannerProvider();
                objp.DeleteBannerContentByID(ImageId);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// Deletes banner and its content for given BannerID.
        /// </summary>
        /// <param name="BannerID">BannerID</param>
        public void DeleteBannerAndItsContentByBannerID(int BannerID)
        {
            try
            {
                SageBannerProvider objp = new SageBannerProvider();
                objp.DeleteBannerAndItsContentByBannerID(BannerID);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// <summary>
        /// Deletes html content for given image id.
        /// </summary>
        /// <param name="ImageId">ImageId</param>

        public void DeleteHTMLContentByID(int ImageId)
        {
            try
            {
                SageBannerProvider objP = new SageBannerProvider();
                objP.DeleteHTMLContentByID(ImageId);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// Obtain all portal pages of sageframe for given PortalID. 
        /// </summary>
        /// <param name="PortalID">PortalID</param>
        /// <returns>SageBannerInfo list.</returns>
        public List<SageBannerInfo> GetAllPagesOfSageFrame(int PortalID)
        {
            SageBannerProvider obj = new SageBannerProvider();
            return obj.GetAllPagesOfSageFrame(PortalID);
        }

        /// <summary>
        ///  obtain image path.
        /// </summary>
        /// <param name="ImageId">ImageId</param>
        /// <returns>Image path.</returns>

        public string GetFileName(int ImageId)
        {

            try
            {
                SageBannerProvider objp = new SageBannerProvider();
                return objp.GetFileName(ImageId);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Obtain sagebanner setting list.
        /// </summary>
        /// <param name="PortalID">PortalID</param>
        /// <param name="UserModuleID">UserModuleID</param>
        /// <param name="CultureCode">CultureCode</param>
        /// <returns>SageBannerSettingInfo object.</returns>

        public SageBannerSettingInfo GetSageBannerSettingList(int PortalID, int UserModuleID, string CultureCode)
        {
            try
            {
                SageBannerSettingInfo objSageBannerSettingInfo = new SageBannerSettingInfo();
                if (HttpRuntime.Cache["BannerSetting_" + CultureCode + "_" + UserModuleID.ToString()] != null)
                {
                    objSageBannerSettingInfo = HttpRuntime.Cache["BannerSetting_" + CultureCode + "_" + UserModuleID.ToString()] as SageBannerSettingInfo;
                }
                else
                {
                    SageBannerProvider objp = new SageBannerProvider();
                    objSageBannerSettingInfo = objp.GetSageBannerSettingList(PortalID, UserModuleID, CultureCode);
                    HttpRuntime.Cache["BannerSetting_" + CultureCode + "_" + UserModuleID.ToString()] = objSageBannerSettingInfo;
                }
                return objSageBannerSettingInfo;
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }
        /// <summary>
        /// Obtain banner images.
        /// </summary>
        /// <param name="BannerID">BannerID</param>
        /// <param name="UserModuleID">UserModuleID</param>
        /// <param name="PortalID">PortalID</param>
        /// <param name="CultureCode">CultureCode</param>
        /// <returns>SageBannerInfo list.</returns>
        public List<SageBannerInfo> GetBannerImages(int BannerID, int UserModuleID, int PortalID, string CultureCode)
        {
            try
            {
                List<SageBannerInfo> objSageBannerLst = new List<SageBannerInfo>();
                if (HttpRuntime.Cache["BannerImages_" + CultureCode + "_" + UserModuleID.ToString()] != null)
                {
                    objSageBannerLst = HttpRuntime.Cache["BannerImages_" + CultureCode + "_" + UserModuleID.ToString()] as List<SageBannerInfo>;
                }
                else
                {
                    SageBannerProvider objp = new SageBannerProvider();
                    objSageBannerLst = objp.GetBannerImages(BannerID, UserModuleID, PortalID, CultureCode);
                    HttpRuntime.Cache["BannerImages_" + CultureCode + "_" + UserModuleID.ToString()] = objSageBannerLst;
                }
                return objSageBannerLst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Sort images in list.
        /// </summary>
        /// <param name="ImageId">ImageId</param>
        /// <param name="MoveUp">MoveUp</param>

        public void SortImageList(int ImageId, bool MoveUp)
        {
            try
            {
                SageBannerProvider objp = new SageBannerProvider();
                objp.SortImageList(ImageId, MoveUp);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Saves html content.
        /// </summary>
        /// <param name="NavImagepath">NavImagepath</param>
        /// <param name="HTMLBodyText">HTMLBodyText</param>
        /// <param name="Bannerid">Bannerid</param>
        /// <param name="UserModuleId">UserModuleId</param>
        /// <param name="ImageID">ImageID</param>
        /// <param name="PortalID">PortalID</param>
        /// <param name="CultureCode">CultureCode</param>
        public void SaveHTMLContent(string NavImagepath, string HTMLBodyText, int Bannerid, int UserModuleId, int ImageID, int PortalID, string CultureCode)
        {
            try
            {
                SageBannerProvider objp = new SageBannerProvider();
                objp.SaveHTMLContent(NavImagepath, HTMLBodyText, Bannerid, UserModuleId, ImageID, PortalID, CultureCode);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
