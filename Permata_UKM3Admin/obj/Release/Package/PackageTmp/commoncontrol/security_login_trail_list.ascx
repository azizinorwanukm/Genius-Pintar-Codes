<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="security_login_trail_list.ascx.vb"
    Inherits="permatapintar.security_login_trail_list" %>

<script type="text/javascript" lang="javascript">

    function calSelect(myTextbox) {
        myCal.select(document.getElementById('<%= txtLogTime.ClientID %>'), 'PERMATApintar', 'yyyyMMdd');
    }

</script>

<table class="fbform">
    <tr class="fbform_header">
        <td colspan="4">Carian security_login_trail
        </td>
    </tr>
    <tr>
        <td>Log Time:
        </td>
        <td>
            <script type="text/javascript" id="myjscal">
                var myCal = new CalendarPopup("calDiv");
                myCal.showNavigationDropdowns();
            </script>
            <asp:TextBox ID="txtLogTime" runat="server" Width="150px" MaxLength="250"></asp:TextBox>
            <a href="#" onclick="calSelect(this)" title="calSelect(this)" name="PERMATApintar" id="PERMATApintar">
                <img src="img/department-store-emoticon.png" alt="X" width="15" height="15" onclick="calSelect(this)" title="calSelect(this)" />
            </a>

        </td>
        <td>Aktivity:
        </td>
        <td>
            <select name="selActivity" id="selActivity" style="width: 255px;" runat="server">
                <option value="ALL" selected="selected">ALL</option>
                <option value="BAD-WORD">BAD-WORD</option>
                <option value="CREATE-PROFILE">CREATE-PROFILE</option>
                <option value="END">END</option>
                <option value="LOGIN">LOGIN</option>
                <option value="LOGOUT">LOGOUT</option>
                <option value="LOGIN-FAILED">LOGIN-FAILED</option>
                <option value="UPDATE-PROFILE">UPDATE-PROFILE</option>
                <option value="PELAJAR_LOGIN">PELAJAR_LOGIN</option>
                <option value="PELAJAR_LOGIN_FAILED">PELAJAR_LOGIN_FAILED</option>
                <option value="PPCS_LOGIN">PPCS_LOGIN</option>
                <option value="PPCS_LOGIN_FAILED">PPCS_LOGIN_FAILED</option>
                <option value="KOKO_LOGIN">KOKO_LOGIN</option>
                <option value="KOKO_LOGIN_FAILED">KOKO_LOGIN_FAILED</option>
                <option value="UPDATE-PROFILE-UNAUTHORIZED">UPDATE-PROFILE-UNAUTHORIZED</option>
            </select>
        </td>
    </tr>
    <tr>
        <td>LoginID:</td>
        <td>
            <asp:TextBox ID="txtLoginID" runat="server" Width="150px" MaxLength="250"></asp:TextBox></td>
        <td>UserHostAddress:</td>
        <td>
            <asp:TextBox ID="txtUserHostAddress" runat="server" Width="250px" MaxLength="250"></asp:TextBox></td>
    </tr>
    <tr>
        <td class="fbform_sap" colspan="4">&nbsp;</td>
    </tr>
    <tr>
        <td colspan="4">
            <asp:Button ID="btnSearch" runat="server" Text="Cari  " CssClass="fbbutton" />
        </td>
    </tr>
    <tr>
        <td colspan="4">
            <asp:Label ID="lblInfo" runat="server" Text="Nota: Kosongkan Log Time untuk carian semua rekod." ForeColor="Red" Font-Italic="true"></asp:Label></td>
    </tr>
</table>
<br />
<table class="fbform">
    <tr class="fbform_header">
        <td>Audit Trail.
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
                    <asp:TemplateField HeaderText="LogTime">
                        <ItemTemplate>
                            <asp:Label ID="LogTime" runat="server" Text='<%# Bind("LogTime") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Aktivity">
                        <ItemTemplate>
                            <asp:Label ID="Activity" runat="server" Text='<%# Bind("Activity") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="LoginID">
                        <ItemTemplate>
                            <asp:Label ID="LoginID" runat="server" Text='<%# Bind("LoginID") %>'></asp:Label>
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

                    <asp:TemplateField HeaderText="URL">
                        <ItemTemplate>
                            <asp:Label ID="AbsoluteUri" runat="server" Text='<%# Bind("AbsoluteUri") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:CommandField SelectText="[Pilih]" ShowSelectButton="True" HeaderText="Pilih" />
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
    <tr class="fbform_msg">
        <td>
            <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label></td>
    </tr>
    <tr>
        <td>
            <asp:Button ID="btnExport" runat="server" Text="EXPORT" CssClass="fbbutton" />&nbsp;
        </td>
    </tr>
</table>
<asp:Label ID="lblLogDate" runat="server" Text=""></asp:Label>
<div id="calDiv" style="position: absolute; visibility: hidden; background-color: white;"></div>
