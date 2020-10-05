<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="admin_ppcsusers_create.ascx.vb"
    Inherits="permatapintar.admin_ppcsusers_create" %>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">
            Pengurusan Pengguna><asp:Label ID="lblUserType" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label>
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
    </tr>
    <tr>
        <td>
            Login ID(E-Mail):
        </td>
        <td>
            <asp:TextBox ID="txtLoginID" runat="server" Width="250px" MaxLength="50"></asp:TextBox>*
        </td>
    </tr>
    <tr>
        <td>
            Kata Laluan:
        </td>
        <td>
            <asp:TextBox ID="txtPwd" runat="server" Width="250px" MaxLength="254" TextMode="SingleLine"></asp:TextBox>*
            &nbsp;
        </td>
    </tr>
    <tr>
        <td>
            Mengesahkan kata laluan:
        </td>
        <td>
            <asp:TextBox ID="txtPwdVerify" runat="server" Width="250px" MaxLength="254" TextMode="SingleLine"></asp:TextBox>*
        </td>
    </tr>
    <tr>
        <td>
            Nama penuh:
        </td>
        <td>
            <asp:TextBox ID="txtFullname" runat="server" Width="250px" MaxLength="254"></asp:TextBox>*&nbsp;
        </td>
    </tr>
    <tr>
        <td>
            Nombor IC:
        </td>
        <td>
            <asp:TextBox ID="txtICNo" runat="server" Width="250px" MaxLength="254"></asp:TextBox>*&nbsp;
        </td>
    </tr>
    <tr>
        <td>
            No. Telefon:
        </td>
        <td>
            <asp:TextBox ID="txtContactNo" runat="server" Width="250px" MaxLength="254"></asp:TextBox>*&nbsp;
        </td>
    </tr>
    <tr>
        <td style="vertical-align: top;">
            Alamat:
        </td>
        <td>
            <asp:TextBox ID="txtAddress" runat="server" Width="350px" MaxLength="254"></asp:TextBox>*
        </td>
    </tr>
    <tr>
        <td>
            Poskod:
        </td>
        <td>
            <asp:TextBox ID="txtPostcode" runat="server" Width="100px" MaxLength="254"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
            Bandar:
        </td>
        <td>
            <asp:TextBox ID="txtCity" runat="server" Width="350px" MaxLength="254"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
            Negeri:
        </td>
        <td>
            <asp:TextBox ID="txtState" runat="server" Width="350px" MaxLength="254"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;
        </td>
        <td class="fbaside_sap">
            <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red" Style="text-align: left"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;
        </td>
        <td>
            <asp:Button ID="btnAdd" runat="server" Text=" Tambah " CssClass="fbbutton" />
            |
            <asp:LinkButton ID="lnkppcsuserlist" runat="server">Senarai Petugas</asp:LinkButton>
        </td>
    </tr>
</table>
<br />
<table class="fbform">
    <tr class="fbform_header">
        <td>
            Senarai Petugas:&nbsp;<asp:Label ID="lblUserType01" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label>
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
                    <asp:TemplateField HeaderText="Nama Penuh">
                        <ItemTemplate>
                            <asp:Label ID="Fullname" runat="server" Text='<%# Bind("Fullname") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Contact#">
                        <ItemTemplate>
                            <asp:Label ID="ContactNo" runat="server" Text='<%# Bind("ContactNo") %>'></asp:Label>
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
                    <asp:TemplateField HeaderText="IsAllow">
                        <ItemTemplate>
                            <asp:Label ID="IsAllow" runat="server" Text='<%# Bind("IsAllow") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:CommandField SelectText="[Pilih]" ShowSelectButton="True" HeaderText="" />
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
    