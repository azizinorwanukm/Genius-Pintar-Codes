<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master" CodeBehind="admin.koko.jadual.sukan.aspx.vb" Inherits="permatapintar.admin_koko_jadual_sukan" %>


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
            <td>Laporan>Jadual Kokurikulum>Sukan & Permainan
            </td>
        </tr>
    </table>
    <asp:Panel ID="pnlContents" runat="server">
        <uc3:koko_jadual_rumahsukan ID="koko_jadual_rumahsukan1" runat="server" />
    </asp:Panel>
    <table class="fbform">
        <tr>
            <td>
                <asp:Button ID="btnPrint" runat="server" Text="Cetak  " CssClass="fbbutton" OnClientClick="return PrintPanel();" /></td>
        </tr>
    </table>
</asp:Content>
