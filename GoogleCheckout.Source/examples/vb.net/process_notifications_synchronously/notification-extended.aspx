<%@ Page Language="VB" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="GCheckout" %>
<%@ Import Namespace="GCheckout.AutoGen" %>
<%@ Import Namespace="GCheckout.Util" %>

<script runat="server">
  Private Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
    ' Extract the XML from the request.
    Dim RequestStream As Stream = Request.InputStream
    Dim RequestStreamReader As StreamReader = New StreamReader(RequestStream)
    Dim RequestXml As String = RequestStreamReader.ReadToEnd
    RequestStream.Close()
    
    'instead of performing a switch statement against the top element
    'you can use an overloaded Deserialize method that will determine
    'what the message type is and return that type.
    'What we have also done is created "Extended" classes that contain
    'helper properties and methods on them.
    'One such class is called NewOrderNotificationExtended.
    
    'This is a class that inherits from NewOrderNotification
    Dim requestObject As Object = EncodeHelper.Deserialize(RequestXml)
    
    'what we are doing is determining the type of the object and performing
    'actions based on that type.
    If TypeOf requestObject Is GCheckout.AutoGen.Extended.NewOrderNotificationExtended Then
      Dim ext As GCheckout.AutoGen.Extended.NewOrderNotificationExtended _
      = CType(requestObject, GCheckout.AutoGen.Extended.NewOrderNotificationExtended)
    
      'we have provided a few short cuts to ShoppingCartItems and The shipping method
      'in the old days, you would need to perform a pretty lengthy loop to obtain this information
      Dim shippingMethod As String = ext.ShippingMethod
    
      'we also provide the ShippingCost
      Dim shippingCost As Decimal = ext.ShippingCost
    End If
  End Sub
</script>

<?xml version="1.0" encoding="UTF-8"?>
<notification-acknowledgment xmlns="http://checkout.google.com/schema/2"/>