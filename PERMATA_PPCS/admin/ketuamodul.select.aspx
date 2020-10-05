<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master"
    CodeBehind="ketuamodul.select.aspx.vb" Inherits="permatapintar.ketuamodul_select" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                Pengurusan Am>Menentukan Ketua Modul
            </td>
        </tr>
    </table>
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                Pilih
                <asp:Label ID="lblUserType" runat="server" Text="" ForeColor="Red" Style="text-align: left"></asp:Label>&nbsp;
                untuk Kursus:
                <asp:Label ID="lblCourseNameBM" runat="server" Text="" ForeColor="Red" Style="text-align: left"></asp:Label>&nbsp;
                [<asp:Label ID="lblType" runat="server" Text="" ForeColor="Red" Style="text-align: left"></asp:Label>]&nbsp;
                Sessi PPCS:
                <asp:Label ID="lblPPCSDate" runat="server" Text="" ForeColor="Red" Style="text-align: left"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                    BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px"
                    CellPadding="3" ForeColor="Black" GridLines="Vertical" PageSize="25" DataKeyNames="myGUID"
                    Width="100%" Style="text-align: left;" HeaderStyle-HorizontalAlign="Left">
                    <Columns>
                        <asp:TemplateField HeaderText="#">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            <ItemStyle VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Nama">
                            <ItemTemplate>
                                <asp:Label ID="lblFullname" runat="server" Text='<%# Bind("Fullname") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
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
                Jumlah Rekod#:<asp:Label ID="lblTotal" runat="server" Text="" ForeColor="Black"></asp:Label>
                &nbsp;
            </td>
        </tr>
        <tr style="text-align: left">
            <td>
                <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red" Style="text-align: left"></asp:Label>&nbsp;
                <asp:Button ID="btnReset" runat="server" Text="Ketua Modul Baru" CssClass="fbbutton" />
            </td>
        </tr>
    </table>
</asp:Content>
