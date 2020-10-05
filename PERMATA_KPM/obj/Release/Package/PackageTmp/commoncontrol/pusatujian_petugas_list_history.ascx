<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="pusatujian_petugas_list_history.ascx.vb"
    Inherits="permatapintar.pusatujian_petugas_list_history" %>
<table class="fbform">
    <tr class="fbform_header">
        <td>
            Petugas Pusat Ujian History List
        </td>
    </tr>
    <tr>
        <td>
            <asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="PusatIDPetugasID"
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
                    <asp:TemplateField HeaderText="Nama Pusat">
                        <ItemTemplate>
                            <asp:Label ID="PusatName" runat="server" Text='<%# Bind("PusatName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Bandar">
                        <ItemTemplate>
                            <asp:Label ID="PusatCity" runat="server" Text='<%# Bind("PusatCity") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Tahun Ujian">
                        <ItemTemplate>
                            <asp:Label ID="PusatTahun" runat="server" Text='<%# Bind("PusatTahun") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Tarikh">
                        <ItemTemplate>
                            <asp:Label ID="Tarikh" runat="server" Text='<%# Bind("Tarikh") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Pagi">
                        <ItemTemplate>
                            <asp:Label ID="SesiPagi" runat="server" Text='<%# Bind("SesiPagi") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Tgh. Hari">
                        <ItemTemplate>
                            <asp:Label ID="SesiTghari" runat="server" Text='<%# Bind("SesiTghari") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Petang">
                        <ItemTemplate>
                            <asp:Label ID="SesiPetang" runat="server" Text='<%# Bind("SesiPetang") %>'></asp:Label>
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
    <tr><td>
        <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
    </td></tr>
</table>
