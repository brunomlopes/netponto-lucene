<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="SearchApp.Controllers"%>

<asp:Content ID="indexTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Home Page
</asp:Content>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
    <p>
    <% using (Html.BeginForm("Search","Home")){ %>
        <%= Html.TextBox("query")%>
        <input type="submit" />
        <% } %>
    </p>
    <h2><%= Html.Encode(ViewData["Message"]) %></h2>
</asp:Content>
