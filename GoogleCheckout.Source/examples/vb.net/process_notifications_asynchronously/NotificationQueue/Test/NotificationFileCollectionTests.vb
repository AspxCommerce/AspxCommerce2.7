'/*************************************************
' * Copyright (C) 2007 Google Inc.
' *
' * Licensed under the Apache License, Version 2.0 (the "License");
' * you may not use this file except in compliance with the License.
' * You may obtain a copy of the License at
' *
' *      http://www.apache.org/licenses/LICENSE-2.0
' *
' * Unless required by applicable law or agreed to in writing, software
' * distributed under the License is distributed on an "AS IS" BASIS,
' * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
' * See the License for the specific language governing permissions and
' * limitations under the License.
'*************************************************/

Imports System
Imports NUnit.Framework

Namespace GCheckout.NotificationQueue.Test
    <TestFixture()> _
    Public Class NotificationFileCollectionTests

        <Test()> _
        Public Sub FilterOutNewFiles()
            Dim NFiles As NotificationFileCollection = New NotificationFileCollection
            Dim TenSecondsAgo As DateTime = DateTime.Now.Subtract(New TimeSpan(0, 0, 10))
            Dim OneMinuteAgo As DateTime = DateTime.Now.Subtract(New TimeSpan(0, 1, 0))
            Dim TwoMinutesAgo As DateTime = DateTime.Now.Subtract(New TimeSpan(0, 2, 0))
            NFiles.Add(New NotificationFile("new-order-notification--123--1.xml", TenSecondsAgo))
            NFiles.Add(New NotificationFile("new-order-notification--123--2.xml", OneMinuteAgo))
            NFiles.Add(New NotificationFile("new-order-notification--123--3.xml", TwoMinutesAgo))
            NFiles.RemoveNewFiles(30)
            Assert.AreEqual(2, NFiles.Count)
            NFiles.SortByAge()
            Dim NFile As NotificationFile
            NFile = NFiles.GetFileAt(0)
            Assert.AreEqual("new-order-notification--123--3.xml", NFile.Name)
            NFile = NFiles.GetFileAt(1)
            Assert.AreEqual("new-order-notification--123--2.xml", NFile.Name)
        End Sub

        <Test()> _
        Public Sub Sort()
            Dim NFiles As NotificationFileCollection = New NotificationFileCollection
            NFiles.Add(New NotificationFile("new-order-notification--123--1.xml", New DateTime(2006, 12, 10)))
            NFiles.Add(New NotificationFile("new-order-notification--123--2.xml", New DateTime(2006, 12, 9)))
            NFiles.Add(New NotificationFile("new-order-notification--123--3.xml", New DateTime(2006, 12, 11)))
            NFiles.SortByAge()
            Dim NFile As NotificationFile
            NFile = NFiles.GetFileAt(0)
            Assert.AreEqual("new-order-notification--123--2.xml", NFile.Name)
            NFile = NFiles.GetFileAt(1)
            Assert.AreEqual("new-order-notification--123--1.xml", NFile.Name)
            NFile = NFiles.GetFileAt(2)
            Assert.AreEqual("new-order-notification--123--3.xml", NFile.Name)
        End Sub
    End Class
End Namespace
