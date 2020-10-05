<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ppcs_users_list.ascx.vb"
    Inherits="permatapintar.ppcs_users_list" %>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="4">
            Carian|<asp:Label ID="lblUserType" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="fbtd_left">
            PPCS Date:
        </td>
        <td>
            <asp:DropDownList ID="ddlPPCSDate" runat="server" Width="200px">
            </asp:DropDownList>
        </td>
        <td>
            User Type:
        </td>
        <td>
            <asp:DropDownList ID="ddlUserType" runat="server" AutoPostBack="false" Width="250px">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>
            Nama Penuh:
        </td>
        <td>
            <asp:TextBox ID="txtFullname" runat="server" Width="200px" MaxLength="150"></asp:TextBox>&nbsp;
        </td>
        <td>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td></td>
        <td class="fbsection_sap" colspan="3">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td></td>
        <td colspan="3">
            <asp:Button ID="btnSearch" runat="server" Text="Cari " CssClass="fbbutton" />&nbsp;
            <asp:Button ID="btnExport" runat="server" Text="Export " CssClass="fbbutton" />
        </td>
    </tr>
</table>
<br />
<table class="fbform">
    <tr class="fbform_header">
        <td>
            Senarai Petugas
        </td>
    </tr>
    <tr>
        <td>
            <asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px"
                CellPadding="3" ForeColor="Black" GridLines="Vertical" PageSize="25" DataKeyNames="myGUID"
                Width="100%" Style="text-align: left;" HeaderStyle-HorizontalAlign="Left">
                <Columns>
                    <asp:TemplateField HeaderText="No.">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Nama Penuh">
                        <ItemTemplate>
                            <asp:Label ID="lblFullname" runat="server" Text='<%# Bind("Fullname") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Contact#">
                        <ItemTemplate>
                            <asp:Label ID="lblContactNo" runat="server" Text='<%# Bind("ContactNo") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Usertype">
                        <ItemTemplate>
                            <asp:Label ID="lblUsertype" runat="server" Text='<%# Bind("Usertype") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Kursus atau Kelas">
                        <ItemTemplate>
                            <asp:Label ID="lblCourseClass" runat="server" Text=''></asp:Label>
                        </ItemTemplate>
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
        <td>
            <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Black"></asp:Label>
        </td>
    </tr>
</table>
<asp:Label ID="lblGUID" runat="server" Text=""></asp:Label>