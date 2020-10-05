<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="master_config_list.ascx.vb" Inherits="permatapintar.master_config_list" %>


<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Carian.
        </td>
    </tr>
    <tr>
        <td class="fbtd_left">configCode:
           
        </td>
        <td>
            <asp:TextBox ID="txtconfigCode" runat="server" Width="200px" MaxLength="150"></asp:TextBox></td>
    </tr>
    <tr>
        <td class="fbform_sap" colspan="2">&nbsp;</td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:Button ID="btnSearch" runat="server" Text="Cari " CssClass="fbbutton" />&nbsp;
        </td>
    </tr>
</table>
<br />
<table class="fbform">
    <tr class="fbform_header">
        <td>Senarai Sistem Konfigurasi&nbsp;|&nbsp;<asp:LinkButton ID="lnkCreate" runat="server">Tambah Baru</asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td>
            <asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="configID" Width="100%"
                PageSize="50">
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <Columns>
                    <asp:TemplateField HeaderText="#">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="configCode">
                        <ItemTemplate>
                            <asp:Label ID="configCode" runat="server" Text='<%# Bind("configCode") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="configString">
                        <ItemTemplate>
                            <asp:Label ID="configString" runat="server" Text='<%# Bind("configString") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="configDesc">
                        <ItemTemplate>
                            <asp:Label ID="configDesc" runat="server" Text='<%# Bind("configDesc") %>'></asp:Label>
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
</table>
