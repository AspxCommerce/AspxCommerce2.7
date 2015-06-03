<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DashboardQuickLinks.ascx.cs" Inherits="Controls_DashboardQuickLinks" %>
<script type="text/javascript">
    //<![CDATA[    
    $(function () {
        var QuickLinks = {
            config: {
                isPostBack: false,
                async: false,
                cache: false,
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                data: '{}',
                dataType: 'json',
                method: "",
                url: "",
                categoryList: "",
                ajaxCallMode: 0,
                arr: [],
                arrModules: [],
                baseURL: '<%=appPath%>' + '/Modules/Dashboard/Services/DashboardWebService.asmx/',
                PortalID: 1,
                Path: '<%=appPath%>' + '/Modules/Dashboard/',
                UserName: '<%=UserName%>',
                ShowSideBar: '<%=IsSideBarVisible%>',
                PortalID: '<%=PortalID%>'
            },
            init: function () {
                $('div.sfquicklinks').jcarousel();
                $('ul.jcarousel-list li').each(function () {
                    $(this).tooltip({
                        bodyHandler: function () {
                            return $(this).find("span").text()
                        },
                        showURL: false,
                        fade: 250,
                        track: true
                    });
                });
            }
        };
        QuickLinks.init();
    });
    //]]>	
</script>

<div class="sfTopwrapper">
  <!--Logo-->
  <div class="sfLogo"> <a href="../Admin/Admin<%=Extension %>"> <img src="<%=appPath%>/Administrator/Templates/Default/images/sagecomers-logoicon.png" alt="AspxCommerce" /></a>
    <asp:Label runat="server" ID="lblVersion"></asp:Label>
  </div>
  <!--Quick Links-->
  <div class="sfquicklinks ">
    <asp:Literal ID="ltrQuicklinks" runat="server"></asp:Literal>
  </div>
  <div class="clear"></div>
</div>
