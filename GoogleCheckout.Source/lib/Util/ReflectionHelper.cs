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
 *  August 2008   Joe Feser joe.feser@joefeser.com
 *  Initial release. Reflection methods.
 *  
*/
using System;
using System.Reflection;

namespace GCheckout.Util
{
	/// <summary>
	/// Summary description for ReflectionHelper.
	/// </summary>
	public abstract class ReflectionHelper
	{
		
    /// <summary>
    /// Determine if a Field Exists for a property
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="property"></param>
    /// <returns></returns>
    public static bool FieldExists(object obj, string property) {
      //TODO Fix when we goto .net 2.0 this will be properties instead of fields.
      Type t = obj.GetType();
      FieldInfo pi;
      pi = t.GetField(property);
      return pi != null;
    }

    /// <summary>
    /// Determine if a Field Exists for a property
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="property"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static bool FieldValueEqual(object obj, string property, object value) {
      //TODO Fix when we goto .net 2.0 this will be properties instead of fields.
      Type t = obj.GetType();
      FieldInfo pi;
      pi = t.GetField(property);
      object foundValue = pi.GetValue(obj); 
      bool retVal = foundValue == value;
      if (!retVal) {
        //TODO make this more generic for numeric types.
        if (foundValue.GetType() == typeof(int) && value.GetType() == typeof(int)) {
           return (int)foundValue == (int)value;
        }
      }
      return retVal;
    }

    /// <summary>
    /// Set the value on a Field
    /// </summary>
    /// <param name="property"></param>
    /// <param name="includeSpecified"></param>
    /// <param name="obj"></param>
    /// <param name="value"></param>
    public static void SetFieldValue(string property, 
      bool includeSpecified, object obj, object value) {
      //TODO Fix when we goto .net 2.0 this will be properties instead of fields.
      Type t = obj.GetType();
      FieldInfo pi;
      pi = t.GetField(property);
      pi.SetValue(obj, value);
      if (includeSpecified){
        pi = t.GetField(property + "Specified");
        pi.SetValue(obj, true);
      }
    }

	}
}
