<%@ Import Namespace="System.Configuration" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="GCheckout.Util" %>
<script runat="server" language="c#">

  public void Page_Load(Object sender, EventArgs e) {
    // Extract the XML from the request.
    string RequestXml = EncodeHelper.Utf8StreamToString(Request.InputStream);
    // Write the XML to file.
    string FileName = string.Format("{0}--{1}--{2}.xml",
      EncodeHelper.GetTopElement(RequestXml),
      EncodeHelper.GetElementValue(RequestXml, "google-order-number"),
      EncodeHelper.GetElementValue(RequestXml, "serial-number"));
    string FullFileName = GetPathFromConfigFile() + FileName;
    StreamWriter OutputFile = 
      new StreamWriter(FullFileName, false, Encoding.UTF8);
    OutputFile.Write(RequestXml);
    OutputFile.Close();
  }
  
  private string GetPathFromConfigFile() {
    string RetVal = ConfigurationSettings.AppSettings["NotificationStore"];
    if (RetVal == null) throw new ApplicationException(
      "Set the 'NotificationStore' key in the config file.");
    if (!RetVal.EndsWith("\\")) RetVal += "\\";
    return RetVal;
  }

</script>
<?xml version="1.0" encoding="UTF-8"?>
<notification-acknowledgment xmlns="http://checkout.google.com/schema/2"/>
