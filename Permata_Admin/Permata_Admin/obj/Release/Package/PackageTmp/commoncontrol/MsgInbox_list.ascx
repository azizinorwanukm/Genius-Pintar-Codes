<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="MsgInbox_list.ascx.vb"
    Inherits="permatapintar.MsgInbox_list" %>

<script type="text/javascript" lang="javascript">
    function CheckAll(Checkbox) {
        var GridVwHeaderChckbox = document.getElementById("<%=datRespondent.ClientID%>");
        for (i = 1; i < GridVwHeaderChckbox.rows.length; i++) {
            GridVwHeaderChckbox.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
        }
    }
</script>

<table class="fbform">
    <tr class="fbform_header">
        <td colspan="4">Carian Mesej Inbox<asp:Label ID="lblUserType" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="fbtd_left">Dari:
        </td>
        <td>
            <asp:DropDownList ID="ddlMsgFrom" runat="server" AutoPostBack="false" Width="200px">
            </asp:DropDownList>
        </td>
        <td class="fbtd_left">Kepada:
        </td>
        <td>
            <asp:DropDownList ID="ddlMsgTo" runat="server" AutoPostBack="false" Width="250px">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td class="fbform_sap" colspan="4">&nbsp;</td>
    </tr>
    <tr>
        <td colspan="4">
            <asp:Button ID="btnSearch" runat="server" Text="Cari " CssClass="fbbutton" />&nbsp;
            <asp:Button ID="btnCompose" runat="server" Text="Mesej Baru " CssClass="fbbutton" />&nbsp;
        </td>
    </tr>
</table>
<asp:Label ID="lblMsgTop" runat="server" Text="" ForeColor="Red"></asp:Label>
<table class="fbform">
    <tr class="fbform_header">
        <td>Mesej Inbox
        </td>
    </tr>
    <tr>
        <td>
            <asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="MsgCode" Width="100%"
                PageSize="20">
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <Columns>
                     <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:CheckBox ID="chkboxSelectAll" Text="" runat="server" onclick="CheckAll(this);" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="chkSelect" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="#">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Tarikh">
                        <ItemTemplate>
                            <asp:Label ID="MsgDate" runat="server" Text='<%# Eval("MsgDate", "{0:dd-MM-yyyy hh:mm tt}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Dari">
                        <ItemTemplate>
                            <asp:Label ID="MsgFrom" runat="server" Text='<%# Bind("MsgFrom") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Kepada">
                        <ItemTemplate>
                            <asp:Label ID="MsgTo" runat="server" Text='<%# Bind("MsgTo") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:HyperLinkField DataNavigateUrlFields="MsgCode" DataTextField="MsgSubject" DataTextFormatString=""
                        HeaderText="Subject" DataNavigateUrlFormatString="~\admin.msginbox.view.aspx?msgcode={0}" />
                   

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
        <td class="fbform_sap">
            <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
        </td>
    </tr>
    <tr>
        <td style="text-align: left;">
            <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="fbbutton" />&nbsp;
        </td>
    </tr>
</table>

