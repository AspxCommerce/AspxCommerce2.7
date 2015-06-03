<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FileBrowser.aspx.cs" Inherits="FileBrowser" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Browse Server Filer</title>

    <script type="text/javascript">

        function GoBack(url) {

            var parentWindow = (window.parent == window) ? window.opener : window.parent;
            parentWindow['CKEDITOR'].tools.callFunction(2, url);
            window.close();

        }
    
    </script>

    <style type="text/css">
       html,body{margin:0; padding:0;}

#form1{color:#0082B5;paddding:10px;font:12px/18px Arial;margin-bottom:20px;}
.top{background-color:#eee;padding:10px;}
#dirLabel{COLOR:#666;}

#wrapper{width:700px;margin:0 auto;font:11px/18px Arial;color:#666;}

li {
    border: 1px solid #CCCCCC;
    border-radius: 5px;-moz-border-radius:5px; -webkit-border-radius:5px;
    float: left;
    height: 160px;
    list-style: none outside none;
    margin: 5px;
    padding:10px;
    text-align: left;
    width: 125px;
	-webkit-transition: background 1s ease;
	-moz-transition: background 1s ease;
	-o-transition: background 1s ease;
	transition: background 1s ease;
	cursor:pointer;
}
li img{border:1px solid #eee;}
li b{color:#333;font-size:12px;}
li:hover{background-color:#eee;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="top">
	Server Folder :
    <asp:Label ID="dirLabel" runat="server" Text="Label"></asp:Label>
      <div>
    Upload a new file:  <asp:FileUpload ID="fuImage" runat="server" />
                <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="btnUpload_Click" />
    </div>
    </div>
	
    <div id="wrapper">
        <ul>
            <%  if (this.ImageFiles != null)
                {
                    if (this.ImageFiles.Count == 0)
                    { 
            %>Empty!!<%
                    }
                    else
                        foreach (ImageFile file in this.ImageFiles)
                        {
                      
            %><li>
			
                <img src="<%=file.ThumbImageFileName %>" onclick="GoBack('<%=file.FileName %>')"/>
				
                <b><%=file.FileName.LastIndexOf("/") > 1 ? file.FileName.Substring(file.FileName.LastIndexOf("/") + 1) : file.FileName%>
                </b><br>
                <%=file.Size%> KB
                <br />
                <%= file.CreatedDate.Substring(0, file.CreatedDate.IndexOf(" "))%>
            </li>
            <% }
                } %>
        </ul>
        
        
    </div>
  
    </form>
</body>
</html>