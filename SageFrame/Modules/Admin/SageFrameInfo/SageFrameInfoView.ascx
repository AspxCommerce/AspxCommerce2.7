<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SageFrameInfoView.ascx.cs"
    Inherits="Modules_Admin_SageFrameInfo_SageFrameInfoView" %>

<script language="javascript" type="text/javascript">

//<![CDATA[   
$(function(){
        var EnableLiveFeed='<%=LoadLiveFeed%>';       
        if(EnableLiveFeed=="True")
        {
            DashBoardGrid.Init();
        }   
});
var DashBoardGrid={  
   Init:function(){
               this.TutorialList();
               this.ModuleList();
               this.BlogList();  
               this.NewsList();
            },   
   ModuleList:function(){          
                          this.JsonRequest('<%=appPath%>'+'/Modules/Dashboard/Services/ModuleHandler.ashx', JSON2.stringify({count:5}),
                                 DashBoardGrid.successFunction, function(msg) {DashBoardGrid.GetDefaultModuleList();},false);
                        },                          
  GetDefaultModuleList :function(){          
                                this.JsonRequest('<%=appPath%>'+'/Modules/Dashboard/Services/DashboardWebService.asmx/GetModuleWebInfo',{},
                                    function(msg) {                                                   
                                              var content="";
                                              $.each(msg.d,function(i,element){ 
                                                    
                                                     content+="<div style='border:1px solid #000'>"+ element.ModuleName+" &nbsp; &nbsp;&nbsp;&nbsp; Version: "+element.Version+"<br>";
                                                     if(element.Description.length>1) content+=element.Description+"<br>";
                                                     if(element.ReleaseDate.length>1){
                                                           var str='new '+element.ReleaseDate.replace(/[/]/gi, '');          
                                                            date = eval(str);
                                                            strDate=date.getMonth() +'/'+ date.getDate()+'/'+date.getFullYear();                           
                                         
                                                            content+="Release Date:&nbsp;"+strDate+"&nbsp;&nbsp;&nbsp;&nbsp;";
                                                     }
                                                     content+="<a href='"+element.DownloadUrl+"'>Download</a>";
                                                     content+="</div><br>";
                                                } ,false);
                                                  
                                              $("div.moduleList").html(content);                             
                                    }, function(msg) { $("div.moduleList").html(msg);}); 
                         },
   TutorialList:function(){                          
                          
                            DashBoardGrid.PopulateRSSFeed('http://www.sageframe.com/Resources/Tutorials.aspx?RSS=Blog','<%=appPath%>'+'/Modules/Dashboard/Services/DashboardWebService.asmx/GetDefaultTutorial','sfTutorialContent','<%=appPath%>'+'/Modules/Dashboard/Services/DashboardWebService.asmx/UpdateDefaultTutorial','http://www.sageframe.com/Tutorials.aspx','Tutorials');
                         
                          },
   BlogList:function(){
                          
                           DashBoardGrid.PopulateRSSFeed('http://www.sageframe.com/Community/Blog.aspx?RSS=Blog','<%=appPath%>'+'/Modules/Dashboard/Services/DashboardWebService.asmx/GetDefaultBlog','sfblogContent','<%=appPath%>'+'/Modules/Dashboard/Services/DashboardWebService.asmx/UpdageDefaultBlog','http://www.sageframe.com/Blog.aspx','Blogs');
                                                                        
                      },
   NewsList:function(){                        
                           DashBoardGrid.PopulateRSSFeed('http://www.sageframe.com/Home.aspx?RSS=news&total=5','<%=appPath%>'+'/Modules/Dashboard/Services/DashboardWebService.asmx/GetDefaultNews','sfnewsContent','<%=appPath%>'+'/Modules/Dashboard/Services/DashboardWebService.asmx/UpdateDefaultNews','http://www.sageframe.com/MoreNews.aspx','News');
                                                           
                      },
   PopulateRSSFeed:function(url,alternateURL,cssClassName,updateURL,linkUrl,label){ 
                              $.jGFeed(url,
                                function(feeds){
                                
                                    var content="";                                   
                                    if(!feeds){
                                                                        
                                         DashBoardGrid.JsonRequest(alternateURL,{},
                                                  function(data){ $("div."+cssClassName).html(data); }, 
                                                     function(msg) { alert(msg);},false );
                                     }else{           
                                                                      
                                              for(var i=0; i<feeds.entries.length; i++){
                                              
                                                    var entry = feeds.entries[i];
                                                    var title=entry.title;
                                                    var link=entry.link;
                                                    var description=entry.description;
                                                    var pubDate=entry.pubDate;
                                                
                                                   content+="<li>";
                                                   content+="<a href='" + link + "' target='new'>" + title + "</a>";
                                                   content+="</li>";
                                                   
                                                    var strTemp = $(description).text();
                                                    
                                                    if (strTemp.Length > 200)
                                                    {
                                                        strTemp =strTemp.substring(0,200);  
                                                        strTemp += "...";                     
                                                    }                                                                                                          
                                                   content+=strTemp;
                                                   
                                                   if(pubDate) content+="    "+pubDate;
                                                                                                    
                                                  
                                        }
                                         content= '<div class="sfLivefeed"><ul>'+content+"</ul></div>";
                                        
                                       $("div."+cssClassName).html(content).append("<br><a href='"+linkUrl+"' target='_blank'>More "+label+"..</a>");                                       
                                       
                                       //updating db
                                       $.post(updateURL,{content:content},function(data){ }); 
                                  }                                
                                }, 10);  },
                                 
     successFunction:function(msg) { 
                                              var result=msg;                       
                                              var content="";
                                            
                                              if(msg.length<1)
                                              { 
                                                  $("div.moduleList div.wrapperResult").html("No record found.");
                                             
                                              }else{
                                                      content+="<div class='wrapperResult'>";
                                                      
                                                      $.each(result,function(i,element){ 
                                                            
                                                             content+="<div><label>"+ element.Name+"</label>-<label>Version:</label> "+element.Version+"<br>";
                                                           
                                                            
                                                             if(element.Description.length>1) content+=element.Description+"<br>";
                                                             
                                                                if( typeof(element.AddedOn)!='undefined' && element.AddedOn.length>1){
                                                           var str='new '+element.AddedOn.replace(/[/]/gi, '');          
                                                            date = eval(str);
                                                            strDate=date.getMonth() +'/'+ date.getDate()+'/'+date.getFullYear();                           
                                         
                                                            content+="Release Date:&nbsp;"+strDate+"&nbsp;&nbsp;&nbsp;&nbsp;";
                                                            }
                                                            
                                                             content+="<a href='"+element.URL+"'>Download</a>";
                                                              content+="</div>";
                                                          } );
                                                          
                                                     content+="</div>"; 
                                                     if(result.length>0)
                                                     $("div.moduleList").html(content); 
                                                     $("div.moduleList").prepend(' <div class="searchDiv sfFormwrapper"> <input type="text" id="txtModuleSearch" class="sfInputbox" /> <input type="button" value="Search" id="btnSearchModules" class="sfBtn" /></div>').find("#btnSearchModules").bind("click",function(){
                                         
                                                     var searhKey=$("#txtModuleSearch").val();
                              
                                                     $.ajax({ type: "POST",
                                                      dataType: "json",
                                                              url:'<%=appPath%>'+'/Modules/Dashboard/Services/ModuleHandler.ashx',
                                                              data:JSON2.stringify({skey:searhKey}) ,
                                                              success:DashBoardGrid.successFunction,
                                                              error: function(msg) {                                    
                                                            $("div.moduleList").html("No record found for Module with name "+searhKey+" .");                                     
                                                             }});                     
                                                });
                                              }                                
                },                                
   JsonRequest:function(url,data,successFn,errorFn,isCrossDomain){  
                   var  contentType="application/json; charset=utf-8";
                   var  dataType="json";                      
                                      
                      $.ajax({
                              type: "POST",
                              url:url,
                              data:data,
                              contentType:contentType,
                              dataType: dataType,
                              success:function(data){successFn(data); },
                              error:function(data){errorFn(data)}          
                       });                                     
                  }
}
 //]]>	

</script>

<h1>
    <asp:Label ID="lblSfInfo" runat="server" Text="SageFrame Information Panel"></asp:Label>
</h1>
<!--Accordion-->
<div class="sfInformation">
    <div class="sfInformationholder">
        <div class="sfInformationheader Curve">
            <h2 class="Curve">
                Welcome to Sageframe</h2>
        </div>
        <div class="sfInformationcontent">
            <p>
                Sageframe is an open source ASP.NET web development framework developed using ASP.NET
                3.5 with service pack 1 (sp1) technology. It is designed specifically to help developers
                build dynamic websites by providing core functionalities common to most web applications.
                SageFrame is ideal for creating and deploying projects such as e-commerce site,
                corporate intranets and extranets, online publishing portals, and custom vertical
                applications. It can be easily installed and hosted. When you are planning to develop
                web application for your product or services you have a purpose, a certain goal
                in mind. But unless the site is fully secured, authenticated and easy to navigate
                you cannot achieve the desire goals. No matter what the main functionality of the
                website is, security and user friendliness have always been our prime concern. After
                Developing many Web Applications, developers realize that everytime they start writing
                a web application, there are some basic codes that they need to write repetitively.
                Consistently,they have to set the database connection classes for the database access
                layer.<span><a href="http://www.sageframe.com/Get-Started.aspx">+ read more</a></span>
            </p>
        </div>
    </div>
    <div class="sfInformationholder">
        <div class="sfInformationheader Curve">
            <h2 class="Curve">
                SageFrame Tutorials</h2>
        </div>
        <div class="sfInformationcontent sfTutorialContent">
            <div class="sfLivefeed">
                <ul>
                    <li><a target="new" href="http://www.sageframe.com/Resources/Tutorials.aspx?RSS=Blog&amp;blogid=14">
                        Document Library</a></li><li><a target="new" href="http://www.sageframe.com/Resources/Tutorials.aspx?RSS=Blog&amp;blogid=13">
                            How to create a module on SageFrame</a></li></ul>
            </div>
            <br>
            <a target="_blank" href="http://www.sageframe.com/Tutorials.aspx">More Tutorials..</a></div>
    </div>
    <div class="sfInformationholder">
        <div class="sfInformationheader Curve">
            <h2 class="Curve">
                SageFrame News Feed</h2>
        </div>
        <div class="sfInformationcontent sfnewsContent">
            <div class="sfLivefeed">
                <ul>
                    <li><a target="new" href="http://www.sageframe.com/MoreNews.aspx?RSS=news&amp;newscode=42">
                        Revising SageFrame Templating</a></li><li><a target="new" href="http://www.sageframe.com/MoreNews.aspx?RSS=news&amp;newscode=41">
                            SAGEFRAME FULL VERSION RELEASED!</a></li><li><a target="new" href="http://www.sageframe.com/MoreNews.aspx?RSS=news&amp;newscode=37">
                                Upgraded from Alpha to Beta Version Successfully</a></li><li><a target="new" href="http://www.sageframe.com/MoreNews.aspx?RSS=news&amp;newscode=38">
                                    SageFrame Features coming soon</a></li><li><a target="new" href="http://www.sageframe.com/MoreNews.aspx?RSS=news&amp;newscode=39">
                                        SageFrame Beta Testing</a></li></ul>
            </div>
            <br>
            <a target="_blank" href="http://www.sageframe.com/MoreNews.aspx">More News..</a></div>
    </div>
    <div class="sfInformationholder">
        <div class="sfInformationheader Curve ">
            <h2 class="Curve">
                SageFrame Module List</h2>
        </div>
        <div class="sfInformationcontent moduleList">
            <div class="wrapperResult">
                <div>
                    <label>
                        VideoGallery</label>-<label>Version:</label>
                    01.00.00<br>
                    This module is used for SageFrame Video Gallery<br>
                    <a href="www.braindigit.com">Download</a></div>
                <div>
                    <label>
                        BreadCrumb</label>-<label>Version:</label>
                    01.00.00<br>
                    BreadCrumb<br>
                    <a href="">Download</a></div>
                <div>
                    <label>
                        SitemapGenerator</label>-<label>Version:</label>
                    01.00.00<br>
                    SiteMapGenerator<br>
                    <a href="">Download</a></div>
                <div>
                    <label>
                        SageMenu</label>-<label>Version:</label>
                    01.00.00<br>
                    SageMenu<br>
                    <a href="">Download</a></div>
                <div>
                    <label>
                        CountOnlineUser</label>-<label>Version:</label>
                    01.00.00<br>
                    <a href="">Download</a></div>
            </div>
        </div>
    </div>
    <div class="sfInformationholder">
        <div class="sfInformationheader Curve">
            <h2 class="Curve">
                SageFrame Blog</h2>
        </div>
        <div class="sfInformationcontent sfblogContent">
            <div class="sfLivefeed">
                <ul>
                    <li><a target="new" href="http://www.sageframe.com/Blog.aspx?RSS=Blog&amp;blogid=15">
                        Developing a website with SageFrame - The dynamic Asp.Net CMS</a></li><li><a target="new"
                            href="http://www.sageframe.com/Blog.aspx?RSS=Blog&amp;blogid=12">How to create a
                            module on SageFrame</a></li><li><a target="new" href="http://www.sageframe.com/Blog.aspx?RSS=Blog&amp;blogid=11">
                                A website in SageFrame</a></li><li><a target="new" href="http://www.sageframe.com/Blog.aspx?RSS=Blog&amp;blogid=10">
                                    SageFrame Installation Guide for Windows7</a></li><li><a target="new" href="http://www.sageframe.com/Blog.aspx?RSS=Blog&amp;blogid=9">
                                        Installing Sageframe Visual Studio (VS) Template and Module Starter Kit</a></li></ul>
            </div>
            <br>
            <a target="_blank" href="http://www.sageframe.com/Blog.aspx">More Blogs..</a></div>
    </div>
</div>
