<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="user_check.ascx.vb" Inherits="permatapintar.user_check" %>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">
            Carian Pelajar
        </td>
    </tr>
    <tr>
        <td style="width: 20%;">
            <asp:Label ID="Label1" runat="server" Text="MYKAD/MYKID#"></asp:Label>
        </td>
        <td>
            :<asp:TextBox ID="txtMYKAD" runat="server" Text="" Width="200px" MaxLength="20"></asp:TextBox>&nbsp;*&nbsp;[Contoh MYKAD/MYKID#:020820086011. Tanpa "-"]<br />
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;
        </td>
        <td>
            <asp:Button ID="btnNext" runat="server" Text="Cari >>" CssClass="fbbutton" />&nbsp;
            
        </td>
    </tr>
</table>
<div class="info" id="divMsg" runat="server">
    <asp:Label ID="lblMsg" runat="server" Text="Medan bertanda * adalah wajib diisi." ></asp:Label></div>
