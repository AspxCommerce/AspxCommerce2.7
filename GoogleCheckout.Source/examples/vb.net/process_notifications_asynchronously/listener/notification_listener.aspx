<%@ Page Language="VB" %>
<%@ Import Namespace="System.Configuration" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="GCheckout.Util" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
	Public Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
		' Extract the XML from the request.
		Dim RequestXml As String = EncodeHelper.Utf8StreamToString(Request.InputStream)
		' Write the XML to file.
		Dim FileName As String = String.Format("{0}--{1}--{2}.xml", EncodeHelper.GetTopElement(RequestXml), _
		 EncodeHelper.GetElementValue(RequestXml, "google-order-number"), EncodeHelper.GetElementValue(RequestXml, "serial-number"))
		Dim FullFileName As String = (GetPathFromConfigFile + FileName)
		Dim OutputFile As StreamWriter = New StreamWriter(FullFileName, False, Encoding.UTF8)
		OutputFile.Write(RequestXml)
		OutputFile.Close()
	End Sub
    
	Private Function GetPathFromConfigFile() As String
		Dim RetVal As String = ConfigurationSettings.AppSettings("NotificationStore")
		If (RetVal = Nothing) Then
			Throw New ApplicationException("Set the 'NotificationStore' key in the config file.")
		End If
		If Not RetVal.EndsWith("\\") Then
			RetVal = (RetVal + "\\")
		End If
		Return RetVal
	End Function


</script>

<?xml version="1.0" encoding="UTF-8"?>
<notification-acknowledgment xmlns="http://checkout.google.com/schema/2"/>