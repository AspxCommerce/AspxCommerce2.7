<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="GCheckout" %>
<%@ Import Namespace="GCheckout.AutoGen" %>
<%@ Import Namespace="GCheckout.Util" %>

<script runat="server" language="c#">

  void Page_Load(Object sender, EventArgs e) {
    // Extract the XML from the request.
    Stream RequestStream = Request.InputStream;
    StreamReader RequestStreamReader = new StreamReader(RequestStream);
    string RequestXml = RequestStreamReader.ReadToEnd();
    RequestStream.Close();

    //instead of performing a switch statement against the top element
    //you can use an overloaded Deserialize method that will determine
    //what the message type is and return that type.

    //What we have also done is created "Extended" classes that contain
    //helper properties and methods on them.
    //One such class is called NewOrderNotificationExtended.
    //This is a class that inherits from NewOrderNotification

    object requestObject = EncodeHelper.Deserialize(RequestXml);

    //what we are doing is determining the type of the object and performing
    //actions based on that type.
    if (requestObject is GCheckout.AutoGen.Extended.NewOrderNotificationExtended) {
      GCheckout.AutoGen.Extended.NewOrderNotificationExtended ext
        = requestObject as GCheckout.AutoGen.Extended.NewOrderNotificationExtended;
     
      //we have provided a few short cuts to ShoppingCartItems and The shipping method
      //in the old days, you would need to perform a pretty lengthy loop to obtain this information
      string shippingMethod = ext.ShippingMethod;
      
      //we also provide the ShippingCost
      decimal shippingCost = ext.ShippingCost;
    }
  }

</script>

<?xml version="1.0" encoding="UTF-8" ?>
<notification-acknowledgment xmlns="http://checkout.google.com/schema/2" />
