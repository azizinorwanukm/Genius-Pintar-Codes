<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="laporan_keseluruhan_print.ascx.vb"
    Inherits="permatapintar.laporan_keseluruhan_print1" %>
<%@ Register Src="laporan_harian_print.ascx" TagName="laporan_harian_print" TagPrefix="uc1" %>
<%@ Register Src="studentprofile_header.ascx" TagName="studentprofile_header" TagPrefix="uc2" %>
<uc2:studentprofile_header ID="studentprofile_header1" runat="server" />
&nbsp;
<%--<uc1:laporan_harian_print ID="laporan_harian_print1" runat="server" />
--%>
<table class="fbform">
    <tr class="fbform_header">
        <td>
            Laporan Penaksiran Akademik
        </td>
    </tr>
</table>
<table>
    <tr>
        <td>
            <asp:Label ID="lblMsgTop" runat="server" Text="" ForeColor="Red"></asp:Label>
        </td>
    </tr>
</table>
<table class="fbform" width="100%">
    <tr class="fbform_header">
        <td>
            Laporan Harian
            <asp:Label ID="lblCourse" runat="server" Text="" ForeColor="white"></asp:Label>
        </td>
        <td style="text-align: right;">
            <asp:LinkButton ID="lnkEditHarian" runat="server" ForeColor="Black" Visible="false">Kemaskini</asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px"
                CellPadding="3" ForeColor="Black" GridLines="Vertical" PageSize="25" DataKeyNames="ppcsevalid"
                Width="100%" Style="text-align: left;" HeaderStyle-HorizontalAlign="Left">
                <Columns>
                    <asp:TemplateField HeaderText="No.">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Tarikh">
                        <ItemTemplate>
                            <asp:Label ID="lblTokenid" runat="server" Text='<%# Bind("DateCreated") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Q001">
                        <ItemTemplate>
                            <asp:Label ID="lblQ001" runat="server" Text='<%# Bind("Q001") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Q002">
                        <ItemTemplate>
                            <asp:Label ID="lblQ002" runat="server" Text='<%# Bind("Q002") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Q003">
                        <ItemTemplate>
                            <asp:Label ID="lblQ003" runat="server" Text='<%# Bind("Q003") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Q004">
                        <ItemTemplate>
                            <asp:Label ID="lblQ004" runat="server" Text='<%# Bind("Q004") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Q005">
                        <ItemTemplate>
                            <asp:Label ID="lblQ005" runat="server" Text='<%# Bind("Q005") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Q006">
                        <ItemTemplate>
                            <asp:Label ID="lblQ006" runat="server" Text='<%# Bind("Q006") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Q007">
                        <ItemTemplate>
                            <asp:Label ID="lblQ007" runat="server" Text='<%# Bind("Q007") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Q008">
                        <ItemTemplate>
                            <asp:Label ID="lblQ008" runat="server" Text='<%# Bind("Q008") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Q009">
                        <ItemTemplate>
                            <asp:Label ID="lblQ009" runat="server" Text='<%# Bind("Q009") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Q010">
                        <ItemTemplate>
                            <asp:Label ID="lblQ010" runat="server" Text='<%# Bind("Q010") %>'></asp:Label>
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
</table>
&nbsp;
<table class="fbform" width="100%">
    <tr class="fbform_header">
        <td>
            Laporan Mingguan
            <asp:Label ID="Label1" runat="server" Text="" ForeColor="white"></asp:Label>
        </td>
        <td style="text-align: right;">
            <asp:LinkButton ID="lnkEditMingguan" runat="server" ForeColor="Black">Kemaskini</asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:GridView ID="datMingguan" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px"
                CellPadding="3" ForeColor="Black" GridLines="Vertical" PageSize="5" DataKeyNames="ppcsevalweekid"
                Width="100%" Style="text-align: left;" HeaderStyle-HorizontalAlign="Left">
                <Columns>
                    <asp:TemplateField HeaderText="No.">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Tarikh">
                        <ItemTemplate>
                            <asp:Label ID="lblTokenid" runat="server" Text='<%# Bind("DateCreated") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Catatan Minggu Pertama">
                        <ItemTemplate>
                            <asp:Label ID="lblQ001Remarks" runat="server" Text='<%# Bind("Q001Remarks") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Catatan Minggu Kedua">
                        <ItemTemplate>
                            <asp:Label ID="lblQ002Remarks" runat="server" Text='<%# Bind("Q002Remarks") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Catatan Minggu Ketiga">
                        <ItemTemplate>
                            <asp:Label ID="lblQ003Remarks" runat="server" Text='<%# Bind("Q003Remarks") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle VerticalAlign="Middle" />
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
            <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Black"></asp:Label>
        </td>
    </tr>
</table>
&nbsp;
<table class="fbform">
    <tr class="fbform_header">
        <td>
            Laporan Akhir
            <asp:Label ID="Label2" runat="server" Text="" ForeColor="white"></asp:Label>
        </td>
        <td style="text-align: right;">
            <asp:LinkButton ID="lnkEditAkhir" runat="server" ForeColor="Black">Kemaskini</asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:TextBox ID="txtQ001Remarks" runat="server" TextMode="MultiLine" Rows="25" Width="800px"
                ReadOnly="true"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            &nbsp;
        </td>
    </tr>
</table>
