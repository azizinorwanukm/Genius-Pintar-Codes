<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master"
    CodeBehind="pelajar.class.assign.02.aspx.vb" Inherits="permatapintar.pelajar_class_assign_02" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td colspan="2">
                Maklumat Kursus
            </td>
        </tr>
        <tr>
            <td>
                *Kod Kursus:
            </td>
            <td>
                <asp:TextBox ID="txtCourseCode" runat="server" Width="150px" MaxLength="254" ReadOnly="true"
                    CssClass="fbreadonly"></asp:TextBox>&nbsp;
            </td>
        </tr>
        <tr>
            <td>
                *Nama Kursus (BM):
            </td>
            <td>
                <asp:TextBox ID="txtCourseNameBM" runat="server" Width="350px" MaxLength="150" ReadOnly="true"
                    CssClass="fbreadonly"></asp:TextBox>&nbsp;
            </td>
        </tr>
    </table>
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                Menentukan Kelas Pelajar: Pilih Kelas
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
                <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
