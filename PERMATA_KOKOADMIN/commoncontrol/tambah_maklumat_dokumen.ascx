<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="tambah_maklumat_dokumen.ascx.vb" Inherits="permatapintar.tambah_maklumat_dokumen" %>

<table class="fbform" style="height: 150px; margin-bottom: 10px">
    <tr class="fbform_header">
        <td colspan="4">Tambah Pengumuman
        </td>
    </tr>

    <tr style="margin-top: 5px">
        <td>Pengumuman:
        </td>
        <td style="text-align: left">
            <asp:FileUpload ID="uploadPhoto" runat="server" class="w3-text-black" onchange="readURL(this)" />
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
