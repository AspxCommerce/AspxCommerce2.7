/*************************************************
 * Copyright (C) 2006-2012 Google Inc.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
*************************************************/
/*
 Edit History:
 *  9/2009   Joe Feser joe.feser@joefeser.com
 *  Initial Release.
 *  4/21/2011 Joe Feser joe.feser@joefeser.com
 *  Marked Obsolete, there is no need for this class based on the rest of the design.
*/
using System;
using System.Collections.Generic;
using System.Text;

namespace GCheckout.Checkout {

  /// <summary>
  /// An item that is a subscription item, containing subscription information.
  /// </summary>
  [Obsolete("ShoppingCartSubscriptionItem is not required, Please use ShoppingCartItem.")]
  public class ShoppingCartSubscriptionItem : ShoppingCartItem, IShoppingCartItem {


  }

}
