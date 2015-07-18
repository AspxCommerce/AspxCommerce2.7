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
  /// The &lt;carrier-pickup&gt; tag specifies how the package will be
  /// transferred from the merchant to the shipper. Valid values for this
  /// tag are REGULAR_PICKUP, SPECIAL_PICKUP and DROP_OFF. The default value
  /// for this tag is DROP_OFF. Please note that these names are 
  /// case-sensitive.
  /// </summary>
  public enum CarrierPickup {

    /// <summary>DROP OFF (Default Value)</summary>
    DROP_OFF = 0,
    /// <summary>REGULAR PICKUP</summary>
    REGULAR_PICKUP = 1,
    /// <summary>SPECIAL PICKUP</summary>
    SPECIAL_PICKUP
  }
}
