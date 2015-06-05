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

/*

Built by joe feser Feb 22, 2007 joseph.feser@gmail.com
Please send comments to email address
*/

using System;

namespace GCheckout.Checkout
{
	
  /// <summary>
  /// The &lt;mode&gt; tag allows you to specify the methodology 
  /// that will be used to round values to two decimal places. 
  /// This tag may contain any of the following values, each of which is 
  /// defined in the Java 1.5 specification for the RoundingMode enumeration
  /// </summary>
  public enum RoundingMode {
        
    /// <summary>This mode rounds numbers away from zero. 
    /// For example, the number 1.111 would be rounded to 1.12.</summary>
    UP,

    /// <summary>This mode rounds numbers toward zero.
    /// For example, the number 1.666 would be rounded to 1.66.</summary>
    DOWN,
        
    /// <summary>This mode rounds numbers toward positive infinity. 
    /// For positive numbers, the CEILING mode functions identically to the 
    /// UP rounding mode. For negative numbers, the CEILING mode functions 
    /// identically to the DOWN rounding mode.</summary>
    CEILING,
        
    /// <summary>
    /// TODO Not defined in pdf
    ///</summary>
    FLOOR,
        
    /// <summary>This mode rounds numbers to the nearest digit.
    /// However, if the number being rounded is followed by a five and no
    /// additional nonzero digits, then then that number will be rounded up.
    /// For example, the number 1.165 would be rounded to 1.17.</summary>
    HALF_UP,
        
    /// <summary>This mode rounds numbers to the nearest digit. 
    /// However, if the number being rounded is followed by a five and no
    /// additional nonzero digits, then then that number will be rounded down.
    /// For example, the number 1.165 would be rounded to 1.16.</summary>
    HALF_DOWN,
        
    /// <summary>HALF_EVEN - This mode, which is also known as banker's rounding,
    /// is described earlier in this section.
    /// Google Checkout uses HALF_EVEN as its default rounding mode.</summary>
    HALF_EVEN,
        
    /// <summary>
    /// TODO Not defined in pdf
    /// </summary>
    UNNECESSARY,
  }
    
  /// <summary>The &lt;rule&gt; tag specifies when rounding rules should be 
  /// applied to monetary values while Google Checkout is computing an order
  /// total. This tag may contain either of the following values:</summary>
  public enum RoundingRule {
        
    /// <summary>This rounding rule indicates that Google should calculate 
    /// the tax for each line item and round each of those tax values using 
    /// the specified rounding mode. After each tax value has been rounded, 
    /// Google should then add the tax values to determine the total tax.
    /// </summary>
    ///<remarks>Note: A line item is comprised of one or more units of the same
    /// item. For example, if a shopping cart contains two identical items that
    ///  each cost $1.00 and the sales tax rate is 7.5 percent, then the 
    ///  total cost of the line item is $2.00 and the total tax for the line 
    ///  item is 15 cents.</remarks>
    PER_LINE,
        
    /// <summary>This rounding rule indicates that Google should 
    /// calculate the tax for each item and add those tax values to calculate
    /// the total unrounded tax amount for the order. Google should then
    /// apply the specified rounding mode to the total tax to determine the 
    /// total tax for the order.
    ///</summary>
    TOTAL,
  }
}
