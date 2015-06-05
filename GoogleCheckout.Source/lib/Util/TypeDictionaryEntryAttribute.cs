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
  /// Provides a name/value dictionary Attribute for items
  /// </summary>
  [AttributeUsageAttribute(AttributeTargets.All, AllowMultiple=true)]
  public class TypeDictionaryEntryAttribute : Attribute {

    private string _name = string.Empty;
    private string _value = string.Empty;

    /// <summary>
    /// The Name to serialize to the Google Checkout Classes
    /// </summary>
    public string Name {
      get {
        return _name;
      }
      set {
        if (value != null)
          value = value.ToLower();
        _name = value;
      }
    }

    /// <summary>
    /// The Value to serialize to the Google Checkout Classes
    /// </summary>
    public string Value {
      get {
        return _value;
      }
      set {
        _value = value;
      }
    }

    /// <summary>
    /// Create a new TypeDictionaryEntryAttribute
    /// </summary>
    public TypeDictionaryEntryAttribute() {

    }

    /// <summary>
    /// Create a new TypeDictionaryEntryAttribute
    /// </summary>
    /// <param name="name">The Name to serialize</param>
    /// <param name="value">The Value to serialize
    /// to the Google Checkout classes</param>
    public TypeDictionaryEntryAttribute(string name, string value) {
      Name = name;
      Value = value;
    }

    /// <summary>
    /// Helper method to get the value of an instance of the type on
    /// a MemberInfo (Property or Field)
    /// </summary>
    /// <param name="mi">The MethodInfo to obtain the value</param>
    /// <param name="name">The Name of the Dictionary Entry to return</param>
    /// <returns></returns>
    public static string GetValue(MemberInfo mi, string name) {
      name = name.ToLower();
      TypeDictionaryEntryAttribute[] atts
        = mi.GetCustomAttributes(typeof(TypeDictionaryEntryAttribute),
        false) as TypeDictionaryEntryAttribute[];
      if (atts != null && atts.Length > 0) {
        foreach (TypeDictionaryEntryAttribute att in atts) {
          if (att.Name == name)
            return att.Value;   
        }
      }
      return string.Empty;
    }
  }
}
