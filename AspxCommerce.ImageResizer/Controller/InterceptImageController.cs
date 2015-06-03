
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
using System.Reflection;
using System.Web;
using System.Drawing;
using ImageResizer.Configuration;
using ImageResizer.Plugins.Watermark;


namespace AspxCommerce.ImageResizer
{
    public enum ImageType
    {
        Large, Medium, Small
    }
    /// <summary>
    /// ImageCategoryType defines whether the image is of item or brand,you can add more 
    /// </summary>
    public enum ImageCategoryType
    {
        Brand,Item,Category
    }

    public enum IsWaterMark
    {
        True,False
    }

    public class InterceptImageController
    {
        #region "Public Properties"
         
        public static string StrRootPath
        {
            get
            {
                return HostingEnvironment.MapPath("~/").ToString();
            }

        }
        #endregion
        /// <summary>
        /// Builds the image dynamically based upon the store setting sizes call this before giving image source
        /// </summary>
        /// <param name="imageFile">Name of the image as in db</param>
        /// <param name="type">Type could be small,medium or large based on front-end requirement</param>
        /// <param name="aspxCommonObj"></param>
        public static void ImageBuilder(string imageFile, ImageType type,ImageCategoryType imgCategory,AspxCommonInfo aspxCommonObj)
        {

            try
            {
                string filename = imageFile;
                bool isExistImage = false;
                string sourceFolder=string.Empty;
                string imgDestintionloc = string.Empty;
                string imageSourceloc = string.Empty;
                string destinationFolder = string.Empty;
                IsWaterMark isWM = IsWaterMark.False;
                if (imgCategory == ImageCategoryType.Item)
                {
                    sourceFolder = StrRootPath + @"Modules\AspxCommerce\AspxItemsManagement\uploads\" + @"" + filename + "";
                    imgDestintionloc = StrRootPath + @"Modules\AspxCommerce\AspxItemsManagement\uploads\";
                    imageSourceloc = StrRootPath + @"Modules\AspxCommerce\AspxItemsManagement\uploads\";
                    isWM = IsWaterMark.True;
                }
                else if (imgCategory == ImageCategoryType.Brand)
                {
                    sourceFolder = StrRootPath + @"Modules\AspxCommerce\AspxBrandManagement\uploads\" + @"" + filename + "";
                    imgDestintionloc = StrRootPath + @"Modules\AspxCommerce\AspxBrandManagement\uploads\";
                    imageSourceloc = StrRootPath + @"Modules\AspxCommerce\AspxBrandManagement\uploads\";
                    isWM = IsWaterMark.False;
                }
                else if (imgCategory == ImageCategoryType.Category)
                {
                    sourceFolder = StrRootPath + @"Modules\AspxCommerce\AspxCategoryManagement\uploads\" + @"" + filename + "";
                    imgDestintionloc = StrRootPath + @"Modules\AspxCommerce\AspxCategoryManagement\uploads\";
                    imageSourceloc = StrRootPath + @"Modules\AspxCommerce\AspxCategoryManagement\uploads\";
                    isWM = IsWaterMark.False;

                }
                ResizeSettings resizeSetings = new ResizeSettings();
                resizeSetings.Format = "jpg";
                resizeSetings.Mode = FitMode.Carve;
                resizeSetings.Scale = ScaleMode.Both;
                resizeSetings.Quality = 75;

                switch (type)
                {
                    case ImageType.Large:
                        {
                            string imgFolder = imageSourceloc + @"" + type + "";
                            destinationFolder = imgDestintionloc+ @"" + type + @"\" + filename + "";
                            isExistImage = CheckIfImageExists(filename, imgFolder);
                            if (!(isExistImage))
                            {
                                ImageSettings imgStng = new ImageSettings(aspxCommonObj);
                                if (Convert.ToBoolean(imgStng.ResizeImagesProportionally) == true)
                                {
                                    resizeSetings.Width = imgStng.ItemLargeThumbNailWidth;
                                }
                                else
                                {
                                    resizeSetings.Width = imgStng.ItemLargeThumbNailWidth;
                                    resizeSetings.Height = imgStng.ItemLargeThumbNailHeight;
                                }
                                copyOriginalImageToRespectives(resizeSetings, sourceFolder, destinationFolder, aspxCommonObj, isWM);
                            }
                            break;
                        }
                    case ImageType.Medium:
                        {
                            string imgFolder = imageSourceloc + @"" + type + "";
                            destinationFolder = imgDestintionloc + @"" + type + @"\" + filename + "";
                            isExistImage = CheckIfImageExists(filename, imgFolder);
                            if (!(isExistImage))
                            {
                                ImageSettings imgStng = new ImageSettings(aspxCommonObj);
                                if (Convert.ToBoolean(imgStng.ResizeImagesProportionally) == true)
                                {
                                    resizeSetings.Width = imgStng.ItemMediumThumbNailWidth;
                                }
                                else
                                {
                                    resizeSetings.Width = imgStng.ItemMediumThumbNailWidth;
                                    resizeSetings.Height = imgStng.ItemMediumThumbNailWidth;
                                }
                                copyOriginalImageToRespectives(resizeSetings, sourceFolder, destinationFolder, aspxCommonObj, isWM);
                            }
                            break;
                        }
                    case ImageType.Small:
                        {
                            string imgFolder = imageSourceloc + @"" + type + "";
                            destinationFolder = imgDestintionloc + @"" + type + @"\" + filename + "";
                            isExistImage = CheckIfImageExists(filename, imgFolder);
                            if (!(isExistImage))
                            {
                                ImageSettings imgStng = new ImageSettings(aspxCommonObj);
                                if (Convert.ToBoolean(imgStng.ResizeImagesProportionally) == true)
                                {
                                    resizeSetings.Width = imgStng.ItemSmallThumbNailHeight;
                                }
                                else
                                {
                                    resizeSetings.Width = imgStng.ItemSmallThumbNailHeight;
                                    resizeSetings.Height = imgStng.ItemSmallThumbNailHeight;
                                }
                                copyOriginalImageToRespectives(resizeSetings, sourceFolder, destinationFolder, aspxCommonObj, isWM);
                            }
                            break;
                        }

                }

            }
            catch
            {

            }
        }

        /// <summary>
        /// Copies image file to the destination.The destination need not have the file but the destination must add the original file name in its path
        /// </summary>
        /// <param name="rs">The resize setting</param>
        /// <param name="SourceFile">The original image file</param>
        /// <param name="DestinationFolder">The path of the destination folder including the filename same as original image file</param>
        public static void copyOriginalImageToRespectives(ResizeSettings rs, string SourceFile, string DestinationFolder,AspxCommonInfo aspxCommonObj,IsWaterMark isWaterMark)
        {
            
            // string combinedPath = Path.Combine(SourceFolder, filename);
          
                if (isWaterMark.ToString() == "True")
                {
                    //ResizeSettings settingObj = 
                    ApplyWaterMarkToImage(aspxCommonObj, rs,SourceFile, DestinationFolder);
                }
                else
                {
                    using (FileStream fileStream = new FileStream(SourceFile, FileMode.Open))
                    {
                        ImageJob i = new ImageJob(fileStream, DestinationFolder, new Instructions(rs));
                        i.CreateParentDirectory = false; //Auto-create the uploads directory.
                        i.Build();
                    }
                
            }
           

        }
        /// <summary>
        /// Applyes watermark to the image based on the isWaterMark property of that image
        /// </summary>
        /// <param name="aspxCommonObj">Common Object aspx</param>
        /// <param name="rs">Resize settings</param>
        /// <param name="SourceFile">The full source path of image</param>
        /// <param name="DestinationFolder">The full destination path of the image</param>
        public static void ApplyWaterMarkToImage(AspxCommonInfo aspxCommonObj, ResizeSettings rs, string SourceFile,string DestinationFolder)
        {
             
            StoreSettingConfig ssc = new StoreSettingConfig();
            string waterMarkText = ssc.GetStoreSettingsByKey(StoreSetting.WaterMarkText, aspxCommonObj.StoreID,
                                                             aspxCommonObj.PortalID, aspxCommonObj.CultureName);


            try
            {

                if (!string.IsNullOrEmpty(waterMarkText))
                {
                    string waterMarkposition = ssc.GetStoreSettingsByKey(StoreSetting.WaterMarkTextPosition,
                                                                         aspxCommonObj.StoreID, aspxCommonObj.PortalID,
                                                                         aspxCommonObj.CultureName);
                    double waterMarkRotAngle =
                        double.Parse(ssc.GetStoreSettingsByKey(StoreSetting.WaterMarkTextRotation, aspxCommonObj.StoreID,
                                                               aspxCommonObj.PortalID, aspxCommonObj.CultureName));

                   bool showWaterMarkImage = bool.Parse(ssc.GetStoreSettingsByKey(StoreSetting.ShowWaterMarkImage,
                                                                                   aspxCommonObj.StoreID,
                                                                                   aspxCommonObj.PortalID,
                                                                                   aspxCommonObj.CultureName));
                  

                    //You can have multiple configurations. Config.Current contains web.config settings, but you can use new Config(); to get a clean slate.
                    Config c = Config.Current;

                    //Get a reference to the instance 
                    WatermarkPlugin wp = c.Plugins.Get<WatermarkPlugin>();
                    if (wp == null)
                    { //Install it if it's missing
                        wp = new WatermarkPlugin();
                        wp.Install(c);
                    }
                    //Re-query in case another thread beat us  to installation.
                        wp = c.Plugins.Get<WatermarkPlugin>();
                    
                     //Adding single text layer multiple layer can be added
                     TextLayer t = new TextLayer();
                     t.Text = waterMarkText;
                     t.Fill = true; //Fill the image with the text
                     t.TextColor = System.Drawing.Color.White;
                     t.Angle = waterMarkRotAngle;
                     TextPosition posType = (TextPosition)Enum.Parse(typeof(TextPosition), waterMarkposition);
                     if (!string.IsNullOrEmpty(posType.ToString()))
                     {

                         switch (posType)
                         {
                             case TextPosition.BOTTOM_CENTER:
                                 t.Left = new DistanceUnit(30, DistanceUnit.Units.Percentage);
                                 t.Right = new DistanceUnit(30, DistanceUnit.Units.Percentage);
                                 t.Width = new DistanceUnit(50, DistanceUnit.Units.Percentage);
                                 t.Bottom = new DistanceUnit(5, DistanceUnit.Units.Percentage);

                                 break;
                             case TextPosition.MID_CENTER:
                                 t.Left = new DistanceUnit(30, DistanceUnit.Units.Percentage);
                                 t.Right = new DistanceUnit(30, DistanceUnit.Units.Percentage);
                                 t.Width = new DistanceUnit(50, DistanceUnit.Units.Percentage);
                                 t.Bottom = new DistanceUnit(50, DistanceUnit.Units.Percentage);

                                 break;
                             case TextPosition.TOP_CENTER:
                                 t.Left = new DistanceUnit(30, DistanceUnit.Units.Percentage);
                                 t.Right = new DistanceUnit(30, DistanceUnit.Units.Percentage);
                                 t.Width = new DistanceUnit(50, DistanceUnit.Units.Percentage);
                                 t.Top = new DistanceUnit(5, DistanceUnit.Units.Percentage);

                                 break;
                             case TextPosition.BOTTOM_LEFT:
                                 t.Left = new DistanceUnit(5, DistanceUnit.Units.Percentage);
                                 t.Width = new DistanceUnit(50, DistanceUnit.Units.Percentage);
                                 t.Bottom = new DistanceUnit(5, DistanceUnit.Units.Percentage);

                                 break;
                             case TextPosition.MID_LEFT:
                                 t.Left = new DistanceUnit(5, DistanceUnit.Units.Percentage);
                                 t.Width = new DistanceUnit(50, DistanceUnit.Units.Percentage);
                                 t.Top = new DistanceUnit(50, DistanceUnit.Units.Percentage);

                                 break;
                             case TextPosition.TOP_LEFT:
                                 t.Left = new DistanceUnit(5, DistanceUnit.Units.Percentage);
                                 t.Width = new DistanceUnit(50, DistanceUnit.Units.Percentage);
                                 t.Top = new DistanceUnit(5, DistanceUnit.Units.Percentage);
                                 break;
                             default:
                                 t.Left = new DistanceUnit(30, DistanceUnit.Units.Percentage);
                                 t.Right = new DistanceUnit(30, DistanceUnit.Units.Percentage);
                                 t.Width = new DistanceUnit(50, DistanceUnit.Units.Percentage);
                                 t.Bottom = new DistanceUnit(5, DistanceUnit.Units.Percentage);
                                 break;
                         }
                     }
                     else
                     {
                         t.Left = new DistanceUnit(30, DistanceUnit.Units.Percentage);
                         t.Right = new DistanceUnit(30, DistanceUnit.Units.Percentage);
                         t.Width = new DistanceUnit(50, DistanceUnit.Units.Percentage);
                         t.Bottom = new DistanceUnit(5, DistanceUnit.Units.Percentage);
                     }
                   
                    wp.NamedWatermarks["text"] = new Layer[] { t };
                    ResizeSettings rsWM;
                    if (showWaterMarkImage)
                    {
                        string waterMarkImageposition = ssc.GetStoreSettingsByKey(StoreSetting.WaterMarkImagePosition,
                                                                             aspxCommonObj.StoreID,
                                                                             aspxCommonObj.PortalID,
                                                                             aspxCommonObj.CultureName);
                        double waterMarkImageRotAngle =
                            double.Parse(ssc.GetStoreSettingsByKey(StoreSetting.WaterMarkImageRotation,
                                                                   aspxCommonObj.StoreID, aspxCommonObj.PortalID,
                                                                   aspxCommonObj.CultureName));

                        string imagePath =@"Modules\AspxCommerce\AspxStoreSettingsManagement\uploads\noitem.png";
                        ImageLayer i = new ImageLayer(c); //ImageLayer needs a Config instance so it knows where to locate images
                        i.Path = "~/" + imagePath;
                        i.DrawAs = ImageLayer.LayerPlacement.Overlay;
                        i.Width = new DistanceUnit(20, DistanceUnit.Units.Percentage);
                        i.Height = new DistanceUnit(20, DistanceUnit.Units.Percentage);
                        i.ImageQuery.Rotate = waterMarkImageRotAngle;
                        ImagePosition imgPosType = (ImagePosition)Enum.Parse(typeof(ImagePosition), waterMarkImageposition);
                        if (!string.IsNullOrEmpty(imgPosType.ToString()))
                        {
                            switch (imgPosType)
                            {
                                case ImagePosition.CENTER:
                                    break;

                                case ImagePosition.TOP_RIGHT:
                                    i.Top = new DistanceUnit(5, DistanceUnit.Units.Percentage);
                                    i.Right = new DistanceUnit(5, DistanceUnit.Units.Percentage);
                                    break;

                                case ImagePosition.TOP_LEFT:
                                    i.Top = new DistanceUnit(5, DistanceUnit.Units.Percentage);
                                    i.Left = new DistanceUnit(5, DistanceUnit.Units.Percentage);
                                    break;

                                case ImagePosition.BOTTOM_RIGHT:
                                    i.Bottom = new DistanceUnit(5, DistanceUnit.Units.Percentage);
                                    i.Right = new DistanceUnit(5, DistanceUnit.Units.Percentage);
                                    break;

                                case ImagePosition.BOTTOM_LEFT:
                                    i.Bottom = new DistanceUnit(5, DistanceUnit.Units.Percentage);
                                    i.Left = new DistanceUnit(5, DistanceUnit.Units.Percentage);
                                    break;
                                default:
                                    i.Width = new DistanceUnit(20, DistanceUnit.Units.Percentage);
                                    i.Height = new DistanceUnit(20, DistanceUnit.Units.Percentage);
                                    break;
                            }
                        }
                        
                       wp.NamedWatermarks["img"] = new Layer[] { i };
                       rsWM = new ResizeSettings("watermark=text,img;");
                    }
                    else
                    {
                       rsWM = new ResizeSettings("watermark=text");
                    }
                    rsWM.Format = rs.Format;
                    rsWM.Mode = rs.Mode;
                    rsWM.Scale = rs.Scale;
                    rsWM.Quality = rs.Quality;
                    rsWM.Width = rs.Width;
                    rsWM.Height = rs.Height;
                    wp.keepAspectRatio = true; //Maintains the aspect ratio of the watermark itself.
                    //Build the Image
                    c.CurrentImageBuilder.Build(SourceFile, DestinationFolder, rsWM);
                }
            }
            catch 
            {
            }
        }
        /// <summary>
        /// Checks if the image already exists in the medium large or small folder of item mgmt
        /// </summary>
        /// <param name="type">Type could be small,medium or large based on front-end requirement</param>
        /// <param name="filename">The name of the image file </param>
        /// <param name="imageFolder">Folder where the origianl image resides</param>
        /// <returns></returns>
        public static bool CheckIfImageExists(string filename, string imageFolder)
        {
            string fileExt = filename.Substring(filename.LastIndexOf("."));
            //If directory doesnot exists then create the directory
            if (!Directory.Exists(imageFolder))
            {
                Directory.CreateDirectory(imageFolder);
                return false;
            }
            if (File.Exists(Path.Combine(imageFolder, filename)))
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        /// <summary>
        /// Resizes Images wit a single type
        /// </summary>
        /// <param name="imgCollection"></param>
        /// <param name="type"></param>
        /// <param name="imageCatType">Single category type which may be Item,Category or Brand in string</param>
        /// <param name="aspxCommonObj"></param>
        public void DynamicImageResizer(string imgCollection, string type, string imageCatType, AspxCommonInfo aspxCommonObj)
        {
            List<string> images = imgCollection.Split(';').ToList();
            var imgCatType = (ImageCategoryType)Enum.Parse(typeof(ImageCategoryType), imageCatType);
            foreach (var img in images)
            {
                var imgtype = (ImageType)Enum.Parse(typeof(ImageType), type);
                if (!(img.Trim() == string.Empty))
                {
                    string resizeImage = img.ToString();
                    if (!(resizeImage == null || resizeImage == string.Empty))
                        ImageBuilder(resizeImage, imgtype, imgCatType, aspxCommonObj);
                }
            }

        }
        
        /// <summary>
        /// Resizes images with multiples types.
        /// </summary>
        /// <param name="imgCollection">ImageName Separated with semicolon</param>
        /// <param name="types">Image type medium,large or small separted with semicolons</param>
        /// <param name="imageCatType">Single category type which may be Item,Category or Brand in string</param>
        /// <param name="aspxCommonObj"></param>
        public void MultipleImageResizer(string imgCollection, string types,string imageCatType,AspxCommonInfo aspxCommonObj)
        {
            List<string> images = imgCollection.Split(';').ToList();
            List<string> imageTypes = types.Split(';').ToList();
            var imgCatType = (ImageCategoryType)Enum.Parse(typeof(ImageCategoryType), imageCatType);
            foreach (var img in images)
            {
                for (int count = 0; count < imageTypes.Count; count++)
                {
                    if (!(imageTypes[count].ToString().Trim() == string.Empty))
                    {
                        var imgtype = (ImageType)Enum.Parse(typeof(ImageType), imageTypes[count]);
                        if (!(img.Trim() == string.Empty))
                        {
                            string resizeImage = img.ToString();
                            if (!(resizeImage == null || resizeImage == string.Empty))
                                ImageBuilder(resizeImage, imgtype, imgCatType, aspxCommonObj);
                        }
                    }
                }

            }

        }
        /// <summary>
        /// Resizes multiple images of different category types-brand,item,category and different sizes-small,medium,large
        /// </summary>
        /// <param name="imgMultipleTypes">List of class ImageTypeInfo</param>
        /// <param name="aspxCommonObj">AspxCommonObj</param>
        public void MultipleImageMultipleTypeResizer(List<ImageTypeInfo> imgMultipleTypes,AspxCommonInfo aspxCommonObj)
        {
            if (imgMultipleTypes.Count > 0)
            {
                foreach (ImageTypeInfo imgInfo in imgMultipleTypes)
                {

                    List<string> images = imgInfo.imgCollection.Split(';').ToList();
                    List<string> imageTypes = imgInfo.imgType.Split(';').ToList();
                    var imgCatType = (ImageCategoryType)Enum.Parse(typeof(ImageCategoryType), imgInfo.imgCatType.ToString());
                    foreach (var img in images)
                    {
                        for (int count = 0; count < imageTypes.Count; count++)
                        {
                            if (!(imageTypes[count].ToString().Trim() == string.Empty))
                            {
                                var imgtype = (ImageType)Enum.Parse(typeof(ImageType), imageTypes[count]);
                                if (!(img.Trim() == string.Empty))
                                {
                                    string resizeImage = img.ToString();
                                    if (!(resizeImage == null || resizeImage == string.Empty))
                                        ImageBuilder(resizeImage, imgtype, imgCatType, aspxCommonObj);
                                }
                            }
                        }

                    }

                }
            }
           
        }
        
        /// <summary>
        /// Deletes all the folders inside item mgmt containing images --medium,small,large upon updating store settings
        /// </summary>
        public void DeleteAllResizedImages(AspxCommonInfo aspxCommonObj)
        {
            string storeImageSource = StrRootPath + @"Modules\AspxCommerce\AspxStoreSettingsManagement\uploads\";
            string largeImageFolder = storeImageSource + "Large\\";
            string mediumImageFolder = storeImageSource + "Medium\\";
            string smallImageFolder = storeImageSource + "Small\\";
            if (Directory.Exists(largeImageFolder))
                DeleteFilesFromDirectory(largeImageFolder);
            if (Directory.Exists(mediumImageFolder))
                DeleteFilesFromDirectory(mediumImageFolder);
            if (Directory.Exists(smallImageFolder))
                DeleteFilesFromDirectory(smallImageFolder);
            //Delete and add new store logo
            AddImagesForStoreSettings(storeImageSource, "storelogo.png", largeImageFolder, mediumImageFolder, smallImageFolder, aspxCommonObj);
            //Delete and add new store no item image
            AddImagesForStoreSettings(storeImageSource, "noitem.png", largeImageFolder, mediumImageFolder, smallImageFolder, aspxCommonObj);
            //Delete Items
            string destinationFolder = StrRootPath + @"Modules\AspxCommerce\AspxItemsManagement\uploads\";
            largeImageFolder = destinationFolder + "Large\\";
            mediumImageFolder = destinationFolder + "Medium\\";
            smallImageFolder = destinationFolder + "Small\\";
            if (Directory.Exists(largeImageFolder))
                DeleteFilesFromDirectory(largeImageFolder);
            if (Directory.Exists(mediumImageFolder))
                DeleteFilesFromDirectory(mediumImageFolder);
            if (Directory.Exists(smallImageFolder))
                DeleteFilesFromDirectory(smallImageFolder);

            //Delete Categories
            destinationFolder = StrRootPath + @"Modules\AspxCommerce\AspxCategoryManagement\uploads\";
            largeImageFolder = destinationFolder + "Large\\";
            mediumImageFolder = destinationFolder + "Medium\\";
            smallImageFolder = destinationFolder + "Small\\";
            if (Directory.Exists(largeImageFolder))
                DeleteFilesFromDirectory(largeImageFolder);
            if (Directory.Exists(mediumImageFolder))
                DeleteFilesFromDirectory(mediumImageFolder);
            if (Directory.Exists(smallImageFolder))
                DeleteFilesFromDirectory(smallImageFolder);
            //Delete Brands
            destinationFolder = StrRootPath + @"Modules\AspxCommerce\AspxBrandManagement\uploads\";
            largeImageFolder = destinationFolder + "Large\\";
            mediumImageFolder = destinationFolder + "Medium\\";
            smallImageFolder = destinationFolder + "Small\\";
            if (Directory.Exists(largeImageFolder))
                DeleteFilesFromDirectory(largeImageFolder);
            if (Directory.Exists(mediumImageFolder))
                DeleteFilesFromDirectory(mediumImageFolder);
            if (Directory.Exists(smallImageFolder))
                DeleteFilesFromDirectory(smallImageFolder);
           // AspxCommonController.AvoidAppDomainRestartOnFolderDelete();
        }
       

        public void DeleteDirectory(string dir)
        {
            System.IO.Directory.Delete(dir, true);
        }

        public void DeleteFilesFromDirectory(string dir)
        {
            foreach (object mFile_loopVariable in System.IO.Directory.GetFiles(dir))
            {
               string  mFile = mFile_loopVariable.ToString();
                System.IO.File.Delete(mFile);
            }

            foreach (object mDirectory_loopVariable in System.IO.Directory.GetDirectories(dir))
            {
                string mDirectory = mDirectory_loopVariable.ToString();
                DeleteFilesFromDirectory(mDirectory);
            }
        }

        public void AddImagesForStoreSettings(string storeSettingImageFolder, string fileName, string largeImageFolder, string mediumImageFolder, string smallImageFolder, AspxCommonInfo aspxCommonObj)
        {
            //Copy resized noitemImage and storelogo Image to the respective folders
            ResizeSettings resizeSetings = new ResizeSettings();
            resizeSetings.Format = "jpg";
            resizeSetings.Mode = FitMode.Carve;
            resizeSetings.Scale = ScaleMode.Both;
            resizeSetings.Quality = 75;
            ImageSettings imgStng = new ImageSettings(aspxCommonObj);
            ResizeSettings resizeSettingsLarge = new ResizeSettings(resizeSetings);
            ResizeSettings resizeSettingsMedium = new ResizeSettings(resizeSetings);
            ResizeSettings resizeSettingsSmall = new ResizeSettings(resizeSetings);
            if (Convert.ToBoolean(imgStng.ResizeImagesProportionally) == true)
            {
                resizeSettingsLarge.Width = imgStng.ItemLargeThumbNailWidth;
                resizeSettingsMedium.Width = imgStng.ItemMediumThumbNailWidth;
                resizeSettingsSmall.Width = imgStng.ItemSmallThumbNailWidth;

            }
            else
            {
                resizeSettingsLarge.Width = imgStng.ItemLargeThumbNailWidth;
                resizeSettingsLarge.Height = imgStng.ItemLargeThumbNailHeight;
                resizeSettingsMedium.Width = imgStng.ItemMediumThumbNailWidth;
                resizeSettingsMedium.Height = imgStng.ItemMediumThumbNailHeight;
                resizeSettingsSmall.Width = imgStng.ItemSmallThumbNailWidth;
                resizeSettingsSmall.Height = imgStng.ItemSmallThumbNailHeight;
                
            }
            copyOriginalImageToRespectives(resizeSettingsSmall, Path.Combine(storeSettingImageFolder, fileName), Path.Combine(smallImageFolder,fileName),aspxCommonObj,IsWaterMark.False);
            copyOriginalImageToRespectives(resizeSettingsMedium, Path.Combine(storeSettingImageFolder, fileName), Path.Combine(mediumImageFolder, fileName),aspxCommonObj, IsWaterMark.False);
            copyOriginalImageToRespectives(resizeSettingsLarge, Path.Combine(storeSettingImageFolder, fileName), Path.Combine(largeImageFolder, fileName),aspxCommonObj, IsWaterMark.False);

        }
        /// <summary>
        /// Resizes the sageframe banner Images
        /// </summary>
        /// <param name="ImageFilePath">The source laoction of image</param>
        /// <param name="width">Width of the image to be set</param>
        /// <param name="TargetLocation">Destination of the image to be saved</param>
        /// <param name="fileName">Name of the file</param>
        /// <param name="height">You can or not provide heigth</param>
        public static void ResizeBannerImage(string ImageFilePath, int width, string TargetLocation, string fileName,int height=0)
        {
            try
            {
                if (!Directory.Exists(TargetLocation))
                {
                    Directory.CreateDirectory(TargetLocation);
                }
            string SavePath = string.Empty;
            SavePath = TargetLocation + fileName;
            ResizeSettings resizeSetings = new ResizeSettings();
            //resizeSetings.Format = "jpg";
            resizeSetings.Mode = FitMode.Carve;
            resizeSetings.Scale = ScaleMode.Both;
            resizeSetings.Quality = 75;
            if (height == 0)
            {
                resizeSetings.Width = width;
            }
            else
            {
                resizeSetings.Width = width;
                resizeSetings.Height = height;
            }
            AspxCommonInfo aspxCommonObj = new AspxCommonInfo();
            copyOriginalImageToRespectives(resizeSetings, ImageFilePath, SavePath, aspxCommonObj,IsWaterMark.False);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Crop the Sageframe Banner Image  to the respective width and height as selected using Jcrop
        /// </summary>
        /// <param name="ImageFilePath"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="TargetLocation"></param>
        /// <param name="fileName"></param>
        public static void ResizeBannerImageAndCrop(string ImageFilePath, int width,int height, string TargetLocation, string fileName)
        {
            try
            {
                if (!Directory.Exists(TargetLocation))
                {
                    Directory.CreateDirectory(TargetLocation);
                }
            string SavePath = string.Empty;
            SavePath = TargetLocation + fileName;
            //Check if file exists in the default folder If Yes then delete it before rewriting
            if (File.Exists(Path.Combine(TargetLocation, fileName)))
            {
                File.Delete(Path.Combine(TargetLocation, fileName));
            }
            ResizeSettings resizeSetings = new ResizeSettings();
            //resizeSetings.Format = "jpg";
            resizeSetings.Mode = FitMode.Crop;//cropping the image
            resizeSetings.Scale = ScaleMode.Both;
            resizeSetings.Quality = 75;
            resizeSetings.Width = width;
            resizeSetings.Height = height;
            AspxCommonInfo aspxCommonObj = new AspxCommonInfo();
            copyOriginalImageToRespectives(resizeSetings, ImageFilePath, SavePath,aspxCommonObj, IsWaterMark.False);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
    }
}
