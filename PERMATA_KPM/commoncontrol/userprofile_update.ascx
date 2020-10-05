<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="userprofile_update.ascx.vb"
    Inherits="permatapintar.userprofile_update" %>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">
            Kemaskini Maklumat Pengguna Sistem
        </td>
    </tr>
    <tr>
        <td>
            Nama Pengguna
        </td>
        <td>
            :<asp:TextBox ID="txtFullname" runat="server" Width="250px" MaxLength="250" Text=""></asp:TextBox>&nbsp;
        </td>
    </tr>
    <tr>
        <td style="width: 20%;">
            LoginID
        </td>
        <td>
            :<asp:TextBox ID="txtLoginID" runat="server" Width="100px" MaxLength="50"></asp:TextBox>*
        </td>
    </tr>
    <tr>
        <td>
            Password
        </td>
        <td>
            :<asp:TextBox ID="txtPwd" runat="server" Width="100px" MaxLength="50"></asp:TextBox>*
        </td>
    </tr>
    <tr>
        <td>
            UserType
        </td>
        <td>
            :<asp:DropDownList ID="ddlUserType" runat="server" AutoPostBack="false" Width="100px">
            </asp:DropDownList>
            *
        </td>
    </tr>
    <tr>
        <td>
            Negeri
        </td>
        <td>
            :<asp:DropDownList ID="ddlSchoolState" runat="server" AutoPostBack="true" Width="250px">
            </asp:DropDownList>*
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;
        </td>
        <td class="fbform_sap">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;
        </td>
        <td style="text-align: left;">
            <asp:Button ID="btnUpdate" runat="server" Text="Kemaskini" CssClass="fbbutton" />&nbsp;
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="fbbutton" />
            <asp:Button ID="btnList" runat="server" Text="System Users List" CssClass="fbbutton" />
        </td>
    </tr>
</table>
<div class="info" id="divMsg" runat="server">
    <asp:Label ID="lblMsg" runat="server" Text="System message..."></asp:Label></div>
<asp:Label ID="lblLoginID" runat="server" Text=""></asp:Label>
