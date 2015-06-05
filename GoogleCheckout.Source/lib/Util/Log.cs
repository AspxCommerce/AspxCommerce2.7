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
 *  08-03-2012    Joe Feser joe.feser@joefeser.com
 *  Added logging
 * 
*/

using System;
using System.IO;
using System.Diagnostics;
using System.Configuration;

namespace GCheckout.Util {
  /// <summary>
  /// This class contains methods that write messages, including debugging 
  /// and error information, to log files.
  /// </summary>
  public class Log {

    internal static readonly object lockObject = new object();

    static Log() {
      try {
        if (LoggingOn()) {
          lock (lockObject) {
            if (!string.IsNullOrEmpty(GetLogPath())) {
              if (!Directory.Exists(GetLogPath())) {
                Directory.CreateDirectory(GetLogPath());
              }
            }
          }
        }
      }
      catch (Exception ex) {
        Err("Error Attempting to create LogPath directory:" + ex.Message);
      }
      try {
        if (LoggingOn()) {
          lock (lockObject) {
            if (!string.IsNullOrEmpty(GetLogPathXml())) {
              if (!Directory.Exists(GetLogPathXml())) {
                Directory.CreateDirectory(GetLogPathXml());
              }
            }
          }
        }
      }
      catch (Exception ex) {
        Err("Error Attempting to create LogPathXml directory:" + ex.Message);
      }
    }

    /// <summary>
    /// Writes a string to a file.
    /// </summary>
    /// <param name="strFileName">Path and name of the file.</param>
    /// <param name="strLine">The line to write.</param>
    public static void WriteToFile(string strFileName, string strLine) {
      if (LoggingOn()) {
        lock (lockObject) {
          using (StreamWriter objStreamWriter = new StreamWriter(strFileName, true)) {
            // Write a line of text.
            objStreamWriter.WriteLine(strLine);
          }
        }
      }
      else {
        System.Diagnostics.Debug.WriteLine(DateTime.Now + " - " + strLine);
      }
    }

    /// <summary>
    /// Writes a debug message to the file X\debug.txt where X is read from
    /// the config file key "LogDirectory".
    /// </summary>
    /// <param name="strLine">The debug message to write.</param>
    public static void Debug(string strLine) {
      if (LoggingOn()) {
        WriteToFile(GetLogPath() + "debug.txt", DateTime.Now + " - " +
          strLine);
      }
      else {
        System.Diagnostics.Debug.WriteLine(DateTime.Now + " - " + strLine);
      }
    }

    /// <summary>
    /// Writes an error message to the file X\error.txt where X is read from
    /// the config file key "LogDirectory".
    /// </summary>
    /// <param name="strLine">The error message to write.</param>
    public static void Err(string strLine) {
      if (LoggingOn()) {
        WriteToFile(GetLogPath() + "error.txt", DateTime.Now + " - " +
          strLine + (new StackTrace()).ToString());
      }
      else {
        System.Diagnostics.Debug.WriteLine(DateTime.Now + " - " +
          strLine + (new StackTrace()).ToString());
      }
    }

    /// <summary>
    /// Writes an error message to the file X\error.txt where X is read from
    /// the config file key "LogDirectory".
    /// </summary>
    /// <param name="name">The name of the file to write.</param>
    /// <param name="xml">The message to write.</param>
    public static void Xml(string name, string xml) {
      try {
        if (string.IsNullOrEmpty(name)) {
          name = "NoSerial" + Guid.NewGuid().ToString();
        }

        //just in case the same serial number comes in more than once.
        name = name + DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH'-'mm'-'s") + ".xml";

        if (LoggingOn()) {
          WriteToFile(GetLogPathXml() + name, xml);
        }
        else {
          System.Diagnostics.Debug.WriteLine(name + " - " + xml);
        }
      }
      catch (Exception ex) {
        Err("Xml error:" + ex.Message + " | " + name + " | " + xml);
      }
    }

    /// <summary>
    /// Returns true if logging is on, that is if the config file contains
    /// values for a key for "LogDirectory" and if the value for the key
    /// "Logging" is "true".
    /// </summary>
    /// <returns>True if the config keys are correct.</returns>
    public static bool LoggingOn() {
      bool retVal = true;
      if ((GetLogPath() == null || GetLogPath() == string.Empty)
        || !GCheckoutConfigurationHelper.Logging) {
        retVal = false;
      }
      return retVal;
    }

    /// <summary>
    /// Gets the log path, that is the value of the "LogDirectory" key in the
    /// config file.
    /// </summary>
    /// <returns>Log path.</returns>
    private static string GetLogPath() {
      return GCheckoutConfigurationHelper.LogDirectory;
    }

    /// <summary>
    /// Gets the log path, that is the value of the "LogDirectoryXml" key in the
    /// config file.
    /// </summary>
    /// <returns>Log path.</returns>
    private static string GetLogPathXml() {
      var retVal = GCheckoutConfigurationHelper.LogDirectoryXml;
      if (!string.IsNullOrEmpty(retVal) && !retVal.EndsWith("/")) {
        return retVal + "/";
      }
      return retVal;
    }
  }
}
