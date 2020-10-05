<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="activity.list.ascx.vb"
    Inherits="permatapintar.activity_list" %>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">
            Aktiviti Program Perkhemahan Cuti Sekolah
        </td>
    </tr>
    <tr>
        <td class="fbsection_sap">
            <asp:Calendar ID="calToday" runat="server"></asp:Calendar>
        </td>
        <td class="fbsection_sap">
            <asp:Button ID="btnRefresh" runat="server" Text="Refresh " CssClass="fbbutton" />&nbsp;
            Tarikh:<asp:Label ID="lblSelectedDate" runat="server" Text="" CssClass="lblH1Red"></asp:Label>&nbsp;
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:Label ID="lblMsgTop" runat="server" Text="" ForeColor="Red"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px"
                CellPadding="3" ForeColor="Black" GridLines="Vertical" PageSize="25" DataKeyNames="activityid"
                Width="100%" Style="text-align: left;" HeaderStyle-HorizontalAlign="Left">
                <Columns>
                    <asp:TemplateField HeaderText="#">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Oleh">
                        <ItemTemplate>
                            <asp:Label ID="lblcreatedby" runat="server" Text='<%# Bind("createdby") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Pengguna">
                        <ItemTemplate>
                            <asp:Label ID="lblusertype" runat="server" Text='<%# Bind("usertype") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Aktiviti">
                        <ItemTemplate>
                            <asp:Label ID="lblactivitydesc" runat="server" Text='<%# Bind("activitydesc") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Tarikh">
                        <ItemTemplate>
                            <asp:Label ID="lblcreatedate" runat="server" Text='<%# Bind("createdate") %>'></asp:Label>
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
            Jumlah Rekod:<asp:Label ID="lblTotal" runat="server" Text="" ForeColor="Black"></asp:Label>
            &nbsp;
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="red"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            &nbsp;
        </td>
    </tr>
</table>
