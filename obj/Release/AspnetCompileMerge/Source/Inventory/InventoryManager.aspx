<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="InventoryManager.aspx.cs" Inherits="SeniorProjectWebsite.Inventory.InventoryManager" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="col-md-4" id="invItem" runat="server" visible="false">
        <asp:HiddenField id="hfInventoryId" runat="server" Value=""/>
        <table>
        <tbody>
        <tr>
        <td>Item Name: <asp:TextBox ID="txtName" runat="server"></asp:TextBox></td>
        <td>Quantity: <asp:TextBox ID="txtQuantity" runat="server" TextMode="Number"></asp:TextBox></td>
        </tr>
        <tr>
        <td>SKU: <asp:TextBox ID="txtSku" runat="server" ></asp:TextBox></td>
            <td>Price: <asp:TextBox ID="txtprice" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
        <td colspan="2"><asp:Button Text="Save" id="btnSave" runat="server" OnClick="btnSave_Click"/></td>
        </tr>
        </tbody>
        </table>
    </div>
    <br />
    <asp:Button runat="server" ID="btnNew" Text="New Item" OnClick="btnNew_Click"/>
    <br />
    <div class="col-md-4">
<asp:GridView ID="gvInventory" runat="server" AutoGenerateSelectButton="true" 
        OnSelectedIndexChanged="gvInventory_SelectedIndexChanged" AutoGenerateColumns="true">
    </asp:GridView>
    </div>

    

</asp:Content>
