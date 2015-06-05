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
  /// Google Checkout button that will display on your web page.
  /// </summary>
  public class GCheckoutButton : GCheckoutButtonBase {

    private ButtonSize _Size = ButtonSize.Medium;

    /// <summary>
    /// The name of the gif file for the image
    /// </summary>
    protected override string GifFileName {
      get {
        return "checkout";
      }
    }

    /// <summary>
    /// The <b>Size</b> property value determines the size of the 
    /// Google Checkout button that will display on your web page.
    /// Valid values for this property are "Small", "Medium" and 
    /// "Large". A small button is 160 pixels wide and 43 pixels high.
    /// A medium button is 168 pixels wide and 44 pixels high. A large
    /// button is 180 pixels wide and 46 pixels high.
    /// </summary>
    [Category("Google")]
    [Description("Small: 160 by 43 pixels\nMedium: 168 by 44 pixels\n" +
       "Large: 180 by 46 pixels")]
    public ButtonSize Size {
      get {
        return _Size;
      }
      set {
        _Size = value;
        SetImageUrl();
      }
    }

    /// <summary>
    /// This method creates the URL for the Google Checkout button image. 
    /// This method uses the value of the "Size" property to set the width 
    /// and height of the image. It also uses the value of the "Background"
    /// property to set a style that dictates the background color for the 
    /// image. Finally, the method uses the value of the "Environment
    /// </summary>
    protected override void SetImageUrl() {
      int width = 0;
      int height = 0;
      switch (Size) {
        case ButtonSize.Small:
          width = 160;
          height = 43;
          break;
        case ButtonSize.Medium:
          width = 168;
          height = 44;
          break;
        case ButtonSize.Large:
          width = 180;
          height = 46;
          break;
      }
      this.Width = width;
      this.Height = height;
      base.SetImageUrl();
    }
  }

  /// <summary>
  /// This enumeration defines valid sizes for the Google Checkout button.
  /// Valid values for the "Size" property are "Small", "Medium" and "Large".
  /// </summary>
  public enum ButtonSize {
    /// <summary>160 x 43 pixels</summary>
    Small = 0,
    /// <summary>168 by 44 pixels</summary>
    Medium = 1,
    /// <summary>180 x 46 pixels</summary>
    Large = 2
  }

  /// <summary>
  /// This enumeration defines valid background colors for the Google Checkout
  /// button. Valid values for the "Background" property are "White" and 
  /// "Transparent".
  /// </summary>
  public enum BackgroundType {
    /// <summary>You are placing the button on a white background</summary>
    White = 0,
    /// <summary>You are placing the button on a colored background</summary>
    Transparent = 1
  }

}
