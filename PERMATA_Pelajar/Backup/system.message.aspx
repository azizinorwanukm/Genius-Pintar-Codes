<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/main.Master" CodeBehind="system.message.aspx.vb" Inherits="permatapintar.system_message" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:Label ID="lblPerhatian" runat="server" Text="SYSTEM MESSAGE!"></asp:Label>
            </td>
        </tr>
        <tr><td>&nbsp;</td></tr>
        <tr>
            <td>
                <asp:Label ID="lblMsg" runat="server" Text="" CssClass="labelMsg"></asp:Label>
            </td>
        </tr>
        <tr><td>&nbsp;</td></tr>
    </table>
</asp:Content>
