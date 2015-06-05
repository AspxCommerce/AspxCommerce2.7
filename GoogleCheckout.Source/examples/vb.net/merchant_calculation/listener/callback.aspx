<%@ Page Language="VB" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="GCheckout.MerchantCalculation" %>
<%@ Import Namespace="GCheckout.Util" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
	
	'This page is meant to receive callbacks for merchant-calculated values from Google.
	'The page currently uses the example business logic from ExampleRules.cs.
	'To substitute your own business logic, write a class that inherits from CallbackRules.cs
	'and overrides one or more of the methods in that class. Instantiate that class
	'where ExampleRules is being instantiated below.

	Private Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
		Try
			' Extract the XML from the request.
			Dim RequestStream As Stream = Request.InputStream
			Dim RequestStreamReader As StreamReader = New StreamReader(RequestStream)
			Dim RequestXml As String = RequestStreamReader.ReadToEnd
			RequestStream.Close()
			Log.Debug(("Request XML: " + RequestXml))
			' Process the incoming XML.
			Dim P As CallbackProcessor = New CallbackProcessor(New ExampleRules)
			Dim ResponseXML() As Byte = P.Process(RequestXml)
			Log.Debug(("Response XML: " + EncodeHelper.Utf8BytesToString(ResponseXML)))
			Response.BinaryWrite(ResponseXML)
		Catch ex As Exception
			Log.Debug(ex.ToString)
		End Try
	End Sub
</script>
