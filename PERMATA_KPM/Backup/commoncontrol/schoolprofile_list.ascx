<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="schoolprofile_list.ascx.vb"
    Inherits="permatapintar.schoolprofile_list" %>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="4">
            Senarai Sekolah Negeri.
        </td>
    </tr>
    <tr>
        <td>
            Negeri:
        </td>
        <td>
            <asp:DropDownList ID="ddlSchoolState" runat="server" AutoPostBack="true" Width="250px">
            </asp:DropDownList>
        </td>
        <td>
            PPD:
        </td>
        <td>
            <asp:DropDownList ID="ddlSchoolPPD" runat="server" AutoPostBack="true" Width="250px">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>
            Bandar:
        </td>
        <td>
            <asp:DropDownList ID="ddlSchoolCity" runat="server" Width="250px">
            </asp:DropDownList>
        </td>
        <td>
            Nama Sekolah:
        </td>
        <td>
            <asp:TextBox ID="txtSchoolname" runat="server" Width="250px" MaxLength="150"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
        </td>
        <td>
        </td>
        <td>
        </td>
        <td>
            <asp:CheckBox ID="cbXXX" runat="server" Text="Kod Sekolah XXX" />
        </td>
    </tr>
    <tr>
        <td class="fbform_sap" colspan="4">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td colspan="4">
            <asp:Button ID="btnSearch" runat="server" Text="Cari " CssClass="fbbutton" />&nbsp;
        </td>
    </tr>
</table>
<br />
<table class="fbform">
    <tr class="fbform_header">
        <td>
            Senarai Sekolah Negeri.
        </td>
    </tr>
    <tr>
        <td>
            <asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="SchoolID"
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
                    <asp:TemplateField HeaderText="Nama Sekolah">
                        <ItemTemplate>
                            <asp:Label ID="lblNamaSekolah" runat="server" Text='<%# Bind("SchoolName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Kod Sekolah">
                        <ItemTemplate>
                            <asp:Label ID="lblKodSekolah" runat="server" Text='<%# Bind("SchoolCode") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Bandar">
                        <ItemTemplate>
                            <asp:Label ID="SchoolCity" runat="server" Text='<%# Bind("SchoolCity") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Jenis Sekolah">
                        <ItemTemplate>
                            <asp:Label ID="SchoolType" runat="server" Text='<%# Bind("SchoolType") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Tel#">
                        <ItemTemplate>
                            <asp:Label ID="SchoolNoTel" runat="server" Text='<%# Bind("SchoolNoTel") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:CommandField SelectText="[UPDATE]" ShowSelectButton="True" HeaderText="UPDATE" />
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
    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label><br />
    <asp:Label ID="lblTotal" runat="server" Text=""></asp:Label>
</div>
