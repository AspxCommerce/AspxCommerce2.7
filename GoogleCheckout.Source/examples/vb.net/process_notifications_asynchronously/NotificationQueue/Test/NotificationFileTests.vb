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
Imports System.Collections
Imports NUnit.Framework

Namespace GCheckout.NotificationQueue.Test
    <TestFixture()> _
    Public Class NotificationFileTests

        <Test()> _
        Public Sub Create()
            Dim Id As String = "85f54628-538a-44fc-8605-ae62364f6c71"
            Dim Type As String = "order-state-change-notification"
            Dim OrderId As String = "841171949013218"
            Dim CreationTime As DateTime = DateTime.Now
            Dim NFile As NotificationFile = New NotificationFile(Id, Type, OrderId)
            Assert.AreEqual(Id, NFile.Id)
            Assert.AreEqual(Type, NFile.Type)
            Assert.AreEqual(OrderId, NFile.OrderId)
            Assert.AreEqual(String.Format("{0}--{1}--{2}.xml", Type, OrderId, Id), NFile.Name)
        End Sub

        <Test()> _
        Public Sub CreateAndParse_OK()
            Dim Name As String = "new-order-notification--123--456-789.xml"
            Dim CreationTime As DateTime = DateTime.Now
            Dim NFile As NotificationFile = New NotificationFile(Name, CreationTime)
            Assert.AreEqual(Name, NFile.Name)
            Assert.IsTrue((NFile.Age.TotalSeconds < 1))
            Assert.IsTrue((NFile.Age.TotalSeconds >= 0))
            Assert.AreEqual("new-order-notification", NFile.Type)
            Assert.AreEqual("123", NFile.OrderId)
            Assert.AreEqual("456-789", NFile.Id)
        End Sub

        <Test(), _
         ExpectedException(GetType(ApplicationException))> _
        Public Sub CreateAndParse_Failure()
            Dim Name As String = "new-order-notification-123-456-789.xml"
            Dim CreationTime As DateTime = DateTime.Now
            Dim NFile As NotificationFile = New NotificationFile(Name, CreationTime)
        End Sub
    End Class
End Namespace