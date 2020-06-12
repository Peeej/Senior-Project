<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OrderEntry.aspx.cs" Inherits="SeniorProjectWebsite.OrderEntry.OrderEntry" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="col-md-4">
        <table>
            <tr>
                <td>
                    Name : &nbsp; <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                </td>
                <td>
                    Quantity : &nbsp; <asp:TextBox ID="txtQuantity" TextMode="Number" runat="server"></asp:TextBox>
                </td>
                <td>
                    SKU : &nbsp; <asp:TextBox ID="txtSku" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:Button id="btnSave" runat="server" OnClick="btnSave_Click"/>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
