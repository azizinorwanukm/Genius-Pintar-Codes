<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="admin_ppcs_user_list.ascx.vb"
    Inherits="permatapintar.admin_ppcs_user_list" %>

<script type="text/javascript" lang="javascript">
    function CheckAll(Checkbox) {
        var GridVwHeaderChckbox = document.getElementById("<%=datRespondent.ClientID%>");
        for (i = 1; i < GridVwHeaderChckbox.rows.length; i++) {
            GridVwHeaderChckbox.rows[i].cells[7].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
        }
    }
</script>

<table class="fbform">
    <tr class="fbform_header">
        <td colspan="4">Carian.
        </td>
    </tr>
    <tr>
        <td class="fbtd_left">PPCS Date:
        </td>
        <td>
            <asp:DropDownList ID="ddlPPCSDate" runat="server" Width="200px">
            </asp:DropDownList>
        </td>
        <td>User Type:
        </td>
        <td>
            <asp:DropDownList ID="ddlUserType" runat="server" AutoPostBack="false" Width="250px">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>Nama Penuh:
        </td>
        <td>
            <asp:TextBox ID="txtFullname" runat="server" Width="200px" MaxLength="150"></asp:TextBox>&nbsp;
        </td>
        <td></td>
        <td></td>
    </tr>
    <tr>
        <td>&nbsp;
        </td>
        <td class="fbaside_sap" colspan="3">
            <asp:Label ID="lblMsgTop" runat="server" Text="" ForeColor="Red" Style="text-align: left"></asp:Label>
        </td>
    </tr>
    <tr>
        <td></td>
        <td>
            <asp:Button ID="btnSearch" runat="server" Text="Cari " CssClass="fbbutton" />
        </td>
        <td></td>
        <td></td>
    </tr>
</table>
<br />
<table class="fbform">
    <tr class="fbform_header">
        <td>Senarai Petugas PPCS
        </td>
    </tr>
    <tr>
        <td>
            <asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="myGUID" Width="100%"
                PageSize="25">
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <Columns>
                    <asp:TemplateField HeaderText="#">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="User Type">
                        <ItemTemplate>
                            <asp:Label ID="UserType" runat="server" Text='<%# Bind("UserType") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Nama Penuh">
                        <ItemTemplate>
                            <asp:Label ID="Fullname" runat="server" Text='<%# Bind("Fullname") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="LoginID">
                        <ItemTemplate>
                            <asp:Label ID="LoginID" runat="server" Text='<%# Bind("LoginID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Password">
                        <ItemTemplate>
                            <asp:Label ID="Pwd" runat="server" Text='<%# Bind("Pwd") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Kelas">
                        <ItemTemplate>
                            <asp:Label ID="lblCourse" runat="server" Text=''></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="IsAllow">
                        <ItemTemplate>
                            <asp:Label ID="IsAllow" runat="server" Text='<%# Bind("IsAllow") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:CheckBox ID="chkboxSelectAll" Text="ALL" runat="server" onclick="CheckAll(this);" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="chkSelect" runat="server" />
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
    <tr>
        <td>
            <asp:Button ID="btnIsAllowY" runat="server" Text="Allow" CssClass="fbbutton" />&nbsp;
            <asp:Button ID="btnIsAllowN" runat="server" Text="Not Allow" CssClass="fbbutton" />&nbsp;
            <asp:Button ID="btnRemove" runat="server" Text="Remove " CssClass="fbbutton" />&nbsp;
            <asp:Button ID="btnExport" runat="server" Text="Export " CssClass="fbbutton" />&nbsp;
        </td>
    </tr>
</table>
<div class="info" id="divMsg" runat="server">
    <asp:Label ID="lblMsg" runat="server" Text="System message..."></asp:Label>
</div>
