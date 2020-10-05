<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ppcs_alumni_select.ascx.vb"
    Inherits="permatapintar.ppcs_alumni_select" %>

<script type="text/javascript" lang="javascript">
    function CheckAll(Checkbox) {
        var GridVwHeaderChckbox = document.getElementById("<%=datRespondent.ClientID%>");
        for (i = 1; i < GridVwHeaderChckbox.rows.length; i++) {
            GridVwHeaderChckbox.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
        }
    }
</script>

<table class="fbform">
    <tr class="fbform_header">
        <td>Carian.
        </td>
    </tr>
    <tr>
        <td>Nama Pelajar:
            <asp:TextBox ID="txtStudentFullname" runat="server" Width="200px" MaxLength="150"></asp:TextBox>
            &nbsp; MYKAD#:
            <asp:TextBox ID="txtMYKAD" runat="server" Width="150px" MaxLength="20"></asp:TextBox>
            &nbsp; Tahun Lahir:
            <asp:DropDownList ID="ddlDOB_Year" runat="server" AutoPostBack="false" Width="100px">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td class="fbform_sap">&nbsp;
        </td>
    </tr>
    <tr>
        <td>
            <asp:Button ID="btnSearch" runat="server" Text="Cari " CssClass="fbbutton" />
        </td>
    </tr>
    <tr>
        <td>*Pelajar PERMATApintar yang mempunyai AlumniID sahaja.
        </td>
    </tr>
</table>
<br />
<table class="fbform">
    <tr class="fbform_header">
        <td>Senarai pelajar
        </td>
    </tr>
    <tr>
        <td>
            <asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="StudentID"
                Width="100%" PageSize="25">
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
                    <asp:TemplateField HeaderText="Nama Pelajar">
                        <ItemTemplate>
                            <asp:Label ID="StudentFullname" runat="server" Text='<%# Bind("StudentFullname") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="MYKAD">
                        <ItemTemplate>
                            <asp:Label ID="MYKAD" runat="server" Text='<%# Bind("MYKAD") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="AlumniID">
                        <ItemTemplate>
                            <asp:Label ID="AlumniID" runat="server" Text='<%# Bind("AlumniID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Tahun Lahir">
                        <ItemTemplate>
                            <asp:Label ID="DOB_Year" runat="server" Text='<%# Bind("DOB_Year") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Jantina">
                        <ItemTemplate>
                            <asp:Label ID="StudentGender" runat="server" Text='<%# Bind("StudentGender") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:CommandField SelectText="[Pilih]" ShowSelectButton="True" HeaderText="Papar" />
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
</table>
<table class="fbform">
    <tr class="fbform_msg">
        <td>
            <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label></td>
    </tr>
    <tr>
        <td>Sessi PPCS:<asp:DropDownList ID="ddlPPCSDate" runat="server" Width="200px">
        </asp:DropDownList>
            &nbsp;<asp:Button ID="btnLayak" runat="server" Text="LAYAK" CssClass="fbbutton" />&nbsp;
            <asp:Button ID="btnTidakLayak" runat="server" Text="TIDAK LAYAK" CssClass="fbbutton" />&nbsp;
        </td>
    </tr>

</table>

<p>
    <b>MANUAL</b><br />
    Jika tiada rekod asal. Rekod baru akan dicreate di dalam database berdasarkan Tahun
    Ujian dan Sessi PPCS yang dipilih. Table yang terlibat:<br />
    2.UKM1<br />
    3.UKM2<br />
    1.PPCS<br />
</p>
<p>
    Jika rekod Tahun Ujian dan Sessi PPCS sudah ada. Tiada perubahan.
</p>
