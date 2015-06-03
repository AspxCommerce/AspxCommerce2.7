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



using System.Collections.Generic;
using SageFrame.Web.Utilities;

namespace AspxCommerce.Core
{
   public class UserDashboardSQLProvider
    {
       public void AddUpdateUserAddress(AddressInfo addressObj,AspxCommonInfo aspxCommonObj)
       {
           List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
           parameter.Add(new KeyValuePair<string, object>("@AddressID", addressObj.AddressID));
           parameter.Add(new KeyValuePair<string, object>("@CustomerID", aspxCommonObj.CustomerID));
           parameter.Add(new KeyValuePair<string, object>("@FirstName", addressObj.FirstName));
           parameter.Add(new KeyValuePair<string, object>("@LastName", addressObj.LastName));
           parameter.Add(new KeyValuePair<string, object>("@Email", addressObj.Email));
           parameter.Add(new KeyValuePair<string, object>("@Company", addressObj.Company));
           parameter.Add(new KeyValuePair<string, object>("@Address1", addressObj.Address1));
           parameter.Add(new KeyValuePair<string,object>("@Address2",addressObj.Address2));
           parameter.Add(new KeyValuePair<string, object>("@City", addressObj.City));
           parameter.Add(new KeyValuePair<string, object>("@State", addressObj.State));
           parameter.Add(new KeyValuePair<string, object>("@Zip", addressObj.Zip));
           parameter.Add(new KeyValuePair<string, object>("@Phone", addressObj.Phone));
           parameter.Add(new KeyValuePair<string, object>("@Mobile", addressObj.Mobile));
           parameter.Add(new KeyValuePair<string, object>("@Fax", addressObj.Fax));
           parameter.Add(new KeyValuePair<string, object>("@WebSite", addressObj.Website));
           parameter.Add(new KeyValuePair<string, object>("@Country", addressObj.Country));
           parameter.Add(new KeyValuePair<string, object>("@IsDefaultShipping", addressObj.DefaultShipping));
           parameter.Add(new KeyValuePair<string, object>("@IsDefaultBilling", addressObj.DefaultBilling));
           parameter.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
           parameter.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
           parameter.Add(new KeyValuePair<string, object>("@UserName", aspxCommonObj.UserName));
           parameter.Add(new KeyValuePair<string, object>("@CultureName", aspxCommonObj.CultureName));
           SQLHandler sqlH = new SQLHandler();
           sqlH.ExecuteNonQuery("usp_Aspx_AddUpdateUserAddress", parameter);
       }
       public List<AddressInfo> GetUserAddressDetails(AspxCommonInfo aspxCommonObj)
       {
           List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
           parameter.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
           parameter.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
           parameter.Add(new KeyValuePair<string, object>("@CustomerID", aspxCommonObj.CustomerID));
           parameter.Add(new KeyValuePair<string, object>("@UserName", aspxCommonObj.UserName));
           parameter.Add(new KeyValuePair<string, object>("@CultureName", aspxCommonObj.CultureName));
           SQLHandler sqlh = new SQLHandler();
           return sqlh.ExecuteAsList<AddressInfo>("usp_Aspx_GetUserAddressBookDetails", parameter);
       }
       public void DeleteAddressBookDetails(int addressID,AspxCommonInfo aspxCommonObj)
       {
           List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
           parameter.Add(new KeyValuePair<string, object>("@AddressID", addressID));
           parameter.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
           parameter.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
           parameter.Add(new KeyValuePair<string, object>("@UserName", aspxCommonObj.UserName));
           parameter.Add(new KeyValuePair<string, object>("@CultureName", aspxCommonObj.CultureName));
           SQLHandler sqlH = new SQLHandler();
           sqlH.ExecuteNonQuery("usp_Aspx_DeleteAddressBook", parameter);
       }
       public List<UserProductReviewInfo> GetUserProductReviews(AspxCommonInfo aspxCommonObj)
       {
           List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
           parameter.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
           parameter.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
           parameter.Add(new KeyValuePair<string, object>("@UserName", aspxCommonObj.UserName));
           parameter.Add(new KeyValuePair<string, object>("@CultureName", aspxCommonObj.CultureName));
           SQLHandler sqlH = new SQLHandler();
          return  sqlH.ExecuteAsList<UserProductReviewInfo>("usp_Aspx_GetUserProductReviews", parameter);
       }
       public void UpdateUserProductReview(ItemReviewBasicInfo productReviewObj,AspxCommonInfo aspxCommonObj)
       {
           List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
           parameter.Add(new KeyValuePair<string, object>("@ItemID", productReviewObj.ItemID));
           parameter.Add(new KeyValuePair<string, object>("@ItemReviewID", productReviewObj.ItemReviewID));
           parameter.Add(new KeyValuePair<string, object>("@RatingIDs", productReviewObj.RatingIDs));
           parameter.Add(new KeyValuePair<string, object>("@RatingValues", productReviewObj.RatingValues));
           parameter.Add(new KeyValuePair<string, object>("@ReviewSummary", productReviewObj.ReviewSummary));
           parameter.Add(new KeyValuePair<string, object>("@Review", productReviewObj.Review));
           parameter.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
           parameter.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
           parameter.Add(new KeyValuePair<string, object>("@UserName", aspxCommonObj.UserName));
           SQLHandler sqlH = new SQLHandler();
           sqlH.ExecuteNonQuery("usp_Aspx_GetUserProductReviewUpdate", parameter);
           
       }
       public void DeleteUserProductReview(int itemID, int itemReviewID, AspxCommonInfo aspxCommonObj)
       {
           List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
           parameter.Add(new KeyValuePair<string, object>("@ItemID", itemID));
           parameter.Add(new KeyValuePair<string, object>("@ItemReviewID", itemReviewID));
           parameter.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
           parameter.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
           parameter.Add(new KeyValuePair<string, object>("@UserName", aspxCommonObj.UserName));
           SQLHandler sqlH = new SQLHandler();
           sqlH.ExecuteNonQuery("usp_Aspx_DeleteUserProductReview", parameter);
       }
    }
}
