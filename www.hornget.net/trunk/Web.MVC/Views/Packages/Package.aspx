<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Package>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%= Model.Name %> @ www.hornget.net
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <div class="mod grab">
            <b class="top">
                <b class="tl"></b>
                <b class="tr"></b>
            </b>
            <div class="inner">
                <div class="hd bam">
                    <h3><%=Model.Name + " " + Model.Version %></h3>
                </div>
                <div class="bd">
                
                    <% if (Model.IsError){ %>
                    
                        <div class="ui-widget" style="margin:10px;">
                        <div class="ui-state-error ui-corner-all" style="padding: 0pt 0.7em;">
                        <p>
                        <span class="ui-icon ui-icon-alert" style="float: left; margin-right: 0.3em;"/>
                        <strong>Alert:</strong>
                        <p><%=Model.ErrorMessage %></p>
                        </p>
                        </div>
                        </div>                    
                     <p><div align="center"><img src="/content/44.png" title="download unavailable" alt="download unavailable" /> download unavailable</div></p> 
                    
                    <% } else {%>
                    <p><div align="center"><a href="<%=Model.DownloadUrl()%>" title=""><img src="/content/45.png" title="download" alt="download" style="vertical-align:middle" /> download package</a></div></p> 
                    <%} %>                   
                
                    <% foreach (var metaData in Model.MetaData) {%>
                    
                    <div class="line" style="margin-bottom:0px;">
                        <div class="unit size1of4">
                        <p><strong><%=metaData.Name %>:</strong></p>
                        </div>
                        <div class="unit size3of4 lastUnit">
                        <p><%=metaData.Value %></p>
                        </div>
                    </div>
                        
                    <%}%>
                    
                    <%if (Model.Contents.Count > 0){ %>
                    <h3>Package Contents</h3>
                    <ul class="simpleList">                 
                    <% foreach (var file in Model.Contents) {%>
   
                        <li><%=file.Name %></li>
                        <%} %>
                    <%}%>
                    </ul>
                           
                </div>
            </div>
            <b class="bottom">
                <b class="bl"></b>
                <b class="br"></b>
            </b>
        </div> 

</asp:Content>
