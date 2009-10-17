<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IList<Category>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	www.hornget.net
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>horn</h2>
    <p>&nbsp;</p>    

    <% foreach (var category in Model) { %>

    <div class="ui-helper-reset ui-helper-clearfix ui-widget-header ui-corner-all" style="padding:10px;margin-bottom:10px;text-align:center;">
        <a href="<%=Url.Action("Index", "Packages", new {category.Url}) %>" title="<%=category.Name %>" style="font-size:120%"><%=category.Name %></a>
    </div>
    
    <%}%>
        
</asp:Content>
