/*************************************************
 * Copyright (C) 2007 Google Inc.
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
using System;
using GCheckout.Util;

namespace GCheckout.Checkout {

  /// <summary>
  /// The &lt;delivery-address-category&gt; tag indicates whether the shipping 
  /// method should be applied to a residential or a commercial address. Valid
  /// values for this tag are RESIDENTIAL and COMMERCIAL. Please note that 
  /// these names are case-sensitive.
  /// </summary>
  public enum DeliveryAddressCategory {
    
    /// <summary>UNKNOWN</summary>
    UNKNOWN = 0,
    /// <summary>RESIDENTIAL</summary>
    RESIDENTIAL = 1,
    /// <summary>COMMERCIAL</summary>
    COMMERCIAL = 2    
  }
}
