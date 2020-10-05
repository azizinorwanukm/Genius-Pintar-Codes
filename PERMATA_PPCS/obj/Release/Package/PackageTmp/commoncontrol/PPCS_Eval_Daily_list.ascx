<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="PPCS_Eval_Daily_list.ascx.vb"
    Inherits="permatapintar.PPCS_Eval_Daily_list" %>
<table class="fbform">
    <tr class="fbform_header">
        <td>
            PPCS Laporan Harian
        </td>
    </tr>
    <tr>
        <td>
            <asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px"
                CellPadding="3" ForeColor="Black" GridLines="Vertical" PageSize="25" DataKeyNames="ppcsevalid"
                Width="100%" Style="text-align: left;" HeaderStyle-HorizontalAlign="Left">
                <Columns>
                    <asp:TemplateField HeaderText="#">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="PPCSDate">
                        <ItemTemplate>
                            <asp:Label ID="PPCSDate" runat="server" Text='<%# Bind("PPCSDate") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="DateCreated">
                        <ItemTemplate>
                            <asp:Label ID="DateCreated" runat="server" Text='<%# Bind("DateCreated") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Q001">
                        <ItemTemplate>
                            <asp:Label ID="Q001" runat="server" Text='<%# Bind("Q001") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Q002">
                        <ItemTemplate>
                            <asp:Label ID="Q002" runat="server" Text='<%# Bind("Q002") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Q003">
                        <ItemTemplate>
                            <asp:Label ID="Q003" runat="server" Text='<%# Bind("Q003") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Q004">
                        <ItemTemplate>
                            <asp:Label ID="Q004" runat="server" Text='<%# Bind("Q004") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Q005">
                        <ItemTemplate>
                            <asp:Label ID="Q005" runat="server" Text='<%# Bind("Q005") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Q006">
                        <ItemTemplate>
                            <asp:Label ID="Q006" runat="server" Text='<%# Bind("Q006") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Q007">
                        <ItemTemplate>
                            <asp:Label ID="Q007" runat="server" Text='<%# Bind("Q007") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Q008">
                        <ItemTemplate>
                            <asp:Label ID="Q008" runat="server" Text='<%# Bind("Q008") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Q009">
                        <ItemTemplate>
                            <asp:Label ID="Q009" runat="server" Text='<%# Bind("Q009") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Q010">
                        <ItemTemplate>
                            <asp:Label ID="Q010" runat="server" Text='<%# Bind("Q010") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="TotalMark">
                        <ItemTemplate>
                            <asp:Label ID="TotalMark" runat="server" Text='<%# Bind("TotalMark") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:CommandField SelectText="[Pilih]" ShowSelectButton="True" />
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
