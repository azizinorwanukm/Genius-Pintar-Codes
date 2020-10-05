<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="user_select.ascx.vb" Inherits="permatapintar.user_select" %>

<script type="text/javascript" lang="javascript">
    function CheckAll(Checkbox) {
        var GridVwHeaderChckbox = document.getElementById("<%=datRespondent.ClientID%>");
        for (i = 1; i < GridVwHeaderChckbox.rows.length; i++) {
            GridVwHeaderChckbox.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
        }
    }
</script>

<table class="tablelogin" border="0px">
    <tr class="fbform_header">
        <td>Carian.
        </td>
    </tr>
    <tr>
        <td>Nama Penuh:
        </td>
    </tr>
    <tr>
        <td>
            <asp:TextBox ID="txtStudentFullname" Width="200px" MaxLength="20" runat="server" Height="25px" Font-Bold="true" Font-Size="16px"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>Tahun:
        </td>
    </tr>
    <tr>
        <td>
            <asp:TextBox ID="txtExamYear" Width="200px" MaxLength="20" runat="server" Height="25px" Font-Bold="true" Font-Size="16px"></asp:TextBox>
        </td>
    </tr>

    <tr>
        <td class="fbsection_sap">&nbsp;
        </td>
    </tr>
    <tr>
        <td>
            <asp:Button ID="btnSearch" runat="server" Text="Cari " CssClass="mybutton" />
        </td>
    </tr>
</table>

<br />
<table class="tablelogin" border="0px">
    <tr class="fbform_header">
        <td>Senarai Pengguna
        </td>
    </tr>
    <tr>
        <td>
            <asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="StudentID" Width="100%"
                PageSize="25">
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <Columns>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:CheckBox ID="chkboxSelectAll" Text="" runat="server" onclick="CheckAll(this);" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="chkSelect" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="#">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="MYKAD">
                        <ItemTemplate>
                            <asp:Label ID="MYKAD" runat="server" Text='<%# Bind("MYKAD") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Nama Penuh">
                        <ItemTemplate>
                            <asp:Label ID="StudentFullname" runat="server" Text='<%# Bind("StudentFullname") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ExamYear">
                        <ItemTemplate>
                            <asp:Label ID="ExamYear" runat="server" Text='<%# Bind("ExamYear") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Exam Start">
                        <ItemTemplate>
                            <asp:Label ID="ExamStart" runat="server" Text='<%# Bind("ExamStart") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status">
                        <ItemTemplate>
                            <asp:Label ID="Status" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Last Page">
                        <ItemTemplate>
                            <asp:Label ID="LastPage" runat="server" Text='<%# Bind("LastPage") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="IsHadir">
                        <ItemTemplate>
                            <asp:Label ID="IsHadir" runat="server" Text='<%# Bind("IsHadir") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="IsLogin">
                        <ItemTemplate>
                            <asp:Label ID="IsLogin" runat="server" Text='<%# Bind("IsLogin") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:CommandField SelectText="[Pilih]" ShowSelectButton="True" HeaderText="" />
                </Columns>
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" CssClass="cssPager" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" VerticalAlign="Middle"
                    HorizontalAlign="Left" />
                <EditRowStyle BackColor="#999999" />
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            </asp:GridView>
        </td>
    </tr>
    <tr>
        <td>&nbsp;
        </td>
    </tr>
    <tr class="fbform_msg">
        <td>
            <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label></td>
    </tr>
    <tr>
        <td>
            <asp:Button ID="btnExport" runat="server" Text="Export " CssClass="mybutton" />&nbsp;
            <asp:Button ID="btnHadir" runat="server" Text="Hadir " CssClass="mybutton" />&nbsp;
            <asp:Button ID="btnTidakHadir" runat="server" Text="Tidak Hadir " CssClass="mybutton" />&nbsp;
            <asp:Button ID="btnLogout" runat="server" Text="Logout " CssClass="mybutton" />&nbsp;
            <asp:Button ID="btnDelete" runat="server" Text="Delete " CssClass="mybutton" Visible="false" />&nbsp;
        </td>
    </tr>

</table>

