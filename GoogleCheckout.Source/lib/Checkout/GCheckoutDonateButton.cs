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

using System;
using System.Web.UI;
using System.ComponentModel;
using System.Configuration;
using GCheckout.Util;
using ch = GCheckout.Util.GCheckoutConfigurationHelper;

namespace GCheckout.Checkout {

  /// <summary>
  /// Google Checkout "Donate" button that will display on your web page.
  /// </summary>
  public class GCheckoutDonateButton : GCheckoutButtonBase {

    /// <summary>
    /// True if this is a donation to a non-profit, false if it is a regular 
    /// purchase. The Checkout pages have slightly different wording for 
    /// donations.
    /// </summary>
    protected override bool IsDonation {
      get {
        return true;
      }
    }

    /// <summary>
    /// The name of the gif file for the image
    /// </summary>
    protected override string GifFileName {
      get {
        return "donateNow";
      }
    }

    /// <summary>
    /// Gets or sets the height of the Web server control.
    /// </summary>
    public override System.Web.UI.WebControls.Unit Height {
      get {
        return 50;
      }
      set {
        //do not allow the person to change the size
      }
    }

    /// <summary>
    /// Gets or sets the width of the Web server control.
    /// </summary>
    public override System.Web.UI.WebControls.Unit Width {
      get {
        return 115;
      }
      set {
        //do not allow the person to change the size
      }
    }

    /// <summary>
    /// This method sets the URL for the Google Checkout "Donate" button image.
    /// </summary>
    protected override void SetImageUrl() {
      this.Width = 115;
      this.Height = 50;
      base.SetImageUrl();
    }

  }

}
