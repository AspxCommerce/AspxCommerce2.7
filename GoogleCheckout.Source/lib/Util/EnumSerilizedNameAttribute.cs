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
using System.Reflection;

namespace GCheckout.Util {
  
  /// <summary>
  /// Class used to set the string that will be passed to the classes sent
  /// to Google Checkout
  /// </summary>
  /// <remarks>
  /// This allows us to Create an Enum that is a little easier on the eyes
  /// and we can still send whatever value is needed to the serialized classes.
  /// </remarks>
  public class EnumSerilizedNameAttribute : Attribute {

    private string _Value = string.Empty;

    /// <summary>
    /// The Value to serialize to the Google Checkout Classes
    /// </summary>
    public string Value {
      get {
        return _Value;
      }
      set {
        _Value = value;
      }
    }

    /// <summary>
    /// Create a new EnumSerilizedNameAttribute
    /// </summary>
    public EnumSerilizedNameAttribute() {

    }

    /// <summary>
    /// Create a new EnumSerilizedNameAttribute
    /// </summary>
    /// <param name="InValue">The Name to serialize
    /// to the Google Checkout classes</param>
    public EnumSerilizedNameAttribute(string InValue) {
      Value = InValue;
    }

    /// <summary>
    /// Helper method to get the value of an instance of the type on
    /// a MemberInfo (Property or Field)
    /// </summary>
    /// <param name="mi">The MethodInfo to obtain the value</param>
    /// <returns></returns>
    public static string GetValue(MemberInfo mi) {
      EnumSerilizedNameAttribute[] att
        = mi.GetCustomAttributes(typeof(EnumSerilizedNameAttribute),
        false) as EnumSerilizedNameAttribute[];
      if (att != null && att.Length > 0)
        return att[0].Value;
      return string.Empty;
    }
  }
}
