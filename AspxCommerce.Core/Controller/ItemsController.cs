/*
AspxCommerce® - http://www.aspxcommerce.com
Copyright (c) 2011-2015 by AspxCommerce

Permission is hereby granted, free of charge, to any person obtaining
a copy of this software and associated documentation files (the
"Software"), to deal in the Software without restriction, including
without limitation the rights to use, copy, modify, merge, publish,
distribute, sublicense, and/or sell copies of the Software, and to
permit persons to whom the Software is furnished to do so, subject to
the following conditions:

The above copyright notice and this permission notice shall be
included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
WITH THE SOFTWARE OR THE USE OF OTHER DEALINGS IN THE SOFTWARE. 
*/



using System;
using System.Collections.Generic;

namespace AspxCommerce.Core
{
    public class ItemsController
    {
        #region Constructor

        public ItemsController()
        {
        }

        #endregion

        public int SaveUpdateItemAndAttributes(ItemsInfo.ItemSaveBasicInfo itemObj, AspxCommonInfo aspxCommonObj)
        {
            bool isModified = false;
            bool updateFlag = false;
            int storeId = aspxCommonObj.StoreID;
            int portalId = aspxCommonObj.PortalID;
            string culture = aspxCommonObj.CultureName;
            string userName = aspxCommonObj.UserName;
            if (itemObj.ItemId > 0)
            {
                isModified = true;
                updateFlag = true;
            }
            StoreSettingConfig ssc = new StoreSettingConfig();
            int itemLargeThumbNailHeight =
                Convert.ToInt32(ssc.GetStoreSettingsByKey(StoreSetting.ItemLargeThumbnailImageHeight, storeId, portalId, culture));
            int itemLargeThumbNailWidth =
                Convert.ToInt32(ssc.GetStoreSettingsByKey(StoreSetting.ItemLargeThumbnailImageWidth, storeId, portalId, culture));
            int itemMediumThumbNailHeight =
                Convert.ToInt32(ssc.GetStoreSettingsByKey(StoreSetting.ItemMediumThumbnailImageHeight, storeId, portalId, culture));
            int itemMediumThumbNailWidth =
               Convert.ToInt32(ssc.GetStoreSettingsByKey(StoreSetting.ItemMediumThumbnailImageWidth, storeId, portalId, culture));
            int itemSmallThumbNailHeight =
                Convert.ToInt32(ssc.GetStoreSettingsByKey(StoreSetting.ItemSmallThumbnailImageHeight, storeId, portalId, culture));
            int itemSmallThumbNailWidth =
                Convert.ToInt32(ssc.GetStoreSettingsByKey(StoreSetting.ItemSmallThumbnailImageWidth, storeId, portalId, culture));
            int _attributeID = 0;
            int _inputTypeID = 0;
            int _ValidationTypeID = 8;
            int _attributeSetGroupID = 0;
            bool _isIncludeInPriceRule = false;
            bool _isIncludeInPromotions = false;
            int _displayOrder = 0;

            string sku = "";
            string activeFrom = "";
            string activeTo = "";
            string hidePrice = "";
            string isHideInRSS = "";
            string isHideToAnonymous = "";

            bool toInsertIntoDB = true;
            bool isFormValid = true;
            string imageVar = string.Empty;
            int imageCounterFirst = 0;

            ItemsManagementSqlProvider obj = new ItemsManagementSqlProvider();
            var formVars = JSONHelper.Deserialise<List<AspxNameValue>>(itemObj.FormVars);
            //    for (int i = 0; i < formVars.Length; i++)
            for (int i = 0; i < formVars.Count; i++)
            {
                string attribValue = formVars[i].value;
                //string jsonResult = formVars[i].name.Replace('%', '_');
                string jsonResult = formVars[i].name.Replace('-', ' ');
                string[] jsonVar = jsonResult.Split('_');
                FormValidation formValidation = new FormValidation();

                if (jsonVar.Length > 1)
                {
                    _attributeID = Int32.Parse(jsonVar[0]);
                    _inputTypeID = Int32.Parse(jsonVar[1]);
                    _ValidationTypeID = Int32.Parse(jsonVar[2]);
                    _attributeSetGroupID = Int32.Parse(jsonVar[4]);
                    _isIncludeInPriceRule = bool.Parse(jsonVar[5]);
                    _isIncludeInPromotions = bool.Parse(jsonVar[6]);
                    _displayOrder = Int32.Parse(jsonVar[7]);

                    //Save To Database 1. [aspx_Items] 2. Others 
                    if (_attributeID == 4)
                    {
                        sku = formVars[i].value;
                    }
                    else if (_attributeID == 22)
                    {
                        activeFrom = formVars[i].value;
                    }
                    else if (_attributeID == 23)
                    {
                        activeTo = formVars[i].value;
                    }
                    else if (_attributeID == 26)
                    {
                        hidePrice = formVars[i].value;
                    }
                    else if (_attributeID == 27)
                    {
                        isHideInRSS = formVars[i].value;
                    }
                    else if (_attributeID == 28)
                    {
                        isHideToAnonymous = formVars[i].value;
                    }


                    if (itemObj.ItemId == 0 && updateFlag == false)
                    {
                        itemObj.ItemId = obj.AddItem(itemObj, aspxCommonObj, true, isModified, sku, activeFrom, activeTo,
                                                     hidePrice, isHideInRSS, isHideToAnonymous, updateFlag);
                    }
                    else if (itemObj.ItemId > 0 && i == (formVars.Count - 1))
                    {
                        itemObj.ItemId = obj.AddItem(itemObj, aspxCommonObj, true, isModified, sku, activeFrom, activeTo,
                                                     hidePrice, isHideInRSS, isHideToAnonymous, updateFlag);
                    }
                    if (itemObj.ItemId > 0)
                    {
                        if (_inputTypeID == 1)
                        {
                            if (_ValidationTypeID == 3)
                            {
                                if (formValidation.ValidateValue(formVars[i].name, _ValidationTypeID, formVars[i].value))
                                {
                                    attribValue = formVars[i].value;
                                }
                                else
                                {
                                    isFormValid = false;
                                    break;
                                }
                            }
                            else if (_ValidationTypeID == 5)
                            {
                                if (formValidation.ValidateValue(formVars[i].name, _ValidationTypeID, formVars[i].value))
                                {
                                    attribValue = formVars[i].value;
                                }
                                else
                                {
                                    isFormValid = false;
                                    break;
                                }
                            }
                            else
                            {
                                if (formValidation.ValidateValue(formVars[i].name, _ValidationTypeID, formVars[i].value))
                                {
                                    attribValue = formVars[i].value;
                                }
                                else
                                {
                                    isFormValid = false;
                                    break;
                                }
                            }
                        }
                        else if (_inputTypeID == 2)
                        {
                            if (formValidation.ValidateValue(formVars[i].name, _ValidationTypeID, formVars[i].value))
                            {
                                attribValue = formVars[i].value;
                            }
                            else
                            {
                                isFormValid = false;
                                break;
                            }
                        }
                        else if (_inputTypeID == 3)
                        {
                            if (formValidation.ValidateValue(formVars[i].name, _ValidationTypeID, formVars[i].value))
                            {
                                if (!string.IsNullOrEmpty(formVars[i].value))
                                {
                                    attribValue = formVars[i].value;
                                }
                            }
                            else
                            {
                                isFormValid = false;
                                break;
                            }
                        }
                        else if (_inputTypeID == 4)
                        {
                            if (formValidation.ValidateValue(formVars[i].name, _ValidationTypeID, formVars[i].value))
                            {
                                if (!string.IsNullOrEmpty(formVars[i].value))
                                {
                                    attribValue = formVars[i].value;
                                }
                            }
                            else
                            {
                                isFormValid = false;
                                break;
                            }
                        }
                        else if (_inputTypeID == 5 || _inputTypeID == 6 || _inputTypeID == 9 || _inputTypeID == 10 ||
                                 _inputTypeID == 11 || _inputTypeID == 12)
                        {
                            if (formValidation.ValidateValue(formVars[i].name, _ValidationTypeID, formVars[i].value))
                            {
                                attribValue = formVars[i].value;
                            }
                            else
                            {
                                isFormValid = false;
                                break;
                            }
                        }
                        else if (_inputTypeID == 7)
                        {
                            if (formValidation.ValidateValue(formVars[i].name, _ValidationTypeID, formVars[i].value))
                            {
                                attribValue = formVars[i].value;
                            }
                            else
                            {
                                isFormValid = false;
                                break;
                            }
                        }

                        else if (_inputTypeID == 8)
                        {
                            if (imageCounterFirst % 2 == 0)
                            {
                                toInsertIntoDB = false;
                                if (!string.IsNullOrEmpty(formVars[i].value) &&
                                    formVars[i].value.ToLower() != "undefined")
                                {
                                    if (formValidation.ValidateValue(formVars[i].name, _ValidationTypeID,
                                                                     formVars[i].value))
                                    {
                                        imageVar = attribValue;
                                    }
                                    else
                                    {
                                        isFormValid = false;
                                        break;
                                    }
                                }
                                else
                                {
                                    imageVar = "";
                                }
                            }
                            else
                            {
                                toInsertIntoDB = true;

                                if (attribValue != imageVar)
                                {
                                    //_imageVar = _imageVar.Replace("../", "");
                                    imageVar = imageVar.Replace("/", "\\");
                                    //attribValue = attribValue.Replace("../", "");
                                    attribValue = attribValue.Replace("/", "\\");

                                    string tempFolder = @"Upload\temp";
                                    FileHelperController fileObj = new FileHelperController();
                                    attribValue = fileObj.MoveFileToModuleFolder(tempFolder, attribValue, imageVar,
                                                                                 itemLargeThumbNailHeight,
                                                                                  itemLargeThumbNailWidth,
                                                                                 itemMediumThumbNailHeight,
                                                                                  itemMediumThumbNailWidth,
                                                                                 itemSmallThumbNailHeight,
                                                                                  itemSmallThumbNailWidth,
                                                                                 @"Modules\AspxCommerce\AspxItemsManagement\uploads\",
                                                                                 itemObj.ItemId, "item_");
                                    
                                }
                            }
                            imageCounterFirst++;
                        }
                    }
                    if (isFormValid && toInsertIntoDB)
                    {
                        obj.SaveUpdateItemAttributes(itemObj.ItemId, itemObj.AttributeSetId, aspxCommonObj, true,
                                                     isModified,
                                                     attribValue,
                                                     _attributeID, _inputTypeID, _ValidationTypeID, _attributeSetGroupID,
                                                     _isIncludeInPriceRule, _isIncludeInPromotions,
                                                     _displayOrder);

                    }
                }
            }
            if (itemObj.ItemTypeId == 1 || itemObj.ItemTypeId == 2)
            {
                obj.InsertBrandMapping(itemObj.ItemId, itemObj.BrandId, storeId, portalId, userName, culture);
            }
            if (itemObj.ItemVideoIDs != "" || updateFlag == true)
                obj.InsertAndUpdateItemVideos(itemObj.ItemId, itemObj.ItemVideoIDs, aspxCommonObj);
            return itemObj.ItemId;
        }
    }
}
