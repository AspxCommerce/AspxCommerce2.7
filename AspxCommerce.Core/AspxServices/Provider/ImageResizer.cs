using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web.Hosting;
using System.Collections;
using SageFrame.Web;

namespace AspxCommerce.Core
{
   public class ImageResizer
    {

       public void ResizeImage()//int itemID, string imgRootPath, string sourceFileCol, string pathCollection, string isActive, string imageType, string itemImageIds, string description, string displayOrder, string imgPreFix, int itemLargeThumbNailSize, int itemMediumThumbNailSize, int itemSmallThumbNailSize, string cultureName, int portalID
       {
           //string pathList = string.Empty;
          // string[] sourceFileList = sourceFileCol.Split('%');
          // string destpath = HostingEnvironment.MapPath("~/" + imgRootPath);
           //if (!Directory.Exists(destpath))
          // {
               //Directory.CreateDirectory(destpath);
           //}

          // Random randVar = new Random();
           int itemLargeThumbNailSize = 600;
           int itemMediumThumbNailSize = 300;
           int itemSmallThumbNailSize = 125;

            try
            {
                //ArrayList Images = new ArrayList();
                FileInfo[] Images;
                DirectoryInfo Folder = new DirectoryInfo(HostingEnvironment.MapPath("~/" + "Modules/AspxCommerce/AspxItemsManagement/uploads/"));
                Images = Folder.GetFiles();
                string destpath = HostingEnvironment.MapPath("~/" + "Modules/AspxCommerce/AspxItemsManagement/uploads/");
                string destination = "";
                for (int i = 0; i < Images.Length; i++)
                {

                    destination = destpath + Images[i].Name;
                    //image Thumbnails generates here
                    string vertualUrl0 = destpath + "Large\\";
                    string vertualUrl1 = destpath + "Medium\\";
                    string vertualUrl2 = destpath + "Small\\";

                    if (!Directory.Exists(vertualUrl0))
                        Directory.CreateDirectory(vertualUrl0);
                    if (!Directory.Exists(vertualUrl1))
                        Directory.CreateDirectory(vertualUrl1);
                    if (!Directory.Exists(vertualUrl2))
                        Directory.CreateDirectory(vertualUrl2);

                    vertualUrl0 = vertualUrl0 + Images[i].Name;
                    vertualUrl1 = vertualUrl1 + Images[i].Name;
                    vertualUrl2 = vertualUrl2 + Images[i].Name;




                    PictureManager.CreateThmnail(destination, itemLargeThumbNailSize, vertualUrl0);
                    PictureManager.CreateThmnail(destination, itemMediumThumbNailSize, vertualUrl1);
                    PictureManager.CreateThmnail(destination, itemSmallThumbNailSize, vertualUrl2);

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
       }
    }
}
