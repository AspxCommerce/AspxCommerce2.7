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
Imports System.IO
Imports System.Text
Imports System.Threading
Imports NUnit.Framework

Namespace GCheckout.NotificationQueue.Test


    <TestFixture()> _
    Public Class FileSystemNotificationQueueTests

        Private INBOX_DIR As String = "inbox"
        Private INPROCESS_DIR As String = "inprocess"
        Private SUCCESS_DIR As String = "success"
        Private FAILURE_DIR As String = "failure"
        Private Shared TYPE1 As String = "new-order-notification"
        Private Shared ORDERID1 As String = "259835294"
        Private Shared ID1 As String = "78sd-877f-r34f"
        Private Shared FILENAME1 As String = String.Format("{0}--{1}--{2}.xml", TYPE1, ORDERID1, ID1)
        Private Shared TYPE2 As String = "order-state-change-notification"
        Private Shared ORDERID2 As String = "245629824"
        Private Shared ID2 As String = "345g-3w4f-6yg4"
        Private Shared FILENAME2 As String = String.Format("{0}--{1}--{2}.xml", TYPE2, ORDERID2, ID2)
        Private Shared TYPE3 As String = "charge-amount-notification"
        Private Shared ORDERID3 As String = "345475345"
        Private Shared ID3 As String = "3rcf-f4wtvgw-gwg5"
        Private Shared FILENAME3 As String = String.Format("{0}--{1}--{2}.xml", TYPE3, ORDERID3, ID3)
        Private Randomizer As Random = New Random

        Private XML As String = "<new-order-notification xmlns=""http://checkout.google.com/schema/2"" serial-number=""742cfe78-b780-4681-b486-df37550c78e4"">" & _
"  <timestamp>2006-08-14T18:58:36.226Z</timestamp>" & _
"  <google-order-number>934180849872676</google-order-number>" & _
"  <shopping-cart>" & _
"    <items>" & _
"      <item>" & _
"        <quantity>2</quantity>" & _
"        <unit-price currency=""USD"">0.75</unit-price>" & _
"        <item-name>Snickers</item-name>" & _
"        <item-description>Packed with peanuts</item-description>" & _
"      </item>" & _
"      <item>" & _
"        <quantity>1</quantity>" & _
"        <unit-price currency=""USD"">2.99</unit-price>" & _
"        <item-name>Gallon of Milk</item-name>" & _
"        <item-description>Milk goes great with candy bars!</item-description>" & _
"      </item>" & _
"    </items>" & _
"  </shopping-cart>" & _
"  <buyer-shipping-address>" & _
"    <email>someone@email.com</email>" & _
"    <address1>123 Main St</address1>" & _
"    <address2 />" & _
"    <company-name />" & _
"    <contact-name>Martin Omander</contact-name>" & _
"    <phone />" & _
"    <fax />" & _
"    <country-code>US</country-code>" & _
"    <city>Nome</city>" & _
"    <region>AK</region>" & _
"    <postal-code>99763</postal-code>" & _
"  </buyer-shipping-address>" & _
"  <buyer-billing-address>" & _
"    <email>someone@email.com</email>" & _
"    <address1>123 Main St</address1>" & _
"    <address2 />" & _
"    <company-name />" & _
"    <contact-name>Martin Omander</contact-name>" & _
"    <phone />" & _
"    <fax />" & _
"    <country-code>US</country-code>" & _
"    <city>Nome</city>" & _
"    <region>AK</region>" & _
"    <postal-code>99763</postal-code>" & _
"  </buyer-billing-address>" & _
"  <buyer-marketing-preferences>" & _
"    <email-allowed>false</email-allowed>" & _
"  </buyer-marketing-preferences>" & _
"  <order-adjustment>" & _
"    <merchant-codes />" & _
"    <shipping>" & _
"      <flat-rate-shipping-adjustment>" & _
"        <shipping-name>USPS</shipping-name>" & _
"        <shipping-cost currency=""USD"">3.08</shipping-cost>" & _
"      </flat-rate-shipping-adjustment>" & _
"    </shipping>" & _
"    <total-tax currency=""USD"">0.0</total-tax>" & _
"    <adjustment-total currency=""USD"">3.08</adjustment-total>" & _
"  </order-adjustment>" & _
"  <order-total currency=""USD"">7.57</order-total>" & _
"  <fulfillment-order-state>NEW</fulfillment-order-state>" & _
"  <financial-order-state>REVIEWING</financial-order-state>" & _
"  <buyer-id>352162941084959</buyer-id>" & _
"</new-order-notification>"


        <SetUp(), _
         TearDown()> _
        Public Sub DeleteTempDirs()
            DeleteDirAndFiles(INBOX_DIR)
            DeleteDirAndFiles(INPROCESS_DIR)
            DeleteDirAndFiles(SUCCESS_DIR)
            DeleteDirAndFiles(FAILURE_DIR)
        End Sub

        <Test(), _
         ExpectedException(GetType(ApplicationException))> _
        Public Sub Create_Failure_1()
            Dim Q As FileSystemNotificationQueue = New FileSystemNotificationQueue(INBOX_DIR, INPROCESS_DIR, SUCCESS_DIR, FAILURE_DIR)
        End Sub

        <Test(), _
         ExpectedException(GetType(ApplicationException))> _
        Public Sub Create_Failure_2()
            Directory.CreateDirectory(INBOX_DIR)
            Directory.CreateDirectory(INPROCESS_DIR)
            Directory.CreateDirectory(SUCCESS_DIR)
            Dim Q As FileSystemNotificationQueue = New FileSystemNotificationQueue(INBOX_DIR, INPROCESS_DIR, SUCCESS_DIR, FAILURE_DIR)
        End Sub

        <Test()> _
        Public Sub Create_OK()
            CreateTempDirs()
            Dim Q As FileSystemNotificationQueue = New FileSystemNotificationQueue(INBOX_DIR, INPROCESS_DIR, SUCCESS_DIR, FAILURE_DIR)
            Assert.AreEqual(0, Q.GetLength)
        End Sub

        <Test(), _
         ExpectedException(GetType(ApplicationException))> _
        Public Sub Receive_Failure()
            Dim Q As FileSystemNotificationQueue = New FileSystemNotificationQueue(INBOX_DIR)
            Q.Receive()
        End Sub

        <Test()> _
        Public Sub GetLength()
            CreateTempDirs()
            Dim Q As FileSystemNotificationQueue = New FileSystemNotificationQueue(INBOX_DIR, INPROCESS_DIR, SUCCESS_DIR, FAILURE_DIR)
            WriteFile(INBOX_DIR, FILENAME1, XML, 60)
            WriteFile(INBOX_DIR, FILENAME2, XML, 60)
            WriteFile(INBOX_DIR, FILENAME3, XML, 10)
            Assert.AreEqual(2, Q.GetLength)
        End Sub

        <Test()> _
        Public Sub Send()
            CreateTempDirs()
            Dim Q As FileSystemNotificationQueue = New FileSystemNotificationQueue(INBOX_DIR, INPROCESS_DIR, SUCCESS_DIR, FAILURE_DIR)
            Q.Send(New NotificationQueueMessage(ID1, TYPE1, ORDERID1, XML))
            Assert.AreEqual(1, Directory.GetFiles(INBOX_DIR).Length)
            Assert.AreEqual(GetFullFileName(INBOX_DIR, FILENAME1), Directory.GetFiles(INBOX_DIR)(0))
            Assert.AreEqual(0, Directory.GetFiles(INPROCESS_DIR).Length)
            Assert.AreEqual(0, Directory.GetFiles(SUCCESS_DIR).Length)
            Assert.AreEqual(0, Directory.GetFiles(FAILURE_DIR).Length)
            Q.Send(New NotificationQueueMessage(ID2, TYPE2, ORDERID2, XML))
            Q.Send(New NotificationQueueMessage(ID3, TYPE3, ORDERID3, XML))
            Assert.AreEqual(3, Directory.GetFiles(INBOX_DIR).Length)
        End Sub

        <Test()> _
        Public Sub ProcessFiles()
            CreateTempDirs()
            WriteFile(INBOX_DIR, FILENAME1, XML, 40)
            WriteFile(INBOX_DIR, FILENAME2, XML, 50)
            WriteFile(INBOX_DIR, FILENAME3, XML, 60)
            Dim Q As FileSystemNotificationQueue = New FileSystemNotificationQueue(INBOX_DIR, INPROCESS_DIR, SUCCESS_DIR, FAILURE_DIR)
            Assert.AreEqual(3, Q.GetLength)
            Dim M As NotificationQueueMessage = Q.Receive
            Assert.AreEqual("charge-amount-notification", M.Type)
            Assert.AreEqual("345475345", M.OrderId)
            Assert.AreEqual("3rcf-f4wtvgw-gwg5", M.Id)
            Assert.AreEqual(2, Q.GetLength)
            Assert.AreEqual(2, Directory.GetFiles(INBOX_DIR).Length)
            Assert.AreEqual(1, Directory.GetFiles(INPROCESS_DIR).Length)
            Assert.AreEqual(GetFullFileName(INPROCESS_DIR, FILENAME3), Directory.GetFiles(INPROCESS_DIR)(0))
            Assert.AreEqual(0, Directory.GetFiles(SUCCESS_DIR).Length)
            Assert.AreEqual(0, Directory.GetFiles(FAILURE_DIR).Length)
            Q.ProcessingSucceeded(M)
            Assert.AreEqual(2, Directory.GetFiles(INBOX_DIR).Length)
            Assert.AreEqual(0, Directory.GetFiles(INPROCESS_DIR).Length)
            Assert.AreEqual(1, Directory.GetFiles(SUCCESS_DIR).Length)
            Assert.AreEqual(GetFullFileName(SUCCESS_DIR, FILENAME3), Directory.GetFiles(SUCCESS_DIR)(0))
            Assert.AreEqual(0, Directory.GetFiles(FAILURE_DIR).Length)
            M = Q.Receive
            Assert.AreEqual("order-state-change-notification", M.Type)
            Assert.AreEqual("245629824", M.OrderId)
            Assert.AreEqual("345g-3w4f-6yg4", M.Id)
            Assert.AreEqual(1, Q.GetLength)
            Assert.AreEqual(1, Directory.GetFiles(INBOX_DIR).Length)
            Assert.AreEqual(1, Directory.GetFiles(INPROCESS_DIR).Length)
            Assert.AreEqual(GetFullFileName(INPROCESS_DIR, FILENAME2), Directory.GetFiles(INPROCESS_DIR)(0))
            Assert.AreEqual(1, Directory.GetFiles(SUCCESS_DIR).Length)
            Assert.AreEqual(0, Directory.GetFiles(FAILURE_DIR).Length)
            Q.ProcessingFailed(M)
            Assert.AreEqual(1, Directory.GetFiles(INBOX_DIR).Length)
            Assert.AreEqual(0, Directory.GetFiles(INPROCESS_DIR).Length)
            Assert.AreEqual(1, Directory.GetFiles(SUCCESS_DIR).Length)
            Assert.AreEqual(1, Directory.GetFiles(FAILURE_DIR).Length)
            Assert.AreEqual(GetFullFileName(FAILURE_DIR, FILENAME2), Directory.GetFiles(FAILURE_DIR)(0))
        End Sub

        <Test(), Explicit()> _
        Public Sub PerformanceTest()
            Dim BACKLOG As Integer = 1000
            ' Seed the queue with a backlog.
            CreateTempDirs()
            Dim Q As FileSystemNotificationQueue = New FileSystemNotificationQueue(INBOX_DIR, INPROCESS_DIR, SUCCESS_DIR, FAILURE_DIR)
            Dim i As Integer = 0
            Do While (i < BACKLOG)
                Q.Send(New NotificationQueueMessage(GetRandomId, GetRandomType, GetRandomOrderId, XML))
                i = (i + 1)
            Loop
            ' Sleep 30 seconds to make sure the notification files are old enough.
            Thread.Sleep(30000)
            ' Run the actual test.
            Dim StartTime As DateTime = DateTime.Now
            i = 0
            Do While (i < BACKLOG)
                Q.Send(New NotificationQueueMessage(GetRandomId, GetRandomType, GetRandomOrderId, XML))
                Dim M As NotificationQueueMessage = Q.Receive
                Q.ProcessingSucceeded(M)
                i = (i + 1)
            Loop
            Dim Duration As TimeSpan = DateTime.Now.Subtract(StartTime)
            Console.WriteLine("Took {0} seconds to catch up with a backlog of {1} messages, while new ones were being added.", Duration.TotalSeconds, BACKLOG)
            Console.WriteLine("Throughput: {0} messages per second.", (BACKLOG / Duration.TotalSeconds))
        End Sub

        Private Function GetRandomType() As String
            Dim RetVal As String = ""
            Dim Type As Integer = Randomizer.Next(5)
            If (Type = 0) Then
                RetVal = "new-order-notification"
            End If
            If (Type = 1) Then
                RetVal = "order-state-change-notification"
            End If
            If (Type = 2) Then
                RetVal = "risk-information-notification"
            End If
            If (Type = 3) Then
                RetVal = "refund-amount-notification"
            End If
            If (Type = 4) Then
                RetVal = "charge-amount-notification"
            End If
            If (Type = 5) Then
                RetVal = "chargeback-amount-notification"
            End If
            Return RetVal
        End Function

        Private Function GetRandomOrderId() As String
            Dim SB As StringBuilder = New StringBuilder
            Dim i As Integer = 0
            Do While (i < 14)
                SB.Append(Randomizer.Next(9))
                i = (i + 1)
            Loop
            Return SB.ToString
        End Function

        Private Function GetRandomId() As String
            Dim SB As StringBuilder = New StringBuilder
            Dim i As Integer = 0
            Do While (i < 4)
                Dim j As Integer = 0
                Do While (j < 8)
                    SB.Append(Randomizer.Next(9))
                    j = (j + 1)
                Loop
                If (i < 3) Then
                    SB.Append("-")
                End If
                i = (i + 1)
            Loop
            Return SB.ToString
        End Function

        Private Sub CreateTempDirs()
            Directory.CreateDirectory(INBOX_DIR)
            Directory.CreateDirectory(INPROCESS_DIR)
            Directory.CreateDirectory(SUCCESS_DIR)
            Directory.CreateDirectory(FAILURE_DIR)
        End Sub

        Private Shared Sub WriteFile(ByVal Dir As String, ByVal FileName As String, ByVal Contents As String, ByVal BackDateSeconds As Integer)
            Dim FullFileName As String = GetFullFileName(Dir, FileName)
            Dim OutputFile As StreamWriter = New StreamWriter(FullFileName, False, Encoding.UTF8)
            OutputFile.Write(Contents)
            OutputFile.Close()
            If (BackDateSeconds > 0) Then
                Dim FI As FileInfo = New FileInfo(FullFileName)
                FI.CreationTime = DateTime.Now.Subtract(New TimeSpan(0, 0, BackDateSeconds))
            End If
        End Sub

        Private Shared Sub DeleteDirAndFiles(ByVal Dir As String)
            If Directory.Exists(Dir) Then
                For Each FileName As String In Directory.GetFiles(Dir)
                    File.Delete(FileName)
                Next
                Directory.Delete(Dir)
            End If
        End Sub

        Private Shared Function GetFullFileName(ByVal Dir As String, ByVal FileName As String) As String
            Dim RetVal As String = Dir
            If Not RetVal.EndsWith("\") Then
                RetVal = (RetVal + "\")
            End If
            RetVal = (RetVal + FileName)
            Return RetVal
        End Function
    End Class
End Namespace


