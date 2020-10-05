<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="koko_list_sukan_tempat.ascx.vb" Inherits="permatapintar.koko_list_sukan_tempat" %>

<table class="fbform">
    <tr class="fbform_header">
        <td style="width:80%;">Tempat Sukan & Permainan&nbsp;<asp:Label ID="lblTahun" runat="server" Text="" ForeColor="Red"></asp:Label>
        </td>
        <td style="text-align: right;">Tahun:&nbsp;<asp:DropDownList ID="ddlTahun" runat="server" AutoPostBack="true" Width="100px">
        </asp:DropDownList></td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:GridView ID="datSukan" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px"
                CellPadding="3" ForeColor="Black" GridLines="Vertical" PageSize="25" DataKeyNames="KokoID"
                Width="100%" Style="text-align: left;" HeaderStyle-HorizontalAlign="Left">
                <Columns>
                    <asp:TemplateField HeaderText="#">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Nama Sukan & Permainan">
                        <ItemTemplate>
                            <asp:Label ID="Nama" runat="server" Text='<%# Bind("Nama")%>'></asp:Label>
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
