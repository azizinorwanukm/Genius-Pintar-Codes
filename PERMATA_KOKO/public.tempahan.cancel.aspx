<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/default.master" CodeBehind="public.tempahan.cancel.aspx.vb" Inherits="permatapintar.public_tempahan_cancel" %>


<%@ Register Src="commoncontrol/tempahan_view_kodtempahan.ascx" TagName="tempahan_view_kodtempahan" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" lang="javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=pnlContents.ClientID %>");
        var printWindow = window.open('', '', 'height=400,width=800');
        printWindow.document.write('<html><head><title>Cetak Tempahan</title>');
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
            <td>Kemudahan>Cetak atau Batal Tempahan
            </td>

        </tr>
    </table>
    <asp:Panel ID="pnlContents" runat="server">
        <uc1:tempahan_view_kodtempahan ID="tempahan_view_kodtempahan1" runat="server" />
    </asp:Panel>
    <table class="fbform">
        <tr>
            <td colspan="2">&nbsp;<asp:Button ID="btnPrint" runat="server" Text="Cetak  " CssClass="fbbutton" OnClientClick="return PrintPanel();" />
                &nbsp;<asp:Button ID="btnDelete" runat="server" Text="Batalkan" CssClass="fbbutton" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="red"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>
