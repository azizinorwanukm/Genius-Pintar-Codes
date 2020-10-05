<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pengarah/pengarah.Master" CodeBehind="pengarah.tempahan.view.aspx.vb" Inherits="permatapintar.pengarah_tempahan_view" %>

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
            <td class="fbtd_left">Status:</td>
            <td>
                <select name="selStatusTempahan" id="selStatusTempahan" style="width: 300px;" runat="server">
                    <option value="POHON">POHON</option>
                    <option value="LULUS" selected="selected">LULUS</option>
                    <option value="TIDAK LULUS">TIDAK LULUS</option>
                </select>&nbsp;
                
            </td>
        </tr>
        <tr>
            <td>Catatan:</td>
            <td>
                <asp:TextBox ID="txtCatatanPengarah" runat="server" Width="300px" MaxLength="255"></asp:TextBox>&nbsp;</td>
        </tr>
        <tr class="fbform_msg">
            <td colspan="2">
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td></td>
            <td>
                <asp:Button ID="btnUpdate" runat="server" Text="Kemaskini " CssClass="fbbutton" />
                &nbsp;<asp:Button ID="btnPrint" runat="server" Text="Cetak  " CssClass="fbbutton" OnClientClick="return PrintPanel();" />
                &nbsp;|&nbsp;<asp:LinkButton ID="lnkList" runat="server">Senarai Tempahan</asp:LinkButton></td>
        </tr>

    </table>
</asp:Content>
