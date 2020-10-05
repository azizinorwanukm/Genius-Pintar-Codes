<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="tambah_maklumat_pengumuman.ascx.vb" Inherits="permatapintar.tambah_maklumat_pengumuman" %>

<table class="fbform" style="height:150px;margin-bottom:10px">
    <tr class="fbform_header">
        <td colspan="4">Tambah Pengumuman
        </td>
    </tr>

    <tr style="margin-top:5px">
        <td>Pengumuman:
        </td>
        <td style="text-align:left">
            <textarea id="txtPengumuman" cols="100" rows="10"  runat="server"></textarea>
        </td>
    </tr>
    <tr style="margin-top:5px">
        <td>Kokurikulum:
        </td>
        <td style="text-align:left">
            <asp:DropDownList ID="ddlKokurikulum" runat="server" AutoPostBack="false" Width="200px">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
            <asp:Button ID="btnSave" runat="server" Text="Simpan" CssClass="fbbutton" Visible="true" />&nbsp;
            <asp:Button ID="btnBack" runat="server" Text="Kembali" CssClass="fbbutton" Visible="true" />&nbsp;
        </td>
    </tr>
</table>
