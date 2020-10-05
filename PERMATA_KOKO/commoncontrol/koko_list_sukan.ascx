<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="koko_list_sukan.ascx.vb" Inherits="permatapintar.koko_list_sukan" %>


<table class="fbform">
    <tr class="fbform_header">
        <td style="width: 80%;">Senarai Sukan & Permainan&nbsp;<asp:Label ID="lblTahun" runat="server" Text="" ForeColor="Red"></asp:Label>&nbsp;|
            <asp:LinkButton ID="lnkUniform" runat="server">Badan Beruniform</asp:LinkButton>
            &nbsp;|&nbsp;<asp:LinkButton ID="lnkPersatuan" runat="server">Kelab & Persatuan</asp:LinkButton>
            &nbsp;|&nbsp;<asp:LinkButton ID="lnkSukan" runat="server">Sukan & Permainan</asp:LinkButton>
            &nbsp;|&nbsp;<asp:LinkButton ID="lnkRumahsukan" runat="server">Rumah Sukan</asp:LinkButton>
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
                    <asp:TemplateField HeaderText="Instruktor">
                        <ItemTemplate>
                            <asp:Label ID="lblInstruktor" runat="server" Text=''></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Ketua Instruktor">
                        <ItemTemplate>
                            <asp:Label ID="lblKetuaInstruktor" runat="server" Text=''></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Tel#">
                        <ItemTemplate>
                            <asp:Label ID="lblKetuaInstruktorTelefon" runat="server" Text=''></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Pelajar#">
                        <ItemTemplate>
                            <asp:Label ID="JumlahPelajar" runat="server" Text='<%# Bind("JumlahPelajar")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:CommandField SelectText="[Pilih]" ShowSelectButton="True" HeaderText="Paparan" />
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
    <tr class="fbform_msg">
        <td colspan="2">
            <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label></td>
    </tr>
</table>
