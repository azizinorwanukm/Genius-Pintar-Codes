<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="userprofile_update.ascx.vb"
    Inherits="permatapintar.userprofile_update" %>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Kemaskini Maklumat Pengguna Sistem
        </td>
    </tr>
    <tr>
        <td class="fbtd_left">Nama Pengguna
        </td>
        <td>:<asp:TextBox ID="txtFullname" runat="server" Width="350px" MaxLength="250" Text=""></asp:TextBox>&nbsp;
        </td>
    </tr>
    <tr>
        <td>LoginID
        </td>
        <td>:<asp:TextBox ID="txtLoginID" runat="server" Width="350px" MaxLength="50"></asp:TextBox>*
        </td>
    </tr>
    <tr>
        <td>Password
        </td>
        <td>:<asp:TextBox ID="txtPwd" runat="server" Width="350px" MaxLength="50"></asp:TextBox>*
        </td>
    </tr>
    <tr>
        <td>UserType
        </td>
        <td>:<asp:DropDownList ID="ddlUserType" runat="server" AutoPostBack="false" Width="350px">
        </asp:DropDownList>
            *
        </td>
    </tr>
    <tr>
        <td>Negeri
        </td>
        <td>:<asp:DropDownList ID="ddlSchoolState" runat="server" AutoPostBack="true" Width="350px">
        </asp:DropDownList>*
        </td>
    </tr>
    <tr>
        <td></td>
        <td class="fbform_sap">
            <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>&nbsp;
        </td>
        <td style="text-align: left;">
            <asp:Button ID="btnUpdate" runat="server" Text="Kemaskini" CssClass="fbbutton" />
            &nbsp;|&nbsp;<asp:LinkButton ID="lnkList" runat="server">Senarai Pengguna Sistem</asp:LinkButton>
        </td>
    </tr>
</table>
<asp:Label ID="lblLoginID" runat="server" Text="" Visible="false"></asp:Label>
