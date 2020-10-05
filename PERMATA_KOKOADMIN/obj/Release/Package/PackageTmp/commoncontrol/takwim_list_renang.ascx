<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="takwim_list_renang.ascx.vb" Inherits="permatapintar.takwim_list_renang" %>

<table class="fbform">
    <tr class="fbform_header">
        <td style="width:80%;">Senarai Takwim&nbsp;<asp:Label ID="lblKategori" runat="server" Text="Sukan Renang"></asp:Label>&nbsp;<asp:Label ID="lblTahun" runat="server" Text="" ForeColor="Red"></asp:Label>
            &nbsp;|&nbsp;<asp:LinkButton ID="lnkUniform" runat="server">Badan Beruniform</asp:LinkButton>
            &nbsp;|&nbsp;<asp:LinkButton ID="lnkSukan" runat="server">Sukan & Permainan</asp:LinkButton>
            &nbsp;|&nbsp;<asp:LinkButton ID="lnkTempatSukan" runat="server">Tempat Sukan & Permainan</asp:LinkButton>
            &nbsp;|&nbsp;<asp:LinkButton ID="lnkRenang" runat="server">Sukan Renang</asp:LinkButton>

        </td>
        <td style="text-align:right;">Tahun:&nbsp;<asp:DropDownList ID="ddlTahun" runat="server" AutoPostBack="true" Width="100px">
        </asp:DropDownList></td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px"
                CellPadding="3" ForeColor="Black" GridLines="Vertical" PageSize="25" DataKeyNames="TakwimID"
                Width="100%" Style="text-align: left;" HeaderStyle-HorizontalAlign="Left">
                <Columns>
                    <asp:TemplateField HeaderText="#">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Kategori">
                        <ItemTemplate>
                            <asp:Label ID="Kategori" runat="server" Text='<%# Bind("Kategori")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Tarikh">
                        <ItemTemplate>
                            <asp:Label ID="Tarikh" runat="server" Text='<%# Bind("Tarikh", "{0:dddd dd-MM-yyyy}") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Masa">
                        <ItemTemplate>
                            <asp:Label ID="Masa" runat="server" Text='<%# Bind("Masa")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Tempat">
                        <ItemTemplate>
                            <asp:Label ID="Tempat" runat="server" Text='<%# Bind("Tempat")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
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
    <tr>
        <td colspan="2">
            <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
        </td>
    </tr>
</table>
