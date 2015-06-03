using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspxCommerce.Core
{
  public  class AspxCategoryListController
    {
      public AspxCategoryListController()
      {
      }

      public static List<CategoryInfo> GetCategoryMenuList(AspxCommonInfo aspxCommonObj)
      {
          List<CategoryInfo> catInfo = AspxCategoryListProvider.GetCategoryMenuList(aspxCommonObj);
          return catInfo;
      }

    }
}
