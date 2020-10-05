<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master" CodeBehind="admin.takwim.renang.aspx.vb" Inherits="permatapintar.admin_takwim_renang" %>

<%@ Register Src="../commoncontrol/takwim_list_renang.ascx" TagName="takwim_list_renang" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=pnlContents.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head><title>KOKOSystem-Takwim Kokurikulum</title>');
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
            <td>Laporan>Takwim Kokurikulum>Sukan Renang
            </td>
        </tr>
    </table>
    <asp:Panel ID="pnlContents" runat="server">
        <uc1:takwim_list_renang ID="takwim_list_renang1" runat="server" />
    </asp:Panel>
    &nbsp;
    <table class="fbform">
        <tr>
            <td>
                <asp:Button ID="btnPrint" runat="server" Text="Cetak  " CssClass="fbbutton" OnClientClick="return PrintPanel();" /></td>
        </tr>
    </table>


</asp:Content>