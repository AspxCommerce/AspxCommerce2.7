<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="GCheckout.MerchantCalculation" %>
<%@ Import Namespace="GCheckout.Util" %>
<script runat="server" language="c#">

/*
	This page is meant to receive callbacks for merchant-calculated values from Google.
	The page currently uses the example business logic from ExampleRules.cs.
	To substitute your own business logic, write a class that inherits from CallbackRules.cs
	and overrides one or more of the methods in that class. Instantiate that class
	where ExampleRules is being instantiated below.
*/

	void Page_Load(Object sender, EventArgs e)
	{
		try
		{
			// Extract the XML from the request.
			Stream RequestStream = Request.InputStream;
			StreamReader RequestStreamReader = new StreamReader(RequestStream);
			string RequestXml = RequestStreamReader.ReadToEnd();
			RequestStream.Close();
			
			Log.Debug("Request XML: " + RequestXml);
			
			// Process the incoming XML.
			CallbackProcessor P = new CallbackProcessor(new ExampleRules());
			byte[] ResponseXML = P.Process(RequestXml);
			
			Log.Debug("Response XML: " + EncodeHelper.Utf8BytesToString(ResponseXML));
			
			Response.BinaryWrite(ResponseXML);
		}
		catch (Exception ex)
		{
			Log.Debug(ex.ToString());
		}
  }

</script>