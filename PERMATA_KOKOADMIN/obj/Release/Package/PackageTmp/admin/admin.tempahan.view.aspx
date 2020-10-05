<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master" CodeBehind="admin.tempahan.view.aspx.vb" Inherits="permatapintar.admin_tempahan_view" %>

<%@ Register Src="../commoncontrol/tempahan_view.ascx" TagName="tempahan_view" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=pnlContents.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head><title>KOKOSystem-TEMPAHAN</title>');
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
    <asp:Panel ID="pnlContents" runat="server">
        <table class="fbform">
            <tr class="fbform_bread">
                <td>Lain-Lain>Tempahan>Paparan
                </td>
            </tr>
        </table>
        <uc1:tempahan_view ID="tempahan_view1" runat="server" />
    </asp:Panel>
    <table class="fbform">
        <tr>
            <td>&nbsp;<asp:Button ID="btnPrint" runat="server" Text="Cetak  " CssClass="fbbutton" OnClientClick="return PrintPanel();" />
                &nbsp;<asp:Button ID="btnDelete" runat="server" Text="Batalkan " CssClass="fbbutton" />
                &nbsp;|&nbsp;<asp:LinkButton ID="lnkList" runat="server">Senarai Kemudahan</asp:LinkButton>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="red"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>
