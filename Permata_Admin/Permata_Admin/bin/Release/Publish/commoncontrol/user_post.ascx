<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="user_post.ascx.vb"
    Inherits="permatapintar.user_post" %>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="4">
            Notification Center
        </td>
    </tr>
    <tr>
        <td>
            <asp:GridView ID="gvTable" runat="server" AutoGenerateColumns="False" DataKeyNames="user_postid"
                CellPadding="4" ForeColor="#333333" AllowPaging="True" Width="100%" GridLines="None"
                PageSize="20">
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <PagerStyle CssClass="gvPagerCss" HorizontalAlign="Center" />
                <Columns>
                    <asp:TemplateField HeaderText="No.">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" Width="5%" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="PostDate" HeaderText="Date" DataFormatString="{0:dd-MM-yyyy hh:mm tt}">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="15%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="PostBy" HeaderText="PostBy">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="10%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Message" HeaderText="Message">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="70%" />
                    </asp:BoundField>
                    <asp:CommandField HeaderText="Comment" SelectText="Reply" ShowSelectButton="True" />
                </Columns>
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" CssClass="cssPager" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <EditRowStyle BackColor="#999999" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            </asp:GridView>
        </td>
    </tr>
    <tr>
        <td>
            New Post:&nbsp;<asp:TextBox ID="txtMessage" runat="server" Width="550px"></asp:TextBox>&nbsp;<asp:Button
                ID="btnSubmit" runat="server" Text="Post" CssClass="fbbutton" />&nbsp;<asp:Button
                    ID="btnRefresh" runat="server" Text="Refresh" CssClass="fbbutton" />
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
        </td>
    </tr>
</table>
