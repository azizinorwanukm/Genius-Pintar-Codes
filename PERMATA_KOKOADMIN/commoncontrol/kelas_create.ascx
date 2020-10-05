<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="kelas_create.ascx.vb" Inherits="permatapintar.kelas_create" %>

<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Tambah Kelas Baru
        </td>
    </tr>
    <tr>
        <td class="fbtd_left">Tahun:
        </td>
        <td>
            <asp:DropDownList ID="ddlTahun" runat="server" AutoPostBack="false" Width="300px">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>Nama Kelas:
        </td>
        <td>
            <asp:TextBox ID="txtKelas" runat="server" Width="300px" MaxLength="250"></asp:TextBox>&nbsp;
        </td>
    </tr>
    <tr>
        <td></td>
        <td class="fbform_sap">&nbsp;</td>
    </tr>
    <tr>
        <td class="column_width">&nbsp;
        </td>
        <td>
            <asp:Button ID="btnadd" runat="server" Text=" Tambah " CssClass="fbbutton" />
            &nbsp;|&nbsp;<asp:LinkButton ID="lnkList" runat="server">Senarai Kelas</asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td class="column_width"></td>
        <td>
            <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="red"></asp:Label>
        </td>
    </tr>
</table>
