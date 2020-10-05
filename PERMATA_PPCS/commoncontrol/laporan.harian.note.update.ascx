<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="laporan.harian.note.update.ascx.vb"
    Inherits="permatapintar.laporan_harian_note_update" %>
<table class="fbform">
    <tr class="fbform_header">
        <td>
            Catatan Pengurus Akademik:<br />
            <asp:TextBox ID="txtRemarksPengurusAkademik" runat="server" TextMode="MultiLine" Rows="5" Width="600px"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="fbsection_sap">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td>
            Catatan Ketua Modul:<br />
            <asp:TextBox ID="txtRemarksKetuaModul" runat="server" TextMode="MultiLine" Rows="5" Width="600px"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="fbsection_sap">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td>
            Catatan Pengajar:<br />
            <asp:TextBox ID="txtRemarksPengajar" runat="server" TextMode="MultiLine" Rows="5" Width="600px"></asp:TextBox>
        </td>
    </tr>

    <tr>
        <td class="fbsection_sap">
            <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Button ID="btnUpdate" runat="server" Text=" Kemaskini " CssClass="fbbutton" />&nbsp;
            <asp:Button ID="btnClose" runat="server" Text=" Close Window " CssClass="fbbutton" />
        </td>
    </tr>
</table>
