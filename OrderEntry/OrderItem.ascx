<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OrderItem.ascx.cs" Inherits="SeniorProjectWebsite.OrderEntry.OrderItem" %>
<table>
    <tr>
        <td>
            Item : <asp:DropDownList ID="ddlItem" AutoPostBack="true" runat="server"></asp:DropDownList>
        </td>
        <td>
            Quantity : <asp:TextBox ID="txtQuantity" runat="server"></asp:TextBox>
        </td>
    </tr>
    <asp:Button id="btnSave" runat="server" Text="AddItem" OnClick="btnSave_Click"/>
</table>