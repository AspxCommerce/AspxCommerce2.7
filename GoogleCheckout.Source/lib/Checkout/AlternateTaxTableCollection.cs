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
 *  3-14-2009   Joe Feser joe.feser@joefeser.com
 *  Verify tax rules and better code coverage.  
 * 
*/
using System;
using System.Collections;
using System.Collections.Specialized;

namespace GCheckout.Checkout {

  /// <summary>
  /// Summary description for AlternateTaxTables.
  /// </summary>
  public class AlternateTaxTableCollection {

    private Hashtable _taxTables = new Hashtable();

    /// <summary>
    /// The Number of Tax Tables
    /// </summary>
    public int Count {
      get {
        return _taxTables.Count;
      }
    }

    /// <summary>
    /// Return if a Tax table with the name exists
    /// </summary>
    /// <param name="name">The Name of the tax table</param>
    /// <returns>true or false if the key exists</returns>
    public bool Exists(string name) {
      name = name.ToLower();
      return _taxTables.ContainsKey(name);
    }

    /// <summary>
    /// Container for the Alternate Tax Tables.
    /// </summary>
    public AlternateTaxTableCollection() {

    }

    /// <summary>
    /// Return the <see cref="AlternateTaxTable"/> with the key
    /// </summary>
    public AlternateTaxTable this[string name] {
      get {
        name = name.ToLower();
        return _taxTables[name] as AlternateTaxTable;
      }
    }

    /// <summary>
    /// Create a new instance or return the Tax table if it exists.
    /// </summary>
    /// <param name="name">
    /// The name attribute value contains a string that can be used to identify
    /// a tax table or shipping method. The attribute value must be at least
    /// one non-space character and may not be longer than 255 characters.
    /// </param>
    /// <returns>A reference to the <see cref="AlternateTaxTable"/></returns>
    [Obsolete("You should just create a tax table and add it to the collection.")]
    public AlternateTaxTable Factory(string name) {
      return Factory(name, false);
    }

    /// <summary>
    /// Create a new instance or return the Tax table if it exists.
    /// </summary>
    /// <param name="name">
    /// The name attribute value contains a string that can be used to identify
    /// a tax table or shipping method. The attribute value must be at least
    /// one non-space character and may not be longer than 255 characters.
    /// </param>
    /// <param name="standalone">
    /// The standalone attribute indicates how taxes should be calculated 
    /// if there is no matching alternate-tax-rule for the given state,
    ///  zip code or country area. If this attribute's value is TRUE and 
    ///  there is no matching alternate-tax-rule, the tax amount will be 
    ///  zero. If the attribute's value is FALSE and there is no matching 
    ///  alternate-tax-rule, the tax amount will be calculated using 
    ///  the default tax table.
    /// </param>
    /// <returns>A reference to the <see cref="AlternateTaxTable"/></returns>
    [Obsolete("You should just create a tax table and add it to the collection.")]
    public AlternateTaxTable Factory(string name, bool standalone) {
      if(name == null || name == string.Empty || name.Trim() == string.Empty) {
        throw new ArgumentNullException("name", "The name of the Tax table must" +
          " not be empty. If you wish to use an Empty Tax table, please call" +
          " AlternateTaxTable.Empty");
      }
      
      name = name.ToLower();
      AlternateTaxTable retVal = this[name];

      if (retVal == null) {
        retVal = new AlternateTaxTable(name, standalone);
        _taxTables.Add(name, retVal);
      }
      else {
        //a duplicate tax table is being added.
        throw new ApplicationException(GetDuplicateTaxTableError(name));
      }
      
      return retVal;
    }

    /// <summary>
    /// Add an <see cref="AlternateTaxTable"/> to the collection
    /// </summary>
    /// <param name="taxTable">The tax table to add.</param>
    public void Add(AlternateTaxTable taxTable) {
      VerifyTaxRule(taxTable);
    }

    /// <summary>
    /// Delete an Alternate Tax Table from the collection.
    /// </summary>
    /// <param name="name">The Name of the <see cref="AlternateTaxTable"/></param>
    public void Delete(string name) {
      name = name.ToLower();
      if (_taxTables.ContainsKey(name))
        _taxTables.Remove(name);
    }

    /// <summary>
    /// Verify that the tax table is unique and that it not empty
    /// </summary>
    /// <param name="taxTable">the <see cref="AlternateTaxTable"/> to verify.</param>
    internal void VerifyTaxRule(AlternateTaxTable taxTable) {
      //Now we need to see if the item exists in the collection

      AlternateTaxTable existing = this[taxTable.Name];

      if (existing != null) {
        if (!object.ReferenceEquals(existing, taxTable)) {
          throw new ApplicationException(GetDuplicateTaxTableError(taxTable.Name));
        }
      }
      else {
        //Add the item to the collection   
        _taxTables.Add(taxTable.Name.ToLower(), taxTable);
      }
    }

    private string GetDuplicateTaxTableError(string name) {
      return "The Tax table '" + name + 
        "' already exists and the references are not equal. Please use" + 
        " the AlternateTaxTable this[string name]" +
        " method of to ensure the references are equal.";
    }

    /// <summary>
    /// Append this collection of tax tables to the request object.
    /// </summary>
    /// <param name="taxTables">The <see cref="AutoGen.TaxTables"/> used in the request</param>
    internal void AppendToRequest(AutoGen.TaxTables taxTables) {
      if (this.Count > 0) {
        AlternateTaxTable[] items = new AlternateTaxTable[this.Count];
        _taxTables.Values.CopyTo(items, 0);
        
        AutoGen.AlternateTaxTable[] altTables
          = new GCheckout.AutoGen.AlternateTaxTable[items.Length];

        for(int i = 0; i < items.Length; i++) {
          altTables[i] = items[i].GetAutoGenTable();
        }

        taxTables.alternatetaxtables = altTables;
      }
      else {
        //Console.WriteLine("AlternateTaxTable:" + 0);   
      }
    }
  }
}
