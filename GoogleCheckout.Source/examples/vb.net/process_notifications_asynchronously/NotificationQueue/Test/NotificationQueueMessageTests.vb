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
    Public Class NotificationQueueMessageTests

        <Test()> _
        Public Sub Create()
            Dim Id As String = "85f54628-538a-44fc-8605-ae62364f6c71"
            Dim Type As String = "order-state-change-notification"
            Dim OrderId As String = "841171949013218"
            Dim Xml As String = "<a>blah</a>" & vbCrLf & "<b>blah</b>"
            Dim M As NotificationQueueMessage = New NotificationQueueMessage(Id, Type, OrderId, Xml)
            Assert.AreEqual(Id, M.Id)
            Assert.AreEqual(Type, M.Type)
            Assert.AreEqual(OrderId, M.OrderId)
            Assert.AreEqual(Xml, M.Xml)
        End Sub
    End Class
End Namespace

