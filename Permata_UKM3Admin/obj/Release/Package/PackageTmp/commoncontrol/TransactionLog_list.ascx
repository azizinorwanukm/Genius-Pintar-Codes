<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="TransactionLog_list.ascx.vb" Inherits="permatapintar.TransactionLog_list" %>

<script type="text/javascript" lang="javascript">

    function calSelect(myTextbox) {
        myCal.select(document.getElementById('<%= txtDateCreated.ClientID %>'), 'PERMATApintar', 'yyyyMMdd');
    }

</script>

<table class="fbform">
    <tr class="fbform_header">
        <td colspan="4">Carian TransactionLog
        </td>
    </tr>
    <tr>
        <td class="fbtd_left">Date Created:
        </td>
        <td>
            <script type="text/javascript" id="myjscal">
                var myCal = new CalendarPopup("calDiv");
                myCal.showNavigationDropdowns();
            </script>
            <asp:TextBox ID="txtDateCreated" runat="server" Width="150px" MaxLength="250"></asp:TextBox>
            <a href="#" onclick="calSelect(this)" title="calSelect(this)" name="PERMATApintar" id="PERMATApintar">
                <img src="img/department-store-emoticon.png" alt="X" width="15" height="15" onclick="calSelect(this)" title="calSelect(this)" />
            </a>
        </td>

        <td></td>
        <td></td>
    </tr>
    <tr>

        <td>LoginID</td>
        <td>
            <asp:TextBox ID="txtLoginID" runat="server" Width="150px" MaxLength="250"></asp:TextBox></td>
        <td>SQLAction:</td>
        <td>
            <asp:TextBox ID="txtSQLAction" runat="server" Width="150px" MaxLength="250"></asp:TextBox></td>
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
                CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="TransactionID"
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
                    <asp:TemplateField HeaderText="DateCreated">
                        <ItemTemplate>
                            <asp:Label ID="DateCreated" runat="server" Text='<%# Bind("DateCreated") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="LoginID and SQLAction">
                        <ItemTemplate>
                            <asp:Label ID="LoginID" runat="server" Text='<%# Bind("LoginID") %>'></asp:Label><br />
                            <asp:Label ID="SQLAction" runat="server" Text='<%# Bind("SQLAction") %>' ToolTip='<%#Bind("SQLAction")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="URL and SQLStatement">
                        <ItemTemplate>
                            <asp:Label ID="AbsoluteUri" runat="server" Text='<%# Bind("AbsoluteUri") %>' ToolTip='<%#Bind("AbsoluteUri")%>'></asp:Label><br />
                            <div id="SQLStatementID" style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 650px;">
                                <asp:Label ID="SQLStatement" runat="server" Text='<%# Bind("SQLStatement") %>' ToolTip='<%#Bind("SQLStatement")%>'></asp:Label>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="IPAddress">
                        <ItemTemplate>
                            <asp:Label ID="IPAddress" runat="server" Text='<%# Bind("IPAddress") %>'></asp:Label>
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
        </td>
    </tr>
    <tr class="fbform_msg">
        <td>
            <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label></td>
    </tr>
</table>
<asp:Label ID="lblDateCreated" runat="server" Text="" Visible="false"></asp:Label>
<div id="calDiv" style="position: absolute; visibility: hidden; background-color: white;"></div>
