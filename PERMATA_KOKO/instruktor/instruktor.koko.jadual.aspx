<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/instruktor/instruktor.Master" CodeBehind="instruktor.koko.jadual.aspx.vb" Inherits="permatapintar.instruktor_koko_jadual" %>

<%@ Register Src="../commoncontrol/koko_jadual_persatuan.ascx" TagName="koko_jadual_persatuan" TagPrefix="uc2" %>
<%@ Register Src="../commoncontrol/koko_jadual_rumahsukan.ascx" TagName="koko_jadual_rumahsukan" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=pnlContents.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head><title>KOKOSystem-Jadual Kokurikulum</title>');
            printWindow.document.write('</head><body >');
            printWindow.document.write(panel.innerHTML);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            setTimeout(function () {
                printWindow.print();
            }, 500);
            return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_bread">
            <td>Laporan>Jadual Kokurikulum
            </td>
        </tr>
    </table>
    &nbsp;
    <asp:Panel ID="pnlContents" runat="server">
        <uc2:koko_jadual_persatuan ID="koko_jadual_persatuan1" runat="server" />
        &nbsp;
    <uc3:koko_jadual_rumahsukan ID="koko_jadual_rumahsukan1" runat="server" />
    </asp:Panel>
    &nbsp;
    <table class="fbform">
        <tr>
            <td>
                <asp:Button ID="btnPrint" runat="server" Text="Cetak  " CssClass="fbbutton" OnClientClick="return PrintPanel();" /></td>
        </tr>
    </table>
</asp:Content>
