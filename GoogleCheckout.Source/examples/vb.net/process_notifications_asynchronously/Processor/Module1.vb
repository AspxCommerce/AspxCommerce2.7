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
Imports System.Configuration
Imports GCheckout.NotificationQueue
Imports GCheckout.AutoGen
Imports GCheckout.Util

Module Module1

    Sub Main()
        Dim Q As INotificationQueue = New FileSystemNotificationQueue(GetPathFromConfigFile("InboxDir"), GetPathFromConfigFile("InProcessDir"), GetPathFromConfigFile("SuccessDir"), GetPathFromConfigFile("FailureDir"))

        While True
            Console.WriteLine("" & vbLf & "Waiting for the next notification.")
            Dim M As NotificationQueueMessage = Q.Receive
            Console.WriteLine("Processing {0} for order {1}.", M.Type, M.OrderId)
            Try
                ProcessNotification(M.Type, M.Xml)
                Q.ProcessingSucceeded(M)
            Catch e As Exception
                Console.WriteLine(e.ToString)
                Q.ProcessingFailed(M)
            End Try
            Console.WriteLine("{0} notifications in queue.", Q.GetLength)

        End While
    End Sub

    Private Function GetPathFromConfigFile(ByVal Key As String) As String
        Dim RetVal As String = ConfigurationSettings.AppSettings(Key)
        If (RetVal = Nothing) Then
            Throw New ApplicationException(("Set the '" _
                            + (Key + "' key in the config file.")))
        End If
        Return RetVal
    End Function

    Private Sub ProcessNotification(ByVal Type As String, ByVal Xml As String)
        Select Case (Type)
            Case "new-order-notification"
                Dim N1 As NewOrderNotification = CType(EncodeHelper.Deserialize(Xml, GetType(NewOrderNotification)), NewOrderNotification)
                ' Add call to existing business system here, passing data from N1.
            Case "risk-information-notification"
                Dim N2 As RiskInformationNotification = CType(EncodeHelper.Deserialize(Xml, GetType(RiskInformationNotification)), RiskInformationNotification)
                ' Add call to existing business system here, passing data from N2.
            Case "order-state-change-notification"
                Dim N3 As OrderStateChangeNotification = CType(EncodeHelper.Deserialize(Xml, GetType(OrderStateChangeNotification)), OrderStateChangeNotification)
                ' Add call to existing business system here, passing data from N3.
            Case "charge-amount-notification"
                Dim N4 As ChargeAmountNotification = CType(EncodeHelper.Deserialize(Xml, GetType(ChargeAmountNotification)), ChargeAmountNotification)
                ' Add call to existing business system here, passing data from N4.
            Case "refund-amount-notification"
                Dim N5 As RefundAmountNotification = CType(EncodeHelper.Deserialize(Xml, GetType(RefundAmountNotification)), RefundAmountNotification)
                ' Add call to existing business system here, passing data from N5.
            Case "chargeback-amount-notification"
                Dim N6 As ChargebackAmountNotification = CType(EncodeHelper.Deserialize(Xml, GetType(ChargebackAmountNotification)), ChargebackAmountNotification)
                ' Add call to existing business system here, passing data from N6.
            Case Else
                Throw New ApplicationException(("Unknown notification type: " + Type))
        End Select
    End Sub

End Module




