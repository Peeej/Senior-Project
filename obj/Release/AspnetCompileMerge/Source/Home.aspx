<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="SeniorProjectWebsite._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>Current Orders</h1>
        <br />
        <asp:GridView ID="grdOrders" runat="server" AutoGenerateColumns="true">
            </asp:GridView>
    </div>

   

</asp:Content>
