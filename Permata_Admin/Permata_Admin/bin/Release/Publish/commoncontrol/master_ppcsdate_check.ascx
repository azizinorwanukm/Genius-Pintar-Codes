<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="master_ppcsdate_check.ascx.vb" Inherits="permatapintar.master_ppcsdate_check" %>

<asp:GridView ID="datPPCSDate" runat="server" AutoGenerateColumns="False" AllowPaging="True"
    CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="ppcsid"
    Width="100%" PageSize="25">
    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
    <Columns>
        <asp:TemplateField>
            <ItemTemplate>
                <asp:CheckBox ID="chkSelect" runat="server" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="PPCSDate">
            <ItemTemplate>
                <asp:Label ID="PPCSDate" runat="server" Text='<%# Bind("PPCSDate") %>'></asp:Label>
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
