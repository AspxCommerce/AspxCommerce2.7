<%@ Page Language="VB" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="GCheckout" %>
<%@ Import Namespace="GCheckout.AutoGen" %>
<%@ Import Namespace="GCheckout.Util" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
	Private Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
		' Extract the XML from the request.
		Dim RequestStream As Stream = Request.InputStream
		Dim RequestStreamReader As StreamReader = New StreamReader(RequestStream)
		Dim RequestXml As String = RequestStreamReader.ReadToEnd
		RequestStream.Close()
		' Act on the XML.
		Select Case (EncodeHelper.GetTopElement(RequestXml))
			Case "new-order-notification"
				Dim N1 As NewOrderNotification = CType(EncodeHelper.Deserialize(RequestXml, GetType(NewOrderNotification)), NewOrderNotification)
				Dim OrderNumber1 As String = N1.googleordernumber
				Dim ShipToName As String = N1.buyershippingaddress.contactname
				Dim ShipToAddress1 As String = N1.buyershippingaddress.address1
				Dim ShipToAddress2 As String = N1.buyershippingaddress.address2
				Dim ShipToCity As String = N1.buyershippingaddress.city
				Dim ShipToState As String = N1.buyershippingaddress.region
				Dim ShipToZip As String = N1.buyershippingaddress.postalcode
				For Each ThisItem As Item In N1.shoppingcart.items
					Dim Name As String = ThisItem.itemname
					Dim Quantity As Integer = ThisItem.quantity
					Dim Price As Decimal = ThisItem.unitprice.Value
				Next
			Case "risk-information-notification"
				Dim N2 As RiskInformationNotification = CType(EncodeHelper.Deserialize(RequestXml, GetType(RiskInformationNotification)), RiskInformationNotification)
				' This notification tells us that Google has authorized the order and it has passed the fraud check.
				' Use the data below to determine if you want to accept the order, then start processing it.
				Dim OrderNumber2 As String = N2.googleordernumber
				Dim AVS As String = N2.riskinformation.avsresponse
				Dim CVN As String = N2.riskinformation.cvnresponse
				Dim SellerProtection As Boolean = N2.riskinformation.eligibleforprotection
			Case "order-state-change-notification"
				Dim N3 As OrderStateChangeNotification = CType(EncodeHelper.Deserialize(RequestXml, GetType(OrderStateChangeNotification)), OrderStateChangeNotification)
				' The order has changed either financial or fulfillment state in Google's system.
				Dim OrderNumber3 As String = N3.googleordernumber
				Dim NewFinanceState As String = N3.newfinancialorderstate.ToString
				Dim NewFulfillmentState As String = N3.newfulfillmentorderstate.ToString
				Dim PrevFinanceState As String = N3.previousfinancialorderstate.ToString
				Dim PrevFulfillmentState As String = N3.previousfulfillmentorderstate.ToString
			Case "charge-amount-notification"
				Dim N4 As ChargeAmountNotification = CType(EncodeHelper.Deserialize(RequestXml, GetType(ChargeAmountNotification)), ChargeAmountNotification)
				' Google has successfully charged the customer's credit card.
				Dim OrderNumber4 As String = N4.googleordernumber
				Dim ChargedAmount As Decimal = N4.latestchargeamount.Value
			Case "refund-amount-notification"
				Dim N5 As RefundAmountNotification = CType(EncodeHelper.Deserialize(RequestXml, GetType(RefundAmountNotification)), RefundAmountNotification)
				' Google has successfully refunded the customer's credit card.
				Dim OrderNumber5 As String = N5.googleordernumber
				Dim RefundedAmount As Decimal = N5.latestrefundamount.Value
			Case "chargeback-amount-notification"
				Dim N6 As ChargebackAmountNotification = CType(EncodeHelper.Deserialize(RequestXml, GetType(ChargebackAmountNotification)), ChargebackAmountNotification)
				' A customer initiated a chargeback with his credit card company to get her money back.
				Dim OrderNumber6 As String = N6.googleordernumber
				Dim ChargebackAmount As Decimal = N6.latestchargebackamount.Value
		End Select
	End Sub

</script>

<?xml version="1.0" encoding="UTF-8"?>
<notification-acknowledgment xmlns="http://checkout.google.com/schema/2"/>