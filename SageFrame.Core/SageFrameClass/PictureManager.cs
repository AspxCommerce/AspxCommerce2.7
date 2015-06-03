#region "Copyright"

/*
FOR FURTHER DETAILS ABOUT LICENSING, PLEASE VISIT "LICENSE.txt" INSIDE THE SAGEFRAME FOLDER
*/

#endregion

#region "References"

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web.UI.WebControls;
using SageFrame.Setting;
using System.Drawing.Drawing2D;
using System.Text.RegularExpressions;
//using AjaxControlToolkit;

#endregion


namespace SageFrame.Web
{
    /// <summary>
    /// PictureManager class provides properties for  image manipulation
    /// </summary>
    public static class PictureManager
    {
        /// <summary>
        /// Returns image path including file name which is formed by combining image name with prefix provided.
        /// </summary>
        /// <param name="imageType">Image type.</param>
        /// <param name="prefix">File name prefix.</param>
        /// <param name="localImagePath">Local image Path.</param>
        /// <returns>New image path.</returns>
        public static string GetImagePathWithFileName(int imageType, string prefix, string localImagePath)
        {
            string SavePath = string.Empty;
            string localFilename = string.Empty;
            if (string.IsNullOrEmpty(prefix))
            {
                prefix = "img";
            }
            localFilename = prefix;
            switch (imageType)
            {
                case 1:
                    SavePath = Path.Combine(PictureManager.LocalMediumThumbImagePath, localFilename);
                    break;
                case 2:
                    SavePath = Path.Combine(localImagePath, localFilename);
                    break;
                case 3:
                    SavePath = Path.Combine(localImagePath, localFilename);
                    break;
            }

            return SavePath;
        }

        /// <summary>
        /// Returns unique File name for any give file name in the application.
        /// </summary>
        /// <param name="prefix">Prefix.</param>
        /// <returns>unique file name.</returns>
        public static string GetFileName(string prefix)
        {
            string strFileName = string.Empty;
            strFileName = prefix + "_" + DateTime.Now.ToString().Replace(" ", "").Replace("/", "").Replace(":", "");
            return strFileName;
        }

        /// <summary>
        /// Saves image for the given location.
        /// </summary>
        /// <param name="Fu">File Uploader.</param>
        /// <param name="prefix">Image prefix.</param>
        /// <param name="localImagePath">Local file path.</param>
        /// <returns>Returns the saved file path.</returns>
        public static string SaveImage(FileUpload Fu, string prefix, string localImagePath)
        {
            if (!Directory.Exists(localImagePath))
                Directory.CreateDirectory(localImagePath);
            string strImage = string.Empty;
            string SavePath = string.Empty;
            //SavePath = GetImagePathWithFileName(3, prefix, localImagePath);
            SavePath = localImagePath;
            SavePath += '\\' + prefix;
            Fu.SaveAs(SavePath);
            Fu.FileContent.Dispose();
            strImage = SavePath;
            //Fu.PostedFile.ContentLength
            return strImage;
        }

        /// <summary>
        /// Creats thumbnail for any image and returns 
        /// </summary>
        /// <param name="strImage">Local image full path upto image.</param>
        /// <param name="TargetSize">Targeted  file size.</param>
        /// <param name="SavePath">Save Path.</param>
        public static void CreateThmnail(string strImage, int TargetSize, string SavePath)
        {
            Bitmap b = new Bitmap(strImage);
            Size newSize = CalculateDimensions(b.Size, TargetSize);
            System.Drawing.Image image = System.Drawing.Image.FromFile(strImage);
            if (newSize.Width < 1)
                newSize.Width = 1;
            if (newSize.Height < 1)
                newSize.Height = 1;
            b.Dispose();
            var newWidth = (int)(newSize.Width);
            var newHeight = (int)(newSize.Height);
            var thumbnailImg = new Bitmap(newWidth, newHeight);
            var thumbGraph = Graphics.FromImage(thumbnailImg);
            thumbGraph.CompositingQuality = CompositingQuality.HighQuality;
            thumbGraph.SmoothingMode = SmoothingMode.HighQuality;
            thumbGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
            var imageRectangle = new Rectangle(0, 0, newWidth, newHeight);
            thumbGraph.DrawImage(image, imageRectangle);
            thumbnailImg.Save(SavePath, image.RawFormat);
            thumbnailImg.Dispose();
            thumbGraph.Dispose();
            image.Dispose();    
        }
          public static void CreateThmnail(string strImage, int TargetHeight,int TargetWidth, string SavePath)
        {
            Bitmap b = new Bitmap(strImage);
            Size newSize =new Size();// CalculateDimensions(b.Size, TargetHeight, TargetWidth);      
            newSize.Height = TargetHeight;
            newSize.Width = TargetWidth;
            System.Drawing.Image image = System.Drawing.Image.FromFile(strImage);
            if (newSize.Width < 1)
                newSize.Width = 1;
            if (newSize.Height < 1)
                newSize.Height = 1;
            b.Dispose();
            var newWidth = (int)(newSize.Width);
            var newHeight = (int)(newSize.Height);
            var thumbnailImg = new Bitmap(newWidth, newHeight);
            var thumbGraph = Graphics.FromImage(thumbnailImg);
            thumbGraph.CompositingQuality = CompositingQuality.HighQuality;
            thumbGraph.SmoothingMode = SmoothingMode.HighQuality;
            thumbGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
            var imageRectangle = new Rectangle(0, 0, newWidth, newHeight);
            thumbGraph.DrawImage(image, imageRectangle);
            thumbnailImg.Save(SavePath, image.RawFormat);
        }

 public static void CreateThmnail(string strImage, long Size, string SavePath)
        {
            // System.Drawing.Image imgRetPhoto = null;
            //Bitmap b = new Bitmap(strImage);
            //Size newSize = CalculateDimensions(b.Size, TargetSize);

            //if (newSize.Width < 1)
            //    newSize.Width = 1;
            //if (newSize.Height < 1)
            //    newSize.Height = 1;
            System.Drawing.Image myImg = System.Drawing.Image.FromFile(strImage);
            CompressAndSaveImage(myImg, SavePath, Size);
            //System.Drawing.Image image = System.Drawing.Image.FromFile(strImage);
            //System.Drawing.Image thumb = image.GetThumbnailImage(newSize.Width, newSize.Height, () => false, IntPtr.Zero);
            //thumb.Save(Path.ChangeExtension(strImage, SavePath));
            //long quality = 80L;
            // var qualityEncoder = Encoder.Quality;
            // var encoderParameter = new EncoderParameter(qualityEncoder, quality);
            // var encoderParams = new EncoderParameters(1);
            // encoderParams.Param[0] = encoderParameter;
            // var jpegEncode = GetEncoder(ImageFormat.Jpeg);
            // b.Save(SavePath, jpegEncode, encoderParams);
            // b.Dispose();
            // b = ScaleByPercent(strImage, newSize.Height, newSize.Width);
            //imgRetPhoto = ScaleByPercent(strImage, newSize.Height, newSize.Width);
            //imgRetPhoto.Save(SavePath);
            // imgRetPhoto.Dispose();

        }
        private static void CompressAndSaveImage(System.Drawing.Image img, string fileName, long quality)
        {
            EncoderParameters parameters = new EncoderParameters(1);
            parameters.Param[0] = new EncoderParameter(Encoder.Quality, quality);
            img.Save(fileName, GetCodecInfo("image/jpeg"), parameters);
        }
        private static ImageCodecInfo GetCodecInfo(string mimeType)
        {
            foreach (ImageCodecInfo encoder in ImageCodecInfo.GetImageEncoders())
                if (encoder.MimeType == mimeType)
                    return encoder;
            throw new ArgumentOutOfRangeException(
                string.Format("'{0}' not supported", mimeType));
        }
        private static ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();

            return codecs
                .Where(codec => codec.FormatID == format.Guid)
                .FirstOrDefault();
        }
        public static Size CalculateDimensions(Size OriginalSize, int TargetSize)
        {
            Size newSize = new Size();
            if (OriginalSize.Height > OriginalSize.Width) // portrait 
            {
                newSize.Width = (int)(OriginalSize.Width * (float)(TargetSize / (float)OriginalSize.Height));
                newSize.Height = TargetSize;
            }
            else // landscape or square
            {
                newSize.Height = (int)(OriginalSize.Height * (float)(TargetSize / (float)OriginalSize.Width));
                newSize.Width = TargetSize;
            }
            return newSize;
        }

        /// <summary>
        /// Crop the image in the given height and width.
        /// </summary>
        /// <param name="strImage">Image full path.</param>
        /// <param name="dblImgHt">New image height.</param>
        /// <param name="dblImgWd">New image width.</param>
        /// <returns>Image in bitmap format.</returns>
        public static System.Drawing.Image ScaleByPercent(string strImage, double dblImgHt, double dblImgWd)
        {
            Bitmap imgRetPhoto = null;
            double dblWdRatio, dblHtRatio;

            try
            {
                imgRetPhoto = new Bitmap(strImage);
                if (imgRetPhoto.Height > Convert.ToInt32(dblImgHt) || imgRetPhoto.Width > Convert.ToInt32(dblImgWd))
                {
                    if (imgRetPhoto.Height > dblImgHt)
                    {
                        dblHtRatio = dblImgHt / Convert.ToDouble(imgRetPhoto.Height);
                        dblWdRatio = Convert.ToDouble(imgRetPhoto.Width) * dblHtRatio;
                        imgRetPhoto = new Bitmap(imgRetPhoto, Convert.ToInt32(dblWdRatio), Convert.ToInt32(dblImgHt));
                        imgRetPhoto.SetResolution(imgRetPhoto.HorizontalResolution, imgRetPhoto.VerticalResolution);
                    }

                    if (imgRetPhoto.Width > dblImgWd)
                    {
                        dblWdRatio = dblImgWd / Convert.ToDouble(imgRetPhoto.Width);
                        dblHtRatio = Convert.ToDouble(imgRetPhoto.Height) * dblWdRatio;
                        imgRetPhoto = new Bitmap(imgRetPhoto, Convert.ToInt32(dblImgWd), Convert.ToInt32(dblHtRatio));
                        imgRetPhoto.SetResolution(imgRetPhoto.HorizontalResolution, imgRetPhoto.VerticalResolution);
                    }
                    return imgRetPhoto;
                }
                else
                    return imgRetPhoto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Checks the extenstion of the icon
        /// </summary>
        /// <param name="extension">Extension.</param>
        /// <returns>True if the extension is valid icon extension.</returns>
        public static bool IsValidIconContentType(string extension)
        {
            // array of allowed file type extensions
            string[] validFileExtensions = { "jpg", "jpeg", "jpe", "gif", "bmp", "png", "swf", "ico" };
            var flag = false;
            // loop over the valid file extensions to compare them with uploaded file
            for (var index = 0; index < validFileExtensions.Length; index++)
            {
                if (extension.ToLower() == validFileExtensions[index].ToString().ToLower())
                {
                    flag = true;
                }
            }
            return flag;
        }

        /// <summary>
        /// Checks the image extension if it is valid to upload
        /// </summary>
        /// <param name="filename">Image name.</param>
        /// <returns>True if the file has valid extension.</returns>
        public static bool ValidImageExtension(string fileName)
        {
            return Regex.IsMatch(fileName.ToLower(), @"^.*\.(jpg|gif|jpeg|swf|ico|png|bmp)$");
        }

        /// <summary>
        /// Checks the file extension if it is valid to upload
        /// </summary>
        /// <param name="fileName">File name.</param>
        /// <returns>True if the file extension is valid.</returns>
        public static bool ValidFileExtension(string fileName)
        {
            return Regex.IsMatch(fileName.ToLower(), @"^.*\.(css|html|xml|ascx|js|config)$");
        }

        /// <summary>
        /// Checks the image mime type if the mime type is valid.
        /// </summary>
        /// <param name="extension">Mime type.</param>
        /// <returns>True if the mime type is valid.</returns>
        public static bool IsValidIImageTypeWitMime(string extension)
        {
            // array of allowed file type extensions
            string[] validFileExtensions = { "image/jpg", "image/jpeg", "image/jpe", "image/gif", "image/png" };
            var flag = false;
            // loop over the valid file extensions to compare them with uploaded file
            for (var index = 0; index < validFileExtensions.Length; index++)
            {
                if (extension.ToLower() == validFileExtensions[index].ToString().ToLower())
                {
                    flag = true;
                }
            }
            return flag;
        }

        /// <summary>
        ///  Checks if the image path has valid file extension.
        /// </summary>
        /// <param name="imagePath">Image path.</param>
        /// <returns>True if the image has valid extesion.</returns>
        public static bool IsVAlidImageContentType(string imagePath)
        {
            // extract and store the file extension into another variable
            // array of allowed file type extensions
            string[] validFileExtensions = { "jpg", "jpeg", "jpe", "gif", "bmp", "png", "swf", "ico" };
            var flag = false;
            //String fileExtension = imagePath.Substring(imagePath.Length - 3, 3);
            if (imagePath.Contains("."))
            {
                String fileExtension = imagePath.Substring(imagePath.LastIndexOf(".") + 1);
                // loop over the valid file extensions to compare them with uploaded file
                for (var index = 0; index < validFileExtensions.Length; index++)
                {
                    if (fileExtension.ToLower() == validFileExtensions[index].ToString().ToLower())
                    {
                        flag = true;
                    }
                }
            }
            return flag;
        }

        /// <summary>
        /// Gets a local medium thumb image path
        /// </summary>
        /// 
        public static string LocalMediumThumbImagePath
        {
            get
            {
                string path = HttpContext.Current.Request.PhysicalApplicationPath + "bdimages\\mediumthumb";
                return path;
            }
        }

        /// <summary>
        /// Gets a local small thumb image path
        /// </summary>
        public static string LocalSmallThumbImagePath
        {
            get
            {
                string path = HttpContext.Current.Request.PhysicalApplicationPath + "bdimages\\Smallthumbs";
                return path;
            }
        }

        /// <summary>
        /// Gets the local image path
        /// </summary>
        public static string LocalImagePath
        {
            get
            {
                string path = HttpContext.Current.Request.PhysicalApplicationPath + "bdimages";
                return path;
            }
            set
            {
                string path = value;

            }
        }

        /// <summary>
        /// Creates medium thumbnail.
        /// </summary>
        /// <param name="strImage">Image name.</param>
        /// <param name="PortalID">Portal ID.</param>
        /// <param name="prefix">Image Prefix.</param>
        /// <param name="localImagePath">Local image path.</param>
        /// <param name="imageSize">Image size.</param>
        /// <returns>Saved medium thumbnail path.</returns>
        public static string CreateMediumThumnail(string strImage, int PortalID, string prefix, string localImagePath, int imageSize)
        {
            if (!Directory.Exists(localImagePath))
                Directory.CreateDirectory(localImagePath);
            string SavePath = string.Empty;
            //SavePath = GetImagePathWithFileName(1, prefix, localImagePath);
            SavePath = localImagePath;
            SavePath += strImage.Substring(strImage.LastIndexOf("\\"));
            //int imageSize = 200;
            CreateThmnail(strImage, imageSize, SavePath);
            return SavePath;
        }

        /// <summary>
        /// Creates small thumbnail.
        /// </summary>
        /// <param name="strImage">Image name.</param>
        /// <param name="PortalID">Portal ID.</param>
        /// <param name="prefix">Image Prefix.</param>
        /// <param name="localImagePath">Local image path.</param>
        /// <param name="imageSize">Image size.</param>
        /// <returns>Saved medium thumbnail path.</returns>
        public static string CreateSmallThumnail(string strImage, int PortalID, string prefix, string localImagePath, int imageSize)
        {
            if (!Directory.Exists(localImagePath))
                Directory.CreateDirectory(localImagePath);
            string SavePath = string.Empty;
            //SavePath = GetImagePathWithFileName(2, prefix, localImagePath);
            SavePath = localImagePath;
            SavePath += strImage.Substring(strImage.LastIndexOf("\\"));
            //int imageSize = 125;
            CreateThmnail(strImage, imageSize, SavePath);
            return SavePath;
        }
        [Obsolete("Not in Use In SageFrame2.1")]
        public static string CreateCategoryMediumThumnail(string strImage, int PortalID, string prefix, string localImagePath)
        {
            string SavePath = string.Empty;
            SavePath = GetImagePathWithFileName(1, prefix, localImagePath);
            SavePath += strImage.Substring(strImage.LastIndexOf("."));
            //int imageSidge = Int32.Parse(dbSetting.sp_SettingPortalBySettingID((int)SettingKey.Media_Category_Medium_ThumbnailImageSize, PortalID).SingleOrDefault().Value);
            //CreateThmnail(strImage, imageSidge, SavePath);
            return SavePath;
        }

        [Obsolete("Not in Use In SageFrame2.1")]
        public static string CreateCategorySmallThumnail(string strImage, int PortalID, string prefix, string localImagePath)
        {
            string SavePath = string.Empty;
            SavePath = GetImagePathWithFileName(2, prefix, localImagePath);
            SavePath += strImage.Substring(strImage.LastIndexOf("."));
            //int imageSidge = Int32.Parse(dbSetting.sp_SettingPortalBySettingID((int)SettingKey.Media_Category_Small_ThumbnailImageSize, PortalID).SingleOrDefault().Value);
            //CreateThmnail(strImage, imageSidge, SavePath);
            return SavePath;
        }
    }
}
