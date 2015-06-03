using System;
using System.Web;
using SageFrame.Web;
using AspxCommerce.Core;
using System.Collections.Generic;
using SageFrame;
using System.Text;
using AspxCommerce.ImageResizer;

public partial class Modules_AspxCommerce_AspxImageCloudGallery_ItemImageCloudGallery : BaseUserControl
{
    private int StoreID, PortalID, CustomerID, ItemID,ItemTypeID=0;
    public string  ItemImageGalleryModulePath, referImagePath;
    private string UserName, CultureName, SessionCode = string.Empty;
    public int ImageCount = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            GetPortalCommonInfo(out StoreID, out PortalID, out CustomerID, out UserName, out CultureName, out SessionCode);
            AspxCommonInfo aspxCommonObj = new AspxCommonInfo(StoreID, PortalID, UserName, CultureName, CustomerID, SessionCode);
            ItemImageGalleryModulePath = ResolveUrl(this.AppRelativeTemplateSourceDirectory);
            if (!IsPostBack)
            {
                IncludeJs("ItemImageGallery", "/Modules/AspxCommerce/AspxImageCloudGallery/js/ItemImageGallery.js", "/Modules/AspxCommerce/AspxImageCloudGallery/js/cloud-zoom.js"
                    , "/Modules/AspxCommerce/AspxImageCloudGallery/js/jquery.jcarousel.js");
                IncludeCss("ItemImageGallery", "/Modules/AspxCommerce/AspxImageCloudGallery/css/cloud-zoom.css", "/Modules/AspxCommerce/AspxImageCloudGallery/css/Slider.css");
            }
            BindItemImageGallery(aspxCommonObj);
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }
    private void BindItemImageGallery(AspxCommonInfo aspxCommonObj)
    {
        SageFrameRoute parentPage = (SageFrameRoute)this.Page;
        string itemSKU = parentPage.Key;
        string costCombination = string.Empty;
        AspxCoreController obj = new AspxCoreController();
        ItemCommonInfo objItemInfo = obj.GetItemInfoFromSKU(itemSKU, aspxCommonObj);
        if (objItemInfo != null)
        {
            ItemID = objItemInfo.ItemID;
            ItemTypeID = objItemInfo.ItemTypeID;
        }
        if (ItemTypeID == 3)
        {
            List<GiftCardInfo> lstItemGallery = obj.GetGiftCardThemeImagesByItem(ItemID, aspxCommonObj);
            BindGiftGallery(lstItemGallery,aspxCommonObj);
        }
        else
        {
            List<VariantCombination> lstItemCombination = obj.GetCostVariantCombinationbyItemSku(itemSKU, aspxCommonObj);
            if (lstItemCombination != null && lstItemCombination.Count > 0)
            {
                costCombination = lstItemCombination[0].CombinationID.ToString();
            }
            else
            {
                costCombination = "0";
            }
            List<ItemsInfoSettings> lstItemGallery = obj.GetItemsImageGalleryInfoBySKU(itemSKU, aspxCommonObj, costCombination);
            BindGallery(lstItemGallery, aspxCommonObj);
        }
    }

    private void BindGallery(List<ItemsInfoSettings> lstItemGallery,AspxCommonInfo aspxCommonObj)
    {
        string aspxRootPath = ResolveUrl("~/");
        StoreSettingConfig ssc = new StoreSettingConfig();
        string NoImagePath = ssc.GetStoreSettingsByKey(StoreSetting.DefaultProductImageURL, StoreID, PortalID, CultureName);
        StringBuilder galleryContainer = new StringBuilder();
        StringBuilder galleryThumbContainer = new StringBuilder();
        galleryThumbContainer.Append("<div id='divBindThumbs' class='thumbs jcarousel-skin'><ul id='thumblist'>");
        galleryContainer.Append("<div id='divMainImage'>");
        ImageCount = lstItemGallery.Count;
        if (lstItemGallery != null && lstItemGallery.Count > 0)
        {            
            foreach (ItemsInfoSettings item in lstItemGallery)
            {
                string imagePath = "Modules/AspxCommerce/AspxItemsManagement/uploads/" + item.ImagePath;
                string altImagePath = "Modules/AspxCommerce/AspxItemsManagement/uploads/" + item.AlternateText;
                if (item.ImagePath == string.Empty)
                {
                    imagePath = NoImagePath;
                }
                else
                {   //Resize Image Dynamically
                    InterceptImageController objImage = new InterceptImageController();
                    objImage.MultipleImageResizer(item.ImagePath, (ImageType.Medium + ";" + ImageType.Large + ";" + ImageType.Small), ImageCategoryType.Item.ToString(), aspxCommonObj);
                }
                if (lstItemGallery.IndexOf(item) == 0)
                {
                    referImagePath = imagePath;
                    galleryContainer.Append("<a rel='' href=\"");
                    galleryContainer.Append(aspxRootPath);
                    galleryContainer.Append(imagePath);
                    galleryContainer.Append("\" id='zoom1' class='cloud-zoom'  title=\"");
                    galleryContainer.Append(item.AlternateText);
                    galleryContainer.Append("\"><img title=\"");
                    galleryContainer.Append(item.AlternateText);
                    galleryContainer.Append("\" src=\"");
                    galleryContainer.Append(aspxRootPath);
                    galleryContainer.Append(imagePath.Replace("uploads", "uploads/Large"));
                    galleryContainer.Append("\"></a>");
                }
                string rel = "useZoom: 'zoom1', smallImage: '" + aspxRootPath + imagePath.Replace("uploads", "uploads/Large") + "' ";
                galleryThumbContainer.Append("<li><a rel=\"");
                galleryThumbContainer.Append(rel);
                galleryThumbContainer.Append("\" href=\"");
                galleryThumbContainer.Append(aspxRootPath);
                galleryThumbContainer.Append(imagePath);
                galleryThumbContainer.Append("\" class='cloud-zoom-gallery'><img title=\"");
                galleryThumbContainer.Append(item.AlternateText);
                galleryThumbContainer.Append("\" src=\"");
                galleryThumbContainer.Append(aspxRootPath);
                galleryThumbContainer.Append(imagePath.Replace("uploads", "uploads/Small"));
                galleryThumbContainer.Append("\" class='zoom-tiny-image'></a></li>");
            }
        }
        else
        {
            galleryContainer.Append("<a rel='' href=\"");
            galleryContainer.Append(aspxRootPath);
            galleryContainer.Append(NoImagePath);
            galleryContainer.Append("\" id='zoom1' class='cloud-zoom' ");           
            galleryContainer.Append("><img src=\"");
            galleryContainer.Append(aspxRootPath);
            galleryContainer.Append(NoImagePath.Replace("uploads", "uploads/Large"));
            galleryContainer.Append("\"></a>");
            string rel = "useZoom: 'zoom1', smallImage: '" + aspxRootPath + NoImagePath.Replace("uploads", "uploads/Large") + "' ";
            galleryThumbContainer.Append("<li><a rel=\"");
            galleryThumbContainer.Append(rel);
            galleryThumbContainer.Append("\" href=\"");
            galleryThumbContainer.Append(aspxRootPath);
            galleryThumbContainer.Append(NoImagePath);
            galleryThumbContainer.Append("\" class='cloud-zoom-gallery'><img src=\"");
            galleryThumbContainer.Append(aspxRootPath);
            galleryThumbContainer.Append(NoImagePath.Replace("uploads", "uploads/Small"));
            galleryThumbContainer.Append("\" class='zoom-tiny-image'></a></li>");
        }
        galleryContainer.Append("</ul></div>");
        galleryThumbContainer.Append("</div>");
        ltrItemGallery.Text = galleryContainer.ToString();
        ltrItemThumb.Text = galleryThumbContainer.ToString();
    }

    private void BindGiftGallery(List<GiftCardInfo> lstItemGallery,AspxCommonInfo aspxCommonObj)
    {
        string aspxRootPath = ResolveUrl("~/");
        StoreSettingConfig ssc = new StoreSettingConfig();
        string NoImagePath = ssc.GetStoreSettingsByKey(StoreSetting.DefaultProductImageURL, StoreID, PortalID, CultureName);
        StringBuilder galleryContainer = new StringBuilder();
        StringBuilder galleryThumbContainer = new StringBuilder();
        galleryThumbContainer.Append("<div id='divBindThumbs' class='jcarousel-skin'><ul id='thumblist'>");
        galleryContainer.Append("<div id='divMainImage'>");
        ImageCount = lstItemGallery.Count;
        if (lstItemGallery != null && lstItemGallery.Count > 0)
        {
            foreach (GiftCardInfo item in lstItemGallery)
            {
                string imagePath =  item.GraphicImage;
                if (item.GraphicImage == "")
                {
                    imagePath = NoImagePath;
                }
                else
                {  //Resize Image Dynamically
                    InterceptImageController objImage = new InterceptImageController();
                    objImage.MultipleImageResizer(item.GraphicImage, (ImageType.Medium + ";" + ImageType.Large + ";" + ImageType.Small), ImageCategoryType.Item.ToString(), aspxCommonObj);
                }
                string rel = "useZoom: 'zoom1', smallImage: '" + aspxRootPath + imagePath + "' ";
                if (lstItemGallery.IndexOf(item) == 0)
                {
                    referImagePath = imagePath;
                    galleryContainer.Append("<a rel='' href=\"");
                    galleryContainer.Append(aspxRootPath);
                    galleryContainer.Append(imagePath);
                    galleryContainer.Append("\" id='zoom1' class='cloud-zoom'  title=\"");
                    galleryContainer.Append(item.GraphicName);
                    galleryContainer.Append("\"><img title=\"");
                    galleryContainer.Append(item.GraphicName);
                    galleryContainer.Append("\" src=\"");
                    galleryContainer.Append(aspxRootPath);
                    galleryContainer.Append(imagePath);
                    galleryContainer.Append("\"></a>");
                    galleryThumbContainer.Append("<li><a class='selected' rel=\"");
                    galleryThumbContainer.Append(rel);
                    galleryThumbContainer.Append("\" data-id=\"");
                    galleryThumbContainer.Append(item.GiftCardGraphicId);
                    galleryThumbContainer.Append("\" href=\"");
                    galleryThumbContainer.Append(aspxRootPath);
                    galleryThumbContainer.Append(imagePath);
                    galleryThumbContainer.Append("\" class='cloud-zoom-gallery'><img title=\"");
                    galleryThumbContainer.Append(item.GraphicName);
                    galleryThumbContainer.Append("\" src=\"");
                    galleryThumbContainer.Append(aspxRootPath);
                    galleryThumbContainer.Append(imagePath);
                    galleryThumbContainer.Append("\" class='zoom-tiny-image'></a></li>");
                }
                else
                {                    
                    galleryThumbContainer.Append("<li><a rel=\"");
                    galleryThumbContainer.Append(rel);
                    galleryThumbContainer.Append("\" data-id=\"");
                    galleryThumbContainer.Append(item.GiftCardGraphicId);
                    galleryThumbContainer.Append("\" href=\"");
                    galleryThumbContainer.Append(aspxRootPath);
                    galleryThumbContainer.Append(imagePath);
                    galleryThumbContainer.Append("\" class='cloud-zoom-gallery'><img title=\"");
                    galleryThumbContainer.Append(item.GraphicName);
                    galleryThumbContainer.Append("\" src=\"");
                    galleryThumbContainer.Append(aspxRootPath);
                    galleryThumbContainer.Append(imagePath);
                    galleryThumbContainer.Append("\" class='zoom-tiny-image'></a></li>");
                }
            }
        }
        else
        {
            galleryContainer.Append("<a rel='' href=\"");
            galleryContainer.Append(aspxRootPath);
            galleryContainer.Append(NoImagePath);
            galleryContainer.Append("\" id='zoom1' class='cloud-zoom' ");
            galleryContainer.Append("><img src=\"");
            galleryContainer.Append(aspxRootPath);
            galleryContainer.Append(NoImagePath.Replace("uploads", "uploads/Large"));
            galleryContainer.Append("\"></a>");
            string rel = "useZoom: 'zoom1', smallImage: '" + aspxRootPath + NoImagePath.Replace("uploads", "uploads/Large") + "' ";
            galleryThumbContainer.Append("<li><a rel=\"");
            galleryThumbContainer.Append(rel);
            galleryThumbContainer.Append("\" href=\"");
            galleryThumbContainer.Append(aspxRootPath);
            galleryThumbContainer.Append(NoImagePath);
            galleryThumbContainer.Append("\" class='cloud-zoom-gallery'><img src=\"");
            galleryThumbContainer.Append(aspxRootPath);
            galleryThumbContainer.Append(NoImagePath.Replace("uploads", "uploads/Small"));
            galleryThumbContainer.Append("\" class='zoom-tiny-image'></a></li>");
        }
        galleryContainer.Append("</ul></div>");
        galleryThumbContainer.Append("</div>");
        ltrItemGallery.Text = galleryContainer.ToString();
        ltrItemThumb.Text = galleryThumbContainer.ToString();
    }
}