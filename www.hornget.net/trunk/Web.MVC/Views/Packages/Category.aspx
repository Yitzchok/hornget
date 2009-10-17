<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Category>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%= Model.Name %> @ www.hornget.net
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>horn/<%= Model.Url %></h2>
    <p>&nbsp;</p>
    
    <% foreach (var category in Model.Categories) { %>

    <div class="ui-helper-reset ui-helper-clearfix ui-widget-header ui-corner-all" style="padding:10px;margin-bottom:10px;text-align:center;">
        <a href="<%=Url.Action("Index", "Packages", new {category.Url}) %>" title="<%=category.Name %>" style="font-size:120%"><%=category.Name %></a>
    </div>
    
    <%}%>
 
    <% foreach (var package in Model.Packages){ %>

    <div class="ui-helper-reset ui-helper-clearfix ui-widget-header ui-corner-all" style="padding:10px;margin-bottom:10px;text-align:center;">
        <a href="<%=Url.Action("Index", "Packages", new {package.Url}) %>" title="<%=package.Name %> <%=package.Version %>" style="font-size:120%"><%=package.Name%> <%=package.Version %></a>
    </div>
    
    <%}%>

</asp:Content>
