<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ukm3_transfer.ascx.vb" Inherits="permatapintar.ukm3_transfer" %>

<script type="text/javascript" lang="javascript">
    function CheckAllEmp(Checkbox) {
        var GridVwHeaderChckbox = document.getElementById("<%=datRespondent.ClientID%>");
        for (i = 1; i < GridVwHeaderChckbox.rows.length; i++) {
            GridVwHeaderChckbox.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
        }
    }

    function PrintPanel() {
        var panel = document.getElementById("<%=pnlContents.ClientID %>");
        var printWindow = window.open('', '', 'height=400,width=800');
        printWindow.document.write('<html><head><title>SENARAI PELAJAR</title>');
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

<table class="fbform">
    <tr class="fbform_header">
        <td colspan="4">Carian
        </td>
    </tr>
    <tr>
        <td class="fbtd_left">Sessi PPCS:
        </td>
        <td>
            <asp:DropDownList ID="ddlPPCSDate" runat="server" Width="200px">
            </asp:DropDownList>
        </td>
        <td></td>
        <td></td>
    </tr>
    <tr>
        <td>Nama Pelajar:
        </td>
        <td>
            <asp:TextBox ID="txtStudentFullname" runat="server" Width="200px" MaxLength="150"></asp:TextBox>
        </td>
        <td>MYKAD#:
        </td>
        <td>
            <asp:TextBox ID="txtMYKAD" runat="server" Width="200px" MaxLength="50"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="fbform_sap" colspan="4">&nbsp;
        </td>
    </tr>
    <tr>
        <td colspan="4">
            <asp:Button ID="btnLoad" runat="server" Text="Cari " CssClass="fbbutton" />&nbsp;
        </td>
    </tr>
</table>
<br />
<asp:Panel ID="pnlContents" runat="server">
    <asp:Label ID="lblMsgTop" runat="server" Text="" ForeColor="Red"></asp:Label>
    <table class="fbform">
        <tr class="fbform_header">
            <td>Senarai Pelajar
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                    BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px"
                    CellPadding="3" ForeColor="Black" GridLines="Vertical" PageSize="25" DataKeyNames="StudentID"
                    Width="100%" Style="text-align: left;" HeaderStyle-HorizontalAlign="Left">
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkboxSelectAll" Text="Pilih" runat="server" onclick="CheckAllEmp(this);" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkSelect" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="#">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            <ItemStyle VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Nama Penuh">
                            <ItemTemplate>
                                <asp:Label ID="lblStudentFullname" runat="server" Text='<%# Bind("StudentFullname") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="MYKAD#">
                            <ItemTemplate>
                                <asp:Label ID="lblMYKAD" runat="server" Text='<%# Bind("MYKAD") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="AlumniID">
                            <ItemTemplate>
                                <asp:Label ID="AlumniID" runat="server" Text='<%# Bind("AlumniID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="NoPelajar">
                            <ItemTemplate>
                                <asp:Label ID="NoPelajar" runat="server" Text='<%# Bind("NoPelajar")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="DOB Year">
                            <ItemTemplate>
                                <asp:Label ID="DOB_Year" runat="server" Text='<%# Bind("DOB_Year") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Jantina">
                            <ItemTemplate>
                                <asp:Label ID="StudentGender" runat="server" Text='<%# Bind("StudentGender") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Program">
                            <ItemTemplate>
                                <asp:Label ID="Program" runat="server" Text='<%# Bind("Program")%>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>

                    </Columns>
                    <FooterStyle BackColor="#CCCCCC" />
                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" CssClass="cssPager" />
                    <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" VerticalAlign="Middle"
                        HorizontalAlign="Left" />
                    <AlternatingRowStyle BackColor="#CCCCCC" />
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td class="fbform_sap">&nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnPrint" runat="server" Text="Cetak" CssClass="fbbutton" Visible="true" OnClientClick="return PrintPanel();" />&nbsp;
                <asp:Button ID="btnTransferSelect" runat="server" Text="Transfer Pilihan" CssClass="fbbutton" Visible="true" />&nbsp;
                <asp:Button ID="btnTransfer" runat="server" Text="Transfer Semua" CssClass="fbbutton" Visible="true" />&nbsp;
                Tahun:<asp:DropDownList ID="ddlTahun" runat="server" AutoPostBack="false" Width="120px"></asp:DropDownList>

            </td>
        </tr>
    </table>
</asp:Panel>

<div class="info" id="divMsg" runat="server">
    <asp:Label ID="lblMsg" runat="server" Text="Sila lakukan carian terlebih dahulu. Transfer ke Kokurikulum sekali sahaja."></asp:Label><br />
    Is Transfered?:<asp:Label ID="lblIsTransfered" runat="server" Text=""></asp:Label><br />
    *INSERT INTO koko_pelajar FROM UKM3 WHERE PPMT='Y' AND PPCSDate=<asp:Label ID="lblPPCSDateSearch" runat="server" Text=""></asp:Label>
</div>
<asp:Label ID="lblDebug" runat="server" Text="" ForeColor="Red"></asp:Label>