<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Package>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Package
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>horn/<%= Model.Url %></h2>
    <p>&nbsp;</p>
    <h3>Version: <%=Model.Version %></h3>
    
    <% foreach (var metaData in Model.MetaData) {%>
    
    <div class="line" style="margin-bottom:0px;">
        <div class="unit size1of2">
        <p><%=metaData.Name %></p>
        </div>
        <div class="unit size1of2 lastUnit">
        <p><%=metaData.Value %></p>
        </div>
    </div>
        
    <%}%>
    <br />
    
</asp:Content>
