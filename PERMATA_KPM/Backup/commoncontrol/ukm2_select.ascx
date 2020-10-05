<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ukm2_select.ascx.vb"
    Inherits="permatapintar.ukm2_select" %>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="4">
            Carian Pelajar UKM2
        </td>
    </tr>
    <tr>
        <td>
            Tahun Ujian:
        </td>
        <td>
            <asp:DropDownList ID="ddlExamYear" runat="server" AutoPostBack="false" Width="200px">
            </asp:DropDownList>
        </td>
        <td>
            Tahun Lahir:
        </td>
        <td>
            <asp:DropDownList ID="ddlDOB_Year" runat="server" AutoPostBack="false" Width="200px">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>
            Negeri:
        </td>
        <td>
            <asp:DropDownList ID="ddlSchoolState" runat="server" AutoPostBack="true" Width="200px">
            </asp:DropDownList>
        </td>
        <td>
            PPD:
        </td>
        <td>
            <asp:DropDownList ID="ddlSchoolPPD" runat="server" Width="200px">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>
            Hadir:
        </td>
        <td>
            <select name="selisHadir" id="selisHadir" style="width: 200px;" runat="server">
                <option value="ALL" selected="selected">ALL</option>
                <option value="Y">HADIR</option>
                <option value="N">TIDAK HADIR</option>
            </select>
        </td>
        <td>
            &nbsp;
        </td>
        <td>
            &nbsp;
        </td>
    </tr>
    <tr>
        <td>
            Nama Penuh:
        </td>
        <td>
            <asp:TextBox ID="txtStudentFullname" runat="server" Width="195px" MaxLength="150"></asp:TextBox>
        </td>
        <td>
            MYKAD#:
        </td>
        <td>
            <asp:TextBox ID="txtMYKAD" runat="server" Width="195px" MaxLength="150"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="fbform_sap" colspan="4">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td colspan="4">
            <asp:Button ID="btnSearch" runat="server" Text="Cari " CssClass="fbbutton" />&nbsp;<asp:Button
                ID="btnExport" runat="server" Text="Export List" CssClass="fbbutton" />
        </td>
    </tr>
</table>
<br />
<table class="fbform">
    <tr class="fbform_header">
        <td>
            Senarai Pelajar UKM2
        </td>
    </tr>
    <tr>
        <td>
            <asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="StudentID"
                Width="100%" PageSize="25" CssClass="gridview_footer">
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <Columns>
                    <asp:TemplateField HeaderText="#">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Kod Sekolah">
                        <ItemTemplate>
                            <asp:Label ID="SchoolCode" runat="server" Text='<%# Bind("SchoolCode") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Nama Sekolah">
                        <ItemTemplate>
                            <asp:Label ID="SchoolName" runat="server" Text='<%# Bind("SchoolName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Bandar">
                        <ItemTemplate>
                            <asp:Label ID="SchoolCity" runat="server" Text='<%# Bind("SchoolCity") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Nama Penuh">
                        <ItemTemplate>
                            <asp:Label ID="StudentFullname" runat="server" Text='<%# Bind("StudentFullname") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="MYKAD">
                        <ItemTemplate>
                            <asp:Label ID="MYKAD" runat="server" Text='<%# Bind("MYKAD") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Tahun Lahir">
                        <ItemTemplate>
                            <asp:Label ID="DOB_Year" runat="server" Text='<%# Bind("DOB_Year") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Tel#">
                        <ItemTemplate>
                            <asp:Label ID="FamilyContactNo" runat="server" Text='<%# Bind("FamilyContactNo") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Nama Bapa">
                        <ItemTemplate>
                            <asp:Label ID="FatherFullname" runat="server" Text='<%# Bind("FatherFullname") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Tarikh Ujian">
                        <ItemTemplate>
                            <asp:Label ID="TarikhUjian" runat="server" Text='<%# Bind("TarikhUjian") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Sessi">
                        <ItemTemplate>
                            <asp:Label ID="SessiUKM2" runat="server" Text='<%# Bind("SessiUKM2") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Pusat Ujian">
                        <ItemTemplate>
                            <asp:Label ID="PusatName" runat="server" Text='<%# Bind("PusatName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Hadir">
                        <ItemTemplate>
                            <asp:Label ID="IsHadir" runat="server" Text='<%# Bind("IsHadir") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Login">
                        <ItemTemplate>
                            <asp:Label ID="IsLogin" runat="server" Text='<%# Bind("IsLogin") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Font-Underline="true" />
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
        <td class="fbform_sap" colspan="4">
            &nbsp;
        </td>
    </tr>
</table>
<table width="100%">
    <tr>
        <td colspan="3">
            <b>Pulau Pinang</b>
        </td>
    </tr>
    <tr style="vertical-align: top;">
        <td>
            Sesi PAGI<br />
            9.00 pagi -12.00 tghhari<br />
        </td>
        <td>
            Sesi TENGAHARI<br />
            TIADA
        </td>
        <td>
            Sesi PETANG<br />
            2:00 petang - 5:00 petang<br />
            2.30 petang - 5.30 petang(Jumaat)
        </td>
    </tr>
    <tr>
        <td colspan="3">
            <b>Negeri-Negeri Lain</b>
        </td>
    </tr>
    <tr style="vertical-align: top;">
        <td>
            Sesi PAGI<br />
            8:00 pagi - 11:00 pagi<br />
        </td>
        <td>
            Sesi TENGAHARI
            <br />
            11:00 pagi - 2:00 petang<br />
        </td>
        <td>
            Sesi PETANG<br />
            2:00 petang - 5:00 petang<br />
            2.30 petang - 5.30 petang(Jumaat)
        </td>
    </tr>
</table>
<div class="info" id="divMsg" runat="server">
    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label><br />
    <asp:Label ID="lblTotal" runat="server" Text=""></asp:Label>
</div>
