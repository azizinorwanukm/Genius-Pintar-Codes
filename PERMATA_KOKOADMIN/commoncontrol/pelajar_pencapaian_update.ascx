<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="pelajar_pencapaian_update.ascx.vb" Inherits="permatapintar.pelajar_pencapaian_update" %>

<script type="text/javascript">
    function PopupCenter(pageURL, title, w, h) {
        var left = (screen.width / 2) - (w / 2);
        var top = (screen.height / 2) - (h / 2);
        var targetWin = window.open(pageURL, title, 'toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no, resizable=no, copyhistory=no, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);
    }

</script>

<asp:Label ID="lblMsgTop" runat="server" Text="" CssClass="fblabel_msg"></asp:Label>
<table class="fbform">
    <tr class="fbform_header">
        <td>Kemaskini Penyertaan dan Pencapaian Tahun&nbsp;<asp:Label ID="lblTahun" runat="server" Text=""></asp:Label>&nbsp;|&nbsp;
            <asp:LinkButton ID="lnkSample" runat="server" OnClientClick="PopupCenter('admin.koko.pencapaian.sample.aspx','Contoh',700,450)">Contoh</asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td>Penyertaan dan Pencapaian:
        </td>
    </tr>
    <tr>
        <td style="vertical-align: top;">
            <asp:TextBox ID="txtPencapaian" runat="server" Width="98%" TextMode="MultiLine" Rows="15"></asp:TextBox>&nbsp;*&nbsp;
        </td>
    </tr>
    <tr>
        <td>Sahkan:
        </td>
    </tr>
    <tr>
        <td>
            <select name="selDisahkan" id="selDisahkan" style="width: 80px;" runat="server">
                <option value="Y">YA</option>
                <option value="N">TIDAK</option>
            </select>
        </td>
    </tr>
    <tr>
        <td>Disahkan Oleh:
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblDisahkanOleh" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="fbform_sap">
            <asp:Label ID="lblMsg" runat="server" Text="" CssClass="fblabel_msg"></asp:Label>
        </td>
    </tr>
    <tr>
        <td style="text-align: left;">
            <asp:Button ID="btnUpdate" runat="server" Text="Kemaskini" CssClass="fbbutton" />&nbsp;
        </td>
    </tr>
</table>
