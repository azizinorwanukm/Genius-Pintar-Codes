<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="security_login_trail_list.ascx.vb"
    Inherits="permatapintar.security_login_trail_list" %>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="4">
            Carian.
        </td>
    </tr>
    <tr>
        <td class="fbtd_left">
            Log Time:
        </td>
        <td>
            <asp:TextBox ID="txtLogTime" runat="server" Width="100px"></asp:TextBox>&nbsp;<asp:ImageButton
                ID="btnCalSearch" runat="server" ImageUrl="../icons/event.png" />
        </td>
        <td>
            Aktivity:
        </td>
        <td>
            <select name="selActivity" id="selActivity" style="width: 255px;" runat="server">
                <option value="BAD-WORD">BAD-WORD</option>
                <option value="CREATE-PROFILE">CREATE-PROFILE</option>
                <option value="END">END</option>
                <option value="LOGIN" selected="selected">LOGIN</option>
                <option value="LOGOUT">LOGOUT</option>
                <option value="UPDATE-PROFILE">UPDATE-PROFILE</option>
                <option value="UPDATE-PROFILE-UNAUTHORIZED">UPDATE-PROFILE-UNAUTHORIZED</option>
            </select>
        </td>
    </tr>
    <tr>
        <td>
        </td>
        <td>
            <asp:Calendar ID="myCalSearch" runat="server" Visible="false"></asp:Calendar>
        </td>
        <td>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td class="fbform_sap" colspan="4">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td colspan="4">
            <asp:Button ID="btnSearch" runat="server" Text="Cari  " CssClass="fbbutton" />
        </td>
    </tr>
</table>
<br />
<table class="fbform">
    <tr class="fbform_header">
        <td>
            Audit Trail.
        </td>
    </tr>
    <tr>
        <td>
            <asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="securityid"
                Width="100%" PageSize="25">
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <Columns>
                    <asp:TemplateField HeaderText="#">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="LoginID">
                        <ItemTemplate>
                            <asp:Label ID="LoginID" runat="server" Text='<%# Bind("LoginID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="LogTime">
                        <ItemTemplate>
                            <asp:Label ID="LogTime" runat="server" Text='<%# Bind("LogTime") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="UserHostAddress">
                        <ItemTemplate>
                            <asp:Label ID="UserHostAddress" runat="server" Text='<%# Bind("UserHostAddress") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="UserHostName">
                        <ItemTemplate>
                            <asp:Label ID="UserHostName" runat="server" Text='<%# Bind("UserHostName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Activity">
                        <ItemTemplate>
                            <asp:Label ID="Activity" runat="server" Text='<%# Bind("Activity") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:CommandField SelectText="[Pilih]" ShowSelectButton="True" HeaderText="Papar" />
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
</table>
<div class="info" id="divMsg" runat="server">
    <asp:Label ID="lblMsg" runat="server" Text="System message..."></asp:Label></div>
