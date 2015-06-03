using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspxCommerce.Core;
using ImageResizer;
using System.Web.Hosting;


namespace AspxCommerce.ImageResizer
{
    public enum ImageType
    {
        Large,Medium,Small
    }

    public class ImageIntercept
    {
        public static void ImageBuilder(string imageFile, ImageType type, bool IsInTempFolder,AspxCommonInfo aspxCommonObj)
        {

            try
            {
                bool disposeSource = true;
                bool addFileExtensions = true;
                string tempFolder = string.Empty;
                string filename = imageFile;
                bool isExistImage = false;
                var resizeSetings = new ResizeSettings();
                resizeSetings.Format = "jpg";
                resizeSetings.Mode = FitMode.Max;
                switch (type)
                {
                    case ImageType.Large:
                        {
                            isExistImage = CheckIfImageExists(ImageType.Large, filename);
                            if (!(isExistImage))
                            {
                               ImageSettings imgStng=new ImageSettings(aspxCommonObj);
                               resizeSetings.MaxWidth = imgStng.itemLargeThumbNailWidth;
                               resizeSetings.MaxHeight = imgStng.itemLargeThumbNailHeight;
                               copyOriginalImageToRespectives(ImageType.Large, filename, resizeSetings,disposeSource, addFileExtensions);
                            }
                            break;
                        }
                    case ImageType.Medium:
                        {
                            isExistImage = CheckIfImageExists(ImageType.Medium, filename);
                            if (!(isExistImage))
                            {
                                ImageSettings imgStng = new ImageSettings(aspxCommonObj);
                                resizeSetings.MaxWidth = imgStng.itemMediumThumbNailWidth;
                                resizeSetings.MaxHeight = imgStng.itemMediumThumbNailHeight;
                                copyOriginalImageToRespectives(ImageType.Medium, filename, resizeSetings, disposeSource, addFileExtensions);
                            }
                            break;
                        }
                    case ImageType.Small:
                        {
                            isExistImage = CheckIfImageExists(ImageType.Medium, filename);
                            if (!(isExistImage))
                            {
                                ImageSettings imgStng = new ImageSettings(aspxCommonObj);
                                resizeSetings.MaxWidth = imgStng.itemSmallThumbNailWidth;
                                resizeSetings.MaxHeight = imgStng.itemSmallThumbNailHeight;
                                copyOriginalImageToRespectives(ImageType.Medium, filename, resizeSetings, disposeSource, addFileExtensions);
                            }
                            break;
                        }

                }

            }
            catch
            {
            }
        }


        public static void copyOriginalImageToRespectives(ImageType type, string filename, ResizeSettings rs,bool disposeSource, bool addFileExtensions)
        {
            string StrRootPath = HostingEnvironment.MapPath("~/");
            string SourceFolder = StrRootPath + @"Modules\AspxCommerce\AspxItemsManagement\uploads\" + @"" + filename + "";
            string DestinationFolder = StrRootPath + @"Modules\AspxCommerce\AspxItemsManagement\uploads\" + @"" + type + @"\" + filename + "";
            using (FileStream fileStream = new FileStream(SourceFolder, FileMode.Open))
            {
                ImageJob i = new ImageJob(fileStream, DestinationFolder,new Instructions(rs));
                i.CreateParentDirectory = false; //Auto-create the uploads directory.
                i.Build();
            }

        }

        public static bool CheckIfImageExists(ImageType type,string filename)
        {
            string fileExt = filename.Substring(filename.LastIndexOf("."));
            string strRootPath = HostingEnvironment.MapPath("~/");
            string destinationFolder = strRootPath + @"Modules\AspxCommerce\AspxItemsManagement\uploads\"+@""+type+"";
            if (File.Exists(Path.Combine(destinationFolder, filename)))
            {
                return true;
            }
            else{
                return false;
            }

        }
        //public string MoveFileToModuleFolder(string tempFolder, string prevFile, string imageVar, int imagWidth,int imageHeight, string destinationFolder, int ID, string imgPreFix,int ImageType)
        //{


        //    string destinationFile = string.Empty;
        //    string destinationLargeFile = string.Empty;
        //    string destinationMediumFile = string.Empty;
        //    string destinationSmallFile = string.Empty;
        //    string strRootPath = HostingEnvironment.MapPath("~/");
        //    destinationFolder = strRootPath + destinationFolder;
        //    if (imageVar != "")
        //    {
        //        string fileExt = imageVar.Substring(imageVar.LastIndexOf("."));
        //        Random rnd = new Random();
        //        string fileName = imgPreFix + ID + '_' + rnd.Next(111111, 999999).ToString() + fileExt;
        //        string sourceFile = strRootPath + imageVar;

        //        try
        //        {
        //            if (File.Exists(sourceFile))
        //            {
        //                // To move a file or folder to a new location:
        //                if (!Directory.Exists(destinationFolder))
        //                {
        //                    Directory.CreateDirectory(destinationFolder);
        //                }

        //                destinationFile = destinationFolder + fileName;

        //                if (sourceFile != destinationFile)
        //                {
        //                    if (prevFile != "")
        //                    {
        //                        string prevFileName = prevFile.Substring(prevFile.LastIndexOf("\\") + 1);
        //                        string prevExistingFile = destinationFolder + prevFileName;
        //                        destinationLargeFile = destinationFolder + "Large\\" + prevFileName;
        //                        destinationMediumFile = destinationFolder + "Medium\\" + prevFileName;
        //                        destinationSmallFile = destinationFolder + "Small\\" + prevFileName;

        //                        if (File.Exists(destinationLargeFile))
        //                        {
        //                            File.Delete(destinationLargeFile);
        //                        }
        //                        if (File.Exists(destinationMediumFile))
        //                        {
        //                            File.Delete(destinationMediumFile);
        //                        }
        //                        if (File.Exists(destinationSmallFile))
        //                        {
        //                            File.Delete(destinationSmallFile);
        //                        }
        //                        if (File.Exists(prevExistingFile))
        //                        {
        //                            File.Delete(prevExistingFile);
        //                        }
        //                    }

        //                    System.IO.File.Move(sourceFile, destinationFile);

        //                    string vertualUrl0 = destinationFolder + "Large\\";
        //                    string vertualUrl1 = destinationFolder + "Medium\\";
        //                    string vertualUrl2 = destinationFolder + "Small\\";

        //                    if (!Directory.Exists(vertualUrl0))
        //                        Directory.CreateDirectory(vertualUrl0);
        //                    if (!Directory.Exists(vertualUrl1))
        //                        Directory.CreateDirectory(vertualUrl1);
        //                    if (!Directory.Exists(vertualUrl2))
        //                        Directory.CreateDirectory(vertualUrl2);

        //                    vertualUrl0 = vertualUrl0 + fileName;
        //                    vertualUrl1 = vertualUrl1 + fileName;
        //                    vertualUrl2 = vertualUrl2 + fileName;

        //                    string[] imageType = new string[] { "jpg", "jpeg", "jpe", "gif", "bmp", "png", "ico" };
        //                    bool isValidImage = false;
        //                    foreach (string x in imageType)
        //                    {
        //                        if (fileExt.Contains(x))
        //                        {
        //                            isValidImage = true;
        //                            break;
        //                        }
        //                    }

        //                    if (isValidImage)
        //                    {
        //                        PictureManager.CreateThmnail(destinationFile, largeThumbNailImageHeight, largeThumbNailImageWidth, vertualUrl0);
        //                        PictureManager.CreateThmnail(destinationFile, mediumThumbNailImageHeight, mediumThumbNailImageWidth, vertualUrl1);
        //                        PictureManager.CreateThmnail(destinationFile, smallThumbNailImageHeight, smallThumbNailImageWidth, vertualUrl2);
        //                    }
        //                    else
        //                    {
        //                        System.IO.File.Copy(destinationFile, vertualUrl0);
        //                        System.IO.File.Copy(destinationFile, vertualUrl1);
        //                        System.IO.File.Copy(destinationFile, vertualUrl2);
        //                    }

                          
        //                    destinationFile = vertualUrl2;
        //                }
        //                destinationFile = destinationFile.Replace(strRootPath, "");
        //                destinationFile = destinationFile.Replace("\\", "/");
        //            }
        //        }
        //        catch (Exception e)
        //        {
        //            throw e;
        //        }
        //    }
        //    else
        //    {
        //        if (prevFile != "")
        //        {
        //            try
        //            {
        //                string prevFileName = prevFile.Substring(prevFile.LastIndexOf("\\") + 1);
        //                string prevExistingFile = destinationFolder + prevFileName;
        //                destinationLargeFile = destinationFolder + "Large\\" + prevFileName;
        //                destinationMediumFile = destinationFolder + "Medium\\" + prevFileName;
        //                destinationSmallFile = destinationFolder + "Small\\" + prevFileName;

        //                if (File.Exists(destinationLargeFile))
        //                {
        //                    File.Delete(destinationLargeFile);
        //                }
        //                if (File.Exists(destinationMediumFile))
        //                {
        //                    File.Delete(destinationMediumFile);
        //                }
        //                if (File.Exists(destinationSmallFile))
        //                {
        //                    File.Delete(destinationSmallFile);
        //                }
        //                if (File.Exists(prevExistingFile))
        //                {
        //                    File.Delete(prevExistingFile);
        //                }
        //            }
        //            catch
        //                (Exception ex)
        //            {
        //                throw ex;
        //            }
        //        }

        //    }
        //    return destinationFile;
        //}

        //public string MoveFile(string tempLocation, string destination, string oldFile)
        //{

        //    string newLocation = HostingEnvironment.MapPath("~/" + destination);
        //    tempLocation = HostingEnvironment.MapPath("~/" + tempLocation);
        //    oldFile = HostingEnvironment.MapPath("~/" + oldFile);
        //    string fileName = Path.GetFileName(tempLocation);
        //    if (newLocation != null && !Directory.Exists(newLocation))
        //    {
        //        Directory.CreateDirectory(newLocation);
        //    }
        //    else
        //    {
        //        if (File.Exists(oldFile) && oldFile != null)
        //            File.Delete(oldFile);
        //    }
        //    if (File.Exists(tempLocation))
        //    {
        //        if (newLocation != null && tempLocation != null)
        //            File.Copy(tempLocation, Path.Combine(newLocation, fileName), true);
        //    }
        //    return destination + fileName;

        //}




    }
}