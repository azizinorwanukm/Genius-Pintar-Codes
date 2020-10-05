<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="PPCS_Eval_Weekly_list.ascx.vb"
    Inherits="permatapintar.PPCS_Eval_Weekly_list" %>
<table class="fbform">
    <tr class="fbform_header">
        <td>
            PPCS Laporan Mingguan
        </td>
    </tr>
    <tr>
        <td>
            <asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px"
                CellPadding="3" ForeColor="Black" GridLines="Vertical" PageSize="10" DataKeyNames="ppcsevalweekid"
                Width="100%" Style="text-align: left;" HeaderStyle-HorizontalAlign="Left">
                <Columns>
                    <asp:TemplateField HeaderText="#">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="PPCSDate" ItemStyle-VerticalAlign="Top">
                        <ItemTemplate>
                            <asp:Label ID="PPCSDate" runat="server" Text='<%# Bind("PPCSDate") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="DateCreated" ItemStyle-VerticalAlign="Top">
                        <ItemTemplate>
                            <asp:Label ID="DateCreated" runat="server" Text='<%# Bind("DateCreated") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Q001Remarks" ItemStyle-VerticalAlign="Top">
                        <ItemTemplate>
                            <asp:Label ID="Q001Remarks" runat="server" Text='<%# Bind("Q001Remarks") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Q002Remarks" ItemStyle-VerticalAlign="Top">
                        <ItemTemplate>
                            <asp:Label ID="Q002Remarks" runat="server" Text='<%# Bind("Q002Remarks") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Q003Remarks" ItemStyle-VerticalAlign="Top">
                        <ItemTemplate>
                            <asp:Label ID="Q003Remarks" runat="server" Text='<%# Bind("Q003Remarks") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:CommandField SelectText="[Pilih]" ShowSelectButton="True" ItemStyle-VerticalAlign="Top" />
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
</table>
