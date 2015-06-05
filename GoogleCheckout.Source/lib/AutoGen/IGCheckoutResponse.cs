/*************************************************
 * Copyright (C) 2008 Google Inc.
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
 *  9-7-2008   Joe Feser joe.feser@joefeser.com
 *  Initial Release.
 * 
*/
using System;
using System.Collections.Generic;
using System.Text;

namespace GCheckout.AutoGen {
  
  /// <summary>
  /// Interface used to extend the xsd to allow us to obtain 
  /// basic stuff like obtain the serial number.
  /// </summary>
  public interface IGCheckoutResponse {

    /// <summary>
    /// The Message Serial Number.
    /// </summary>
    string MessageSerialNumber {
      get;
    }
  }
}
