<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/default.Master" CodeBehind="admin.setup.aspx.vb" Inherits="permatapintar.admin_setup" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">
            Decrypt Password
        </td>
    </tr>
        <tr>
        <td style="width: 80px;">
            <asp:Label ID="Label4" runat="server" Text="Key"></asp:Label>
        </td>
        <td>
            :<asp:TextBox ID="txtKey" runat="server" Text="" Width="400px" MaxLength="150"></asp:TextBox>&nbsp;*<br />
            <asp:Label ID="Label5" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td style="width: 80px;">
            <asp:Label ID="Label1" runat="server" Text="Encrypt"></asp:Label>
        </td>
        <td>
            :<asp:TextBox ID="txtPwdEncrypt" runat="server" Text="" Width="400px" MaxLength="150"></asp:TextBox>&nbsp;*<br />
            <asp:Label ID="lbl15_instruction" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="Label2" runat="server" Text="Decrypt"></asp:Label>
        </td>
        <td>
            :<asp:TextBox ID="txtPwdDecrypt" runat="server" Text="" Width="400px" MaxLength="150"></asp:TextBox>&nbsp;*<br />
            <asp:Label ID="Label3" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;
        </td>
        <td>
            <asp:Button ID="btnDecrypt" runat="server" Text="Decrypt" CssClass="fbbutton" />&nbsp;
        </td>
    </tr>
</table>
<div class="info" id="divMsg" runat="server">
        <asp:Label ID="lblMsg" runat="server" Text="Medan bertanda * adalah wajib diisi."></asp:Label></div>
</asp:Content>
