<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="koko_pelajar_jawatan_sukan.ascx.vb" Inherits="permatapintar.koko_pelajar_jawatan_sukan" %>

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
        printWindow.document.write('<html><head><title>JAWATAN PELAJAR</title>');
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

<asp:Panel ID="pnlContents" runat="server">
    <asp:Label ID="lblMsgTop" runat="server" Text="" CssClass="fblabel_msg"></asp:Label>
    <table class="fbform">
        <tr class="fbform_header">
            <td>Senarai Pelajar&nbsp;|&nbsp;<asp:Label ID="lblKOKOName" runat="server" Text=""></asp:Label>
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
                                <asp:CheckBox ID="chkboxSelectAll" Text="" runat="server" onclick="CheckAllEmp(this);" />
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

                        <asp:TemplateField HeaderText="#Pelajar">
                            <ItemTemplate>
                                <asp:Label ID="NoPelajar" runat="server" Text='<%# Bind("NoPelajar")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Tahun">
                            <ItemTemplate>
                                <asp:Label ID="Tahun" runat="server" Text='<%# Bind("Tahun") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Bangsa">
                            <ItemTemplate>
                                <asp:Label ID="StudentRace" runat="server" Text='<%# Bind("StudentRace")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Jantina">
                            <ItemTemplate>
                                <asp:Label ID="StudentGender" runat="server" Text='<%# Bind("StudentGender") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Kelas">
                            <ItemTemplate>
                                <asp:Label ID="Kelas" runat="server" Text='<%# Bind("Kelas")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Jawatan">
                            <ItemTemplate>
                                <asp:Label ID="Jawatan_Sukan" runat="server" Text='<%# Bind("Jawatan_Sukan")%>'></asp:Label>
                            </ItemTemplate>
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
        <tr class="fbform_msg">
            <td>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td>Jawatan:
                <asp:DropDownList ID="ddlJawatan" runat="server" AutoPostBack="false" Width="200px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="fbform_sap">&nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnAssign" runat="server" Text="Kemaskini" CssClass="fbbutton" Visible="true" />&nbsp;
                <asp:Button ID="btnPrint" runat="server" Text="Cetak" CssClass="fbbutton" Visible="true" OnClientClick="return PrintPanel();" />
            </td>
        </tr>
        <tr>
            <td>Tarikh Cetakan:
                <asp:Label ID="lblPrintDate" runat="server" Text=""></asp:Label>
            </td>
        </tr>
    </table>
</asp:Panel>
