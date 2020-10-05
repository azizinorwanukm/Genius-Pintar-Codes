<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="tempahan_list.ascx.vb" Inherits="permatapintar.tempahan_list" %>


<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Senarai Tempahan
        </td>
    </tr>
    <tr>
        <td>
            <asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px"
                CellPadding="3" ForeColor="Black" GridLines="Vertical" PageSize="25" DataKeyNames="TempahanID"
                Width="100%" Style="text-align: left;" HeaderStyle-HorizontalAlign="Left">
                <Columns>
                    <asp:TemplateField HeaderText="#">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Kemudahan">
                        <ItemTemplate>
                            <asp:Label ID="Kemudahan" runat="server" Text='<%# Bind("Kemudahan")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Tarikh">
                        <ItemTemplate>
                            <asp:Label ID="BookingDate" runat="server" Text='<%# Bind("BookingDate")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="07">
                        <ItemTemplate>
                            <asp:Label ID="Time07" runat="server" Text='<%# Bind("Time07")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="08">
                        <ItemTemplate>
                            <asp:Label ID="Time08" runat="server" Text='<%# Bind("Time08")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="09">
                        <ItemTemplate>
                            <asp:Label ID="Time09" runat="server" Text='<%# Bind("Time09")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="10">
                        <ItemTemplate>
                            <asp:Label ID="Time10" runat="server" Text='<%# Bind("Time10")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="11">
                        <ItemTemplate>
                            <asp:Label ID="Time11" runat="server" Text='<%# Bind("Time11")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="12">
                        <ItemTemplate>
                            <asp:Label ID="Time12" runat="server" Text='<%# Bind("Time12")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="01">
                        <ItemTemplate>
                            <asp:Label ID="Time13" runat="server" Text='<%# Bind("Time13")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="02">
                        <ItemTemplate>
                            <asp:Label ID="Time14" runat="server" Text='<%# Bind("Time14")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="03">
                        <ItemTemplate>
                            <asp:Label ID="Time15" runat="server" Text='<%# Bind("Time15")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="04">
                        <ItemTemplate>
                            <asp:Label ID="Time16" runat="server" Text='<%# Bind("Time16")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="05">
                        <ItemTemplate>
                            <asp:Label ID="Time17" runat="server" Text='<%# Bind("Time17")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="06">
                        <ItemTemplate>
                            <asp:Label ID="Time18" runat="server" Text='<%# Bind("Time18")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="07">
                        <ItemTemplate>
                            <asp:Label ID="Time19" runat="server" Text='<%# Bind("Time19")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="08">
                        <ItemTemplate>
                            <asp:Label ID="Time20" runat="server" Text='<%# Bind("Time20")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="09">
                        <ItemTemplate>
                            <asp:Label ID="Time21" runat="server" Text='<%# Bind("Time21")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="10">
                        <ItemTemplate>
                            <asp:Label ID="Time22" runat="server" Text='<%# Bind("Time22")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="11">
                        <ItemTemplate>
                            <asp:Label ID="Time23" runat="server" Text='<%# Bind("Time23")%>'></asp:Label>
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
     <tr>
        <td class="fbform_sap">
            <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Button ID="btnExport" runat="server" Text="Eksport" CssClass="fbbutton" Visible="true" />
        </td>
    </tr>
</table>
