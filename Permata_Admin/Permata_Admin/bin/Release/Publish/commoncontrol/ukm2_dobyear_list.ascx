﻿<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ukm2_dobyear_list.ascx.vb" Inherits="permatapintar.ukm2_dobyear_list" %>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="4">Carian.
        </td>
    </tr>
    <tr>
        <td class="fbtd_left">Tahun Ujian:
        </td>
        <td>
            <asp:Label ID="lblExamYear" runat="server" Text=""></asp:Label>
        </td>
        <td>Negeri:</td>
        <td>
            <asp:Label ID="lblSchoolState" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td>Bangsa:</td>
        <td>
            <asp:Label ID="lblStudentRace" runat="server" Text=""></asp:Label>
        </td>
        <td>Agama:</td>
        <td>
            <asp:Label ID="lblStudentReligion" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td>Tahun Lahir:</td>
        <td>
            <asp:Label ID="lblDOB_Year" runat="server" Text=""></asp:Label>
        </td>
        <td></td>
        <td></td>
    </tr>
</table>
&nbsp;
<table class="fbform">
    <tr class="fbform_header">
        <td>Senarai Pelajar Layak ke UKM2.
        </td>
    </tr>
    <tr>
        <td>
            <asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="StudentID"
                Width="100%" PageSize="25">
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <Columns>
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
                    <asp:TemplateField HeaderText="Negeri">
                        <ItemTemplate>
                            <asp:Label ID="SchoolState" runat="server" Text='<%# Bind("SchoolState")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Bangsa">
                        <ItemTemplate>
                            <asp:Label ID="StudentRace" runat="server" Text='<%# Bind("StudentRace") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Agama">
                        <ItemTemplate>
                            <asp:Label ID="StudentReligion" runat="server" Text='<%# Bind("StudentReligion")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Tahun Lahir">
                        <ItemTemplate>
                            <asp:Label ID="DOB_Year" runat="server" Text='<%# Bind("DOB_Year") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status Ujian">
                        <ItemTemplate>
                            <asp:Label ID="Status" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:CommandField SelectText="[Pilih]" ShowSelectButton="True" HeaderText="Pilih" />
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
    <tr class="fbform_msg">
        <td>
            <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label></td>
    </tr>
    <tr>
        <td>
            <asp:Button ID="btnExport" runat="server" Text="Export" CssClass="fbbutton" />&nbsp;
        </td>
    </tr>
</table>
