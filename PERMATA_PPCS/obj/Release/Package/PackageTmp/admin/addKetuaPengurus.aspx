<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master"
    CodeBehind="addKetuaPengurus.aspx.vb" Inherits="permatapintar.addKetuaPengurus" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr>
            <td colspan="4">
                Tambah Ketua Pengurus Akademik
            </td>
        </tr>
        <tr>
            <td colspan="4">
                Butir-butir Akaun
            </td>
        </tr>
        <tr>
            <td>
                *Nama penuh:
            </td>
            <td>
                <asp:TextBox ID="txtFullname" runat="server" Width="250px" MaxLength="254"></asp:TextBox>&nbsp;
            </td>
            <td>
                *Login ID(E-Mail):
            </td>
            <td>
                <asp:TextBox ID="txtLoginID" runat="server" Width="250px" MaxLength="254"></asp:TextBox>&nbsp;
            </td>
        </tr>
        <tr>
            <td>
                *Nombor IC:
            </td>
            <td>
                <asp:TextBox ID="txtICNo" runat="server" Width="250px" MaxLength="254"></asp:TextBox>&nbsp;
            </td>
            <td>
                *Kata Laluan:
            </td>
            <td>
                <asp:TextBox ID="txtPwd" runat="server" Width="250px" MaxLength="254" TextMode="Password"></asp:TextBox>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                *No. Telefon:
            </td>
            <td>
                <asp:TextBox ID="txtContactNo" runat="server" Width="250px" MaxLength="254"></asp:TextBox>&nbsp;
            </td>
            <td>
                *Mengesahkan kata laluan:
            </td>
            <td>
                <asp:TextBox ID="txtPwdVerify" runat="server" Width="250px" MaxLength="254" TextMode="Password"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                *Alamat:
            </td>
            <td>
                <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine" Rows="4" Width="250px"
                    MaxLength="254"></asp:TextBox>
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr class="fbform_msg">
            <td>&nbsp;</td>
            <td>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                <asp:Button ID="btnadd" runat="server" Text=" Tambah " CssClass="fbbutton" />
                &nbsp; &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="4">
                Senarai Ketua Pengurus Akademik ( Klik pilih untuk mengemaskini atau menghapus data
                )
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                    BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px"
                    CellPadding="3" ForeColor="Black" GridLines="Vertical" PageSize="25" DataKeyNames="ppcsuserid"
                    Width="100%" Style="text-align: left;" HeaderStyle-HorizontalAlign="Left">
                    <Columns>
                        <asp:TemplateField HeaderText="#">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            <ItemStyle VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Login ID">
                            <ItemTemplate>
                                <asp:Label ID="lblLoginID" runat="server" Text='<%# Bind("LoginID") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="IC#">
                            <ItemTemplate>
                                <asp:Label ID="lblICNo" runat="server" Text='<%# Bind("ICNo") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Nama">
                            <ItemTemplate>
                                <asp:Label ID="lblFullname" runat="server" Text='<%# Bind("Fullname") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Tel. #">
                            <ItemTemplate>
                                <asp:Label ID="lblContactNo" runat="server" Text='<%# Bind("ContactNo") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:CommandField SelectText="Pilih" ShowSelectButton="true" />
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
                Jumlah Rekod:<asp:Label ID="lblTotal" runat="server" Text="" ForeColor="Black"></asp:Label>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Text="" ForeColor="Black"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>
