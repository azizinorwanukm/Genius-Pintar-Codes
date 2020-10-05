<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="event_pelajar_select.ascx.vb" Inherits="permatapintar.event_pelajar_select" %>

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

<asp:Panel ID="pnlContents" runat="server">
    <asp:Label ID="lblMsgTop" runat="server" Text="" ForeColor="Red"></asp:Label>
    <table class="fbform">
        <tr class="fbform_header">
            <td>Senarai Pelajar&nbsp;|&nbsp;<asp:Label ID="lblKOKOName" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <div style="height: 300px; overflow: auto">
                    <asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False"
                        BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px"
                        CellPadding="3" ForeColor="Black" GridLines="Vertical" DataKeyNames="StudentID"
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
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Nama">
                            <ItemTemplate>
                                <asp:Label ID="student_Name" runat="server" Text='<%# Bind("student_Name") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="MYKAD">
                            <ItemTemplate>
                                <asp:Label ID="student_Mykad" runat="server" Text='<%# Bind("student_Mykad") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="#Pelajar">
                            <ItemTemplate>
                                <asp:Label ID="student_ID" runat="server" Text='<%# Bind("student_ID") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Kelas">
                            <ItemTemplate>
                                <asp:Label ID="Kelas" runat="server" Text='<%# Bind("Kelas") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Status">
                            <ItemTemplate>
                                <asp:Label ID="Status" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:TemplateField>

                    </Columns>
                        <FooterStyle BackColor="#CCCCCC" />
                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" CssClass="cssPager" />
                        <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" VerticalAlign="Middle"
                            HorizontalAlign="Left" />
                        <AlternatingRowStyle BackColor="#CCCCCC" />
                    </asp:GridView>
                </div>
            </td>
        </tr>
        <tr class="fbform_msg">
            <td>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnUpdate" runat="server" Text="Kemaskini Kehadiran" CssClass="fbbutton" Visible="true" />&nbsp;
                <asp:Button ID="btnPrint" runat="server" Text="Cetak" CssClass="fbbutton" Visible="true" OnClientClick="return PrintPanel();" />&nbsp;
            </td>
        </tr>
    </table>
    Kehadiran:
    <asp:Label ID="lblTitle" runat="server" Text=""></asp:Label><br />
    Tarikh Cetakan:
    <asp:Label ID="lblPrintDate" runat="server" Text=""></asp:Label>

</asp:Panel>



