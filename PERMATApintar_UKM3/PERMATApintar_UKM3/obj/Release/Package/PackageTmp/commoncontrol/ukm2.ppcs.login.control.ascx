<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ukm2.ppcs.login.control.ascx.vb"
    Inherits="permatapintar.ukm2_ppcs_login_control" %>
<table class="fbform">
    <tr class="fbform_header">
        <td>
            Log Masuk ke Sistem Pentaksiran Pelajar
        </td>
    </tr>
    <tr>
        <td>
            <b>Arahan</b>
            <p>
                Sila dapatkan Email dan Kata Laluan daripada Pihak Pengurusan PERMATApintar.
            </p>
        </td>
    </tr>
</table>
<br />
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">
            Log masuk di sini
        </td>
    </tr>
    <tr>
        <td class="fbtd_left">
            *Email:
        </td>
        <td>
            <asp:TextBox ID="txtemailid" runat="server" Width="250px" MaxLength="254"></asp:TextBox>&nbsp;
        </td>
    </tr>
    <tr>
        <td>
            *Kata Laluan:
        </td>
        <td>
            <asp:TextBox ID="txtpwd" runat="server" Width="250px" MaxLength="254" TextMode="Password"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;
        </td>
        <td class="fbsection_sap">
            <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;
        </td>
        <td>
            <asp:Button ID="btnLogin" runat="server" Text=" Login " CssClass="fbbutton" />&nbsp;
            <asp:Button ID="btnCancel" runat="server" Text=" Cancel " CssClass="fbbutton" />
        </td>
    </tr>
</table>
