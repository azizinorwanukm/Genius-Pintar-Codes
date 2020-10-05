﻿<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ukm2_dobyear_summary.ascx.vb"
    Inherits="permatapintar.ukm2_dobyear_summary" %>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="4">Carian.
        </td>
    </tr>
    <tr>
        <td class="fbtd_left">Tahun Ujian:
        </td>
        <td>
            <asp:DropDownList ID="ddlExamYear" runat="server" AutoPostBack="false" Width="200px">
            </asp:DropDownList></td>
        <td>Tahun Lahir:</td>
        <td>
            <asp:DropDownList ID="ddlDOB_Year" runat="server" AutoPostBack="false" Width="200px">
            </asp:DropDownList></td>
        <tr>
            <td>Bangsa:</td>
            <td>
                <asp:DropDownList ID="ddlStudentRace" runat="server" AutoPostBack="false" Width="200px">
                </asp:DropDownList></td>
            <td>Agama:</td>
            <td>
                <asp:DropDownList ID="ddlStudentReligion" runat="server" AutoPostBack="false" Width="200px">
                </asp:DropDownList></td>
        </tr>
    <tr>
        <td>Negeri:</td>
        <td>
            <asp:DropDownList ID="ddlSchoolState" runat="server" AutoPostBack="false" Width="200px">
            </asp:DropDownList></td>
        <td></td>
        <td></td>

    </tr>
    <tr>
        <td class="fbform_sap" colspan="4"></td>
    </tr>
    <tr>
        <td>
            <asp:Button ID="btnSearch" runat="server" Text="Cari " CssClass="fbbutton" />
        </td>
    </tr>
</table>
<br />
<table class="fbform">
    <tr class="fbform_header">
        <td>Senarai Pelajar UKM2.
        </td>
    </tr>
    <tr>
        <td>
            <asp:GridView ID="datDOB" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="DOB_Year"
                Width="100%" PageSize="95">
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <Columns>
                    <asp:TemplateField HeaderText="#">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Tahun Lahir">
                        <ItemTemplate>
                            <asp:Label ID="DOB_Year" runat="server" Text='<%# Bind("DOB_Year") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Umur">
                        <ItemTemplate>
                            <asp:Label ID="StudentAge" runat="server" Text='<%# Bind("StudentAge") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Jumlah Pelajar">
                        <ItemTemplate>
                            <asp:Label ID="StudentTotal" runat="server" Text='<%# Bind("StudentTotal") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
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
</table>
