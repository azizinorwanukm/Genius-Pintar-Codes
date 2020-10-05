<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="StudentUni_list.ascx.vb" Inherits="permatapintar.StudentUni_list" %>

<table class="fbform">
    <tr class="fbform_header">
        <td style="width:85%;">Senarai Pengajian Tinggi
        </td>
        <td style="text-align: right; width: 15%;">
            <asp:LinkButton ID="lnkCreate" runat="server">Pengajian Tinggi Baru</asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="StudentUniID"
                Width="100%" PageSize="10">
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <Columns>
                    <asp:TemplateField HeaderText="#">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="UniName">
                        <ItemTemplate>
                            <asp:Label ID="UniName" runat="server" Text='<%# Bind("UniName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="UniCountry">
                        <ItemTemplate>
                            <asp:Label ID="UniCountry" runat="server" Text='<%# Bind("UniCountry") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="UniCourse">
                        <ItemTemplate>
                            <asp:Label ID="UniCourse" runat="server" Text='<%# Bind("UniCourse") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="UniStartYear">
                        <ItemTemplate>
                            <asp:Label ID="UniStartYear" runat="server" Text='<%# Bind("UniStartYear") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="UniEndYear">
                        <ItemTemplate>
                            <asp:Label ID="UniEndYear" runat="server" Text='<%# Bind("UniEndYear") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Terkini">
                        <ItemTemplate>
                            <asp:Label ID="IsLatest" runat="server" Text='<%# Bind("IsLatest") %>'></asp:Label>
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
    <tr>
        <td colspan="2">
            <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
        </td>
    </tr>
</table>
