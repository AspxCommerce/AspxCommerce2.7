using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using SageFrame.Web;

namespace AspxCommerce.Core
{
    public class AspxImageManagerController
    {
        private static Graphics ApplyWaterMarkImagePosition(string imagePath, string position, Graphics g,
                                                            Bitmap newImage, double angle)
        {
            if (!string.IsNullOrEmpty(imagePath))
            {
                string logo = imagePath.Replace(" ", "").Replace("%20", "");
                string logoPath = HttpContext.Current.Server.MapPath("~/" + logo);
                Point imgPos = new Point();
                if (File.Exists(logoPath))
                {
                    var wt = new FileStream(logoPath, FileMode.Open);
                    Bitmap storeLogo = (Bitmap)Bitmap.FromStream(wt);
                    //rotation
                    //storeLogo.RotateFlip(RotateFlipType.Rotate90FlipX);

                    int logoWidth = (int)(newImage.Width * 20 / 100);
                    int logoHeight = (int)(newImage.Height * 15 / 100);
                    wt.Close();
                    //move rotation point to center of image
                    //g.TranslateTransform((float)storeLogo.Width / 2, (float)storeLogo.Height / 2);
                    //rotate//rotate graphics
                    //g.RotateTransform((float) angle);
                    ImagePosition imgPosType = (ImagePosition)Enum.Parse(typeof(ImagePosition), position);
                    switch (imgPosType)
                    {
                        case ImagePosition.CENTER:
                            imgPos.X = (int)newImage.Width / 2 - logoWidth / 2;
                            imgPos.Y = (int)((newImage.Height * 50 / 100) - logoHeight / 2);
                            break;
                        case ImagePosition.TOP_RIGHT:
                            imgPos.X = (int)newImage.Width - logoWidth;
                            imgPos.Y = 0;
                            break;
                        case ImagePosition.TOP_LEFT:
                            imgPos.X = 0;
                            imgPos.Y = 0;
                            break;
                        case ImagePosition.BOTTOM_RIGHT:
                            imgPos.X = (int)newImage.Width - logoWidth;
                            imgPos.Y = (int)newImage.Height - logoHeight;
                            break;
                        case ImagePosition.BOTTOM_LEFT:
                            imgPos.X = 0;
                            imgPos.Y = (int)newImage.Height - logoHeight;
                            break;
                    }
                    System.Drawing.Drawing2D.Matrix mat = new System.Drawing.Drawing2D.Matrix();
                    // Rotates around the images top left + centre
                    mat.RotateAt((float)angle,
                                 new PointF(imgPos.X + (storeLogo.Width / 2), imgPos.Y + (storeLogo.Height / 2)));

                    g.Transform = mat;
                    //OPACITY
                    ColorMatrix colormatrix = new ColorMatrix();
                    colormatrix.Matrix33 = (50f/100f);
                    ImageAttributes imgAttribute = new ImageAttributes();
                    imgAttribute.SetColorMatrix(colormatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);     
                 
                    // g.DrawImage(storeLogo, imgPos.X, imgPos.Y, logoWidth, logoHeight);
                   // g.DrawImage(storeLogo, new Rectangle(0, 0, imgPos.X, imgPos.Y), 0, 0, logoWidth, logoHeight, GraphicsUnit.Pixel, imgAttribute);

                    g.DrawImage(storeLogo, new Rectangle(imgPos.X, imgPos.Y, logoWidth, logoHeight), 0, 0, logoWidth, logoHeight, GraphicsUnit.Pixel, imgAttribute);
                   
                    //remove last roteated degree
                    //move image back
                    // g.TranslateTransform(-(float)storeLogo.Width / 2, -(float)storeLogo.Height / 2);
                    // g.RotateTransform((float) -angle);
                    g.Transform = new Matrix();
                    wt.Dispose();
                    storeLogo.Dispose();
                    return g;
                }
            }
            return g;
        }

        private static Graphics ApplyWaterMarkTextPosition(string waterMarkText, string position, Graphics g,
                                                           Bitmap newImage, double angle)
        {
            TextPosition posType = (TextPosition)Enum.Parse(typeof(TextPosition), position);
            double halfHypotenuse = 0;
            StringFormat stringFormat = new StringFormat();

            Point textPos = new Point();
            if (!string.IsNullOrEmpty(position))
            {

                switch (posType)
                {
                    case TextPosition.BOTTOM_CENTER:
                        // Horizontally and vertically aligned the string
                        // This makes the placement Point the physical 
                        // center of the string instead of top-left.
                        // a^2 = b^2 + c^2 ; a = sqrt(b^2 + c^2)
                        halfHypotenuse = (Math.Sqrt((newImage.Height
                                                     * newImage.Height) +
                                                    (newImage.Width *
                                                     newImage.Width))) / 2;
                        textPos.X = (int)halfHypotenuse;
                        textPos.Y = (int)(newImage.Height * 95 / 100);
                        stringFormat.Alignment = StringAlignment.Center;
                        stringFormat.LineAlignment = StringAlignment.Center;

                        break;
                    case TextPosition.MID_CENTER:
                        // a^2 = b^2 + c^2 ; a = sqrt(b^2 + c^2)
                        halfHypotenuse = (Math.Sqrt((newImage.Height
                                                     * newImage.Height) +
                                                    (newImage.Width *
                                                     newImage.Width))) / 2;
                        textPos.X = (int)halfHypotenuse;
                        textPos.Y = (int)(newImage.Height * 50 / 100);
                        stringFormat.Alignment = StringAlignment.Center;
                        stringFormat.LineAlignment = StringAlignment.Center;
                        break;
                    case TextPosition.TOP_CENTER:
                        // a^2 = b^2 + c^2 ; a = sqrt(b^2 + c^2)
                        halfHypotenuse = (Math.Sqrt((newImage.Height
                                                     * newImage.Height) +
                                                    (newImage.Width *
                                                     newImage.Width))) / 2;
                        textPos.X = (int)halfHypotenuse;
                        textPos.Y = (int)(newImage.Height * 5 / 100);
                        stringFormat.Alignment = StringAlignment.Center;
                        stringFormat.LineAlignment = StringAlignment.Center;
                        break;
                    case TextPosition.BOTTOM_LEFT:
                        textPos.X = 0;
                        textPos.Y = (int)(newImage.Height * 95 / 100);
                        stringFormat.Alignment = StringAlignment.Near;
                        stringFormat.LineAlignment = StringAlignment.Near;
                        break;
                    case TextPosition.MID_LEFT:
                        textPos.X = 0;
                        textPos.Y = (int)(newImage.Height * 50 / 100);
                        stringFormat.Alignment = StringAlignment.Near;
                        stringFormat.LineAlignment = StringAlignment.Near;
                        break;
                    case TextPosition.TOP_LEFT:
                        textPos.X = 0;
                        textPos.Y = (int)(newImage.Height * 5 / 100);
                        stringFormat.Alignment = StringAlignment.Near;
                        stringFormat.LineAlignment = StringAlignment.Near;
                        break;
                    default:
                        break;
                }

                // Trigonometry: Tangent = Opposite / Adjacent
                double tangent = (double)newImage.Height /
                                 (double)newImage.Width;

                // convert arctangent to degrees
                // double angle = Math.Atan(tangent)*(180/Math.PI);
                // g.RotateTransform((float) angle);

                Font font = null;
                float fontSize = (float)(newImage.Height * 2.5 / 100);
                font = new Font("Verdana", fontSize, FontStyle.Bold, GraphicsUnit.Pixel);

                //Adds a transparent watermark with an 100 alpha value.
                Color color = Color.White; //Color.FromArgb(100, 0, 0, 0);
                //The position where to draw the watermark on the image
                Point pt = new Point(10, 30);
                SolidBrush sbrush = new SolidBrush(color);
                Rectangle rect1 = new Rectangle();

                rect1.X = 0;
                rect1.Width = newImage.Width;
                rect1.Height = (int)fontSize + 5;
                rect1.Y = textPos.Y - rect1.Height;
                System.Drawing.Drawing2D.Matrix mat = new System.Drawing.Drawing2D.Matrix();
                // Rotates around the images top left + centre
                mat.RotateAt((float)angle, new PointF(textPos.X + (rect1.Width / 2), textPos.Y + (rect1.Height / 2)));

                g.Transform = mat;
                // Create a StringFormat object with the each line of text, and the block 
                // of text centered on the page.
                // Draw the text and the surrounding rectangle.
                g.DrawString(waterMarkText, font, sbrush, rect1, stringFormat);
                g.DrawRectangle(Pens.Transparent, rect1);
                //g.RotateTransform((float) -angle);
                g.Transform = new Matrix();



            }
            else
            {

                halfHypotenuse = (Math.Sqrt((newImage.Height
                                             * newImage.Height) +
                                            (newImage.Width *
                                             newImage.Width))) / 2;
                textPos.X = (int)halfHypotenuse;
                textPos.Y = (int)(newImage.Height * 95 / 100);
                g.RotateTransform((float)angle);

                Font font = null;
                float fontSize = (float)(newImage.Height * 2.5 / 100);
                font = new Font("Verdana", fontSize, FontStyle.Bold, GraphicsUnit.Pixel);

                //Adds a transparent watermark with an 100 alpha value.
                Color color = Color.White; //Color.FromArgb(100, 0, 0, 0);
                //The position where to draw the watermark on the image
                Point pt = new Point(10, 30);
                SolidBrush sbrush = new SolidBrush(color);

                Rectangle rect1 = new Rectangle();

                rect1.X = 0;
                rect1.Width = newImage.Width;
                rect1.Height = (int)fontSize + 5;
                rect1.Y = textPos.Y - rect1.Height;

                System.Drawing.Drawing2D.Matrix mat = new System.Drawing.Drawing2D.Matrix();
                // Rotates around the images top left + centre
                mat.RotateAt((float)angle, new PointF(textPos.X + (rect1.Width / 2), textPos.Y + (rect1.Height / 2)));

                g.Transform = mat;
                // Create a StringFormat object with the each line of text, and the block 
                // of text centered on the page.
                stringFormat.Alignment = StringAlignment.Center;
                stringFormat.LineAlignment = StringAlignment.Center;

                // Draw the text and the surrounding rectangle.
                g.DrawString(waterMarkText, font, sbrush, rect1, stringFormat);
                g.DrawRectangle(Pens.Transparent, rect1);
                //g.DrawString(waterMarkText, font, sbrush, textPos, stringFormat);
                g.RotateTransform((float)-angle);
                g.Transform = new Matrix();
            }
            return g;

        }

        public static void AddWaterMarksItem(FileStream fileStream, string path, string fileName,
                                             AspxCommonInfo aspxCommonObj)
        {
            //string originalFileName = null;
            //string newFileName = null;
            Bitmap tmpImage = default(Bitmap);
            Bitmap newImage = default(Bitmap);
            Graphics g = default(Graphics);
          
            FileStream fs = default(FileStream);
            StoreSettingConfig ssc = new StoreSettingConfig();
            int newWidth = int.Parse(ssc.GetStoreSettingsByKey(StoreSetting.ItemImageMaxWidth, aspxCommonObj.StoreID,
                                                         aspxCommonObj.PortalID, aspxCommonObj.CultureName));
            int newHeight = int.Parse(ssc.GetStoreSettingsByKey(StoreSetting.ItemImageMaxHeight, aspxCommonObj.StoreID,
                                                       aspxCommonObj.PortalID, aspxCommonObj.CultureName));

           
            string imagePath = ssc.GetStoreSettingsByKey(StoreSetting.StoreLogoURL, aspxCommonObj.StoreID,
                                                         aspxCommonObj.PortalID, aspxCommonObj.CultureName);
            string waterMarkText = ssc.GetStoreSettingsByKey(StoreSetting.WaterMarkText, aspxCommonObj.StoreID,
                                                             aspxCommonObj.PortalID, aspxCommonObj.CultureName);


            try
            {

                if (!string.IsNullOrEmpty(imagePath) || !string.IsNullOrEmpty(waterMarkText))
                {
                    //procced
                    // if(string.IsNullOrEmpty(imagePath))
                    string waterMarkposition = ssc.GetStoreSettingsByKey(StoreSetting.WaterMarkTextPosition,
                                                                         aspxCommonObj.StoreID, aspxCommonObj.PortalID,
                                                                         aspxCommonObj.CultureName);
                    double waterMarkRotAngle =
                        double.Parse(ssc.GetStoreSettingsByKey(StoreSetting.WaterMarkTextRotation, aspxCommonObj.StoreID,
                                                               aspxCommonObj.PortalID, aspxCommonObj.CultureName));


                    string waterMarkImageposition = ssc.GetStoreSettingsByKey(StoreSetting.WaterMarkImagePosition,
                                                                              aspxCommonObj.StoreID,
                                                                              aspxCommonObj.PortalID,
                                                                              aspxCommonObj.CultureName);
                    double waterMarkImageRotAngle =
                        double.Parse(ssc.GetStoreSettingsByKey(StoreSetting.WaterMarkImageRotation,
                                                               aspxCommonObj.StoreID, aspxCommonObj.PortalID,
                                                               aspxCommonObj.CultureName));


                    fs = fileStream;
                    tmpImage = (Bitmap)Bitmap.FromStream(fs);
                    fs.Close();
                    fs.Dispose();

                    // newHeight = (newWidth*tmpImage.Height)/tmpImage.Width;

                    newImage = new Bitmap(tmpImage);
                    g = Graphics.FromImage(newImage);

#region Resize

                    //g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    //g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    //g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                    //g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                    //g.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
                    //g.FillRectangle(Brushes.White, 0, 0, newWidth, newHeight);
                    //if (tmpImage.Width > tmpImage.Height || tmpImage.Width < newWidth)
                    //{
                    //    //calculate center of image
                    //    int X = (int)newImage.Width / 2 - tmpImage.Width / 2;
                    //    int Y = (int)((newImage.Height * 50 / 100) - tmpImage.Height / 2);
                    //    g.DrawImage(tmpImage, X, Y, tmpImage.Width, tmpImage.Height);
                    //}
                    //else
                    //{
                    //    g.DrawImage(tmpImage, 0, 0, newWidth, newHeight);
                    //}

                    #endregion                    



                    #region WATERMARK

                    try
                    {


                        //WATERMARK IMAGE
                        bool showWaterMarkImage = bool.Parse(ssc.GetStoreSettingsByKey(StoreSetting.ShowWaterMarkImage,
                                                                                       aspxCommonObj.StoreID,
                                                                                       aspxCommonObj.PortalID,
                                                                                       aspxCommonObj.CultureName));
                        if (showWaterMarkImage)
                            g = ApplyWaterMarkImagePosition(imagePath, waterMarkImageposition, g, newImage,
                                                            waterMarkImageRotAngle);

                        //WATERMARK TEXT
                        g = ApplyWaterMarkTextPosition(waterMarkText, waterMarkposition, g, newImage, waterMarkRotAngle);


                        g.Dispose();
                    }
                    catch (Exception)
                    {

                    }

                    #endregion

                    //  newFileName = NewWidth.ToString() + "_" + ImageName.Replace(" ", "").Replace("%20", "");
                    newImage.Save(HttpContext.Current.Server.MapPath(path + "\\" + fileName),
                                  System.Drawing.Imaging.ImageFormat.Jpeg);
                    newImage.Dispose();
                    tmpImage.Dispose();

                }
                else
                {
                    //no watermarked setting
                }


            }
            catch (Exception)
            {

            }
        }

        public static void AddWaterMarksCostVariantItem(FileStream fileStream, string path, string fileName,
                                             AspxCommonInfo aspxCommonObj )
        {
            //string originalFileName = null;
            //string newFileName = null;
            Bitmap tmpImage = default(Bitmap);
            Bitmap newImage = default(Bitmap);
            Graphics g = default(Graphics);

            FileStream fs = default(FileStream);
            StoreSettingConfig ssc = new StoreSettingConfig();
            int newWidth = int.Parse(ssc.GetStoreSettingsByKey(StoreSetting.ItemImageMaxWidth, aspxCommonObj.StoreID,
                                                         aspxCommonObj.PortalID, aspxCommonObj.CultureName));
            int newHeight = int.Parse(ssc.GetStoreSettingsByKey(StoreSetting.ItemImageMaxHeight, aspxCommonObj.StoreID,
                                                       aspxCommonObj.PortalID, aspxCommonObj.CultureName));


            string imagePath = ssc.GetStoreSettingsByKey(StoreSetting.StoreLogoURL, aspxCommonObj.StoreID,
                                                         aspxCommonObj.PortalID, aspxCommonObj.CultureName);
            string waterMarkText = ssc.GetStoreSettingsByKey(StoreSetting.WaterMarkText, aspxCommonObj.StoreID,
                                                             aspxCommonObj.PortalID, aspxCommonObj.CultureName);


            try
            {

                if (!string.IsNullOrEmpty(imagePath) || !string.IsNullOrEmpty(waterMarkText))
                {
                    //procced
                    // if(string.IsNullOrEmpty(imagePath))
                    string waterMarkposition = ssc.GetStoreSettingsByKey(StoreSetting.WaterMarkTextPosition,
                                                                         aspxCommonObj.StoreID, aspxCommonObj.PortalID,
                                                                         aspxCommonObj.CultureName);
                    double waterMarkRotAngle =
                        double.Parse(ssc.GetStoreSettingsByKey(StoreSetting.WaterMarkTextRotation, aspxCommonObj.StoreID,
                                                               aspxCommonObj.PortalID, aspxCommonObj.CultureName));


                    string waterMarkImageposition = ssc.GetStoreSettingsByKey(StoreSetting.WaterMarkImagePosition,
                                                                              aspxCommonObj.StoreID,
                                                                              aspxCommonObj.PortalID,
                                                                              aspxCommonObj.CultureName);
                    double waterMarkImageRotAngle =
                        double.Parse(ssc.GetStoreSettingsByKey(StoreSetting.WaterMarkImageRotation,
                                                               aspxCommonObj.StoreID, aspxCommonObj.PortalID,
                                                               aspxCommonObj.CultureName));


                    fs = fileStream;
                    tmpImage = (Bitmap)Bitmap.FromStream(fs);
                    fs.Close();
                    fs.Dispose();

                    // newHeight = (newWidth*tmpImage.Height)/tmpImage.Width;

                    newImage = new Bitmap(newWidth, newHeight);
                    g = Graphics.FromImage(newImage);
           
                    #region Resize

                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                    g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                    g.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
                    g.FillRectangle(Brushes.White, 0, 0, newWidth, newHeight);
                    if (tmpImage.Width > tmpImage.Height || tmpImage.Width < newWidth)
                    {
                        //calculate center of image
                        int X = (int)newImage.Width / 2 - tmpImage.Width / 2;
                        int Y = (int)((newImage.Height * 50 / 100) - tmpImage.Height / 2);
                        g.DrawImage(tmpImage, X, Y, tmpImage.Width, tmpImage.Height);
                    }
                    else
                    {
                        g.DrawImage(tmpImage, 0, 0, newWidth, newHeight);
                    }

                    #endregion



                    #region WATERMARK

                    try
                    {


                        //WATERMARK IMAGE
                        bool showWaterMarkImage = bool.Parse(ssc.GetStoreSettingsByKey(StoreSetting.ShowWaterMarkImage,
                                                                                       aspxCommonObj.StoreID,
                                                                                       aspxCommonObj.PortalID,
                                                                                       aspxCommonObj.CultureName));
                        if (showWaterMarkImage)
                            g = ApplyWaterMarkImagePosition(imagePath, waterMarkImageposition, g, newImage,
                                                            waterMarkImageRotAngle);

                        //WATERMARK TEXT
                        g = ApplyWaterMarkTextPosition(waterMarkText, waterMarkposition, g, newImage, waterMarkRotAngle);


                        g.Dispose();
                    }
                    catch (Exception)
                    {

                    }

                    #endregion

                    //  newFileName = NewWidth.ToString() + "_" + ImageName.Replace(" ", "").Replace("%20", "");
                    newImage.Save(HttpContext.Current.Server.MapPath(path + "\\" + fileName),
                                  System.Drawing.Imaging.ImageFormat.Jpeg);

                  
                    newImage.Dispose();
                    tmpImage.Dispose();
                    CreateThumbnails(path, fileName, aspxCommonObj);

                }
                else
                {
                    //no watermarked setting
                }


            }
            catch (Exception)
            {

            }
        }


        private static void CreateThumbnails(string path, string fileName,
                                             AspxCommonInfo aspxCommonObj)
        {
            try
            {


                StoreSettingConfig ssc = new StoreSettingConfig();
                int largeSizeHeight = int.Parse(ssc.GetStoreSettingsByKey(StoreSetting.ItemLargeThumbnailImageHeight, aspxCommonObj.StoreID,
                                                                   aspxCommonObj.PortalID,
                                                                   aspxCommonObj.CultureName));
                int largeSizeWidth = int.Parse(ssc.GetStoreSettingsByKey(StoreSetting.ItemLargeThumbnailImageWidth, aspxCommonObj.StoreID,
                                                                  aspxCommonObj.PortalID,
                                                                  aspxCommonObj.CultureName));
                int midSizeHeight = int.Parse(ssc.GetStoreSettingsByKey(StoreSetting.ItemMediumThumbnailImageHeight, aspxCommonObj.StoreID,
                                                                  aspxCommonObj.PortalID,
                                                                  aspxCommonObj.CultureName));
                int midSizeWidth = int.Parse(ssc.GetStoreSettingsByKey(StoreSetting.ItemMediumThumbnailImageWidth, aspxCommonObj.StoreID,
                                                                  aspxCommonObj.PortalID,
                                                                  aspxCommonObj.CultureName));
                int smallSizeHeight = int.Parse(ssc.GetStoreSettingsByKey(StoreSetting.ItemSmallThumbnailImageHeight, aspxCommonObj.StoreID,
                                                                  aspxCommonObj.PortalID,
                                                                  aspxCommonObj.CultureName));
                int smallSizeWidth = int.Parse(ssc.GetStoreSettingsByKey(StoreSetting.ItemSmallThumbnailImageWidth, aspxCommonObj.StoreID,
                                                                 aspxCommonObj.PortalID,
                                                                 aspxCommonObj.CultureName));

                string largePath = path + "Large/" + fileName;
                string midPath = path + "Medium/" + fileName;
                string smallPath = path + "Small/" + fileName;
                string source = path + fileName;
                PictureManager.CreateThmnail(HttpContext.Current.Server.MapPath(source), largeSizeHeight,largeSizeWidth,
                                             HttpContext.Current.Server.MapPath(largePath));
                PictureManager.CreateThmnail(HttpContext.Current.Server.MapPath(source), midSizeHeight,midSizeWidth, HttpContext.Current.Server.MapPath(midPath));
                PictureManager.CreateThmnail(HttpContext.Current.Server.MapPath(source), smallSizeHeight, smallSizeWidth, HttpContext.Current.Server.MapPath(smallPath));
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static string  CropBannerImage(FileStream fileStream, string filePath, AspxCommonInfo aspxCommonObj, int height, int width, int x, int y)
        {
            Bitmap src = default(Bitmap);
            Bitmap target = default(Bitmap);
            Graphics g = default(Graphics);
            try
            {

                Rectangle cropRect = new Rectangle();
                cropRect.Height = height;
                cropRect.Width = width;
                cropRect.X = x;
                cropRect.Y = y;
                src = Bitmap.FromStream(fileStream) as Bitmap;
                target = new Bitmap(cropRect.Width, cropRect.Height);
                g = Graphics.FromImage(target);
                g.DrawImage(src, new Rectangle(0, 0, target.Width, target.Height),
                            cropRect,
                            GraphicsUnit.Pixel);
                g.Dispose();
                fileStream.Close();
                fileStream.Dispose();
                Random rnd = new Random();
               string filename = System.IO.Path.GetFileNameWithoutExtension(filePath);
               string strExtension = System.IO.Path.GetExtension(filePath);
                filename = filename.Substring(0, (filename.Length - strExtension.Length) - 1);
                filename = filename + '_' + rnd.Next(111111, 999999).ToString() +  strExtension;
               // string filePath = strBaseLocation + "\\" + filename;
                string newfilePath = "Upload/temp/" + filename;
                string newPath = HttpContext.Current.Server.MapPath("~/" + newfilePath);

                target.Save(newPath, System.Drawing.Imaging.ImageFormat.Jpeg);
                target.Dispose();
                src.Dispose();
                if (File.Exists(HttpContext.Current.Server.MapPath("~/" + filePath)))
                    File.Delete(HttpContext.Current.Server.MapPath("~/" + filePath));

                return newfilePath;

            }
            catch (Exception ex)
            {
                fileStream.Close();
                fileStream.Dispose();
                target.Dispose();
                src.Dispose();
                g.Dispose();
                throw ex;
            }
        }

       
    }
}
