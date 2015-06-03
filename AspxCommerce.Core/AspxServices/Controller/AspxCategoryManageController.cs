using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspxCommerce.Core
{
   public class AspxCategoryManageController
    {
       public AspxCategoryManageController()
       {
       }

       
       public static bool CheckUniqueCategoryName(string catName, int catId, AspxCommonInfo aspxCommonObj)
       {
           try
           {

               bool isUnique= AspxCategoryManageProvider.CheckUniqueName(catName, catId, aspxCommonObj);
               return isUnique;
           }
           catch (Exception e)
           {
               throw e;
           }
       }


       public static bool IsUnique(Int32 storeID, Int32 portalID, Int32 itemID, Int32 attributeID, Int32 attributeType, string attributeValue)
       {
           try
           {
               /*
           1	TextField
           2	TextArea
           3	Date
           4	Boolean
           5	MultipleSelect
           6	DropDown
           7	Price
           8	File
           9	Radio
           10	RadioButtonList
           11	CheckBox
           12	CheckBoxList
            */
               bool isUnique = false;
               switch (attributeType)
               {
                   case 1:
                       isUnique = AspxCommonProvider.CheckUniquenessNvarchar(1, storeID, portalID, attributeID, attributeValue);
                       break;
                   case 2:
                       isUnique = AspxCommonProvider.CheckUniquenessText(1, storeID, portalID, attributeID, attributeValue);
                       break;
                   case 3:
                       isUnique = AspxCommonProvider.CheckUniquenessDate(1, storeID, portalID, attributeID, DateTime.Parse(attributeValue));
                       break;
                   case 4:
                       isUnique = AspxCommonProvider.CheckUniquenessBoolean(1, storeID, portalID, attributeID, bool.Parse(attributeValue));
                       break;
                   case 5:
                       isUnique = AspxCommonProvider.CheckUniquenessInt(1, storeID, portalID, attributeID, Int32.Parse(attributeValue));
                       break;
                   case 6:
                       isUnique = AspxCommonProvider.CheckUniquenessInt(1, storeID, portalID, attributeID, Int32.Parse(attributeValue));
                       break;
                   case 7:
                       isUnique = AspxCommonProvider.CheckUniquenessDecimal(1, storeID, portalID, attributeID, decimal.Parse(attributeValue));
                       break;
                   case 8:
                       isUnique = AspxCommonProvider.CheckUniquenessFile(1, storeID, portalID, attributeID, attributeValue);
                       break;
                   case 9:
                       isUnique = AspxCommonProvider.CheckUniquenessInt(1, storeID, portalID, attributeID, Int32.Parse(attributeValue));
                       break;
                   case 10:
                       isUnique = AspxCommonProvider.CheckUniquenessInt(1, storeID, portalID, attributeID, Int32.Parse(attributeValue));
                       break;
                   case 11:
                       isUnique = AspxCommonProvider.CheckUniquenessInt(1, storeID, portalID, attributeID, Int32.Parse(attributeValue));
                       break;
                   case 12:
                       isUnique = AspxCommonProvider.CheckUniquenessInt(1, storeID, portalID, attributeID, Int32.Parse(attributeValue));
                       break;
               }
               return isUnique;
           }
           catch (Exception)
           {
               return false;
           }
       }


       public static List<AttributeFormInfo> GetCategoryFormAttributes(Int32 categoryID, AspxCommonInfo aspxCommonObj)
       {

           List<AttributeFormInfo> frmFieldList = AspxCategoryManageProvider.GetCategoryFormAttributes(categoryID, aspxCommonObj);
           return frmFieldList;

       }

       
       public static List<CategoryInfo> GetCategoryAll(bool isActive, AspxCommonInfo aspxCommonObj)
       {
               List<CategoryInfo> catList = AspxCategoryManageProvider.GetCategoryAll(isActive, aspxCommonObj);
               return catList;
       }

       
       public static List<CategoryAttributeInfo> GetCategoryByCategoryID(Int32 categoryID, AspxCommonInfo aspxCommonObj)
       {
           List<CategoryAttributeInfo> catList = AspxCategoryManageProvider.GetCategoryByCategoryID(categoryID, aspxCommonObj);
           return catList;
       }


       public static int SaveCategory(CategoryInfo.CategorySaveBasicInfo categoryObj, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               int catID;
               CategoryInfo categoryInfo = new CategoryInfo();
               FormValidation formValidation = new FormValidation();
               List<CategoryAttributeInfo> listCategoryAttributes = new List<CategoryAttributeInfo>();
               categoryInfo.CategoryID = categoryObj.CategoryId; //categoryID;
               categoryInfo.ParentID = categoryObj.ParentId; //parentID;
               categoryInfo.IsShowInCatalog = true;
               categoryInfo.IsShowInMenu = true;
               categoryInfo.IsShowInSearch = true;
               categoryInfo.PortalID = aspxCommonObj.PortalID;
               categoryInfo.StoreID = aspxCommonObj.StoreID;
               categoryInfo.ActiveFrom = new DateTime(1970, 1, 1);
               categoryInfo.ActiveTo = new DateTime(2999, 12, 30);
               categoryInfo.SelectedItems = categoryObj.SelectedItems;
               bool toInsertIntoDB = true;
               bool isFormValid = true;
               //int _imageCounter = 0;
               string imageVar = string.Empty;
               int imageCounterFirst = 0;

               var formVarss = JSONHelper.Deserialise<List<AspxNameValue>>(categoryObj.FormVars);


               foreach (var formVars in formVarss)
               {

                   int inputTypeID;
                   int validationTypeID;
                   string attribName = formVars.name;
                   string attribValue = formVars.value;
                   string jsonResult = formVars.name.Replace('-', ' ');
                   string[] jsonVar = jsonResult.Split('_');

                   CategoryAttributeInfo categoryAttribute = new CategoryAttributeInfo();
                   categoryAttribute.AttributeID = int.Parse(jsonVar[0]);
                   inputTypeID = int.Parse(jsonVar[1]);
                   validationTypeID = int.Parse(jsonVar[2]);

                   categoryAttribute.InputTypeID = inputTypeID;
                   categoryAttribute.ValidationTypeID = validationTypeID;
                   int _attributeID = categoryAttribute.AttributeID;
                   if (_attributeID == 36)
                   {
                       categoryInfo.IsService = int.Parse(formVars.value) != 9;
                   }
                   else if (_attributeID == 1)
                   {
                       categoryInfo.CategoryName = formVars.value;
                   }
                   else if (_attributeID == 9)
                   {
                       categoryInfo.MetaTitle = formVars.value;
                   }
                   else if (_attributeID == 2)
                   {
                       categoryInfo.Description = formVars.value;
                   }
                   else if (_attributeID == 3)
                   {
                       categoryInfo.ShortDescription = formVars.value;
                   }
                   else if (_attributeID == 10)
                   {
                       categoryInfo.MetaKeyword = formVars.value;
                   }
                   else if (_attributeID == 11)
                   {
                       categoryInfo.MetaDescription = formVars.value;
                   }
                   else if (_attributeID == 16)
                   {
                       categoryInfo.IsShowInMenu = bool.Parse(formVars.value);
                   }
                   else if (_attributeID == 17)
                   {
                       categoryInfo.IsShowInSearch = bool.Parse(formVars.value);
                   }
                   else if (_attributeID == 18)
                   {
                       categoryInfo.IsShowInCatalog = bool.Parse(formVars.value);
                   }
                   else if (_attributeID == 19)
                   {
                       categoryInfo.ActiveFrom = DateTime.Parse(formVars.value);
                   }
                   else if (_attributeID == 20)
                   {
                       categoryInfo.ActiveTo = DateTime.Parse(formVars.value);
                   }
                   else if (_attributeID == 12 || _attributeID == 21 || _attributeID == 22)
                   {
                       string _imagePath = string.Empty;
                       if (imageCounterFirst%2 == 0)
                       {
                           toInsertIntoDB = false;
                           if (!string.IsNullOrEmpty(attribValue) && attribValue.ToLower() != "undefined")
                           {
                               if (formValidation.ValidateValue(attribName, validationTypeID, attribValue))
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
                           if (imageVar != "")
                           {
                               string[] strArray = imageVar.Split('/');
                               if (strArray[1] == "temp")
                               {
                                   if (attribValue != imageVar)
                                   {
                                       imageVar = imageVar.Replace("/", "\\");
                                       attribValue = attribValue.Replace("/", "\\");

                                       string tempFolder = @"Upload\temp";
                                       FileHelperController fileObj = new FileHelperController();
                                       attribValue = fileObj.MoveFileToModuleFoldr(tempFolder, attribValue, imageVar,
                                                                                    categoryObj.LargeThumbNailImageHeight,
                                                                                     categoryObj.LargeThumbNailImageWidth,
                                                                                    categoryObj.MediumImageHeight,
                                                                                    categoryObj.MediumImageWidth,
                                                                                    categoryObj.SmallImageHeight,
                                                                                    categoryObj.SmallImageWidth,
                                                                                    @"Modules\AspxCommerce\AspxCategoryManagement\uploads\",
                                                                                    categoryObj.CategoryId, "cat_");
                                       attribValue =
                                           attribValue.Replace("Modules/AspxCommerce/AspxCategoryManagement/uploads/Small/", "");
                                       _imagePath = attribValue;
                                   }
                                   //else if (_imageVar == "")
                                   //{
                                   //    categoryAttribute.FileValue = _imageVar;
                                   //}
                                   else
                                   {
                                       _imagePath = attribValue;
                                   }
                               }
                               else
                               {
                                   _imagePath = strArray[strArray.Length - 1];
                               }
                           }
                           else
                           {
                               _imagePath = attribValue;
                           }
                       }
                       imageCounterFirst++;
                       if (toInsertIntoDB)
                       {
                           if (_attributeID == 12)
                           {
                               categoryInfo.CategoryBaseImage = _imagePath;
                           }
                           else if (_attributeID == 21)
                           {
                               categoryInfo.CategoryThumbnailImage = _imagePath;
                           }
                           else if (_attributeID == 22)
                           {
                               categoryInfo.CategorySmallImage = _imagePath;
                           }
                           //listCategoryAttributes.Add(categoryAttribute);
                       }
                   }
                   else if (categoryAttribute.AttributeID > 43)
                   {
                       if (categoryInfo.HasSystemAttribute)
                       {
                           categoryInfo.HasSystemAttribute = false;
                       }
                       if (inputTypeID == 1)
                       {
                           if (validationTypeID == 3)
                           {
                               if (formValidation.ValidateValue(attribName, validationTypeID, attribValue))
                               {
                                   categoryAttribute.DecimalValue = decimal.Parse(attribValue);
                               }
                               else
                               {
                                   isFormValid = false;
                                   break;
                               }
                           }
                           else if (validationTypeID == 5)
                           {
                               if (formValidation.ValidateValue(attribName, validationTypeID, attribValue))
                               {
                                   categoryAttribute.IntValue = Int32.Parse(attribValue);
                               }
                               else
                               {
                                   isFormValid = false;
                                   break;
                               }
                           }
                           else
                           {
                               if (formValidation.ValidateValue(attribName, validationTypeID, attribValue))
                               {
                                   categoryAttribute.NvarcharValue = attribValue;
                               }
                               else
                               {
                                   isFormValid = false;
                                   break;
                               }
                           }
                       }
                       else if (inputTypeID == 2)
                       {
                           if (formValidation.ValidateValue(attribName, validationTypeID, attribValue))
                           {
                               categoryAttribute.TextValue = attribValue;
                           }
                           else
                           {
                               isFormValid = false;
                               break;
                           }
                       }
                       else if (inputTypeID == 3)
                       {
                           if (formValidation.ValidateValue(attribName, validationTypeID, attribValue))
                           {
                               if (!string.IsNullOrEmpty(attribValue))
                               {
                                   categoryAttribute.DateValue = DateTime.Parse(attribValue);
                               }
                           }
                           else
                           {
                               isFormValid = false;
                               break;
                           }
                       }
                       else if (inputTypeID == 4)
                       {
                           if (formValidation.ValidateValue(attribName, validationTypeID, attribValue))
                           {
                               if (!string.IsNullOrEmpty(attribValue))
                               {
                                   categoryAttribute.BooleanValue = (attribValue == "1" ||
                                                                     attribValue.ToLower() == "true")
                                                                        ? true
                                                                        : false;
                               }
                               else
                               {
                                   categoryAttribute.BooleanValue = false;
                               }
                           }
                           else
                           {
                               isFormValid = false;
                               break;
                           }
                       }
                       else if (inputTypeID == 5 || inputTypeID == 6 || inputTypeID == 9 || inputTypeID == 10 ||
                                inputTypeID == 11 || inputTypeID == 12)
                       {
                           if (formValidation.ValidateValue(attribName, validationTypeID, attribValue))
                           {
                               categoryAttribute.OptionValues = attribValue;
                           }
                           else
                           {
                               isFormValid = false;
                               break;
                           }
                       }
                       else if (inputTypeID == 7)
                       {
                           if (formValidation.ValidateValue(attribName, validationTypeID, attribValue))
                           {
                               categoryAttribute.DecimalValue = decimal.Parse(attribValue);
                           }
                           else
                           {
                               isFormValid = false;
                               break;
                           }
                       }
                       else if (inputTypeID == 8)
                       {
                           if (imageCounterFirst%2 == 0)
                           {
                               toInsertIntoDB = false;
                               if (!string.IsNullOrEmpty(attribValue) && attribValue.ToLower() != "undefined")
                               {
                                   if (formValidation.ValidateValue(attribName, validationTypeID, attribValue))
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
                                   imageVar = imageVar.Replace("/", "\\");
                                   attribValue = attribValue.Replace("/", "\\");

                                   string tempFolder = @"Upload\temp";
                                   FileHelperController fileObj = new FileHelperController();
                                   attribValue = fileObj.MoveFileToModuleFolder(tempFolder, attribValue, imageVar,
                                                                                categoryObj.LargeThumbNailImageHeight,
                                                                                 categoryObj.LargeThumbNailImageWidth,
                                                                                categoryObj.MediumImageHeight,
                                                                                categoryObj.MediumImageWidth,
                                                                                categoryObj.SmallImageHeight,
                                                                                categoryObj.SmallImageWidth,
                                                                                @"Modules\AspxCommerce\AspxCategoryManagement\uploads\",
                                                                                categoryObj.CategoryId, "cat_");
                                   categoryAttribute.FileValue = attribValue;
                               }
                                   //else if (_imageVar == "")
                                   //{
                                   //    categoryAttribute.FileValue = _imageVar;
                                   //}
                               else
                               {
                                   categoryAttribute.FileValue = attribValue;
                               }
                           }
                           imageCounterFirst++;
                       }
                       if (toInsertIntoDB)
                       {
                           listCategoryAttributes.Add(categoryAttribute);
                       }
                   }
               }
               if (isFormValid)
               {
                   string attributeIDs = "1,2,3,9,10,11,12,16,17,18,19,20,21,22,36";
                   catID = AspxCategoryManageProvider.CategoryAddUpdate(categoryInfo,
                                                                        listCategoryAttributes, aspxCommonObj.UserName,
                                                                        aspxCommonObj.CultureName, attributeIDs);

               }
               else
               {
                   throw new Exception("Form is not valid one");
               }
               return catID;
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       public static void DeleteCategory(Int32 storeID, Int32 portalID, Int32 categoryID, string userName, string culture)
       {
           try
           {
               AspxCategoryManageProvider.DeleteCategory(storeID, portalID, categoryID, userName, culture);
           }
           catch (Exception e)
           {
               throw e;
           }
       }


       public static List<CategoryItemInfo> GetCategoryItems(Int32 offset, System.Nullable<int> limit, GetCategoryItemInfo categoryItemsInfo, AspxCommonInfo aspxCommonObj, bool serviceBit)
       {

           List<CategoryItemInfo> listCategoryItem = AspxCategoryManageProvider.GetCategoryItems(offset, limit,categoryItemsInfo,aspxCommonObj,serviceBit);
           return listCategoryItem;
       }
       public static string GetCategoryCheckedItems(int CategoryID, AspxCommonInfo aspxCommonObj)
       {

           string categoryItem = AspxCategoryManageProvider.GetCategoryCheckedItems(CategoryID, aspxCommonObj);
           return categoryItem;
       }

       
       public static bool SaveChangesCategoryTree(string categoryIDs, AspxCommonInfo aspxCommonObj)
       {
           bool isSave = AspxCategoryManageProvider.SaveChangesCategoryTree(categoryIDs, aspxCommonObj);
           return isSave;
       }

       public static void ActivateCategory(int categoryID, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               AspxCategoryManageProvider.ActivateCategory(categoryID, aspxCommonObj);

           }
           catch (Exception e)
           {
               throw e;
           }
       }
       public static void DeActivateCategory(int categoryID, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               AspxCategoryManageProvider.DeActivateCategory(categoryID, aspxCommonObj);

           }
           catch (Exception e)
           {
               throw e;
           }
       }


    }
}
