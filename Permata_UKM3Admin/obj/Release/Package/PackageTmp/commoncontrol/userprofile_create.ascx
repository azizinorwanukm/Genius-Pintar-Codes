<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="userprofile_create.ascx.vb"
    Inherits="permatapintar.userprofile_create" %>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Kemasukkan Maklumat Pengguna Sistem
        </td>
    </tr>
    <tr>
        <td class="fbtd_left">Nama Pengguna
        </td>
        <td>:<asp:TextBox ID="txtFullname" runat="server" Width="250px" MaxLength="250" Text=""></asp:TextBox>&nbsp;
        </td>
    </tr>
    <tr>
        <td>LoginID
        </td>
        <td>:<asp:TextBox ID="txtLoginID" runat="server" Width="250px" MaxLength="50"></asp:TextBox>*
        </td>
    </tr>
    <tr>
        <td>Password
        </td>
        <td>:<asp:TextBox ID="txtPwd" runat="server" Width="250px" MaxLength="15"></asp:TextBox>*
        </td>
    </tr>
    <tr>
        <td>UserType
        </td>
        <td>:<asp:DropDownList ID="ddlUserType" runat="server" AutoPostBack="false" Width="250px">
        </asp:DropDownList>
            *
        </td>
    </tr>
    <tr>
        <td>Negeri
        </td>
        <td>:<asp:DropDownList ID="ddlSchoolState" runat="server" AutoPostBack="true" Width="250px">
        </asp:DropDownList>
            *
        </td>
    </tr>
    <tr>
        <td>Activation
        </td>
        <td>:<select name="selIsActive" id="selIsActive" style="width: 250px;" runat="server">
                <option value="Y" selected="selected">Y</option>
                <option value="N">N</option>
            </select>*
        </td>
    </tr>
    <tr>
        <td>&nbsp;
        </td>
        <td class="fbform_sap">
            <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>&nbsp;
        </td>
        <td style="text-align: left;">
            <asp:Button ID="btnCreate" runat="server" Text="Daftar Pengguna Baru" CssClass="fbbutton" />
            &nbsp;|&nbsp;<asp:LinkButton ID="lnkList" runat="server">Senarai Pengguna Sistem</asp:LinkButton>
        </td>
    </tr>
</table>

<asp:Label ID="lblLoginID" runat="server" Text=""></asp:Label>
