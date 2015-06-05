Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports GCheckout.OrderProcessing
Imports GCheckout.Util

'original source from http://www.capprime.com/software_development_weblog/2010/11/29/UsingTheGoogleCheckout25APIWithASPNETMVCTheMissingSample.aspx

Public Class GoogleCheckoutHelper
    ' Methods
    Private Shared Sub HandleAuthorizationAmountNotification(ByVal inputAuthorizationAmountNotification As GCheckout.AutoGen.AuthorizationAmountNotification)
        'TODO: Add custom processing for this notification type
    End Sub

    Private Shared Sub HandleChargeAmountNotification(ByVal inputChargeAmountNotification As GCheckout.AutoGen.ChargeAmountNotification)
        'TODO: Add custom processing for this notification type
    End Sub

    Private Shared Sub HandleNewOrderNotification(ByVal inputNewOrderNotification As GCheckout.AutoGen.NewOrderNotification)
        Dim hiddenMerchantPrivateData As String = inputNewOrderNotification.shoppingcart.merchantprivatedata.Any(0).InnerText
        'TODO: Process the MerchantPrivateData if provided

        For Each oneItem As GCheckout.AutoGen.Item In inputNewOrderNotification.shoppingcart.items
            ' TODO: Get MerchantItemId from shopping cart item (oneItem.merchantitemid) and process it
        Next

        'TODO: Add custom processing for this notification type

    End Sub

    Private Shared Sub HandleOrderStateChangeNotification(ByVal notification As GCheckout.AutoGen.OrderStateChangeNotification)

        'Charge Order If Chargeable
        If ((notification.previousfinancialorderstate = GCheckout.AutoGen.FinancialOrderState.REVIEWING) _
            AndAlso (notification.newfinancialorderstate = GCheckout.AutoGen.FinancialOrderState.CHARGEABLE)) Then

            Dim oneGCheckoutResponse As GCheckoutResponse = New ChargeOrderRequest(notification.googleordernumber).Send
        End If
        'Update License If Charged
        If ((notification.previousfinancialorderstate = GCheckout.AutoGen.FinancialOrderState.CHARGING) _
            AndAlso (notification.newfinancialorderstate = GCheckout.AutoGen.FinancialOrderState.CHARGED)) Then
            'TODO: For each shopping cart item received in the NewOrderNotification, authorize the license
        End If

        'TODO: Add custom processing for this notification type
    End Sub

    Private Shared Sub HandleRiskInformationNotification(ByVal notification As GCheckout.AutoGen.RiskInformationNotification)
        ' TODO: Add custom processing for this notification type
    End Sub

    Public Shared Sub ProcessNotification(ByVal serialNumber As String)

        'The next two statements set up a request and call google checkout for the details based on that serial number.
        Dim oneNotificationHistoryRequest As New NotificationHistoryRequest(New NotificationHistorySerialNumber(serialNumber))
        Dim oneNotificationHistoryResponse As NotificationHistoryResponse = DirectCast(oneNotificationHistoryRequest.Send, NotificationHistoryResponse)

        'oneNotificationHistoryResponse.ResponseXml contains the complete response

        Dim oneNotification As Object
        For Each oneNotification In oneNotificationHistoryResponse.NotificationResponses
            If oneNotification.GetType.Equals(GetType(GCheckout.AutoGen.NewOrderNotification)) Then
                Dim oneNewOrderNotification As GCheckout.AutoGen.NewOrderNotification = DirectCast(oneNotification, GCheckout.AutoGen.NewOrderNotification)
                If oneNewOrderNotification.serialnumber.Equals(serialNumber) Then
                    GoogleCheckoutHelper.HandleNewOrderNotification(oneNewOrderNotification)
                End If
            ElseIf oneNotification.GetType.Equals(GetType(GCheckout.AutoGen.OrderStateChangeNotification)) Then
                Dim oneOrderStateChangeNotification As GCheckout.AutoGen.OrderStateChangeNotification = DirectCast(oneNotification, GCheckout.AutoGen.OrderStateChangeNotification)
                If oneOrderStateChangeNotification.serialnumber.Equals(serialNumber) Then
                    GoogleCheckoutHelper.HandleOrderStateChangeNotification(oneOrderStateChangeNotification)
                End If
            ElseIf oneNotification.GetType.Equals(GetType(GCheckout.AutoGen.RiskInformationNotification)) Then
                Dim oneRiskInformationNotification As GCheckout.AutoGen.RiskInformationNotification = DirectCast(oneNotification, GCheckout.AutoGen.RiskInformationNotification)
                If oneRiskInformationNotification.serialnumber.Equals(serialNumber) Then
                    GoogleCheckoutHelper.HandleRiskInformationNotification(oneRiskInformationNotification)
                End If
            ElseIf oneNotification.GetType.Equals(GetType(GCheckout.AutoGen.AuthorizationAmountNotification)) Then
                Dim oneAuthorizationAmountNotification As GCheckout.AutoGen.AuthorizationAmountNotification = DirectCast(oneNotification, GCheckout.AutoGen.AuthorizationAmountNotification)
                If oneAuthorizationAmountNotification.serialnumber.Equals(serialNumber) Then
                    GoogleCheckoutHelper.HandleAuthorizationAmountNotification(oneAuthorizationAmountNotification)
                End If
            ElseIf oneNotification.GetType.Equals(GetType(GCheckout.AutoGen.ChargeAmountNotification)) Then
                Dim oneChargeAmountNotification As GCheckout.AutoGen.ChargeAmountNotification = DirectCast(oneNotification, GCheckout.AutoGen.ChargeAmountNotification)
                If oneChargeAmountNotification.serialnumber.Equals(serialNumber) Then
                    GoogleCheckoutHelper.HandleChargeAmountNotification(oneChargeAmountNotification)
                End If
            Else
                Throw New ArgumentOutOfRangeException(String.Concat(New String() {"Unhandled Type [", oneNotification.GetType.ToString, "]!; serialNumber=[", serialNumber, "];"}))
            End If
        Next
    End Sub


End Class
