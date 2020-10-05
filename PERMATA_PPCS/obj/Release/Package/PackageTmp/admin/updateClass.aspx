<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master"
    CodeBehind="updateClass.aspx.vb" Inherits="permatapintar.updateClass" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td colspan="2">
                Kemaskini Kelas
            </td>
        </tr>
        <tr>
            <td>
                *Kod Kursus:
            </td>
            <td>
                <asp:TextBox ID="txtCourseCode" runat="server" Width="150px" MaxLength="254" ReadOnly="true"
                    CssClass="fbreadonly"></asp:TextBox>&nbsp;
                    Tahun Kursus:&nbsp;<asp:TextBox ID="txtCourseYear" runat="server" Width="140px" MaxLength="254" ReadOnly="true" CssClass="fbreadonly"></asp:TextBox>
                    &nbsp;ID:<asp:Label ID="lblCourseID" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                *Nama Kursus (BM):
            </td>
            <td>
                <asp:TextBox ID="txtCourseNameBM" runat="server" Width="379px" MaxLength="150" ReadOnly="true"
                    CssClass="fbreadonly"></asp:TextBox>&nbsp;
            </td>
        </tr>
        <tr>
            <td class="column_width">
                *Kod Kelas:
            </td>
            <td>
                <asp:TextBox ID="txtClassCode" runat="server" Width="150px" MaxLength="254"></asp:TextBox>&nbsp;
            </td>
        </tr>
        <tr>
            <td class="column_width">
                *Nama Kelas:
            </td>
            <td>
                <asp:TextBox ID="txtClassNameBM" runat="server" Width="150px" MaxLength="254"></asp:TextBox>&nbsp;
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                <asp:Button ID="btnkemaskini" runat="server" Text="Kemaskini" CssClass="fbbutton" />&nbsp;
                <asp:Button ID="btndelete" runat="server" Text="  Hapus  " CssClass="fbbutton" />&nbsp;
            </td>
        </tr>
    </table>
    <br />
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                Senarai Kelas
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                    BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px"
                    CellPadding="3" ForeColor="Black" GridLines="Vertical" PageSize="25" DataKeyNames="ClassID"
                    Width="100%" Style="text-align: left;" HeaderStyle-HorizontalAlign="Left">
                    <Columns>
                        <asp:TemplateField HeaderText="#">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            <ItemStyle VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Kod Kelas">
                            <ItemTemplate>
                                <asp:Label ID="lblClassCode" runat="server" Text='<%# Bind("ClassCode") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Nama Kelas">
                            <ItemTemplate>
                                <asp:Label ID="lblClassNameBM" runat="server" Text='<%# Bind("ClassNameBM") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Pengajar">
                            <ItemTemplate>
                                <asp:Label ID="lblPengajar" runat="server" Text='<%# Bind("NamaPengajar") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Pembantu Pengajar">
                            <ItemTemplate>
                                <asp:Label ID="lblPembantuPengajar" runat="server" Text='<%# Bind("NamaPembantuPengajar") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:CommandField SelectText="Pilih" ShowSelectButton="True" />
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
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
