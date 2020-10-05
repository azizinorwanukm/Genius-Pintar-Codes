<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ppcs_alumni_list.ascx.vb"
    Inherits="permatapintar.ppcs_alumni_list" %>
<table class="fbform">
<tr class="fbform_header">
        <td>
            Carian.
        </td>
    </tr>
    <tr>
        <td>
            Nama Pelajar:<asp:TextBox ID="txtStudentFullname" runat="server" Width="200px" MaxLength="150"></asp:TextBox>
            &nbsp; MYKAD#:<asp:TextBox ID="txtMYKAD" runat="server" Width="150px" MaxLength="20"></asp:TextBox>&nbsp;
            Tahun Lahir:
            <asp:DropDownList ID="ddlDOB_Year" runat="server" AutoPostBack="false" Width="100px">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Button ID="btnSearch" runat="server" Text="Cari " CssClass="fbbutton" />&nbsp;
        </td>
    </tr>
</table>
<br />
<table class="fbform">
    <tr class="fbform_header">
        <td>
            Senarai Alumni PPCS
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
                    <asp:TemplateField HeaderText="UKM1Year">
                        <ItemTemplate>
                            <asp:Label ID="UKM1Year" runat="server" Text='<%# Bind("UKM1Year") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ModA">
                        <ItemTemplate>
                            <asp:Label ID="ModA" runat="server" Text='<%# Bind("ModA") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ModB">
                        <ItemTemplate>
                            <asp:Label ID="ModB" runat="server" Text='<%# Bind("ModB") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ModC">
                        <ItemTemplate>
                            <asp:Label ID="ModC" runat="server" Text='<%# Bind("ModC") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="UKM1%">
                        <ItemTemplate>
                            <asp:Label ID="UKM1Percentage" runat="server" Text='<%# Bind("UKM1Percentage","{0:F2}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="UKM2Year">
                        <ItemTemplate>
                            <asp:Label ID="UKM2Year" runat="server" Text='<%# Bind("UKM2Year") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="VCI">
                        <ItemTemplate>
                            <asp:Label ID="VCI" runat="server" Text='<%# Bind("VCI") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="PRI">
                        <ItemTemplate>
                            <asp:Label ID="PRI" runat="server" Text='<%# Bind("PRI") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="WMI">
                        <ItemTemplate>
                            <asp:Label ID="WMI" runat="server" Text='<%# Bind("WMI") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="PSI">
                        <ItemTemplate>
                            <asp:Label ID="PSI" runat="server" Text='<%# Bind("PSI") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="UKM2%">
                        <ItemTemplate>
                            <asp:Label ID="UKM2Percentage" runat="server" Text='<%# Bind("UKM2Percentage","{0:F2}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="PPCSYear">
                        <ItemTemplate>
                            <asp:Label ID="PPCSYear" runat="server" Text='<%# Bind("PPCSYear") %>'></asp:Label>
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
    <tr>
        <td class="fbform_sap">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td>
            <asp:Button ID="btnExport" runat="server" Text="Export" CssClass="fbbutton" />&nbsp;
        </td>
    </tr>
</table>
<div class="info" id="divMsg" runat="server">
    <asp:Label ID="lblMsg" runat="server" Text="System message..."></asp:Label></div>
