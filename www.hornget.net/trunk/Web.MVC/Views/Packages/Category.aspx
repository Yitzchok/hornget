<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Category>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%= Model.Name %> @ www.hornget.net
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<% if (Model.Categories.Count > 0){ %>
    <div class="mod grab">
        <b class="top">
            <b class="tl"></b>
            <b class="tr"></b>
        </b>
        <div class="inner">
            <div class="hd bam">
                <h3><%= Model.Name %> categories</h3>
            </div>
            <div class="bd">
               <ul class="categories">
                    <% foreach (var category in Model.Categories) { %>
                    <li><span><a href="<%=Url.Action("Index", "Packages", new {category.Url}) %>" title="<%=category.Name %>"><%=category.Name %></a></span></li>
                    <%}%>                           
                </ul>                        
            </div>
        </div>
        <b class="bottom">
            <b class="bl"></b>
            <b class="br"></b>
        </b>
    </div>  
<%} %>

<% if (Model.Packages.Count > 0){ %>

        <div class="mod grab">
            <b class="top">
                <b class="tl"></b>
                <b class="tr"></b>
            </b>
            <div class="inner">
                <div class="hd bam">
                    <h3><%= Model.Name %> categories</h3>
                </div>
                <div class="bd">
                   <ul class="categories">
                        <% foreach (var package in Model.Packages) { %>
                        <li><span><a href="<%=Url.Action("Index", "Packages", new {package.Url}) %>" title="<%=package.Name %>"><%=package.Name %> <%=package.Version %></a></span></li>
                        <%}%>                           
                    </ul>                        
                </div>
            </div>
            <b class="bottom">
                <b class="bl"></b>
                <b class="br"></b>
            </b>
        </div> 
<%} %>
</asp:Content>
